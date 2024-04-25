CREATE SCHEMA IF NOT EXISTS "public";

CREATE SEQUENCE bande_de_commande_id_bande_de_commande_seq START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE chef_id_chef_seq START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE demande_besoin_id_demande_besoin_seq START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE departement_id_departement_seq START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE fournisseur_id_fournisseur_seq START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE methode_de_payement_id_payement_seq START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE produit_id_produit_seq START WITH 1 INCREMENT BY 1;

CREATE SEQUENCE regroupement_besoin_id_regroupement_besoin_seq START WITH 1 INCREMENT BY 1;

CREATE  TABLE chef ( 
	id_chef              integer DEFAULT nextval('chef_id_chef_seq'::regclass) NOT NULL  ,
	nom_chef             varchar    ,
	mdp                  varchar    ,
	CONSTRAINT pk_chef PRIMARY KEY ( id_chef )
 );

CREATE  TABLE departement ( 
	id_departement       integer DEFAULT nextval('departement_id_departement_seq'::regclass) NOT NULL  ,
	nom_departement      varchar(255)  NOT NULL  ,
	CONSTRAINT departement_pkey PRIMARY KEY ( id_departement )
 );

CREATE  TABLE fournisseur ( 
	nom_fournisseur      varchar(255)  NOT NULL  ,
	contact              varchar(100)    ,
	email                varchar(255)    ,
	id_fournisseur       integer DEFAULT nextval('fournisseur_id_fournisseur_seq'::regclass) NOT NULL  ,
	CONSTRAINT fournisseur_pkey PRIMARY KEY ( id_fournisseur )
 );

CREATE  TABLE methode_de_payement ( 
	id_payement          integer DEFAULT nextval('methode_de_payement_id_payement_seq'::regclass) NOT NULL  ,
	designation          varchar(255)    ,
	CONSTRAINT methode_de_payement_pkey PRIMARY KEY ( id_payement )
 );

CREATE  TABLE produit ( 
	id_produit           integer DEFAULT nextval('produit_id_produit_seq'::regclass) NOT NULL  ,
	nom_produit          varchar    ,
	tva                  double precision    ,
	CONSTRAINT pk_produit PRIMARY KEY ( id_produit )
 );

CREATE  TABLE stock_produit ( 
	id_fournisseur       integer    ,
	id_produit           integer    ,
	quantite             double precision    ,
	prix_unitaire        double precision  NOT NULL  ,
	CONSTRAINT fk_stock_produit_fournisseur FOREIGN KEY ( id_fournisseur ) REFERENCES fournisseur( id_fournisseur ) ON DELETE CASCADE ON UPDATE CASCADE ,
	CONSTRAINT fk_stock_produit_produit FOREIGN KEY ( id_produit ) REFERENCES produit( id_produit ) ON DELETE CASCADE ON UPDATE CASCADE 
 );

CREATE  TABLE bande_de_commande ( 
	id_bande_de_commande integer DEFAULT nextval('bande_de_commande_id_bande_de_commande_seq'::regclass) NOT NULL  ,
	titre                varchar(255) DEFAULT 'Bon de commande'::character varying   ,
	id_fournisseur       integer  NOT NULL  ,
	date_de_commande     timestamp DEFAULT CURRENT_TIMESTAMP   ,
	id_produit           integer    ,
	quantite             double precision  NOT NULL  ,
	etat                 integer    ,
	id_methode_payement  integer    ,
	CONSTRAINT pk_proforma PRIMARY KEY ( id_bande_de_commande ),
	CONSTRAINT fk_bande_de_commande FOREIGN KEY ( id_fournisseur ) REFERENCES fournisseur( id_fournisseur )   ,
	CONSTRAINT fk_proforma_produit FOREIGN KEY ( id_produit ) REFERENCES produit( id_produit )   
 );

CREATE  TABLE demande_besoin ( 
	id_departement       integer    ,
	id_produit           integer    ,
	quantite             double precision  NOT NULL  ,
	etat                 integer  NOT NULL  ,
	id_demande_besoin    integer DEFAULT nextval('demande_besoin_id_demande_besoin_seq'::regclass) NOT NULL  ,
	CONSTRAINT pk_demande_besoin PRIMARY KEY ( id_demande_besoin ),
	CONSTRAINT fk_demande_besoin_departement FOREIGN KEY ( id_departement ) REFERENCES departement( id_departement ) ON DELETE CASCADE ON UPDATE CASCADE ,
	CONSTRAINT fk_demande_besoin_produit FOREIGN KEY ( id_produit ) REFERENCES produit( id_produit )   
 );

