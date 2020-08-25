-- Table: public."Articulo"

-- DROP TABLE public."Articulo";

CREATE TABLE public."Articulo"
(
    "Id" integer NOT NULL DEFAULT nextval('"TablaArticulo_Id_seq"'::regclass),
    "Descripcion" text COLLATE pg_catalog."default",
    "Ean" text COLLATE pg_catalog."default",
    "Imagen" text COLLATE pg_catalog."default",
    "FechaAlta" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_Articulo" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE public."Articulo"
    OWNER to postgres;


-- Table: public."Tienda"

-- DROP TABLE public."Tienda";

CREATE TABLE public."Tienda"
(
    "Id" integer NOT NULL DEFAULT nextval('"TablaTienda_Id_seq"'::regclass),
    "Nombre" text COLLATE pg_catalog."default" NOT NULL,
    "Latitud" double precision,
    "Longitud" double precision,
    "FechaAlta" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_Tienda" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE public."Tienda"
    OWNER to postgres;


-- Table: public."Precio"

-- DROP TABLE public."Precio";

CREATE TABLE public."Precio"
(
    "Id" integer NOT NULL DEFAULT nextval('"TablaPrecio_Id_seq"'::regclass),
    "Importe" numeric NOT NULL,
    "Fecha" timestamp without time zone NOT NULL,
    "ArticuloId" integer NOT NULL,
    "TiendaId" integer NOT NULL,
    CONSTRAINT "PK_Precio" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Precio_Articulo_ArticuloId" FOREIGN KEY ("ArticuloId")
        REFERENCES public."Articulo" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
    CONSTRAINT "FK_Precio_Tienda_TiendaId" FOREIGN KEY ("TiendaId")
        REFERENCES public."Tienda" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE public."Precio"
    OWNER to postgres;

-- Index: IX_Precio_ArticuloId

-- DROP INDEX public."IX_Precio_ArticuloId";

CREATE INDEX "IX_Precio_ArticuloId"
    ON public."Precio" USING btree
    ("ArticuloId" ASC NULLS LAST)
    TABLESPACE pg_default;
    
-- Index: IX_Precio_TiendaId

-- DROP INDEX public."IX_Precio_TiendaId";

CREATE INDEX "IX_Precio_TiendaId"
    ON public."Precio" USING btree
    ("TiendaId" ASC NULLS LAST)
    TABLESPACE pg_default;
