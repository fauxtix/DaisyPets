BEGIN TRANSACTION;

CREATE TABLE IF NOT EXISTS "PetsLog" (
	"Id"	INTEGER NOT NULL,
	"Message"	TEXT NOT NULL,
	"MessageTemplate"	TEXT,
	"Level"	TEXT,
	"TimeStamp"	TEXT,
	"Exception"	TEXT NOT NULL,
	"Properties"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT)
);

CREATE TABLE IF NOT EXISTS "Especie" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Descricao"	TEXT NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Raca" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Descricao"	TEXT NOT NULL UNIQUE,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Esterilizacao" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"IdPet"	INTEGER NOT NULL,
	"Veterinario"	TEXT NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "MarcaRacao" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Descricao"	TEXT NOT NULL UNIQUE,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "TipoDesparasitanteInterno" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Descricao"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "TipoDesparasitanteExterno" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Descricao"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Temperamento" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Descricao"	TEXT NOT NULL,
	"Observacoes"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "GaleriaFotos" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"IdPet"	INTEGER NOT NULL,
	"Imagem"	TEXT NOT NULL,
	"Data"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Medicacao" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Descricao"	TEXT NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Situacao" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Descricao"	TEXT NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Tamanho" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Descricao"	TEXT NOT NULL UNIQUE,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Idade" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"De"	INTEGER NOT NULL,
	"A"	INTEGER,
	"Descricao"	TEXT,
	"IdTamanho"	INTEGER,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Peso" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"De"	INTEGER NOT NULL,
	"A"	INTEGER NOT NULL,
	"Descricao"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Contacto" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Nome"	TEXT NOT NULL UNIQUE,
	"Morada"	TEXT NOT NULL,
	"Localidade"	TEXT,
	"Movel"	TEXT,
	"EMail"	TEXT,
	"Notas"	TEXT,
	"IdTipoContacto"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "TipoContacto" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Descricao"	TEXT NOT NULL UNIQUE,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Pet" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"IdEspecie"	INTEGER NOT NULL,
	"IdRaca"	REAL NOT NULL,
	"DataNascimento"	BLOB NOT NULL,
	"IdSituacao"	REAL NOT NULL,
	"Nome"	BLOB NOT NULL UNIQUE,
	"Foto"	TEXT NOT NULL,
	"Cor"	TEXT NOT NULL,
	"IdPeso"	INTEGER,
	"IdTemperamento"	INTEGER NOT NULL,
	"Medicacao"	TEXT,
	"IdTamanho"	INTEGER,
	"Genero"	TEXT DEFAULT 'M',
	"Chip"	TEXT NOT NULL,
	"Chipado"	NUMERIC NOT NULL DEFAULT 0,
	"DataChip"	TEXT,
	"NumeroChip"	TEXT,
	"Esterilizado"	NUMERIC NOT NULL DEFAULT 0,
	"Padrinho"	NUMERIC NOT NULL DEFAULT 0,
	"DoencaCronica"	TEXT,
	"Observacoes"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Vacina" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"IdPet"	INTEGER NOT NULL,
	"DataToma"	TEXT NOT NULL,
	"ProximaTomaEmMeses"	INTEGER NOT NULL,
	"Marca"	TEXT NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Racao" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"IdPet"	INTEGER NOT NULL,
	"DataCompra"	TEXT,
	"Marca"	TEXT NOT NULL,
	"QuantidadeDiaria"	INTEGER,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Desparasitante" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"IdPet"	INTEGER NOT NULL,
	"Tipo"	TEXT NOT NULL,
	"Marca"	TEXT NOT NULL,
	"DataAplicacao"	TEXT NOT NULL,
	"DataProximaAplicacao"	TEXT NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "CategoriaDespesa" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Descricao"	TEXT NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Despesa" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"DataCriacao"	TEXT NOT NULL,
	"DataMovimento"	TEXT NOT NULL,
	"Descricao"	TEXT NOT NULL,
	"ValorPago"	REAL NOT NULL,
	"IdTipoDespesa"	INTEGER NOT NULL,
	"IdCategoriaDespesa"	INTEGER NOT NULL,
	"Notas"	TEXT,
	"TipoMovimento"	TEXT NOT NULL DEFAULT 'S',
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "TipoDespesa" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Descricao"	TEXT NOT NULL,
	"IdCategoriaDespesa"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "ConsultaVeterinario" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"DataConsulta"	TEXT NOT NULL,
	"Motivo"	TEXT NOT NULL,
	"Diagnostico"	TEXT,
	"Tratamento"	TEXT,
	"IdPet"	INTEGER NOT NULL,
	"Notas"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Documento" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Title"	TEXT NOT NULL,
	"Description"	TEXT,
	"DocumentPath"	TEXT NOT NULL,
	"CreatedOn"	TEXT NOT NULL,
	"PetId"	INTEGER NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "AppSettings" (
	"CultureName"	TEXT
);
CREATE TABLE IF NOT EXISTS "Comment" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"PostId"	INTEGER NOT NULL,
	"BlogUserId"	INTEGER NOT NULL,
	"CommentText"	TEXT NOT NULL,
	"Created"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "ToDoCategories" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Descricao"	TEXT NOT NULL,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Todo" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"StartDate"	TEXT NOT NULL,
	"EndDate"	TEXT NOT NULL,
	"Status"	INTEGER NOT NULL DEFAULT 0,
	"Description"	TEXT NOT NULL,
	"CategoryId"	INTEGER NOT NULL,
	"Generated"	INTEGER NOT NULL DEFAULT 0,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Post" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Title"	TEXT NOT NULL,
	"Introduction"	TEXT NOT NULL,
	"BodyText"	TEXT NOT NULL,
	"Image"	TEXT NOT NULL,
	"PostUrl"	TEXT,
	PRIMARY KEY("Id" AUTOINCREMENT)
);
INSERT INTO "Especie" ("Id","Descricao") VALUES (1,'Cão');
INSERT INTO "Especie" ("Id","Descricao") VALUES (2,'Gato');
INSERT INTO "Raca" ("Id","Descricao") VALUES (1,'Bolognese');
INSERT INTO "Raca" ("Id","Descricao") VALUES (2,'Terrier');
INSERT INTO "Raca" ("Id","Descricao") VALUES (3,'Bulldog');
INSERT INTO "Raca" ("Id","Descricao") VALUES (4,'Basset Hound');
INSERT INTO "Raca" ("Id","Descricao") VALUES (5,'Boxer');
INSERT INTO "Raca" ("Id","Descricao") VALUES (6,'Labrador');
INSERT INTO "Raca" ("Id","Descricao") VALUES (7,'Galgo');
INSERT INTO "Raca" ("Id","Descricao") VALUES (8,'Pitbull');
INSERT INTO "Raca" ("Id","Descricao") VALUES (9,'Podengo');
INSERT INTO "Raca" ("Id","Descricao") VALUES (10,'Rafeiro do Alentejo');
INSERT INTO "Raca" ("Id","Descricao") VALUES (11,'Sem raça definida');
INSERT INTO "Raca" ("Id","Descricao") VALUES (12,'Cão de fila S. Miguel');
INSERT INTO "MarcaRacao" ("Id","Descricao") VALUES (1,'ACTIVPET CROQUETES COM CARNE');
INSERT INTO "MarcaRacao" ("Id","Descricao") VALUES (2,'AVENAL DOG ADULTO CARNE E CEREAIS');
INSERT INTO "MarcaRacao" ("Id","Descricao") VALUES (3,'CAMPEÃO ADULTO');
INSERT INTO "MarcaRacao" ("Id","Descricao") VALUES (4,'AUCHAN MULTICROC MENÚ COMPLETO');
INSERT INTO "MarcaRacao" ("Id","Descricao") VALUES (5,'BEYOND SIMPLY 9 RICO EN CORDERO COM CEBADA INTEGRAL');
INSERT INTO "MarcaRacao" ("Id","Descricao") VALUES (6,'BRIBON RICO EM CARNE ADULTOS');
INSERT INTO "TipoDesparasitanteInterno" ("Id","Descricao") VALUES (1,'Elanco Capstar 11,4 mg para Gatos e Cães Pequenos');
INSERT INTO "TipoDesparasitanteInterno" ("Id","Descricao") VALUES (2,'Frontpro Comprimidos Antiparasitários Mastigáveis para Cães de 4 a 10 kg');
INSERT INTO "TipoDesparasitanteInterno" ("Id","Descricao") VALUES (3,'Frontpro Comprimidos Antiparasitários Mastigáveis para Cães de 2 a 4 kg');
INSERT INTO "TipoDesparasitanteExterno" ("Id","Descricao") VALUES (1,'Nice Care Coleira Antiparasitária para cães');
INSERT INTO "TipoDesparasitanteExterno" ("Id","Descricao") VALUES (2,'Nice Care Champô Repelente de Insetos para cães');
INSERT INTO "TipoDesparasitanteExterno" ("Id","Descricao") VALUES (3,'Nice Care Pipetas Antiparasitárias para cães');
INSERT INTO "TipoDesparasitanteExterno" ("Id","Descricao") VALUES (4,'FRONTLINE TRI-ACT - Protege contra o vetor da Leishmaniose ');
INSERT INTO "Temperamento" ("Id","Descricao","Observacoes") VALUES (1,'Sociável a pessoas e animais','');
INSERT INTO "Temperamento" ("Id","Descricao","Observacoes") VALUES (2,'Não Sociável a animais','');
INSERT INTO "Temperamento" ("Id","Descricao","Observacoes") VALUES (3,'Não Sociável a pessoas','');
INSERT INTO "Medicacao" ("Id","Descricao") VALUES (1,'Comprimido mastigável contra pulgas e carraças para cães de 1,3 a 2,5 kg - AdTab');
INSERT INTO "Medicacao" ("Id","Descricao") VALUES (2,'Pipetas para cão 40-60 kg - Frontline Combo');
INSERT INTO "Medicacao" ("Id","Descricao") VALUES (3,'Pipetas repelentes naturais para cães até 20 kg - Biospotix Spot On - Biogance');
INSERT INTO "Medicacao" ("Id","Descricao") VALUES (4,'ALIZIN');
INSERT INTO "Medicacao" ("Id","Descricao") VALUES (5,'BOVIGEN SCOUR');
INSERT INTO "Medicacao" ("Id","Descricao") VALUES (6,'CANIGEN CHPL');
INSERT INTO "Situacao" ("Id","Descricao") VALUES (1,'Adotado');
INSERT INTO "Situacao" ("Id","Descricao") VALUES (2,'Fat');
INSERT INTO "Situacao" ("Id","Descricao") VALUES (3,'Em canil');
INSERT INTO "Situacao" ("Id","Descricao") VALUES (4,'Na rua');
INSERT INTO "Situacao" ("Id","Descricao") VALUES (5,'Numa associacao');
INSERT INTO "Situacao" ("Id","Descricao") VALUES (6,'Com dono negligente');
INSERT INTO "Situacao" ("Id","Descricao") VALUES (7,'Num hotel');
INSERT INTO "Situacao" ("Id","Descricao") VALUES (8,'Outra');
INSERT INTO "Tamanho" ("Id","Descricao") VALUES (1,'Pequeno');
INSERT INTO "Tamanho" ("Id","Descricao") VALUES (2,'Médio');
INSERT INTO "Tamanho" ("Id","Descricao") VALUES (3,'Grande');
INSERT INTO "Idade" ("Id","De","A","Descricao","IdTamanho") VALUES (1,1,8,'Cachorro',1);
INSERT INTO "Idade" ("Id","De","A","Descricao","IdTamanho") VALUES (2,9,96,'Adulto',1);
INSERT INTO "Idade" ("Id","De","A","Descricao","IdTamanho") VALUES (3,97,240,'Sénior',1);
INSERT INTO "Idade" ("Id","De","A","Descricao","IdTamanho") VALUES (4,1,12,'Cachorro',2);
INSERT INTO "Idade" ("Id","De","A","Descricao","IdTamanho") VALUES (5,13,84,'Adulto',2);
INSERT INTO "Idade" ("Id","De","A","Descricao","IdTamanho") VALUES (6,85,240,'Sénior',2);
INSERT INTO "Idade" ("Id","De","A","Descricao","IdTamanho") VALUES (7,1,15,'Cachorro',3);
INSERT INTO "Idade" ("Id","De","A","Descricao","IdTamanho") VALUES (8,16,108,'Adulto',3);
INSERT INTO "Idade" ("Id","De","A","Descricao","IdTamanho") VALUES (9,109,240,'Sénior',3);
INSERT INTO "Peso" ("Id","De","A","Descricao") VALUES (1,1,15,'Pequeno');
INSERT INTO "Peso" ("Id","De","A","Descricao") VALUES (2,16,25,'Médio');
INSERT INTO "Peso" ("Id","De","A","Descricao") VALUES (3,26,45,'Grande');
INSERT INTO "Contacto" ("Id","Nome","Morada","Localidade","Movel","EMail","Notas","IdTipoContacto") VALUES (4,'Clínica Veterinária de Odivelas','Rua Guilherme Gomes Fernandes 21 rés do chão dto','Odivelas','219313237','','Horários de funcionamento em:

www.clinicaveterinariadeodivelas.pt
',1);
INSERT INTO "Contacto" ("Id","Nome","Morada","Localidade","Movel","EMail","Notas","IdTipoContacto") VALUES (5,'Clínica Veterinaria Absolut Pets','Praca de Portugal, 3 Loja esq ','Colinas do Cruzeiro - Odivelas','219325418','absolutpets@gmail.com','Horários de funcionamento em:

https://www.absolutpets.com/horario-e-contactos.html
',1);
INSERT INTO "Contacto" ("Id","Nome","Morada","Localidade","Movel","EMail","Notas","IdTipoContacto") VALUES (6,'Clínica Veterinária Dom Dinis','Rua Abel Manta 1','Odivelas','219338652','','https://www.clinicaveterinariadomdinis.pt/clinica-geral
',1);
INSERT INTO "Contacto" ("Id","Nome","Morada","Localidade","Movel","EMail","Notas","IdTipoContacto") VALUES (7,'União Zoófila',' R. Padre Carlos dos Santos','Alto das Furnas - Lisboa','0','uniaozoofila@gmail.com ','Horários de funcionamento em:

www.uniaozoofila.org
',2);
INSERT INTO "Contacto" ("Id","Nome","Morada","Localidade","Movel","EMail","Notas","IdTipoContacto") VALUES (8,'ANIMALIFE','Av. Praia da Victória - 15 - Cave','Lisboa','707309233','geral@animalife.pt','Horários de funcionamento em :

https://www.animalife.pt/pt/home
',2);
INSERT INTO "Contacto" ("Id","Nome","Morada","Localidade","Movel","EMail","Notas","IdTipoContacto") VALUES (9,'Associação Zoófila Portuguesa (AZP)','Av. Luís Bívar 85C','Lisboa','217970827','info@azp.pt','Serviços e horários de funcionamento em:

https://azp.pt/
',2);
INSERT INTO "Contacto" ("Id","Nome","Morada","Localidade","Movel","EMail","Notas","IdTipoContacto") VALUES (16,'A Casota dos bichos','Av 25 Abril 22 Loja B','Ramada - Odivelas','910318189','acasotadosbichos@hotmail.com','https://www.nicepage.com/website-templates
',1);
INSERT INTO "Contacto" ("Id","Nome","Morada","Localidade","Movel","EMail","Notas","IdTipoContacto") VALUES (17,'VetMonti - Drª Susana Barão Alves','R. Dr. Augusto Pereira Coutinho 4 - 2870 309 - Montijo','Montijo','918700387','vetmonti@gmail.com','Horários de funcionamento em:

Site: vetmonti-clinica-veterinaria.negocio.site',1);
INSERT INTO "TipoContacto" ("Id","Descricao") VALUES (1,'Clínicas');
INSERT INTO "TipoContacto" ("Id","Descricao") VALUES (2,'Associações');
INSERT INTO "TipoContacto" ("Id","Descricao") VALUES (3,'Canil');
INSERT INTO "TipoContacto" ("Id","Descricao") VALUES (4,'Util');
INSERT INTO "TipoContacto" ("Id","Descricao") VALUES (5,'Hotel');
INSERT INTO "Pet" ("Id","IdEspecie","IdRaca","DataNascimento","IdSituacao","Nome","Foto","Cor","IdPeso","IdTemperamento","Medicacao","IdTamanho","Genero","Chip","Chipado","DataChip","NumeroChip","Esterilizado","Padrinho","DoencaCronica","Observacoes") VALUES (13,1,11.0,'01/01/2020',1.0,'Shiva','shiva.jpg','Preto e castanho',20,2,'Aluporinol (dosagem a completar)',2,'F','',1,'27/01/2020','112354559998493',1,0,'Leishmaniose','Linda');
INSERT INTO "Pet" ("Id","IdEspecie","IdRaca","DataNascimento","IdSituacao","Nome","Foto","Cor","IdPeso","IdTemperamento","Medicacao","IdTamanho","Genero","Chip","Chipado","DataChip","NumeroChip","Esterilizado","Padrinho","DoencaCronica","Observacoes") VALUES (17,1,11.0,'11/01/2017',1.0,'Olie','Olie.jpg','Preto e branco',0,1,'',2,'M','',1,'06/01/2021','7397879842',1,0,'','');
INSERT INTO "Pet" ("Id","IdEspecie","IdRaca","DataNascimento","IdSituacao","Nome","Foto","Cor","IdPeso","IdTemperamento","Medicacao","IdTamanho","Genero","Chip","Chipado","DataChip","NumeroChip","Esterilizado","Padrinho","DoencaCronica","Observacoes") VALUES (18,1,9.0,'07/05/2018',1.0,'Che','Che.jpg','Castanho',0,1,'Nada conhecido',2,'M','',1,'14/07/2021','2535453541',1,0,'Desconhecida','Observações');
INSERT INTO "Pet" ("Id","IdEspecie","IdRaca","DataNascimento","IdSituacao","Nome","Foto","Cor","IdPeso","IdTemperamento","Medicacao","IdTamanho","Genero","Chip","Chipado","DataChip","NumeroChip","Esterilizado","Padrinho","DoencaCronica","Observacoes") VALUES (19,1,8.0,'02/12/2020',7.0,'Bono','pitbull.jpg','Castanho',0,3,'',3,'M','',1,'08/02/2019','1234',0,0,'','Sr. José - Cascais');
INSERT INTO "Vacina" ("Id","IdPet","DataToma","ProximaTomaEmMeses","Marca") VALUES (43,13,'02/06/2023',3,'Raiva');
INSERT INTO "Vacina" ("Id","IdPet","DataToma","ProximaTomaEmMeses","Marca") VALUES (44,17,'13/04/2023',3,'Para teste de insert com transaction');
INSERT INTO "Vacina" ("Id","IdPet","DataToma","ProximaTomaEmMeses","Marca") VALUES (45,18,'21/07/2023',3,'Parvovirose');
INSERT INTO "Vacina" ("Id","IdPet","DataToma","ProximaTomaEmMeses","Marca") VALUES (48,19,'18/05/2023',3,'Raiva');
INSERT INTO "Vacina" ("Id","IdPet","DataToma","ProximaTomaEmMeses","Marca") VALUES (49,13,'21/05/2023',3,'sfsfsdf');
INSERT INTO "Racao" ("Id","IdPet","DataCompra","Marca","QuantidadeDiaria") VALUES (10,13,'23/06/2023','Purina Extra',180);
INSERT INTO "Desparasitante" ("Id","IdPet","Tipo","Marca","DataAplicacao","DataProximaAplicacao") VALUES (12,13,'I','Cestal Plus','10/06/2023','10/09/2023');
INSERT INTO "Desparasitante" ("Id","IdPet","Tipo","Marca","DataAplicacao","DataProximaAplicacao") VALUES (13,13,'E','Bravecta','13/06/2023','23/09/2023');
INSERT INTO "Desparasitante" ("Id","IdPet","Tipo","Marca","DataAplicacao","DataProximaAplicacao") VALUES (15,18,'I','Vectra 3D','10/05/2023','10/08/2023');
INSERT INTO "Desparasitante" ("Id","IdPet","Tipo","Marca","DataAplicacao","DataProximaAplicacao") VALUES (16,17,'I','Scalibor','10/05/2023','10/08/2023');
INSERT INTO "Desparasitante" ("Id","IdPet","Tipo","Marca","DataAplicacao","DataProximaAplicacao") VALUES (20,19,'I','Vectra 3D','17/05/2023','17/08/2023');
INSERT INTO "CategoriaDespesa" ("Id","Descricao") VALUES (1,'Alimentação');
INSERT INTO "CategoriaDespesa" ("Id","Descricao") VALUES (2,'Saúde');
INSERT INTO "CategoriaDespesa" ("Id","Descricao") VALUES (3,'Medicamentos');
INSERT INTO "CategoriaDespesa" ("Id","Descricao") VALUES (4,'Bem-estar');
INSERT INTO "CategoriaDespesa" ("Id","Descricao") VALUES (5,'Higiene');
INSERT INTO "CategoriaDespesa" ("Id","Descricao") VALUES (10,'Alojamento');
INSERT INTO "Despesa" ("Id","DataCriacao","DataMovimento","Descricao","ValorPago","IdTipoDespesa","IdCategoriaDespesa","Notas","TipoMovimento") VALUES (20,'27/07/2023','27/07/2023','',50.0,21,10,'Julho de 2023','S');
INSERT INTO "Despesa" ("Id","DataCriacao","DataMovimento","Descricao","ValorPago","IdTipoDespesa","IdCategoriaDespesa","Notas","TipoMovimento") VALUES (21,'27/07/2023','24/07/2023','',3.6,16,1,'Continente (nas duas embalagens encomendadas, uma vinha estragada!)','S');
INSERT INTO "Despesa" ("Id","DataCriacao","DataMovimento","Descricao","ValorPago","IdTipoDespesa","IdCategoriaDespesa","Notas","TipoMovimento") VALUES (23,'27/07/2023','27/06/2023','',50.0,21,10,'Junho 2023','S');
INSERT INTO "Despesa" ("Id","DataCriacao","DataMovimento","Descricao","ValorPago","IdTipoDespesa","IdCategoriaDespesa","Notas","TipoMovimento") VALUES (24,'27/07/2023','30/06/2023','',1.0,16,1,'Minipreço','S');
INSERT INTO "Despesa" ("Id","DataCriacao","DataMovimento","Descricao","ValorPago","IdTipoDespesa","IdCategoriaDespesa","Notas","TipoMovimento") VALUES (27,'27/07/2023','27/05/2023','',20.0,21,10,'Maio 2023','S');
INSERT INTO "Despesa" ("Id","DataCriacao","DataMovimento","Descricao","ValorPago","IdTipoDespesa","IdCategoriaDespesa","Notas","TipoMovimento") VALUES (28,'27/07/2023','22/05/2023','',12.0,1,1,'Minipreço (mediterrânica)','S');
INSERT INTO "Despesa" ("Id","DataCriacao","DataMovimento","Descricao","ValorPago","IdTipoDespesa","IdCategoriaDespesa","Notas","TipoMovimento") VALUES (29,'01/08/2023','01/08/2023','',9.0,5,2,'','S');
INSERT INTO "TipoDespesa" ("Id","Descricao","IdCategoriaDespesa") VALUES (1,'Ração',1);
INSERT INTO "TipoDespesa" ("Id","Descricao","IdCategoriaDespesa") VALUES (2,'Biscoitos',1);
INSERT INTO "TipoDespesa" ("Id","Descricao","IdCategoriaDespesa") VALUES (3,'Consultas no veterinário',2);
INSERT INTO "TipoDespesa" ("Id","Descricao","IdCategoriaDespesa") VALUES (4,'Tratamentos',2);
INSERT INTO "TipoDespesa" ("Id","Descricao","IdCategoriaDespesa") VALUES (5,'Seguro',2);
INSERT INTO "TipoDespesa" ("Id","Descricao","IdCategoriaDespesa") VALUES (6,'Vacinação',2);
INSERT INTO "TipoDespesa" ("Id","Descricao","IdCategoriaDespesa") VALUES (7,'Desparasitante Interno',3);
INSERT INTO "TipoDespesa" ("Id","Descricao","IdCategoriaDespesa") VALUES (8,'Desparasitante Externo',3);
INSERT INTO "TipoDespesa" ("Id","Descricao","IdCategoriaDespesa") VALUES (9,'Esterilização',2);
INSERT INTO "TipoDespesa" ("Id","Descricao","IdCategoriaDespesa") VALUES (10,'Caminha',4);
INSERT INTO "TipoDespesa" ("Id","Descricao","IdCategoriaDespesa") VALUES (11,'Brinquedos',4);
INSERT INTO "TipoDespesa" ("Id","Descricao","IdCategoriaDespesa") VALUES (12,'Shampô',5);
INSERT INTO "TipoDespesa" ("Id","Descricao","IdCategoriaDespesa") VALUES (14,'Mais um tipo de despesas',6);
INSERT INTO "TipoDespesa" ("Id","Descricao","IdCategoriaDespesa") VALUES (15,'Vários tipos de despesas',6);
INSERT INTO "TipoDespesa" ("Id","Descricao","IdCategoriaDespesa") VALUES (16,'Paté',1);
INSERT INTO "TipoDespesa" ("Id","Descricao","IdCategoriaDespesa") VALUES (17,'Amaciador',5);
INSERT INTO "TipoDespesa" ("Id","Descricao","IdCategoriaDespesa") VALUES (18,'Escova',5);
INSERT INTO "TipoDespesa" ("Id","Descricao","IdCategoriaDespesa") VALUES (21,'Sr. José Sobral - Cascais',10);
INSERT INTO "ConsultaVeterinario" ("Id","DataConsulta","Motivo","Diagnostico","Tratamento","IdPet","Notas") VALUES (20,'14/06/2023','Rotina','Tudo normal','Continuar atual',13,'Próxima consulta em 23/09/2023. Verificar fim da carência do seguro, para ver se há necessidade de alterar data.');
INSERT INTO "Documento" ("Id","Title","Description","DocumentPath","CreatedOn","PetId") VALUES (19,'Análises bioquímicas','Visita à VetMonti em 14/06','Shiva_Analises_BIOQUIMICAS.pdf','19/07/2023',13);
INSERT INTO "Documento" ("Id","Title","Description","DocumentPath","CreatedOn","PetId") VALUES (20,'Hemograma','Visita à VetMonti em 14/06','Shiva_Analises_HEMOGRAMA.pdf','19/07/2023',13);
INSERT INTO "Documento" ("Id","Title","Description","DocumentPath","CreatedOn","PetId") VALUES (21,'Análises à Urina','Feitas posteriormente (16/06)','Shiva_AnalisesUrina.pdf','19/07/2023',13);
INSERT INTO "Documento" ("Id","Title","Description","DocumentPath","CreatedOn","PetId") VALUES (22,'Titulo para teste','Descrição para teste','Shiva_AnalisesUrina.pdf','27/07/2023',19);
INSERT INTO "AppSettings" ("CultureName") VALUES ('fr');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-US');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-US');
INSERT INTO "AppSettings" ("CultureName") VALUES ('pt-PT');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-US');
INSERT INTO "AppSettings" ("CultureName") VALUES ('fr');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('es');
INSERT INTO "AppSettings" ("CultureName") VALUES ('fr');
INSERT INTO "AppSettings" ("CultureName") VALUES ('pt-PT');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('es');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('pt-PT');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('pt-PT');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('pt-PT');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('pt-PT');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('fr');
INSERT INTO "AppSettings" ("CultureName") VALUES ('pt-PT');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('pt-PT');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "AppSettings" ("CultureName") VALUES ('es');
INSERT INTO "AppSettings" ("CultureName") VALUES ('fr');
INSERT INTO "AppSettings" ("CultureName") VALUES ('en-GB');
INSERT INTO "Comment" ("Id","PostId","BlogUserId","CommentText","Created") VALUES (1,1,'fauxtix.luix@hotmail.com','Comentário do Post','2023-07-26 10:08:11.1564614');
INSERT INTO "Comment" ("Id","PostId","BlogUserId","CommentText","Created") VALUES (2,1,'fauxtix.luix@hotmail.com','Mais um comentário','2023-07-26 10:08:32.7603425');
INSERT INTO "Comment" ("Id","PostId","BlogUserId","CommentText","Created") VALUES (3,1,'fauxtix.luix@hotmail.com','Aind mais um comentário do post.','2023-07-26 10:31:50.9951884');
INSERT INTO "Comment" ("Id","PostId","BlogUserId","CommentText","Created") VALUES (4,13,'fauxtix.luix@hotmail.com','Porreiro!','2023-08-05 13:11:56.0987182');
INSERT INTO "Comment" ("Id","PostId","BlogUserId","CommentText","Created") VALUES (5,13,'fauxtix.luix@hotmail.com','Mais um like','2023-08-05 13:12:13.6940627');
INSERT INTO "Comment" ("Id","PostId","BlogUserId","CommentText","Created") VALUES (6,13,'fauxtix.luix@hotmail.com','Este site é muito interessante. Continuem o bom trabalho!','2023-08-05 13:18:26.7080443');
INSERT INTO "Comment" ("Id","PostId","BlogUserId","CommentText","Created") VALUES (7,13,'fauxtix.luix@hotmail.com','Mais uma tentativa de refresh','2023-08-05 13:19:17.8815342');
INSERT INTO "ToDoCategories" ("Id","Descricao") VALUES (1,'Consulta no Veterinário');
INSERT INTO "ToDoCategories" ("Id","Descricao") VALUES (2,'Comprar medicamentos');
INSERT INTO "ToDoCategories" ("Id","Descricao") VALUES (3,'Chipagem');
INSERT INTO "ToDoCategories" ("Id","Descricao") VALUES (4,'Banhos');
INSERT INTO "ToDoCategories" ("Id","Descricao") VALUES (5,'Medicação');
INSERT INTO "ToDoCategories" ("Id","Descricao") VALUES (6,'Tratamentos');
INSERT INTO "ToDoCategories" ("Id","Descricao") VALUES (7,'Alimentação');
INSERT INTO "ToDoCategories" ("Id","Descricao") VALUES (9,'Vacinação');
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (2,'13/06/2023','14/06/2023',0,'Shiva - VetMonti - 16H00 (Próxima em 13/09/2023)',1,0);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (3,'01/06/2023','02/06/2023',0,'Shiva - BANHO - Uma vez por dia até melhorar (ao pé da cauda) => molhar com água morna, aplicar shampô => passar por água => aplicar condifionador => passar por água. Dar banho de 3/3 ou de 6/6 meses
',4,0);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (4,'10/06/2023','11/06/2023',0,'Shiva -  Comprimido Cestal Plus (Desparasitação Interna)',5,0);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (5,'10/09/2023','11/09/2023',1,'Shiva - Comprimido Cestal Plus (Desparasitação Interna)',5,0);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (6,'01/12/2023','02/12/2023',1,'Banho da Shiva',4,0);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (7,'03/06/2023','04/06/2023',0,'Shiva -  Comprimido Bravecto (Desparasitação Externa)',5,0);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (8,'13/09/2023','14/09/2023',1,'Shiva -  Comprimido Bravecto (Desparasitação Externa)',5,0);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (9,'13/06/2023','14/06/2023',0,'Shiva - FERIDAS - Aplicar OMNIMATRIX 2 vezes por dia até cicratizar

- axila direita
- atrás da orelha direita',6,0);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (10,'13/06/2023','14/06/2023',1,'Shiva - ORELHAS (ouvidos) - Aplicar ABELIA 1x por dia - massajar, desinfetar com seringa e retirar excesso com compressa.',6,0);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (11,'13/06/2023','20/06/2023',0,'Shiva - Uma carteira Fortiflora na ração das 19 horas.',7,0);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (12,'13/06/2023','20/06/2023',0,'Shiva - Ração saca preta: duas canecas por dia',7,0);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (13,'17/06/2023','18/06/2023',0,'Shiva - Margarida traz ração de saca roxa => misturar com ração de saca preta',7,0);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (14,'12/09/2023','13/09/2023',1,'Shiva - VetMonti - 16H00',1,0);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (15,'10/08/2023','11/08/2023',1,'Olie - Vacina da Para teste de insert com transaction',1,0);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (16,'21/10/2023','22/10/2023',1,'Che - Vacina da Parvovirose',9,1);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (17,'02/11/2023','03/11/2023',1,'Che - Desparasitante Vectra 3D',5,1);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (19,'07/06/2023','02/11/2023',1,'Bono - Desparasitante Vectra 3D',5,1);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (21,'18/05/2023','18/08/2023',1,'Bono - Desparasitante Vectra 3D',5,1);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (22,'06/10/2023','07/10/2023',1,'Bono - Vacina da Raiva',9,1);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (23,'09/05/2023','09/11/2023',1,'Bono - Vacina da Raiva',9,1);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (24,'17/05/2023','17/08/2023',1,'Bono - Desparasitante Vectra 3D',5,1);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (25,'18/05/2023','18/08/2023',1,'Bono - Vacina da Raiva',9,1);
INSERT INTO "Todo" ("Id","StartDate","EndDate","Status","Description","CategoryId","Generated") VALUES (26,'21/05/2023','21/08/2023',1,'Shiva - Vacina da sfsfsdf',9,1);
INSERT INTO "Post" ("Id","Title","Introduction","BodyText","Image","PostUrl") VALUES (4,'Como evitar os perigos do Sol','Os animais de estimação também sofrem com o calor...','&lt;p style=" margin: 0px 0px 2em; color: rgb(95, 95, 95); font-family: open sans, arial, sans-serif; font-size: 16px; font-style: normal; font-weight: 300; text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;Todos desfrutam de um dia de sol, mas demasiado calor pode fazer-nos mal e prejudicar também os nossos animais de estimação, especialmente aqueles que já têm uma saúde frágil. O efeito mais imediato do calor de que todos os tutores de animais de estimação precisam de estar conscientes é o g&lt;a href="https://petable.care/pt/2016/07/13/evitar-golpes-de-calor/" style=" background-color: transparent; color: rgb(17, 17, 17); text-decoration: underline;"&gt;olpe de calor&lt;/a&gt;. O golpe de calor é uma condição potencialmente fatal para os animais de estimação.&lt;/p&gt;&lt;p style=" margin: 0px 0px 2em; color: rgb(95, 95, 95); font-family: open sans, arial, sans-serif; font-size: 16px; font-style: normal; font-weight: 300; text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;De forma a manter o seu animal a salvo dos efeitos nocivos do calor, recolhemos estas dicas para ajudar o seu gato ou o seu cão a manterem-se frescos este verão:&lt;/p&gt;&lt;h2 style=" font-family: open sans, arial, sans-serif; font-weight: 400; line-height: 1.4; color: rgb(95, 95, 95); margin: 1.3em 0px 0.5em; font-size: 28px; font-style: normal; text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;Sombra e água&lt;/h2&gt;&lt;p style=" margin: 0px 0px 2em; color: rgb(95, 95, 95); font-family: open sans, arial, sans-serif; font-size: 16px; font-style: normal; font-weight: 300; text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;A sombra irá manter o seu animal de estimação fresco no exterior. Água fria, à qual pode adicionar cubos de gelo, irá ajudar a refrescar o seu cão ou gato a partir do interior. Também pode experimentar congelar fruta ou até caldo de carne diluído, que servirão de formas refrescantes de recompensa, ao mesmo tempo que lhes proporcionam uma forma saborosa de enriquecimento ambiental.&lt;/p&gt;&lt;h2 style=" font-family: open sans, arial, sans-serif; font-weight: 400; line-height: 1.4; color: rgb(95, 95, 95); margin: 1.3em 0px 0.5em; font-size: 28px; font-style: normal; text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;Abrir uma fresta na janela não é suficiente&lt;/h2&gt;&lt;p style=" margin: 0px 0px 2em; color: rgb(95, 95, 95); font-family: open sans, arial, sans-serif; font-size: 16px; font-style: normal; font-weight: 300; text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;Se o carro estiver estacionado ao sol, as aberturas nas janelas não irão manter o seu animal a salvo dos efeitos do calor. “&lt;a href="https://www.rspca.org.uk/adviceandwelfare/pets/dogs/health/dogsinhotcars" style=" background-color: transparent; color: rgb(17, 17, 17); text-decoration: underline;"&gt;Cães morrem em carros quentes&lt;/a&gt;” não é apenas um slogan de uma campanha de consciencialização para o bem-estar animal. Qualquer pessoa que trabalhe no sector da saúde animal dir-lhe-á quantos animais de estimação a sofrer de desidratação, exaustão de calor ou mesmo golpe de calor lhes aparecem a precisar de cuidados de cada vez que as temperaturas sobem. Por vezes, mesmos os tutores com as melhores intenções, podem estar a fazer mal aos seus animais de estimação, sem querer. Mesmo que seja só “por um minuto” evite deixar o seu animal de estimação num carro estacionado no exterior.&lt;/p&gt;&lt;h2 style=" font-family: open sans, arial, sans-serif; font-weight: 400; line-height: 1.4; color: rgb(95, 95, 95); margin: 1.3em 0px 0.5em; font-size: 28px; font-style: normal; text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;Tem um animal de estimação de alto risco?&lt;/h2&gt;&lt;p style=" margin: 0px 0px 2em; color: rgb(95, 95, 95); font-family: open sans, arial, sans-serif; font-size: 16px; font-style: normal; font-weight: 300; text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;Tal como nas pessoas, alguns animais de estimação correm riscos mais elevados devido aos efeitos do sobreaquecimento. Tem um gato sénior? Um cachorrinho hiperactivo? Tem um animal de focinho curto, como um pug ou um bulldog francês? A sua gata ou cadela está prenha? Cuidado com os animais muito novos ou muito velhos. O mesmo se passa se forem animais gestantes ou imunocomprometidos (devido a doenças ou tratamentos como a quimioterapia). Também há risco acrescido com o calor se o seu animal de estimação for de uma raça de focinho achatado ou uma raça muito peluda com um sub-pêlo denso. Todas estas condições tornam estes animais de estimação mais propensos a sofrer os efeitos das altas temperaturas, por isso terá de ter cuidado extra com eles nos dias quentes.&lt;/p&gt;&lt;h2 style=" font-family: open sans, arial, sans-serif; font-weight: 400; line-height: 1.4; color: rgb(95, 95, 95); margin: 1.3em 0px 0.5em; font-size: 28px; font-style: normal; text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;Em caso de golpe de calor&lt;/h2&gt;&lt;p style=" margin: 0px 0px 2em; color: rgb(95, 95, 95); font-family: open sans, arial, sans-serif; font-size: 16px; font-style: normal; font-weight: 300; text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;O que é o&lt;span&gt; &lt;/span&gt;&lt;a href="https://www.rvc.ac.uk/small-animal-vet/teaching-and-research/fact-files/heatstroke-in-dogs-and-cats" style=" background-color: transparent; color: rgb(17, 17, 17); text-decoration: underline;"&gt;golpe de calor&lt;/a&gt;? A insolação é uma condição grave em que a temperatura do teu animal sobe acima dos seus limites normais e tem efeitos graves na sua saúde. Pode até levar à morte. Pode aprender mais sobre a golpe de calor no &lt;a href="https://petable.care/pt/2016/07/13/evitar-golpes-de-calor/" style=" background-color: transparent; color: rgb(17, 17, 17); text-decoration: underline;"&gt;nosso artigo sobre o tema&lt;/a&gt;. Caso suspeite que o seu animal está a sofrer de golpe de calor deve levar o seu gato ou cão a um veterinário o mais depressa possível. No teu caminho para o veterinário, deve manter o seu animal fresco com uma toalha molhada sobre ele e o ar condicionado ligado.&lt;/p&gt;&lt;h2 style=" font-family: open sans, arial, sans-serif; font-weight: 400; line-height: 1.4; color: rgb(95, 95, 95); margin: 1.3em 0px 0.5em; font-size: 28px; font-style: normal; text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;Evitar o exercício ao ar livre&lt;/h2&gt;&lt;p style=" margin: 0px 0px 2em; color: rgb(95, 95, 95); font-family: open sans, arial, sans-serif; font-size: 16px; font-style: normal; font-weight: 300; text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;Embora na maioria dos casos, o exercício ao ar livre seja sempre óptimo para a saúde dos nossos animais de estimação, devemos ter cuidado para evitar o exercício quando o sol está no seu ponto mais alto. O seu animal de estimação pode não ser capaz de evitar o chamamento do ar livre, mas é nossa responsabilidade deixá-los sair apenas quando o dia já tiver arrefecido ou nas primeiras horas da manhã enquanto o chão da rua ainda não começou a escaldar. De certeza que já ouviu isto antes, mas não nos cansamos de repetir repetir:&lt;span&gt; &lt;/span&gt;&lt;a href="https://www.thekennelclub.org.uk/health-and-dog-care/health/health-and-care/a-z-of-health-and-care-issues/hot-pavements/" style=" background-color: transparent; color: rgb(17, 17, 17); text-decoration: underline;"&gt;a calçada pode ficar demasiado quente&lt;/a&gt;&lt;span&gt; &lt;/span&gt;para os animais poderem andar na rua. Se não tem a certeza se a calçada está demasiado quente, basta encostar a parte de trás da tua mão no chão. Se estiver demasiado quente para suportar na mão, definitivamente estará demasiado quente para as patas desprotegidas dos nossos animais de estimação. As consequências das queimaduras das almofadinhas plantares podem ser graves.&lt;/p&gt;&lt;h2 style=" font-family: open sans, arial, sans-serif; font-weight: 400; line-height: 1.4; color: rgb(95, 95, 95); margin: 1.3em 0px 0.5em; font-size: 28px; font-style: normal; text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;Como é que os animais de estimação transpiram?&lt;/h2&gt;&lt;p style=" margin: 0px 0px 2em; color: rgb(95, 95, 95); font-family: open sans, arial, sans-serif; font-size: 16px; font-style: normal; font-weight: 300; text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;Ao contrário dos humanos que têm glândulas sudoríparas por toda a sua pele, os animais de estimação usam principalmente o “arfar” como mecanismo de arrefecimento. Os&lt;a href="https://www.akc.org/expert-advice/health/do-dogs-sweat/" style=" background-color: transparent; color: rgb(17, 17, 17); text-decoration: underline;"&gt;&lt;span&gt; &lt;/span&gt;animais de estimação também transpiram&lt;/a&gt;, principalmente através das almofadinhas plantares. No entanto, arfar é uma forma muito mais eficiente para os seus corpos cobertos de pêlo evaporarem a humidade de forma a arrefecer. Isto pode levar a uma desidratação rápida, por isso certifique-se que há água sempre disponível e longe da luz solar directa. Além disso, o pêlo actua como um isolante natural, por isso se está a pensar &lt;a href="https://petable.care/pt/2019/07/17/tosquiar-ou-nao-a-decisao-deste-verao/" style=" background-color: transparent; color: rgb(17, 17, 17); text-decoration: underline;"&gt;tosquiar o pêlo&lt;/a&gt; como uma forma de ajudar o seu cão ou gato a manter-se fresco, pergunte a opinião ao seu veterinário. Algumas raças ficam melhor se mantiverem o pêlo durante o Verão.&lt;/p&gt;','main.png',NULL);
INSERT INTO "Post" ("Id","Title","Introduction","BodyText","Image","PostUrl") VALUES (5,'Os mitos da comida para cães','Existem algumas crenças populares sobre a alimentação para cães e gatos que vamos ajudar a esclarecer.','&lt;div class="elementor-accordion-title active" data-section="1" style=" padding: 15px 20px; font-weight: 700; line-height: 1; cursor: pointer; color: rgb(12, 12, 12); font-family: Arial, Helvetica, sans-serif; font-size: 15px; font-style: normal; text-align: left; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;&lt;span&gt;Comer doces ou açúcar provoca cegueira&lt;/span&gt;&lt;/div&gt;&lt;div class="elementor-accordion-content" data-section="1" style=" display: block; padding: 15px 20px; border-width: 1px; color: rgb(43, 43, 43); font-family: Arial, Helvetica, sans-serif; font-size: 15px; font-style: normal; font-weight: 400; text-align: left; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;&lt;div&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;Não corresponde à realidade. Se um cão é diabético, ou seja, se já apresenta um grave problema de produção de insulina que o impede de metabolizar os açúcares, ao ingerir um extra de açúcar aumenta o nível de glicose no sangue e pode facilitar a formação de cataratas e, em consequência, originar a perda de visão. Mas, se um cão não é diabético, ingerir açúcar não terá esse efeito.&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;No entanto, há limites à sua ingestão porque os açúcares:&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;- alteram o apetite, facilitam o aparecimento de diarreias e a alteração da flora intestinal;&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;- podem contribuir para um aumento de peso.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;div class="elementor-accordion-title active" data-section="2" style=" padding: 15px 20px; font-weight: 700; line-height: 1; cursor: pointer; color: rgb(12, 12, 12); font-family: Arial, Helvetica, sans-serif; font-size: 15px; font-style: normal; text-align: left; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;&lt;span&gt;A comida dos humanos é má para os cães&lt;/span&gt;&lt;/div&gt;&lt;div class="elementor-accordion-content" data-section="2" style=" display: block; padding: 15px 20px; border-width: 1px; color: rgb(43, 43, 43); font-family: Arial, Helvetica, sans-serif; font-size: 15px; font-style: normal; font-weight: 400; text-align: left; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;&lt;div&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;A comida humana não é boa nem má para os cães. Porém, para o seu cão não ter problemas nutricionais, evite desequilíbrios. &lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;- escolha produtos de origem proteica (carne de vaca, frango ou borrego – sem ossos –; peixes; queijo fresco; iogurte ou requeijão);&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;- sirva pequenas quantidades;&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;- misture com a comida habitual, no recipiente próprio;&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;- assegure-se de que não representa mais do que 5 a 7% da ração diária e subtraia essa quantidade da alimentação normal do seu cão;&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;- evite alimentos que podem ser prejudiciais como. Cebola, alho, passas, uvas, chocolate, alguns frutos secos e alimentos adoçados com xilitol são de evitar.&lt;/p&gt;&lt;div class="elementor-accordion-title active" data-section="3" style=" padding: 15px 20px; font-weight: 700; line-height: 1; cursor: pointer; color: rgb(12, 12, 12); font-family: Arial, Helvetica, sans-serif; font-size: 15px; font-style: normal; text-align: left; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;&lt;span&gt;Snacks são um mau hábito&lt;/span&gt;&lt;/div&gt;&lt;div class="elementor-accordion-content" data-section="3" style=" display: block; padding: 15px 20px; border-width: 1px; color: rgb(43, 43, 43); font-family: Arial, Helvetica, sans-serif; font-size: 15px; font-style: normal; font-weight: 400; text-align: left; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;&lt;div&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;Atenção à quantidade diária recomendável, dependendo do tamanho do cão.&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;Existem muitos tipos de snacks, por exemplo:&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;- alguns são encarados como "mimos" ou "recompensas", mas outros têm um uso funcional (limpeza dental, melhoria da digestão, redução da flatulência, suplementos nutricionais para pele e pelos, entre outros);&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;- alguns snacks são, essencialmente, cereais e podem ser dados na quantidade certa, outros têm mais gordura&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;- muitos snacks são pequenas porções do tamanho de uma ervilha ou avelã e outros são barritas maiores, pelo que deve ter em atenção o recomendado.&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt; &lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;&lt;span style=" text-decoration: underline;"&gt;Pode dar snacks, desde que respeite algumas regras:&lt;/span&gt;&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;- sejam snacks adequados ao tamanho do cão;&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;- o ideal é reservar os snacks para “recompensar” o fiel amigo quando faz algo bem feito, pois assim ele associa o esforço à recompensa;&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;- os snacks indicam sempre para que cães são adequados, em que casos e como devem ser utilizados, incluindo a recomendação de dose diária ou semanal;&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;- é muito importante remover a quantidade proporcional de ração.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;div class="elementor-accordion-title active" data-section="4" style=" padding: 15px 20px; font-weight: 700; line-height: 1; cursor: pointer; color: rgb(12, 12, 12); font-family: Arial, Helvetica, sans-serif; font-size: 15px; font-style: normal; text-align: left; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;&lt;span&gt;Os cães devem consumir carne ou comida crua&lt;/span&gt;&lt;/div&gt;&lt;div class="elementor-accordion-content" data-section="4" style=" display: block; padding: 15px 20px; border-width: 1px; color: rgb(43, 43, 43); font-family: Arial, Helvetica, sans-serif; font-size: 15px; font-style: normal; font-weight: 400; text-align: left; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;&lt;div&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;Este tipo de comida crua à base de carnes e alguns vegetais e batatas ou cereais pode, no entanto, ser mais suscetível de sofrer contaminação microbiana, sobretudo se for preparada em casa. Se o fizer, é importante que, como em qualquer tipo de dieta caseira, garanta que os animais estão a consumir todas as vitaminais e minerais essenciais nas proporções certas.  Escolha ingredientes de fontes seguras para garantir a segurança alimentar. Há vários parasitas e bactérias, como&lt;span&gt; &lt;/span&gt;&lt;em&gt;Salmonella&lt;/em&gt;&lt;span&gt; &lt;/span&gt;que podem ser transmitidos ao animal através de dietas cruas&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt; &lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;Se preparar refeições com carne crua para os seus animais em casa, siga estas regras:&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;- compre carne que esteja em boas condições. Não devem existir sinais visíveis de danos na embalagem;&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;- lave as mãos com água quente e sabonete após manusear a carne;&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;- lave todas as superfícies (https://www.deco.proteste.pt/alimentacao/seguranca-alimentar/noticias/higiene-cozinha-erros-evitar-nao-ficar-doente "") que estiveram em contacto com a carne crua;&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;- após cada uso, lave os comedouros ou qualquer utensílio com detergente e água quente, enxague bem e seque bem antes do próximo uso;&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;- Ao armazenar os alimentos para animais de estimação no frigorífico, certifique-se de que os produtos crus ficam na parte inferior.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;div class="elementor-accordion-title active" data-section="5" style=" padding: 15px 20px; font-weight: 700; line-height: 1; cursor: pointer; color: rgb(12, 12, 12); font-family: Arial, Helvetica, sans-serif; font-size: 15px; font-style: normal; text-align: left; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;&lt;span&gt;As rações mais caras são as melhores&lt;/span&gt;&lt;/div&gt;&lt;div class="elementor-accordion-content" data-section="5" style=" display: block; padding: 15px 20px; border-width: 1px; color: rgb(43, 43, 43); font-family: Arial, Helvetica, sans-serif; font-size: 15px; font-style: normal; font-weight: 400; text-align: left; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;&lt;div&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;Na ração, os preços variam muito, e não é verdade que a ração mais cara seja melhor: é possível encontrar rações de alta qualidade a um bom preço.&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;Compare bem antes de comprar.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;div class="elementor-accordion-title active" data-section="6" style=" padding: 15px 20px; font-weight: 700; line-height: 1; cursor: pointer; color: rgb(12, 12, 12); font-family: Arial, Helvetica, sans-serif; font-size: 15px; font-style: normal; text-align: left; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;&lt;span&gt;Dê alimento na quantidade certa&lt;/span&gt;&lt;/div&gt;&lt;div class="elementor-accordion-content" data-section="6" style=" display: block; padding: 15px 20px; border-width: 1px; color: rgb(43, 43, 43); font-family: Arial, Helvetica, sans-serif; font-size: 15px; font-style: normal; font-weight: 400; text-align: left; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;&lt;div&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;A maioria das embalagens de ração vem acompanhada de uma tabela que permite saber qual a quantidade de ração necessária para o animal, de acordo com o seu peso e idade. No entanto, a energia de que o animal necessita varia de acordo com vários fatores: condição de vida, atividade física diária, stresse, etc.&lt;/p&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;Pese a ração diariamente e o seu cão semanalmente para garantir que se mantém no peso ideal. Se engordar muito, provavelmente terá de diminuir a quantidade de ração que lhe está a dar; se emagrecer, terá de aumentar.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;div data-section="8" style=" padding: 15px 20px; font-weight: 700; line-height: 1; cursor: pointer; color: rgb(12, 12, 12); font-family: Arial, Helvetica, sans-serif; font-size: 15px; font-style: normal; text-align: left; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);" class="pasteContent_RTE"&gt;&lt;span&gt;Coloque água à disposição&lt;/span&gt;&lt;/div&gt;&lt;div data-section="8" style=" display: block; padding: 15px 20px; border-width: 1px; color: rgb(43, 43, 43); font-family: Arial, Helvetica, sans-serif; font-size: 15px; font-style: normal; font-weight: 400; text-align: left; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);" class="pasteContent_RTE"&gt;&lt;div&gt;&lt;p style=" margin-top: 0px; margin-bottom: 0.8rem;"&gt;&lt;span lang="PT"&gt;Se alimenta o seu cão com ração seca, é importante que coloque água suficiente à sua disposição.&lt;/span&gt;&lt;/p&gt;&lt;/div&gt;&lt;/div&gt;&lt;/div&gt;&lt;/div&gt;&lt;/div&gt;&lt;/div&gt;&lt;/div&gt;&lt;/div&gt;&lt;/div&gt;&lt;/div&gt;&lt;/div&gt;&lt;/div&gt;&lt;/div&gt;&lt;/div&gt;','english-cocker-spaniel-img.jpg','');
INSERT INTO "Post" ("Id","Title","Introduction","BodyText","Image","PostUrl") VALUES (6,'Saber como alimentar os seus animais','Como alimentar corretamente um animal de estimação','&lt;h3&gt;&lt;strong&gt;Deixe a comida à disposição&lt;/strong&gt;:&lt;/h3&gt;&lt;p&gt;Ao contrário dos cães, os gatos sabem dosear a comida e raramente comem tudo o que se coloca no comedouro de uma única vez. A menos que o seu médico veterinário recomende o contrário, os gatos devem ter sempre acesso à comida para comerem quando quiserem. Desta forma, comerão entre 12 e 20 pequenas refeições por dia.&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;O mesmo acontece com a água. Apesar de conseguirem viver com pouca água, os gatos devem conseguir aceder à mesma sempre que queiram.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;h3&gt;&lt;strong&gt;Alimentação mista é uma boa opção&lt;/strong&gt;:&lt;/h3&gt;&lt;p&gt;A ração seca é essencial, sobretudo porque contribui para a higiene oral dos animais. Algumas contêm até pirofosfatos, que podem ajudar a prevenir a formação de placa bacteriana nos dentes. Contudo, pode complementar a ração seca com alguma ração húmida. Como sentem pouca sede, os gatos são mais suscetíveis ao desenvolvimento de cálculos no trato urinário. Para garantir que se mantêm hidratados, dê-lhes alimentação húmida, que contém entre 78 e 82% de água, duas a três vezes por semana. Apesar de não ser essencial, a alimentação húmida é uma opção mais agradável ao palato e pode compensar uma alimentação seca mais pobre.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Além disso, independentemente do alimento que escolher dar ao seu gato, o mais importante é que meça a comida todos os dias para controlar as quantidades que este está a ingerir. Consulte as tabelas nutricionais disponíveis nas embalagens das rações comerciais.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;h3&gt;&lt;strong&gt;Evite dietas vegetarianas&lt;/strong&gt;:&lt;/h3&gt;&lt;p&gt;Tanto o chocolate como o café contêm metilxantinas, substâncias que quando são ingeridas pelos animais podem causar vómitos e diarreia, respiração ofegante, sede, hiperatividade, ritmo cardíaco anormal, tremores, convulsões e até morte. Quanto mais escuro o chocolate, mais prejudicial é, uma vez que contém maior quantidade de metilxantinas.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;h2&gt;&lt;strong&gt;Alimentos que deve evitar dar aos animais&lt;/strong&gt;&lt;/h2&gt;&lt;p&gt;Há alimentos consumidos pelos humanos que são tóxicos para os animais de companhia e que os podem deixar doentes.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;h4&gt;&lt;strong&gt;Chocolate, café e cafeína&lt;/strong&gt;&lt;/h4&gt;&lt;p&gt;Os gatos domésticos descendem de carnívoros estritos. Ao caçar, procuram pequenas presas, como ratos, pássaros e insetos. Além disso, a química e a estrutura do sistema gastrointestinal do gato são adequadas para digerir e absorver nutrientes de proteínas e gorduras de origem animal. Por essa razão, as dietas vegetarianas não suplementadas podem resultar em deficiências de certos aminoácidos essenciais, ácidos gordos e vitaminas.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;No caso dos cães, é um pouco diferente. Os cães são omnívoros e, por esse motivo, podem adaptar-se a uma dieta vegetariana equilibrada. Já existem inclusive várias opções comerciais completas e equilibradas. Contudo, antes de optar por este tipo de alimentação para o seu animal, fale com um médico veterinário.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;h4&gt;&lt;strong&gt;Citrinos&lt;/strong&gt;&lt;/h4&gt;&lt;p&gt;Os cães não devem comer a casca, as peles brancas da laranja ou quaisquer partes da planta. É muito importante remover todos os vestígios de pele, caroços e sementes, porque podem conter componentes prejudiciais. Se ingeridos em quantidades significativas, estes podem causar irritação e até depressão do sistema nervoso central. No entanto, pequenas doses de laranja provavelmente não causarão problemas além de uma pequena dor de estômago.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;h4&gt;&lt;strong&gt;Uvas e passas&lt;/strong&gt;&lt;/h4&gt;&lt;p&gt;Ingeridos em pequenas quantidades, o coco e os produtos à base de coco provavelmente não causam problemas graves aos animais de companhia. Contudo, contêm óleos que podem causar dores de estômago ou diarreia. Além disso, a água de coco é rica em potássio e, por isso, não deve ser dada ao seu animal de estimação.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;h4&gt;&lt;strong&gt;Coco e óleo de coco&lt;/strong&gt;&lt;/h4&gt;&lt;p&gt;Embora a substância tóxica presente nas uvas e passas seja desconhecida, estas podem causar insuficiência renal aos cães.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;h4&gt;&lt;strong&gt;Noz-macadâmia e outros frutos secos&lt;/strong&gt;&lt;/h4&gt;&lt;p&gt;A noz-macadâmia pode causar fraqueza, depressão, vómitos, tremores e hipertermia em cães. Os sinais geralmente aparecem 12 horas após a ingestão e podem durar entre 24 e 48 horas.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;h4&gt;&lt;strong&gt;Leite e laticínios&lt;/strong&gt;&lt;/h4&gt;&lt;p&gt;Como os animais de companhia não possuem quantidades significativas de lactase (a enzima que processa a lactose presente no leite), o leite e outros produtos lácteos podem causar diarreia ou outros problemas digestivos.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;h4&gt;&lt;strong&gt;Cebola, alho e cebolinho&lt;/strong&gt;&lt;/h4&gt;&lt;p&gt;Tanto a cebola como o alho e o cebolinho podem causar irritação gastrointestinal aos animais, assim como anemia. Embora os gatos sejam mais suscetíveis, os cães também correm risco se consumirem uma grande quantidade.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;h4&gt;&lt;strong&gt;Carne crua, ovos mal cozinhados e ossos&lt;/strong&gt;&lt;/h4&gt;&lt;p&gt;A carne e os ovos crus podem conter bactérias como Salmonella e E. Coli, que são prejudiciais tanto para os animais como para os humanos. Os ovos crus, por exemplo, contêm uma enzima chamada avidina, que diminui a absorção de biotina (uma vitamina B), o que pode causar problemas de pele.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Além disso, alimentar os animais com ossos pode ser muito perigoso, uma vez que estes se podem engasgar ou sofrer um ferimento grave caso o osso se estilhace e se aloje no trato digestivo do animal.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;h4&gt;&lt;strong&gt;Xilitol&lt;/strong&gt;&lt;/h4&gt;&lt;p&gt;O xilitol, usado como adoçante em muitos produtos, como as pastilhas elásticas e a pasta de dentes, pode estimular a libertação de insulina na maioria das espécies, o que pode levar à insuficiência hepática. Isto pode resultar numa diminuição brusca do nível de açúcar (hipoglicemia). Os sinais iniciais de intoxicação nos animais incluem vómitos, letargia e perda de coordenação.&lt;/p&gt;','jack-russell-img.jpg',NULL);
INSERT INTO "Post" ("Id","Title","Introduction","BodyText","Image","PostUrl") VALUES (10,'Como ensinar o cachorro a fazer xixi no lugar certo','Neste artigo você irá aprender a como ensinar o seu cachorro a fazer xixi no lugar certo, seja filhote ou até mesmo um cachorro adulto que você acabou de adotar','&lt;p&gt;Uma das maiores dores de cabeça para os donos de cães iniciantes é ensinar o seu amiguinho peludo a fazer xixi em um local apropriado,&lt;/p&gt;&lt;p&gt;de modo que facilite na limpeza e evite situações desagradáveis pelo mau cheiro da urina.&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;No entanto, muitas pessoas cometem erro na hora de iniciar este processo…&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;Seja você que mora em um apartamento ou em uma casa com quintal, certamente você deve destinar um lugar correto para que o seu cachorro possa&lt;span style="background-color: unset; text-align: inherit;"&gt; aprender a urinar. Mas como fazer isso? É possível fazer com cães adultos?&lt;/span&gt;&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Ensinar o cachorro a urinar no lugar correto pode ser mais simples do que você imagina e a boa notícia é: sim, você poderá ensinar tanto&lt;/p&gt;&lt;p&gt;filhotes quanto cães adultos, mesmo que estes não sejam acostumados.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;No entanto, ensinar o cachorro a fazer xixi no lugar certo é um desafio que requer tempo e paciência, pois sabemos que os nossos amiguinhos &lt;span style="background-color: unset; text-align: inherit;"&gt;peludos não são acostumados a ter um sanitário só para ele.&lt;/span&gt;&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;Antes de detalhar como você pode educar o seu amigo para urinar no lugar certo, enfatizamos que você nunca deve utilizar a violência,&lt;/p&gt;&lt;p&gt;broncas ou punições como método educativo, ok?&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Xingos e punições podem deixar o seu amigo nervoso e estressado, de modo que ele faça exatamente o oposto do que queremos. Sendo assim, &lt;span style="background-color: unset; text-align: inherit;"&gt;ele irá encontrar novos locais para urinar e até mesmo começar um comportamento destrutivo.&lt;/span&gt;&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;Como ensinar o cachorro a fazer xixi no lugar certo&lt;/p&gt;&lt;p&gt;Como educar o cachorro a urinar no lugar correto&lt;/p&gt;&lt;p&gt;Separamos algumas dicas que serão muito úteis para que você possa começar a aplicar ainda hoje com o seu cachorro.&lt;/p&gt;&lt;p&gt;Confira, a seguir, o resumo do nosso passo a passo que elaboramos para você.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Escolha um canto da sua casa especial para o seu cachorro fazer xixi.&lt;/p&gt;&lt;p&gt;Invista em uma bandeja sanitária ou tapete higiênico.&lt;/p&gt;&lt;p&gt;Crie uma rotina.&lt;/p&gt;&lt;p&gt;Leve o cachorro para o cantinho escolhido na hora que você identificar que ele quer fazer as necessidades.&lt;/p&gt;&lt;p&gt;Recompense-o toda vez que der certo.&lt;/p&gt;&lt;p&gt;Seja constante.&lt;/p&gt;&lt;p&gt;Tenha paciência: nunca dê broncas no seu pet!&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;1. Escolha um canto apropriado na sua casa para ser o novo banheiro do seu cachorro!&lt;/p&gt;&lt;p&gt;Assim como os humanos, os cães também devem ter um local no qual se sintam a vontade para fazer as suas necessidades, deste modo,&lt;/p&gt;&lt;p&gt;o cantinho que você esoclher na sua casa será sempre lembrado por ele na hora que tiver vontade de urinar ou defecar.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Mas como deve ser este cantinho? De preferência, deve ser:&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Longe do cantinho destinado à alimentação, com comedouro e bebedouro.&lt;/p&gt;&lt;p&gt;Se possível, longe de locais sociais da sua casa como sala, cozinha e quarto, onde provavelmente você passa mais tempo e o mau&lt;/p&gt;&lt;p&gt;cheiro poderá lhe incomodar.&lt;/p&gt;&lt;p&gt;De preferência fora da casa, em um quintal, caso você tenha.&lt;/p&gt;&lt;p&gt;Se a sua habitação tiver, você também poderá optar por jardins e áreas verdes.&lt;/p&gt;&lt;p&gt;Escolhido o cantinho, agora é hora de ir para o próximo passo.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;2. Invista em bandeja sanitária ou tapetes higiênicos&lt;/p&gt;&lt;p&gt;Bandejas sanitárias e tapetes higiênicos costumam ajudar muito na limpeza e higiene para os donos de cães, independente da raça e do porte.&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;Isso porque a bandeja e os tapetes são especiais para as necessidades dos cães.&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;Estes acessórios possuem propriedades que irão não só absorver a urina do seu amiguinho, mas também neutralizar os odores.&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;Algumas versões de tapetes higiênicos possuem atrativos que, através do cheiro, despertarão a vontade do seu amiguinho em urinar no lugar certo.&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;Além disso, depois que seu companheiro fizer as necessidades, a limpeza e higiene torna-se muito mais prática.&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;3. Crie uma rotina&lt;/p&gt;&lt;p&gt;Assim como os humanos, cachorros também precisam ter uma rotina para comer, beber, fazer as necessidades e dormir!&lt;/p&gt;&lt;p&gt;Ensinar o seu amigo a fazer as necessidades no lugar certo ficará muito mais fácil se, com o tempo, você souber o horário&lt;/p&gt;&lt;p&gt;que ele costuma urinar ou defecar!&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Mas como criar uma rotina?&lt;/p&gt;&lt;p&gt;Primeiramente, estabeleça horários que seu amiguinho deverá comer. É comum que cães, sejam filhotes ou adultos,&lt;/p&gt;&lt;p&gt;costumem urinar e defecar até 30 minutos após a alimentação.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Passe mais tempo com o seu cachorro&lt;/p&gt;&lt;p&gt;Entendemos que com o trabalho e a correria do dia a dia, infelizmente nem sempre é possível passar muito tempo&lt;/p&gt;&lt;p&gt;com nossos amiguinhos peludos.&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;Mas, se você se dedicar para passar um tempo com o seu cachorro, seja brincando ou passeando, começará a entender os hábitos dele.&lt;/p&gt;&lt;p&gt;Desta forma, poderá saber os horários que ele urina e leva-lo até o seu cantinho escolhido.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;4. Leve o seu cachorro para o cantinho escolhido toda vez que suspeitar que ele quer fazer alguma necessidade&lt;/p&gt;&lt;p&gt;Como ensinar filhotes&lt;/p&gt;&lt;p&gt;Caso o seu cachorro ainda seja filhote, você poderá optar por cercá-lo em um espaço reduzido em sua casa, de modo que ele tenha menos&lt;/p&gt;&lt;p&gt; opções de lugares para fazer as necessidades e então se acostume naturalmente a urinar no espaço reduzido.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Outra alternativa é colocar sanitários ou tapetes higiênicos ao longo da casa, principalmente aqueles que tem atrativos,&lt;/p&gt;&lt;p&gt;assim seu filhote irá fazer as necessidades no sanitário ou tapete mais próximo.&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;Depois, com o tempo, basta ir apenas reduzindo as opções até restar apenas o local que você deseja que seja permanente,&lt;/p&gt;&lt;p&gt;deste modo, ele acostumará a urinar em sanitários ou tapetes higiênicos e fará isso no lugar que sobrou.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Como ensinar adultos&lt;/p&gt;&lt;p&gt;No caso de adultos, você poderá atraí-lo com petiscos, passar um tempo com ele perto do local que você quer que ele urine e, assim&lt;/p&gt;&lt;p&gt;que fizer a necessidade, deverá recompensá-lo.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Uma dica muito boa da eZoo é que tapetes higiênicos com atrativos caninos costumam ser ótimas opções para atrair a atenção&lt;/p&gt;&lt;p&gt;dos peludinhos na hora de urinar, sendo assim, o processo será mais rápido.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;5. Recompense o seu cachorro.&lt;/p&gt;&lt;p&gt;Recompensar o seu cachorro com petiscos, como ossinhos e bifinhos ou até mesmo brinquedos mordedores, costumam ser muito&lt;/p&gt;&lt;p&gt;eficientes no processo de educação comportamental.&lt;/p&gt;&lt;p&gt;Tente dar algum petisco para o seu cão toda vez que ele urinar no lugar que você escolheu. Consequentemente,&lt;/p&gt;&lt;p&gt;o seu amigo irá assimilar que fez a coisa certa e urinar naquele lugar poderá lhe trazer mais recompensas.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Uma boa dica é fazer isso sempre elogiando o animal e fazendo muito carinho nele, ok?&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;6. Seja constante.&lt;/p&gt;&lt;p&gt;O segredo para ensinar cachorro a fazer xixi no lugar certo é a constância.&lt;/p&gt;&lt;p&gt;Isto é, você deverá repetir este processo todos os dias, de preferência no mesmo local e horário.&lt;/p&gt;&lt;p&gt;Uma vez que você faça isso, o cachorro irá aprender os comandos.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Se você fizer os passos anteriores sem constância, o processo dificilmente dará certo, portanto, se dedique&lt;/p&gt;&lt;p&gt;por alguns dias ou semanas em obter êxito.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Deste modo, certamente você conseguirá ensinar o seu amigo a fazer xixi no lugar certo!&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;7. Tenha paciência. Nunca dê broncas!&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Evite dar broncas em animais ao ensinar a urinar no lugar certo&lt;/p&gt;&lt;p&gt;É importante salientar que qualquer ensinamento aos animais é preciso ter muita paciência.&lt;/p&gt;&lt;p&gt;Embora sejam muito inteligentes e dóceis, é importante saber que eles não têm a mesma velocidade e capacidade&lt;/p&gt;&lt;p&gt;de aprendizagem que os seres humanos.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Embora xingamentos pareçam surtir efeitos, broncas só irão atrapalhar todo o processo. Isso vale desde ensinar o&lt;/p&gt;&lt;p&gt;cachorro a fazer xixi como também para educar qualquer outro comportamento.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;As broncas só irão deixar o seu amigo com medo, nervoso e estressado, de modo a não frequentar o local que você escolheu&lt;/p&gt;&lt;p&gt; para as necessidades e até mesmo começar comportamentos destrutivos na sua casa.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Fiz todos os passos citados acima e mesmo assim não deu certo, o que fazer?&lt;/p&gt;&lt;p&gt;O passo a passo elaborado acima costuma ser muito eficaz para a maioria das pessoas, seja para cães filhotes ou até mesmo adultos.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;No entanto, é possível que eventuais problemas em relação a urina e fezes possam continuar existindo.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Sendo assim, uma maneira muito indicada para os donos de cães é investir em um kit educador sanitário, o qual é elaborado&lt;/p&gt;&lt;p&gt;especialmente para ensinar cachorros a urinar no lugar certo.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;O kit é composto por dois frascos contendo líquidos, sendo que um ensina aonde NÃO pode urinar e outro aonde PODE urinar.&lt;/p&gt;&lt;p&gt;O kit é fácil, simples e rápido de aplicar, e o preço é muito acessível e pode ser aplicado tanto para filhotes quanto adultos!&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Caso queira saber mais a respeito do KIT, clique aqui.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Conclusão&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Ensinar o cachorro a fazer xixi no lugar certo costuma ser um desafio que requer atenção e paciência.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Seguindo o passo a passo elaborado neste artigo, certamente você irá conseguir ensinar o seu amigo a&lt;/p&gt;&lt;p&gt;urinar no lugar correto na sua casa.&lt;/p&gt;&lt;p&gt;&lt;br&gt;&lt;/p&gt;&lt;p&gt;Conforme dissemos, sempre evite xingamentos e violência e invista muito em elogios, carinhos e petiscos.&lt;/p&gt;&lt;p&gt;Tudo isso fará com que o processo seja mais rápido e assimilável pelo seu amiguinho!&lt;/p&gt;','BlogPostCaoLendoJornal.png',NULL);
INSERT INTO "Post" ("Id","Title","Introduction","BodyText","Image","PostUrl") VALUES (11,'Por que cachorro faz xixi no tapete de casa, em vez de usar o tapete higiênico','Ao chegar em casa depois de um dia inteiro de trabalho, todo tutor só quer descansar e ficar com os pets bem grudadinhos. Porém, nem tudo é perfeito. Não tem quem não se irrite ao saber que o bichinho fez as necessidades fora do lugar adequado. Porém, você sabe por que cachorro faz xixi no tapete de casa, por exemplo?','&lt;p style=" margin: 0px 0px 1em; padding: 0px; border: 0px; border-radius: 0px; font-style: normal; font-weight: 400; cursor: default; font-size: 18px; line-height: 1.5em; font-family: Roboto, sans-serif; vertical-align: baseline; color: rgb(38, 38, 38); text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;É comum o tutor chegar em casa e ficar estarrecido: “&lt;strong style=" margin: 0px; padding: 0px; border: 0px; border-radius: 0px; font-style: inherit; font-weight: 700; cursor: default; font-size: inherit; line-height: inherit; font-family: inherit; vertical-align: baseline;"&gt;meu cachorro faz xixi no tapete&lt;/strong&gt;”. Nessa hora, é preciso respirar fundo e tentar entender o que está acontecendo.&lt;/p&gt;&lt;p style=" margin: 0px 0px 1em; padding: 0px; border: 0px; border-radius: 0px; font-style: normal; font-weight: 400; cursor: default; font-size: 18px; line-height: 1.5em; font-family: Roboto, sans-serif; vertical-align: baseline; color: rgb(38, 38, 38); text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;&lt;img decoding="async" class="alignnone size-full wp-image-2466371 entered lazyloaded e-rte-image e-imginline" src="https://www.petz.com.br/blog/wp-content/uploads/2023/06/porque-cachorro-faz-xixi-no-tapete5.jpg" alt="cachorro deitado em tapete olhando pra frente" width="718" height="482" data-lazy-srcset="https://www.petz.com.br/blog/wp-content/uploads/2023/06/porque-cachorro-faz-xixi-no-tapete5.jpg 718w, https://www.petz.com.br/blog/wp-content/uploads/2023/06/porque-cachorro-faz-xixi-no-tapete5-300x201.jpg 300w, https://www.petz.com.br/blog/wp-content/uploads/2023/06/porque-cachorro-faz-xixi-no-tapete5-150x101.jpg 150w" data-lazy-sizes="(max-width: 718px) 100vw, 718px" title="Por que cachorro faz xixi no tapete de casa em vez de usar o tapetinho higiênico? 2" data-lazy-src="https://www.petz.com.br/blog/wp-content/uploads/2023/06/porque-cachorro-faz-xixi-no-tapete5.jpg" data-ll-status="loaded" sizes="(max-width: 718px) 100vw, 718px" srcset="https://www.petz.com.br/blog/wp-content/uploads/2023/06/porque-cachorro-faz-xixi-no-tapete5.jpg 718w, https://www.petz.com.br/blog/wp-content/uploads/2023/06/porque-cachorro-faz-xixi-no-tapete5-300x201.jpg 300w, https://www.petz.com.br/blog/wp-content/uploads/2023/06/porque-cachorro-faz-xixi-no-tapete5-150x101.jpg 150w" style=" margin: 1em 0px 2em; padding: 30px 0px; border: 0px; border-radius: 0px; font: inherit; cursor: default; height: auto !important; max-width: 100%; vertical-align: baseline; display: block;"&gt;&lt;/p&gt;&lt;p style=" margin: 0px 0px 1em; padding: 0px; border: 0px; border-radius: 0px; font-style: normal; font-weight: 400; cursor: default; font-size: 18px; line-height: 1.5em; font-family: Roboto, sans-serif; vertical-align: baseline; color: rgb(38, 38, 38); text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;Muitos pets criam uma relação de dependência muito grande com os tutores. Assim, quando os humanos se ausentam, os bichinhos ficam sem saber o que fazer, o que gera insegurança e ansiedade, motivos que respondem à pergunta “por que cachorro faz xixi no tapete de casa?”.&lt;/p&gt;&lt;p style=" margin: 0px 0px 1em; padding: 0px; border: 0px; border-radius: 0px; font-style: normal; font-weight: 400; cursor: default; font-size: 18px; line-height: 1.5em; font-family: Roboto, sans-serif; vertical-align: baseline; color: rgb(38, 38, 38); text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;Para contornar esse problema, é preciso que o pet entenda que tudo fica bem quando o tutor sai de casa. Deixar seu cheiro pela residência acalma o animal, pois é assim que ele se sente seguro.&lt;/p&gt;&lt;p style=" margin: 0px 0px 1em; padding: 0px; border: 0px; border-radius: 0px; font-style: normal; font-weight: 400; cursor: default; font-size: 18px; line-height: 1.5em; font-family: Roboto, sans-serif; vertical-align: baseline; color: rgb(38, 38, 38); text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;Como já dissemos, evite as festas ao retornar para o lar. A rotina de saída também deixa o pet&lt;a href="https://www.petz.com.br/blog/pets/ansiedade-de-separacao/" style=" margin: 0px; padding: 0px; border: 0px; border-radius: 0px; font: inherit; cursor: pointer; text-decoration: underline !important; background: transparent; color: rgb(0, 160, 228); vertical-align: baseline;"&gt;&lt;span&gt; &lt;/span&gt;ansioso&lt;/a&gt;. Comece a pegar as chaves (ou a bolsa) e andar com elas pela casa. A ideia é desconstruir o link entre ações e saídas.&lt;/p&gt;&lt;p style=" margin: 0px 0px 1em; padding: 0px; border: 0px; border-radius: 0px; font-style: normal; font-weight: 400; cursor: default; font-size: 18px; line-height: 1.5em; font-family: Roboto, sans-serif; vertical-align: baseline; color: rgb(38, 38, 38); text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;Problemas de saúde&lt;/p&gt;&lt;p style=" margin: 0px 0px 1em; padding: 0px; border: 0px; border-radius: 0px; font-style: normal; font-weight: 400; cursor: default; font-size: 18px; line-height: 1.5em; font-family: Roboto, sans-serif; vertical-align: baseline; color: rgb(38, 38, 38); text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;Se o pet sempre acertou o local do banheiro, mas passou a errar de uma hora para outra, fique de olho, pois pode ser alguma doença, como infecção urinária ou alterações gastrointestinais. Dessa forma, leve o pet ao veterinário para ter o correto diagnóstico e tratamento.&lt;/p&gt;&lt;p style=" margin: 0px 0px 1em; padding: 0px; border: 0px; border-radius: 0px; font-style: normal; font-weight: 400; cursor: default; font-size: 18px; line-height: 1.5em; font-family: Roboto, sans-serif; vertical-align: baseline; color: rgb(38, 38, 38); text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;Ajudando o pet a não errar&lt;/p&gt;&lt;p style=" margin: 0px 0px 1em; padding: 0px; border: 0px; border-radius: 0px; font-style: normal; font-weight: 400; cursor: default; font-size: 18px; line-height: 1.5em; font-family: Roboto, sans-serif; vertical-align: baseline; color: rgb(38, 38, 38); text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;Com algumas dicas simples, é possível aprender&lt;span&gt; &lt;/span&gt;&lt;strong style=" margin: 0px; padding: 0px; border: 0px; border-radius: 0px; font-style: inherit; font-weight: 700; cursor: default; font-size: inherit; line-height: inherit; font-family: inherit; vertical-align: baseline;"&gt;como fazer o cachorro parar de urinar no lugar errado&lt;/strong&gt;:&lt;/p&gt;&lt;ul style=" margin: 0px 0px 1em; padding: 0px 0px 0px 2em; border: 0px; border-radius: 0px; font-style: normal; font-weight: 400; cursor: default; font-size: 18px; line-height: 1.5em; font-family: Roboto, sans-serif; vertical-align: baseline; list-style: none; color: rgb(38, 38, 38); text-align: start; text-indent: 0px; white-space: normal; background-color: rgb(255, 255, 255);"&gt;&lt;li style=" margin: 0px; padding: 0.2em 0px; border: 0px; border-radius: 0px; font: inherit; cursor: default; vertical-align: baseline; position: relative;"&gt;tente descobrir a causa do xixi fora do lugar.&lt;/li&gt;&lt;li style=" margin: 0px; padding: 0.2em 0px; border: 0px; border-radius: 0px; font: inherit; cursor: default; vertical-align: baseline; position: relative;"&gt;aja na hora certa! Não adianta pegar o pet e correr para o banheiro se ele já fez xixi;&lt;/li&gt;&lt;li style=" margin: 0px; padding: 0.2em 0px; border: 0px; border-radius: 0px; font: inherit; cursor: default; vertical-align: baseline; position: relative;"&gt;ignore-o se ele fizer o xixi no lugar errado. Peça para alguém chamá-lo e limpe tudo sem ele ver;&lt;/li&gt;&lt;li style=" margin: 0px; padding: 0.2em 0px; border: 0px; border-radius: 0px; font: inherit; cursor: default; vertical-align: baseline; position: relative;"&gt;limpe bem o local com produtos específicos para retirar o cheiro;&lt;/li&gt;&lt;li style=" margin: 0px; padding: 0.2em 0px; border: 0px; border-radius: 0px; font: inherit; cursor: default; vertical-align: baseline; position: relative;"&gt;recompense-o sempre que ele acertar.&lt;/li&gt;&lt;/ul&gt;','porque-cachorro-faz-xixi-no-tapete.jpg',NULL);
INSERT INTO "Post" ("Id","Title","Introduction","BodyText","Image","PostUrl") VALUES (12,'Como Ensinar o Cachorro a Fazer as Necessidades no Lugar Certo','Uma das maiores preocupações de quem tem um pet novo em casa é ensinar o cachorro a fazer as necessidades no lugar certo. ','&lt;p&gt;aaa&lt;/p&gt;','cao-de-alto-angulo-brincando-com-papel-higienico.jpg','https://petcare.com.br/ensinar-cachorro-necessidades-no-lugar-certo/');
INSERT INTO "Post" ("Id","Title","Introduction","BodyText","Image","PostUrl") VALUES (13,'Guia para acolher um animal de estimação adotado','O seu animal de estimação acabou de chegar a casa? Os primeiros dias em casa são sempre especiais e críticos para um animal (especialmente para aqueles que estiveram num abrigo ou tiveram más experiências). O seu cão ou gato pode ficar confuso sobre onde está ou o que esperar da sua família.','','dog-temperaments.jpg','https://www.kiwoko.pt/blog/guia-para-acolher-um-animal-de-estimacao-adotado/');
CREATE INDEX IF NOT EXISTS "FK_Pet_Esterilizacao" ON "Esterilizacao" (
	"IdPet"
);
CREATE INDEX IF NOT EXISTS "FK_Pet_Foto" ON "GaleriaFotos" (
	"IdPet"
);
CREATE INDEX IF NOT EXISTS "IX_Data" ON "GaleriaFotos" (
	"Data"
);
CREATE INDEX IF NOT EXISTS "FK_Especie" ON "Pet" (
	"IdEspecie"
);
CREATE INDEX IF NOT EXISTS "FK_Raca" ON "Pet" (
	"IdRaca"
);
CREATE INDEX IF NOT EXISTS "FK_Situacao" ON "Pet" (
	"IdSituacao"
);
CREATE INDEX IF NOT EXISTS "FK_Idade" ON "Pet" (
	"DataNascimento"
);
CREATE INDEX IF NOT EXISTS "FK_Peso" ON "Pet" (
	"IdPeso"
);
CREATE INDEX IF NOT EXISTS "FK_Temperamento" ON "Pet" (
	"IdTemperamento"
);
CREATE INDEX IF NOT EXISTS "IX_Nome" ON "Pet" (
	"Nome"
);
CREATE INDEX IF NOT EXISTS "FK_Pet_Vacina" ON "Vacina" (
	"IdPet"
);
CREATE INDEX IF NOT EXISTS "FK_Pet_Consulta" ON "ConsultaVeterinario" (
	"IdPet"
);
CREATE INDEX IF NOT EXISTS "IX_DataConsultaVeterinario" ON "ConsultaVeterinario" (
	"DataConsulta"
);
COMMIT;