CREATE  TABLE regroupement_besoin ( 
	id_produit           integer  NOT NULL  ,
	quantite             double precision    ,
	etat                 integer  NOT NULL  ,
	id_demande_besoin    integer  NOT NULL  ,
	id_regroupement_besoin integer DEFAULT nextval('regroupement_besoin_id_regroupement_besoin_seq'::regclass) NOT NULL  ,
	CONSTRAINT pk_regroupement_besoin PRIMARY KEY ( id_regroupement_besoin ),
	CONSTRAINT fk_regroupement_besoin FOREIGN KEY ( id_demande_besoin ) REFERENCES demande_besoin( id_demande_besoin )   ,
	CONSTRAINT fk_regroupement_besoin_produit FOREIGN KEY ( id_produit ) REFERENCES produit( id_produit )   
 );

CREATE VIEW v_demande_besoin AS SELECT departement.id_departement,
    departement.nom_departement,
    produit.id_produit,
    produit.nom_produit,
    produit.tva,
    demande_besoin.quantite,
    demande_besoin.etat,
    demande_besoin.id_demande_besoin
   FROM ((demande_besoin
     JOIN departement ON ((demande_besoin.id_departement = departement.id_departement)))
     JOIN produit ON ((demande_besoin.id_produit = produit.id_produit)));

CREATE VIEW v_regroupement_besoin AS SELECT n.id_departement,
    n.id_produit,
    n.quantite,
    n.etat AS etat_demande_besoin,
    i.etat AS etat_regroupement_besoin,
    i.id_demande_besoin,
    i.id_regroupement_besoin
   FROM (demande_besoin n
     JOIN regroupement_besoin i ON ((i.id_demande_besoin = n.id_demande_besoin)));

CREATE VIEW v_stock_produit_fournisseur AS SELECT t.id_produit,
    t.nom_produit,
    t.tva,
    i.id_fournisseur,
    i.prix_unitaire,
    i.quantite,
    r.nom_fournisseur,
    r.contact,
    r.email
   FROM ((produit t
     JOIN stock_produit i ON ((i.id_produit = t.id_produit)))
     JOIN fournisseur r ON ((i.id_fournisseur = r.id_fournisseur)));

