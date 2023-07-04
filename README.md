# Daisy Pets Manager 🐶🐱

This project contains a .net application built using .Net 7 using C#. It comprises a web api and a desktop application (windows forms),
that allows you to manage pets using a SQLite database.
It provides mostly CRUD (Create, Read, Update, Delete) operations for Pets and other entities.

## Requirements

Before running the Daisy Pets tManager project, ensure that you have the following dependencies installed:

- .NET 7 SDK: https://dotnet.microsoft.com/download/dotnet/7.0

## 🥏 Getting Started

To download and run the PetManager project, follow these steps:

1. Clone the repository to your local machine:

   ```bash
   git clone https://github.com/fauxtix/DaisyPets.git
   ```

2. Navigate to the project directory:

   ```bash
   cd DaisyPetsManager
   ```

3. Build the project:

   ```bash
   dotnet build
   ```

4. Apply the database migrations to create the SQLite database:

   ```bash
   dotnet ef database update --project DaisyPetsManager
   ```

5. Run the API:

   ```bash
   dotnet run --project DaisyPetsManager
   ```

The API should now be running locally on `http://localhost:[PORT]`.

## 🧩 API Endpoints

The PetManager API provides the following endpoints:

Carousel
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

This will create a new pet named "Shiva" with the species "Dog" (1) born in 2020.

## 🌟 Contributing

Contributions to the Daisy Pets project are welcome! If you encounter any issues or have suggestions for improvement, please open an issue on the GitHub repository: https://github.com/fauxtix/DaisyPets/issues

When contributing code, please follow the existing code style and submit a pull request with your changes.

## ⚖ License

The DaisyPets project is licensed under the MIT License. You can find more information in the [LICENSE](https://github.com/zangassis/pet-manager/blob/main/LICENSE) file.

## 📞 Contact

If you have any questions or need further assistance, you can reach out to the project maintainers:

- 👨‍💻 Maintainer: Fausto Luís
- ✉ Email: fauxtix.luix@hotmail.com

Feel free to contact me with any feedback or inquiries.

Thank you for using Daisy Pets!
