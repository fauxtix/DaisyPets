

# Daisy Pets 🐶🐱

This project contains a .net application built using .Net 7 with C#. It comprises a web api and a desktop application (windows forms),
that allows anyone to manage their pets, using a SQLite database.

I used windows forms to create a quick prototype, but I intend to test the Api by creating the front-end using React and Blazor.
Maybe later I'll think about using .Net MAUI, when it's more stable...

It's still a work in progress :-) Localization, for instance; the front-end screenshots are shown in Portuguese, but I plan to include English, French and Spanish versions.
Also, the Photo Gallery form needs some reworking.
 
It provides mostly CRUD (Create, Read, Update, Delete) operations for Pets and other entities that may be related to them (history), or not.

Example

- # Pet

  -- Documents (results of lab tests, medical exams, ...) - (1-N)

  -- Veterinary consultations (1-N)
  
  -- Vaccines (1-N)
  
  -- Dewormers (1-N)
  
  -- Different uses of food brands (1-N)

  -- Specie (Dog, Cat, ...)
  
  -- Breed
  
  -- Temperament
  
  -- Situation
  
  -- Genre
    
  -- ...
  
- # Expenses
  
  -- Category type
  
  -- Expense Type
  
- # Contacts
  
  -- Contact type
  
... 

# Screenshots