INSERT INTO chef( id_chef, nom_chef, mdp ) VALUES ( 1, 'Finance', 'finance');
INSERT INTO chef( id_chef, nom_chef, mdp ) VALUES ( 2, 'Directeur General', 'dg');
INSERT INTO chef( id_chef, nom_chef, mdp ) VALUES ( 3, 'Directeur Adjoint', 'da');
INSERT INTO departement( id_departement, nom_departement ) VALUES ( 1, 'Informatique Technologie');
INSERT INTO departement( id_departement, nom_departement ) VALUES ( 2, 'Finance');
INSERT INTO fournisseur( nom_fournisseur, contact, email, id_fournisseur ) VALUES ( 'Jumbo Score', '0345345867', 'judiherinirina04@gmail.com', 1);
INSERT INTO fournisseur( nom_fournisseur, contact, email, id_fournisseur ) VALUES ( 'Shopritte', '0324508954', 'judiherinirina04@gmail.com', 2);
INSERT INTO fournisseur( nom_fournisseur, contact, email, id_fournisseur ) VALUES ( 'Shop Liantsoa', '0342501924', 'judiherinirina04@gmail.com', 3);
INSERT INTO fournisseur( nom_fournisseur, contact, email, id_fournisseur ) VALUES ( 'Vrai Prix', '0332901922', 'judiherinirina04@gmail.com', 4);
INSERT INTO methode_de_payement( id_payement, designation ) VALUES ( 1, 'Check');
INSERT INTO methode_de_payement( id_payement, designation ) VALUES ( 2, 'Especes');
INSERT INTO methode_de_payement( id_payement, designation ) VALUES ( 3, 'Mobile money');
INSERT INTO produit( id_produit, nom_produit, tva ) VALUES ( 1, 'Cache bouche', 0.0);
INSERT INTO produit( id_produit, nom_produit, tva ) VALUES ( 2, 'Stylo', 10.0);
INSERT INTO stock_produit( id_fournisseur, id_produit, quantite, prix_unitaire ) VALUES ( 1, 1, 50.0, 100.0);
INSERT INTO stock_produit( id_fournisseur, id_produit, quantite, prix_unitaire ) VALUES ( 2, 1, 10.0, 900.0);
INSERT INTO stock_produit( id_fournisseur, id_produit, quantite, prix_unitaire ) VALUES ( 3, 1, 30.0, 1100.0);
INSERT INTO stock_produit( id_fournisseur, id_produit, quantite, prix_unitaire ) VALUES ( 4, 1, 25.0, 700.0);
INSERT INTO stock_produit( id_fournisseur, id_produit, quantite, prix_unitaire ) VALUES ( 4, 2, 15.0, 300.0);
INSERT INTO stock_produit( id_fournisseur, id_produit, quantite, prix_unitaire ) VALUES ( 3, 2, 10.0, 400.0);
INSERT INTO stock_produit( id_fournisseur, id_produit, quantite, prix_unitaire ) VALUES ( 2, 2, 20.0, 800.0);
INSERT INTO stock_produit( id_fournisseur, id_produit, quantite, prix_unitaire ) VALUES ( 1, 2, 15.0, 900.0);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 2, 'bon de commande', 4, '2023-11-28 12:00:00 AM', 2, 25.0, 5, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 3, 'bon de commande', 3, '2023-11-21 12:00:00 AM', 2, 26.0, 5, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 4, 'bon de commande', 4, '2023-11-16 12:00:00 AM', 2, 15.0, 5, 3);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 12, 'bon de commande', 3, '2023-11-23 12:00:00 AM', 2, 17.0, 5, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 33, 'bon de commande', 4, '2023-11-21 12:00:00 AM', 1, 29.0, 5, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 34, 'bon de commande', 4, '2023-11-21 12:00:00 AM', 1, 29.0, 5, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 32, 'bon de commande', 1, '2023-11-14 12:00:00 AM', 1, 54.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 5, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 15.0, 10, 1);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 6, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 15.0, 10, 1);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 7, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 15.0, 10, 1);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 8, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 15.0, 10, 1);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 9, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 15.0, 10, 1);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 10, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 15.0, 10, 1);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 11, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 15.0, 10, 1);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 13, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 19.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 14, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 19.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 15, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 16.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 16, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 16.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 17, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 16.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 18, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 16.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 19, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 16.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 20, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 16.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 21, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 16.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 22, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 16.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 23, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 16.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 24, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 16.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 25, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 16.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 26, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 16.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 27, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 16.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 28, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 16.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 29, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 16.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 30, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 16.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 31, 'bon de commande', 4, '2023-11-20 12:00:00 AM', 2, 17.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 35, 'bon de commande', 1, '2023-11-19 12:00:00 AM', 1, 50.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 36, 'bon de commande', 4, '2023-11-16 12:00:00 AM', 1, 25.0, 5, 1);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 38, 'bon de commande', 1, '2023-11-22 12:00:00 AM', 1, 130.0, 5, 3);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 39, 'bon de commande', 1, '2023-11-30 12:00:00 AM', 1, 50.0, 5, 1);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 40, 'bon de commande', 4, '2023-11-28 12:00:00 AM', 2, 15.0, 5, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 41, 'bon de commande', 4, '2023-11-28 12:00:00 AM', 2, 15.0, 5, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 42, 'bon de commande', 4, '2023-11-28 12:00:00 AM', 2, 15.0, 5, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 43, 'bon de commande', 4, '2023-11-28 12:00:00 AM', 2, 15.0, 5, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 37, 'bon de commande', 1, '2023-11-08 12:00:00 AM', 1, 50.0, 10, 2);
INSERT INTO bande_de_commande( id_bande_de_commande, titre, id_fournisseur, date_de_commande, id_produit, quantite, etat, id_methode_payement ) VALUES ( 1, 'bon de commande', 1, '2023-11-20 12:00:00 AM', 1, 35.0, 10, 2);
INSERT INTO demande_besoin( id_departement, id_produit, quantite, etat, id_demande_besoin ) VALUES ( 1, 2, 25.0, 10, 1);
INSERT INTO demande_besoin( id_departement, id_produit, quantite, etat, id_demande_besoin ) VALUES ( 2, 1, 35.0, 10, 2);
INSERT INTO demande_besoin( id_departement, id_produit, quantite, etat, id_demande_besoin ) VALUES ( 2, 1, 20.0, 10, 3);
INSERT INTO demande_besoin( id_departement, id_produit, quantite, etat, id_demande_besoin ) VALUES ( 1, 1, 15.0, 10, 4);
INSERT INTO demande_besoin( id_departement, id_produit, quantite, etat, id_demande_besoin ) VALUES ( 1, 1, 10.0, 10, 5);
INSERT INTO demande_besoin( id_departement, id_produit, quantite, etat, id_demande_besoin ) VALUES ( 1, 1, 100.0, 0, 6);
INSERT INTO demande_besoin( id_departement, id_produit, quantite, etat, id_demande_besoin ) VALUES ( 1, 1, 120.0, 10, 7);
INSERT INTO regroupement_besoin( id_produit, quantite, etat, id_demande_besoin, id_regroupement_besoin ) VALUES ( 2, 25.0, 5, 1, 1);
INSERT INTO regroupement_besoin( id_produit, quantite, etat, id_demande_besoin, id_regroupement_besoin ) VALUES ( 1, 35.0, 5, 2, 2);
INSERT INTO regroupement_besoin( id_produit, quantite, etat, id_demande_besoin, id_regroupement_besoin ) VALUES ( 1, 20.0, 5, 3, 3);
INSERT INTO regroupement_besoin( id_produit, quantite, etat, id_demande_besoin, id_regroupement_besoin ) VALUES ( 1, 15.0, 5, 4, 4);
INSERT INTO regroupement_besoin( id_produit, quantite, etat, id_demande_besoin, id_regroupement_besoin ) VALUES ( 1, 10.0, 5, 5, 5);
INSERT INTO regroupement_besoin( id_produit, quantite, etat, id_demande_besoin, id_regroupement_besoin ) VALUES ( 1, 120.0, 5, 7, 6);
