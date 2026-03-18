-- Lofn Database Creation Script
-- PostgreSQL
-- Generated from EF Core Code First model

CREATE TABLE stores (
    store_id BIGSERIAL NOT NULL,
    name VARCHAR(120) NOT NULL,
    owner_id BIGINT NOT NULL,
    CONSTRAINT stores_pkey PRIMARY KEY (store_id)
);

CREATE TABLE store_users (
    store_user_id BIGSERIAL NOT NULL,
    store_id BIGINT NOT NULL,
    user_id BIGINT NOT NULL,
    CONSTRAINT store_users_pkey PRIMARY KEY (store_user_id),
    CONSTRAINT fk_store_user_store FOREIGN KEY (store_id) REFERENCES stores (store_id) ON DELETE CASCADE
);

CREATE TABLE categories (
    category_id BIGSERIAL NOT NULL,
    slug VARCHAR(120) NOT NULL,
    name VARCHAR(120) NOT NULL,
    store_id BIGINT,
    CONSTRAINT categories_pkey PRIMARY KEY (category_id),
    CONSTRAINT fk_category_store FOREIGN KEY (store_id) REFERENCES stores (store_id)
);

CREATE TABLE products (
    product_id BIGSERIAL NOT NULL,
    user_id BIGINT NOT NULL,
    slug VARCHAR(120) NOT NULL,
    name VARCHAR(120) NOT NULL,
    price DOUBLE PRECISION NOT NULL,
    frequency INTEGER NOT NULL,
    "limit" INTEGER NOT NULL,
    status INTEGER NOT NULL,
    description TEXT,
    image VARCHAR(150),
    store_id BIGINT,
    category_id BIGINT,
    CONSTRAINT products_pkey PRIMARY KEY (product_id),
    CONSTRAINT fk_product_store FOREIGN KEY (store_id) REFERENCES stores (store_id),
    CONSTRAINT fk_product_category FOREIGN KEY (category_id) REFERENCES categories (category_id)
);

CREATE TABLE orders (
    order_id BIGSERIAL NOT NULL,
    user_id BIGINT NOT NULL,
    status INTEGER NOT NULL DEFAULT 1,
    created_at TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    updated_at TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    seller_id BIGINT,
    store_id BIGINT,
    CONSTRAINT orders_pkey PRIMARY KEY (order_id),
    CONSTRAINT fk_order_store FOREIGN KEY (store_id) REFERENCES stores (store_id)
);

CREATE TABLE product_images (
    image_id BIGSERIAL NOT NULL,
    product_id BIGINT NOT NULL,
    image VARCHAR(150),
    sort_order INTEGER NOT NULL DEFAULT 0,
    CONSTRAINT product_images_pkey PRIMARY KEY (image_id),
    CONSTRAINT fk_product_image_product FOREIGN KEY (product_id) REFERENCES products (product_id) ON DELETE CASCADE
);

CREATE TABLE order_items (
    item_id BIGINT NOT NULL,
    order_id BIGINT NOT NULL,
    product_id BIGINT NOT NULL,
    quantity INTEGER NOT NULL DEFAULT 1,
    CONSTRAINT order_items_pkey PRIMARY KEY (item_id),
    CONSTRAINT fk_order_item_order FOREIGN KEY (order_id) REFERENCES orders (order_id),
    CONSTRAINT fk_order_item_product FOREIGN KEY (product_id) REFERENCES products (product_id)
);
