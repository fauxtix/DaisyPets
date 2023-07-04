# Daisy Pets Manager 🐶🐱

This project contains a sample ASP.NET Core app. This app is an example of the article I produced for the Telerik Blog (telerik.com/blogs).

PetManager is a minimal API built using .NET 7 that allows you to manage pets using a SQLite database.
It provides basic CRUD (Create, Read, Update, Delete) operations for pet entities.

## Requirements

Before running the PetManager project, ensure that you have the following dependencies installed:

- .NET 7 SDK: https://dotnet.microsoft.com/download/dotnet/7.0

## 🥏 Getting Started

To download and run the PetManager project, follow these steps:

1. Clone the repository to your local machine:

   ```bash
   git clone https://github.com/zangassis/pet-manager.git
   ```

2. Navigate to the project directory:

   ```bash
   cd PetManager
   ```

3. Build the project:

   ```bash
   dotnet build
   ```

4. Apply the database migrations to create the SQLite database:

   ```bash
   dotnet ef database update --project PetManager
   ```

5. Run the API:

   ```bash
   dotnet run --project PetManager
   ```

The API should now be running locally on `http://localhost:[PORT]`.

## 🧩 API Endpoints

The PetManager API provides the following endpoints:

- `GET /pets`: Retrieves all pets.
- `GET /pets/{id}`: Retrieves a specific pet by its ID.
- `POST /pets`: Creates a new pet.
- `PUT /pets/{id}`: Updates an existing pet.
- `DELETE /pets/{id}`: Deletes a pet.

## 🐶 Example Usage

To create a new pet, you can use a tool like cURL or a REST client of your choice:

```bash
curl -X POST -H "Content-Type: application/json" -d '{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Max",
  "species": "Dog",
  "breed": "Golden Retriever",
  "age": 3,
  "color": "Golden",
  "weight": 30,
  "vaccinated": true,
  "lastVaccinationDate": "2023-05-01",
  "owner": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "name": "John Doe",
    "email": "johndoe@example.com",
    "phone": "123-456-7890"
  }
}
' http://localhost:5000/api/pets
```

This will create a new pet named "Max" with the species "Dog" and age 3.

## 🌟 Contributing

Contributions to the PetManager project are welcome! If you encounter any issues or have suggestions for improvement, please open an issue on the GitHub repository: https://github.com/zangassis/pet-manager/issues

When contributing code, please follow the existing code style and submit a pull request with your changes.

## ⚖ License

The PetManager project is licensed under the MIT License. You can find more information in the [LICENSE](https://github.com/zangassis/pet-manager/blob/main/LICENSE) file.

## 📞 Contact

If you have any questions or need further assistance, you can reach out to the project maintainers:

- 👨‍💻 Maintainer: Assis Zang
- ✉ Email: assiszang@gmail.com

Feel free to contact us with any feedback or inquiries.

Thank you for using PetManager!
