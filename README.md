

# Daisy Pets 🐶🐱

A project for helping busy pet owners keep track of the daily, and long-term routine care of their pets.

It uses C# as the development language, and was built using the .Net 8 platform.
It comprises a web api that serves a desktop application (windows forms) and a web project (Blazor Server). For the Maui project, the SQLite database was chosen for local data storage.

It is up to the user to choose between the desktop (DaisyPets.UI), the web/blazor application (DaisyPets.Web.Blazor) or mobile app (DaisyPets.MauiPets).
Just start/run the Api first, and choose wich of the projects to use ('set as the startup project').

Due to its simplicity, the SQLite database was chosen for data storage ([Structure])(https://github.com/fauxtix/DaisyPets/blob/master/MauiPetsApp/MauiPets/PetsDB.db.sql). 

The ORM chosen to work with the database was Dapper, due to its ease of use and speed, especially in queries.

The front-end screenshots presented in this Read.me file are in Portuguese, but I plan to include English, French and Spanish versions for the three projects.

The web version (Blazor) already includes the four languages.

It provides mostly CRUD (Create, Read, Update, Delete) operations for Pets and other entities related to them.

For the Blazor project, the syncfusion packages (community edition) were chosen.

# Key Features

- Easy to use
- Simple to upload documents related to the pet (medical records, for instance) and others that may apply;

- Record vaccinations, visits to the vet, dewormers, feed dosage and documents (examination reports, blood and urine tests, and others that may apply);
- Alerts for vaccines and application of dewormers, that may be occurring within 15 or 30 days.
  
- Expenses management (with selection of categories and sub-categories);
- Blog entries (web project) - user entries / from url's;
- Todo lists;
- Photo gallery (desktop project, for now);
- Contacts management;
- Scheduler/agenda (web project);
- App Settings;
- Logs management.

# Screenshots

##### Windows forms

- For the web project screenshots, please select the - [DaisyPets.Web.Blazor](./DaisyPets.Web.Blazor) project.
- For the Maui project screenshots, select the [MauiPetsApp](./MauiPetsApp) project.

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

For the database structure and current data usage, please have a look to the .sql file in the webapi project

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

4. Apply the database migrations to create the SQLite database (if you want to use Entity Framework):

   ```bash
   dotnet ef database update --project DaisyPets
   ```

5. Run the API:

   ```bash
   dotnet run --project DaisyPets.WebApi
   ```

The API should now be running locally on `http://localhost:[PORT]`.

## 🧩 API Endpoints

The DaisyPets API provides the following endpoints:

Appointment 

- POST /api/Appointment
- PUT /api/Appointment/{Id}
- DELETE /api/Appointment/{Id}
- GET /api/Appointment/{Id}
- GET /api/Appointment/AllAppointmentsVM
- POST /api/Appointment/ValidateAppointment

AppUtils

- GET /api/AppUtils/BackupSqlite
- GET /api/AppUtils/Settings/Language
- POST /api/AppUtils/Settings/Language
  
Carousel (Photo gallery)
- POST /api/Carousel
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

TipoDespesas (Type of expenses)
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

ToDos (To-do lists)
- POST /api/ToDos
- GET /api/ToDos
- PUT /api/ToDos/{Id}
- DELETE /api/ToDos/{Id}
- GET /api/ToDos/{Id}
- GET /api/ToDos/PendingTodos
- GET /api/ToDos/CompletedTodos
- POST /api/ToDos/ValidateToDo
#

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

Fork the project (https://github.com/fauxtix/DaisyPets/fork)

Create a branch for your modification (git checkout -b fauxtix/DaisyPets)

Commit (git commit -am 'Add some fooBar')

Push_ (git push origin fauxtix/DaisyPets)

Create a new Pull Request

When contributing code, please follow the existing code style and submit a pull request with your changes.

## ⚖ License

The DaisyPets project is licensed under the MIT License. You can find more information in the [LICENSE](https://github.com/fauxtix/daisypets/blob/main/LICENSE) file.

## 📞 Contact

If you have any questions or need further assistance, you can reach out to the project maintainer:

- 👨‍💻 Maintainer: Fausto Luís
- ✉ Email: fauxtix.luix@hotmail.com

Feel free to contact me with any feedback or inquiries.

Thank you for using Daisy Pets!