![main](https://github.com/fauxtix/DaisyPets/assets/49880538/54768f18-9e6d-44f3-8a27-c4cffb7bbae7)
![Pets](https://github.com/fauxtix/DaisyPets/assets/49880538/e9d904d9-1a33-415f-b12e-0833f17e8b85)
![PetDocuments](https://github.com/fauxtix/DaisyPets/assets/49880538/a6eebe33-7f55-4a86-a708-3152a895f176)
![PetVaccines](https://github.com/fauxtix/DaisyPets/assets/49880538/0e52e4e5-7350-45a8-99a5-c960c5be90ae)
![PetAppointments](https://github.com/fauxtix/DaisyPets/assets/49880538/db77a23e-38cf-4e8f-9b35-abb008ef07f8)
![PetFood](https://github.com/fauxtix/DaisyPets/assets/49880538/0575bf89-6340-4ecf-a3ab-f8de6d8733dc)
![PetDewormers](https://github.com/fauxtix/DaisyPets/assets/49880538/1db1de46-f160-41db-816f-7795523d399b)
![PetExpenses_Main](https://github.com/fauxtix/DaisyPets/assets/49880538/c15af416-de2c-40b2-8358-09a075e03a87)
![PetExpenses_Editing](https://github.com/fauxtix/DaisyPets/assets/49880538/d1eaabf7-1b45-46eb-8f3f-4b2dbd97061b)
![PetContacts](https://github.com/fauxtix/DaisyPets/assets/49880538/76f184ce-4087-4dc7-b698-a2739a4271bb)
![PetAlarms](https://github.com/fauxtix/DaisyPets/assets/49880538/2e098b56-ad77-4941-b293-b9eb2fa0106f)
![PetPhotoGallery](https://github.com/fauxtix/DaisyPets/assets/49880538/ebe49d35-2e82-4218-a629-c13d2507ac4f)
![PetLookupTables](https://github.com/fauxtix/DaisyPets/assets/49880538/b583900e-9230-478f-bf26-593cf3064a04)
![PetExpenseTypes](https://github.com/fauxtix/DaisyPets/assets/49880538/fa820aa0-ae98-484e-896c-5f4eda2b2ce3)


# Tables

== ConsultaVeterinario (Veterinary consultations) ==

CREATE TABLE "ConsultaVeterinario" ( "Id"
INTEGER NOT NULL UNIQUE, "DataConsulta" TEXT NOT
NULL, "Motivo" TEXT NOT NULL, "Diagnostico"
TEXT, "Tratamento" TEXT, "IdPet" INTEGER NOT
NULL, PRIMARY KEY("Id" AUTOINCREMENT) )
Id INTEGER "Id" INTEGER NOT NULL UNIQUE
DataConsulta TEXT "DataConsulta" TEXT NOT NULL
Motivo TEXT "Motivo" TEXT NOT NULL
Diagnostico TEXT "Diagnostico" TEXT
Tratamento TEXT "Tratamento" TEXT
IdPet INTEGER "IdPet" INTEGER NOT NULL

== DesparasitanteExterno (external dewormer) == 

CREATE TABLE "DesparasitanteExterno" ( "Id"
INTEGER NOT NULL UNIQUE, "IdPet" INTEGER NOT
NULL, "IdDesparasitanteExterno" INTEGER NOT
NULL, "Data" TEXT NOT NULL, PRIMARY KEY("Id"
AUTOINCREMENT) )
Id INTEGER "Id" INTEGER NOT NULL UNIQUE
IdPet INTEGER "IdPet" INTEGER NOT NULL
IdDesparasitanteExterno INTEGER "IdDesparasitanteExterno" INTEGER NOT NULL
Data TEXT "Data" TEXT NOT NULL

== DesparasitanteInterno (internal dewormer) ==

CREATE TABLE "DesparasitanteInterno" ( "Id"
INTEGER NOT NULL UNIQUE, "IdPet" INTEGER NOT
NULL, "IdDesparasitanteInterno" INTEGER NOT
NULL, "Data" TEXT NOT NULL, PRIMARY KEY("Id"
AUTOINCREMENT) )
Id INTEGER "Id" INTEGER NOT NULL UNIQUE
IdPet INTEGER "IdPet" INTEGER NOT NULL
IdDesparasitanteInterno INTEGER "IdDesparasitanteInterno" INTEGER NOT NULL
Data TEXT "Data" TEXT NOT NULL

== Especie (dog or cat specie) ==

CREATE TABLE "Especie" ( "Id" INTEGER NOT NULL
UNIQUE, "Descricao" TEXT NOT NULL, PRIMARY
KEY("Id" AUTOINCREMENT) )
Id INTEGER "Id" INTEGER NOT NULL UNIQUE
Descricao TEXT "Descricao" TEXT NOT NULL

== Esterilizacao (sterilization) == 

CREATE TABLE "Esterilizacao" ( "Id" INTEGER NOT
NULL UNIQUE, "IdPet" INTEGER NOT NULL,
"Veterinario" TEXT NOT NULL, PRIMARY KEY("Id"
AUTOINCREMENT) )
Id INTEGER "Id" INTEGER NOT NULL UNIQUE
IdPet INTEGER "IdPet" INTEGER NOT NULL
Veterinario TEXT "Veterinario" TEXT NOT NULL

== GaleriaFotos (Photo gallery) == 

CREATE TABLE "GaleriaFotos" ( "Id" INTEGER NOT
NULL UNIQUE, "IdPet" INTEGER NOT NULL, "Imagem"
TEXT NOT NULL, "Data" TEXT, PRIMARY KEY("Id"
AUTOINCREMENT) )
Id INTEGER "Id" INTEGER NOT NULL UNIQUE
IdPet INTEGER "IdPet" INTEGER NOT NULL
Imagem TEXT "Imagem" TEXT NOT NULL
Data TEXT "Data" TEXT

== Idade (Age intervals) == 

CREATE TABLE "Idade" ( "Id" INTEGER NOT NULL
UNIQUE, "De" INTEGER NOT NULL, "A" INTEGER,
"Descricao" TEXT, PRIMARY KEY("Id"
AUTOINCREMENT) )
Id INTEGER "Id" INTEGER NOT NULL UNIQUE
De INTEGER "De" INTEGER NOT NULL
A INTEGER "A" INTEGER
Descricao TEXT "Descricao" TEXT

== MarcaRacao (food brand) == 

CREATE TABLE "MarcaRacao" ( "Id" INTEGER NOT
NULL UNIQUE, "Descricao" TEXT NOT NULL UNIQUE,
PRIMARY KEY("Id" AUTOINCREMENT) )
Id INTEGER "Id" INTEGER NOT NULL UNIQUE
Descricao TEXT "Descricao" TEXT NOT NULL UNIQUE

== Medicacao (medication) ==

CREATE TABLE "Medicacao" ( "Id" INTEGER NOT NULL
UNIQUE, "Descricao" TEXT NOT NULL, PRIMARY
KEY("Id" AUTOINCREMENT) )
Id INTEGER "Id" INTEGER NOT NULL UNIQUE
Descricao TEXT "Descricao" TEXT NOT NULL

== Peso (weight ranges) ==

CREATE TABLE "Peso" ( "Id" INTEGER NOT NULL
UNIQUE, "De" INTEGER NOT NULL, "A" INTEGER NOT
NULL, "Descricao" TEXT, PRIMARY KEY("Id"
AUTOINCREMENT) )
Id INTEGER "Id" INTEGER NOT NULL UNIQUE
De INTEGER "De" INTEGER NOT NULL
A INTEGER "A" INTEGER NOT NULL
Descricao TEXT "Descricao" TEXT

== Pet ==

CREATE TABLE "Pet" ( "Id" INTEGER NOT NULL
UNIQUE, "IdEspecie" INTEGER NOT NULL, "IdRaca"
NUMERIC NOT NULL, "IdIdade" INTEGER NOT NULL,
"IdSituacao" NUMERIC NOT NULL, "Nome" TEXT NOT
NULL UNIQUE, "Foto" TEXT NOT NULL, "Cor" TEXT
NOT NULL, "IdPeso" INTEGER, "IdTemperamento"
INTEGER NOT NULL, "IdMedicacao" INTEGER NOT
NULL, "Chip" TEXT NOT NULL UNIQUE,
"Esterilizado" INTEGER NOT NULL DEFAULT 0,
"Padrinho" INTEGER NOT NULL DEFAULT 0,
"DoencaCronica" TEXT, "Observacoes" TEXT,
PRIMARY KEY("Id" AUTOINCREMENT) )
Id INTEGER "Id" INTEGER NOT NULL UNIQUE
IdEspecie INTEGER "IdEspecie" INTEGER NOT NULL
IdRaca NUMERIC "IdRaca" NUMERIC NOT NULL
IdIdade INTEGER "IdIdade" INTEGER NOT NULL
IdSituacao NUMERIC "IdSituacao" NUMERIC NOT NULL
Nome TEXT "Nome" TEXT NOT NULL UNIQUE
Foto TEXT "Foto" TEXT NOT NULL
Cor TEXT "Cor" TEXT NOT NULL
IdPeso INTEGER "IdPeso" INTEGER
IdTemperamento INTEGER "IdTemperamento" INTEGER NOT NULL
IdMedicacao INTEGER "IdMedicacao" INTEGER NOT NULL
Chip TEXT "Chip" TEXT NOT NULL UNIQUE
Esterilizado INTEGER "Esterilizado" INTEGER NOT NULL DEFAULT 0
Padrinho INTEGER "Padrinho" INTEGER NOT NULL DEFAULT 0
DoencaCronica TEXT "DoencaCronica" TEXT
Observacoes TEXT "Observacoes" TEXT

== Raca (breed) ==

CREATE TABLE "Raca" ( "Id" INTEGER NOT NULL
UNIQUE, "Descricao" TEXT NOT NULL UNIQUE,
PRIMARY KEY("Id" AUTOINCREMENT) )
Id INTEGER "Id" INTEGER NOT NULL UNIQUE
Descricao TEXT "Descricao" TEXT NOT NULL UNIQUE

== Racao (food) ==

CREATE TABLE "Racao" ( "Id" INTEGER NOT NULL
UNIQUE, "IdPet" INTEGER NOT NULL, "DataCompra"
TEXT, "IdMarca" INTEGER NOT NULL,
"QuantidadeDiaria" INTEGER, PRIMARY KEY("Id"
AUTOINCREMENT) )
Id INTEGER "Id" INTEGER NOT NULL UNIQUE
IdPet INTEGER "IdPet" INTEGER NOT NULL
DataCompra TEXT "DataCompra" TEXT
IdMarca INTEGER "IdMarca" INTEGER NOT NULL
QuantidadeDiaria INTEGER "QuantidadeDiaria" INTEGER

== Temperamento (Temperament) ==

CREATE TABLE "Temperamento" ( "Id" INTEGER NOT
NULL UNIQUE, "Descricao" TEXT NOT NULL,
"Observacoes" TEXT, PRIMARY KEY("Id"
AUTOINCREMENT) )
Id INTEGER "Id" INTEGER NOT NULL UNIQUE
Descricao TEXT "Descricao" TEXT NOT NULL
Observacoes TEXT "Observacoes" TEXT

== TipoDesparasitanteExterno (type of external dewormer) ==

CREATE TABLE "TipoDesparasitanteExterno" ( "Id"
INTEGER NOT NULL UNIQUE, "Descricao" TEXT,
PRIMARY KEY("Id" AUTOINCREMENT) )
Id INTEGER "Id" INTEGER NOT NULL UNIQUE
Descricao TEXT "Descricao" TEXT

== TipoDesparasitanteInterno (type of internal dewormer) == 

CREATE TABLE "TipoDesparasitanteInterno" ( "Id"
INTEGER NOT NULL UNIQUE, "Descricao" TEXT,
PRIMARY KEY("Id" AUTOINCREMENT) )
Id INTEGER "Id" INTEGER NOT NULL UNIQUE
Descricao TEXT "Descricao" TEXT

== Vacina (vaccine) ==

CREATE TABLE "Vacina" ( "Id" INTEGER NOT NULL
UNIQUE, "IdPet" INTEGER NOT NULL, "DataToma"
TEXT NOT NULL, "ProximaTomaEmMeses" INTEGER NOT
NULL, PRIMARY KEY("Id" AUTOINCREMENT) )
Id INTEGER "Id" INTEGER NOT NULL UNIQUE
IdPet INTEGER "IdPet" INTEGER NOT NULL
DataToma TEXT "DataToma" TEXT NOT NULL
ProximaTomaEmMeses INTEGER "ProximaTomaEmMeses" INTEGER NOT NULL

sqlite_sequence CREATE TABLE sqlite_sequence(name,seq)
name "name"
seq "seq"


# Indexes

FK_DespExterno_Tipo CREATE INDEX "FK_DespExterno_Tipo" ON
"DesparasitanteExterno" (
"IdDesparasitanteExterno" )
IdDesparasitanteExterno "IdDesparasitanteExterno"

FK_DespInterno_Tipo CREATE INDEX "FK_DespInterno_Tipo" ON
"DesparasitanteInterno" (
"IdDesparasitanteInterno" )
IdDesparasitanteInterno "IdDesparasitanteInterno"

FK_Especie CREATE INDEX "FK_Especie" ON "Pet" (
"IdEspecie" )
IdEspecie "IdEspecie"

FK_Idade CREATE INDEX "FK_Idade" ON "Pet" ( "IdIdade" )
IdIdade "IdIdade"

FK_Medicacao CREATE INDEX "FK_Medicacao" ON "Pet" (
"IdMedicacao" )
IdMedicacao "IdMedicacao"

FK_Peso CREATE INDEX "FK_Peso" ON "Pet" ( "IdPeso" )
IdPeso "IdPeso"

FK_Pet_Consulta CREATE INDEX "FK_Pet_Consulta" ON
"ConsultaVeterinario" ( "IdPet" )
IdPet "IdPet"

FK_Pet_DesparasitanteExterno CREATE INDEX "FK_Pet_DesparasitanteExterno" ON
"DesparasitanteExterno" ( "IdPet" )
IdPet "IdPet"

FK_Pet_DesparasitanteInterno CREATE INDEX "FK_Pet_DesparasitanteInterno" ON
"DesparasitanteInterno" ( "IdPet" )
IdPet "IdPet"

FK_Pet_Esterilizacao CREATE INDEX "FK_Pet_Esterilizacao" ON
"Esterilizacao" ( "IdPet" )
IdPet "IdPet"

FK_Pet_Foto CREATE INDEX "FK_Pet_Foto" ON "GaleriaFotos" (
"IdPet" )
IdPet "IdPet"

FK_Pet_Vacina CREATE INDEX "FK_Pet_Vacina" ON "Vacina" (
"IdPet" )
IdPet "IdPet"

FK_Raca CREATE INDEX "FK_Raca" ON "Pet" ( "IdRaca" )
IdRaca "IdRaca"

FK_Situacao CREATE INDEX "FK_Situacao" ON "Pet" (
"IdSituacao" )
IdSituacao "IdSituacao"

FK_Temperamento CREATE INDEX "FK_Temperamento" ON "Pet" (
"IdTemperamento" )
IdTemperamento "IdTemperamento"

IX_Data CREATE INDEX "IX_Data" ON "GaleriaFotos" (
"Data" )
Data "Data"

IX_DataConsultaVeterinario CREATE INDEX "IX_DataConsultaVeterinario" ON
"ConsultaVeterinario" ( "DataConsulta" )
DataConsulta "DataConsulta"

IX_Nome CREATE INDEX "IX_Nome" ON "Pet" ( "Nome" )
Nome "Nome"


## Requirements

Before running the Daisy Pets project, ensure that you have the following dependencies installed:

- .NET 7 SDK: https://dotnet.microsoft.com/download/dotnet/7.0

## 🥏 Getting Started

To download and run the Daisy Pets project, follow these steps:

1. Clone the repository to your local machine:

   ```bash
   git clone https://github.com/fauxtix/DaisyPets.git
   ```

2. Navigate to the project directory:

   ```bash
   cd DaisyPets
   ```

3. Build the project:

   ```bash
   dotnet build
   ```

4. Apply the database migrations to create the SQLite database:

   ```bash
   dotnet ef database update --project DaisyPets
   ```

5. Run the API:

   ```bash
   dotnet run --project DaisyPets
   ```

The API should now be running locally on `http://localhost:[PORT]`.

## 🧩 API Endpoints

The DaisyPets API provides the following endpoints:

Carousel (Photo gallery)
- POST: /api/Carousel
- PUT /api/Carousel/{Id}
- DELETE /api/Carousel/{Id}
- GET /api/Carousel/{Id}
- GET /api/Carousel/AllPhotosVM
- GET /api/Carousel/PhotoVMById/{Id}
- POST /api/Carousel/ValidatePhoto

Consulta (Veterinary encounters)
- POST /api/Consulta
- PUT /api/Consulta/{Id}
- DELETE /api/Consulta/{Id}
- GET /api/Consulta/{Id}
- GET /api/Consulta/AllConsultaVM
- GET /api/Consulta/ApptVMById/{Id}
- GET /api/Consulta/PetAppointments/{Id}
- POST /api/Consulta/ValidateAppointment

Contacts
- POST /api/Contacts
- PUT /api/Contacts/{Id}
- DELETE /api/Contacts/{Id}
- GET /api/Contacts/{Id}
- GET /api/Contacts/AllContactsVM
- GET /api/Contacts/ContactVMById/{Id}
- POST /api/Contacts/ValidateContacts

Desparasitante (Dewormer)
- POST /api/Desparasitante
- PUT /api/Desparasitante/{Id}
- DELETE /api/Desparasitante/{Id}
- GET /api/Desparasitante/{Id}
- GET /api/Desparasitante/AllWormersVM
- GET /api/Desparasitante/desparasitanteVMById/{Id}
- GET /api/Desparasitante/PetDewormers/{Id}
- POST /api/Desparasitante/ValidateDesparasitantes
- GET /api/Desparasitante/Desparasitante_Info_Pdf

Despesa (Expense)
- POST /api/Despesa
- PUT /api/Despesa/{Id}
- DELETE /api/Despesa/{Id}
- GET /api/Despesa/{Id}
- GET /api/Despesa/VMExpenseByIdAsync/{Id}
- GET /api/Despesa/AllAsync
- GET /api/Despesa/AllVMAsync
- GET /api/Despesa/TipoDespesa_ByCategoriaDespesa/{Id}
- GET /api/Despesa/TipoDespesas
- GET /api/Despesa/DescricaoCategoriaDespesa/{Id}
- POST /api/Despesa/ValidateExpense

Document
- POST /api/Document
- PUT /api/Document/{Id}
- DELETE /api/Document/{Id}
- GET /api/Document/{Id}
- GET /api/Document/AllDocumentsVM/{Id}
- POST /api/Document/ValidateDocument

LookupTables
- GET /api/LookupTables/GetAllRecords/{tableName}
- GET /api/LookupTables/GetDescriptionByIdAndTable/{id}/{tableName}
- GET /api/LookupTables/GetPKByDescriptionAndTable/{description}/{tableName}
- GET /api/LookupTables/{id}/{tableName}
- DELETE /api/LookupTables/{id}/{tableName}
- GET /api/LookupTables/CheckRecordExist/{description}/{tableName}
- POST /api/LookupTables
- PUT /api/LookupTables/{id}
- GET /api/LookupTables/CheckFkInUse/{idFK}/{fieldToCheck}/{tableToCheck}
- GET /api/LookupTables/GetLastInsertedId/{tableToCheck}
- GET /api/LookupTables/GetFirstId/{tableName}

MailMerge
- POST /api/MailMerge/MailMergeDocument
- GET /api/MailMerge/DatabaseStructure

Pets
- POST /api/Pets
- PUT /api/Pets/{Id}
- DELETE /api/Pets/{Id}
- GET /api/Pets/{Id}
- GET /api/Pets/AllPetsVM
- GET /api/Pets/PetVMById/{Id}
- GET /api/Pets/Pesos
- GET /api/Pets/Idade/{tamanho}/{meses}
- POST /api/Pets/ValidatePets

Racao (Dog/Cat food)
- POST /api/Racao
- PUT /api/Racao/{Id}
- DELETE /api/Racao/{Id}
- GET /api/Racao/{Id}
- GET /api/Racao/AllRacoesVM
- GET /api/Racao/RacaoVMById/{Id}
- GET /api/Racao/PetFeeds/{Id}
- POST /api/Racao/ValidateRacao
- GET /api/Racao/DogFood_Info_Pdf

ServerPdf
- GET /api/ServerPdf/Download/{folder}/{filename}
- GET /api/ServerPdf/GetServerPdfName/{folder}/{filename}

TipoDespesas (Expense type)
- POST /api/TipoDespesas
- PUT /api/TipoDespesas/{id}
- DELETE /api/TipoDespesas/{id}
- GET /api/TipoDespesas/AllTipoDespesas
- GET /api/TipoDespesas/AllTipoDespesasVM
- GET /api/TipoDespesas/TipoDespesaById/{id}
- GET /api/TipoDespesas/TipoDespesaByIdVM/{Id}
- POST /api/TipoDespesas/ValidateExpenseType

Vacinacao (Vaccines)
- POST /api/Vacinacao
- PUT /api/Vacinacao/{Id}
- DELETE /api/Vacinacao/{Id}
- GET /api/Vacinacao/{Id}
- GET /api/Vacinacao/AllVacinasVM
- GET /api/Vacinacao/VacinaVMById/{Id}
- GET /api/Vacinacao/PetVaccines/{Id}
- POST /api/Vacinacao/ValidateVaccine
- GET /api/Vacinacao/Vaccines_Info_Pdf

## 🐶 Example Usage

To create a new pet, you can use a tool like cURL or a REST client of your choice:

```bash
curl -X POST -H "Content-Type: application/json" -d '{
  "idEspecie": 1, 
  "idRaca": 11,
  "idTamanho": 2,
  "dataNascimento": "01/01/2020",
  "idSituacao": 1,
  "nome": "Shiva",
  "foto": "C:\\Users\\User\\OneDrive\\Imagens\\dogs\\Shiva.jpg",
  "cor": "Preto e castanho",
  "genero": "F",
  "idPeso": 20,
  "idTemperamento": 2,
  "medicacao": "Aluporinol (dosagem a completar)",
  "chipado": 1,
  "chip": "",
  "dataChip": "28/06/2023",
  "numeroChip": "112354559998493",
  "esterilizado": 1,
  "padrinho": 0,
  "doencaCronica": "Leishmaniose",
  "observacoes": "Linda"
}
' http://localhost:5000/api/pets
```

This will create a new pet named "Shiva".

## 🌟 Contributing

Contributions to the Daisy Pets project are welcome! If you encounter any issues or have suggestions for improvement, please open an issue on the GitHub repository: https://github.com/fauxtix/DaisyPets/issues

When contributing code, please follow the existing code style and submit a pull request with your changes.

## ⚖ License

The DaisyPets project is licensed under the MIT License. You can find more information in the [LICENSE](https://github.com/fauxtix/daisypets/blob/main/LICENSE) file.

## 📞 Contact

If you have any questions or need further assistance, you can reach out to the project maintainer:

- 👨‍💻 Maintainer: Fausto Luís
- ✉ Email: fauxtix.luix@hotmail.com

Feel free to contact me with any feedback or inquiries.

Thank you for using Daisy Pets!
