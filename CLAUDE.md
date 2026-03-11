# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

NSales (MonexUp) is a full-stack sales/e-commerce platform with a .NET 8 backend API and a React 18 TypeScript frontend. It supports multi-tenant networks with sellers, products, orders, and invoices.

## Commands

### Frontend (React/TypeScript - root directory)
```bash
npm start          # Dev server at https://localhost:443
npm run build      # Production build to build/
npm test           # Jest tests in watch mode
```

### Backend (.NET 8 - from repo root)
```bash
dotnet build NSales.sln                          # Build entire solution
dotnet run --project NSales.API                  # Run API (https://localhost:44374)
dotnet run --project NSales.BackgroundService    # Run background jobs
```

### Docker
```bash
docker-compose up        # Starts nginx-proxy (8081) + API services
```

## Architecture

### Backend - Clean Architecture (.NET 8)

```
NSales.API              → Controllers, Startup/DI config, auth middleware
NSales.Application      → DI initializer, wires up services
NSales.Domain           → Business logic, service interfaces/impls, factories, models
NSales.DTO              → Data transfer objects shared across layers
NSales.BackgroundService → Scheduled background jobs
NSales.ACL              → Anti-corruption layer (external API adapters)
Core.Domain             → Base/shared domain abstractions
DB.Infra                → EF Core 9 DbContext (NSalesContext), repositories, Unit of Work
Lib/                    → External DLLs: NAuth.ACL, NAuth.DTO, NTools.ACL, NTools.DTO
```

**Dependency flow:** API → Application → Domain → DB.Infra → PostgreSQL (Npgsql)

**Key patterns:**
- Repository + Unit of Work (DB.Infra)
- EF Core with lazy loading proxies
- Custom `RemoteAuthHandler` for Bearer token auth (delegates to NAuth)
- DI registration centralized in `NSales.Application/Initializer.cs`

### Frontend - Layered React (src/)

```
Pages/        → Route-level components (HomePage, DashboardPage, ProductPage, etc.)
Components/   → Reusable UI (Menu, MessageToast, EditMode, ImageModal, etc.)
Contexts/     → React Context providers per domain (Auth, Product, Order, Network, etc.)
Business/     → Business logic layer with interfaces + implementations
Services/     → API communication layer (Axios-based)
Infra/        → HttpClient wrapper (Axios) with interface
DTO/          → TypeScript types: Domain models, API responses, enums
lib/nauth-core/ → Auth library
```

**Data flow:** Page → Context/Provider → Business → Service → HttpClient (Axios) → Backend API

**Key libraries:** MUI + Bootstrap (UI), React Router 6, Axios, Stripe, Web3, i18next, React DnD, Craft.js

### Routing (App.tsx)
- Public: `/`, `/network`, `/account/login`, `/:networkSlug`, `/@/:sellerSlug`
- Admin (protected): `/admin/dashboard`, `/admin/products`, `/admin/teams`, `/admin/team-structure`

## Environment

- **Dev API URL:** `https://localhost:44374` (from `.env`)
- **Prod API URL:** `https://monexup.com/api` (from `.env.production`)
- **Database:** PostgreSQL
- **TypeScript:** strict mode enabled, strictNullChecks disabled

## Conventions

- Frontend uses `.tsx` extension for all files including services and DTOs (not just components)
- Each domain concept (Product, Order, Network, User, Invoice, Profile, Template) follows the same layered pattern: Context → Business → Service
- Business/Service layers use interface + implementation pattern with factories (e.g., `ProductFactory.tsx` creates `ProductBusiness`)
- Context providers are composed via `ContextBuilder.tsx`
