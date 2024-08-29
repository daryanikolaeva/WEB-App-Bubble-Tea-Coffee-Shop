CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE customer (
    tel_num integer NOT NULL,
    name character(20) NULL,
    password character(100) NULL,
    CONSTRAINT customer_pkey PRIMARY KEY (tel_num)
);

CREATE TABLE product (
    prod_id integer GENERATED ALWAYS AS IDENTITY,
    prod_name character(50) NULL,
    prod_price money NULL,
    CONSTRAINT product_pkey PRIMARY KEY (prod_id)
);

CREATE TABLE ordering (
    order_id integer GENERATED ALWAYS AS IDENTITY,
    tel_num integer NULL,
    order_price money NULL,
    CONSTRAINT ordering_pkey PRIMARY KEY (order_id),
    CONSTRAINT ordering_tel_num_fkey FOREIGN KEY (tel_num) REFERENCES customer (tel_num)
);

CREATE TABLE products_in_order (
    prod_id integer NOT NULL,
    order_id integer NOT NULL,
    amount integer NULL DEFAULT (1),
    CONSTRAINT pk_prod_in_order PRIMARY KEY (prod_id, order_id),
    CONSTRAINT products_in_order_order_id_fkey FOREIGN KEY (order_id) REFERENCES ordering (order_id),
    CONSTRAINT products_in_order_prod_id_fkey FOREIGN KEY (prod_id) REFERENCES product (prod_id)
);

CREATE INDEX "IX_ordering_tel_num" ON ordering (tel_num);

CREATE INDEX "IX_products_in_order_order_id" ON products_in_order (order_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240508153548_Mogration1', '6.0.29');

COMMIT;

