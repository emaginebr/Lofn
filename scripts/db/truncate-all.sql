-- =====================================================================
-- !!! DESTRUCTIVE — APAGA TODOS OS DADOS DO BANCO !!!
--
-- Trunca todas as tabelas do schema `public` exceto `__EFMigrationsHistory`,
-- reseta sequências (IDENTITY/SERIAL) e resolve FKs em cascata.
--
-- O esquema, migrações e estrutura permanecem intactos — apenas os dados
-- são removidos. Após executar, o banco fica no estado pós-migração mas
-- sem registros.
--
-- Uso:
--   psql -h <host> -U <user> -d <database> -f scripts/db/truncate-all.sql
--
-- Envolto em transação: se executado em sessão interativa, é possível
-- abortar com Ctrl-C antes do COMMIT.
-- =====================================================================

BEGIN;

DO $$
DECLARE
    table_list text;
    table_count integer;
BEGIN
    SELECT string_agg(format('%I.%I', schemaname, tablename), ', '),
           count(*)
      INTO table_list, table_count
      FROM pg_tables
     WHERE schemaname = 'public'
       AND tablename <> '__EFMigrationsHistory';

    IF table_count = 0 THEN
        RAISE NOTICE 'Nenhuma tabela encontrada no schema public para truncar.';
        RETURN;
    END IF;

    EXECUTE format('TRUNCATE TABLE %s RESTART IDENTITY CASCADE', table_list);

    RAISE NOTICE '% tabela(s) truncada(s): %', table_count, table_list;
END
$$;

COMMIT;
