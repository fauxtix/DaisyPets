# Daisy Pets / Mobile 🐶🐱


- [Portuguese](./PORTUGUESE.MD)

A project for helping busy pet owners keep track of the daily, and long-term routine care of their pets.
It can also be used if you want to start a rescue operation and take them in as a temporary family host.

It uses C# as the development language, and was built using the .Net 8 platform.

Due to its simplicity, the SQLite database was chosen for data storage ([Structure])(https://github.com/fauxtix/DaisyPets/blob/master/MauiPetsApp/MauiPets/PetsDB.db.sql). 

The ORM chosen to work with the database was Dapper, due to its ease of use and speed, especially in queries.

# Key Features

- Easy to use
- Record vaccinations, visits to the vet, applying dewormers and feed dosage;
- Alerts for vaccinations and deworming treatments that are due within 15 or 30 days.
- Expense management (with category and subcategory selection);
- ToDo lists management - manage important commitments such as vet appointments, medication administration, vaccinations, food purchases, and essential products, among others;
- Contact management, with an option to view the location on a map;
- App settings (reference tables, expense categories/subcategories);
- Log management (analysis and problem resolution) - with options to delete and send an e-mail with the generated error/alert.

# Technologies used

- **C#**
- .**NET 8 MAUI** - Framework for building cross-platform mobile applications for Android, iOS, MacOS and Windows;
- **SQLite** - Local database for storing the various objects/entities used in the app;
- **CommunityToolkit.Maui/MVVM** - Libraries to simplify the development of mobile applications with MAUI;
- **Serilog** - Library for logging information from any . NET application, which works with Sinks as a base.
  Serilog itself is a structure that allows us to store any type of information in an organised way during the execution of the application;
- **AutoMapper** - Library that performs the task of mapping one object (class) to another;
- **FluentValidation** - A library that allows programmers to create data validations quickly and easily.
  With it, we can use Lambda expressions to ‘build validation rules’ with error message returns for each property of the entities.
- Others

# Screenshots

![SplashScreen](https://github.com/user-attachments/assets/26dc5f35-0b42-4f00-9265-c9cb3eff764c)
![HomePage](https://github.com/user-attachments/assets/cc01bcc7-bac6-46bb-b622-7679dc8f614a)
![PetDetailPage_1](https://github.com/user-attachments/assets/dde659f8-e2ee-4bc4-9d01-8364d6fdf213)
![PetDetailPage_2](https://github.com/user-attachments/assets/f3cb8493-e8fe-4950-a1de-761633844abc)
![PetDetailPage_3](https://github.com/user-attachments/assets/4debeeb0-e81b-40e0-9e36-f4dfbdfaf575)
![PetAddOrEditPage_1](https://github.com/user-attachments/assets/43a58263-5466-49cf-98e4-df8bdb789198)
![PetAddOrEditPage_2](https://github.com/user-attachments/assets/4c7fa20a-4829-4f6e-957c-6f41573a1ec0)
![PetAddOrEditPage_2_1](https://github.com/user-attachments/assets/d030a3f8-05c7-43df-80e4-45848b6f52c4)
![PetAddOrEditPage_2_2](https://github.com/user-attachments/assets/cac39949-5b9e-45df-8160-fbf1b4d5b973)
![PetAddOrEditPage_2_3](https://github.com/user-attachments/assets/2ba9c8fd-1372-442d-9e5f-e619fb6260fc)
![PetAddOrEditPage_2_4](https://github.com/user-attachments/assets/da6e8db0-6627-42c7-9207-75799f3bbefd)

##### App options

![AppOptionsPage](https://github.com/user-attachments/assets/fcf7b839-2c81-4395-a9ae-d6926ed1188b)

##### Expenses

![ExpensesMainPage](https://github.com/user-attachments/assets/b5f368be-8b70-4452-a05c-97f9156bec76)
![ExpensesEditPage](https://github.com/user-attachments/assets/f2f5c0b8-806e-4da6-a8e2-aeafce2a4efe)
![ExpensesEdit_DeleteExpensePage](https://github.com/user-attachments/assets/8669fb15-cd88-47e8-bcf5-884c6d90f8ca)
![Expenses_Group_Page](https://github.com/user-attachments/assets/872c1632-6746-4d67-8baf-43f83c45520e)

### Contacts

![ContactsMainPage](https://github.com/user-attachments/assets/e145f13b-6f82-415f-813a-39aadd3a2e7d)
![ContactsDetailPage](https://github.com/user-attachments/assets/ef499037-6d9d-44f9-a0ce-89afce54d34c)
![ContactsDetail_MapPage](https://github.com/user-attachments/assets/79b83b0b-c717-416a-8205-1c15d492f704)
![ContactsEditPage](https://github.com/user-attachments/assets/6f13f976-2712-4cbf-806a-ae574749e38f)
![ContactsEdit_Delete_Page](https://github.com/user-attachments/assets/4a718e6f-6de3-4367-93d3-94bd0028b916)

### ToDo List

![TodoListMainPage](https://github.com/user-attachments/assets/a173456e-e66f-4ab8-9bd7-34923f5b0d73)
![TodoListEditPage](https://github.com/user-attachments/assets/b54614d3-74c5-4af9-b831-ab78e035f81f)
![TodoListPendingPage](https://github.com/user-attachments/assets/8222bd72-1e9e-4e39-82ff-fe877c93fb05)

### Settings

![SettingsPage_1](https://github.com/user-attachments/assets/1bafe3dc-e6e3-4e4b-80ef-63ca8a95a574)
![SettingsPage_2](https://github.com/user-attachments/assets/4d9802c5-0802-4618-ab50-fb9418786677)
![SettingsPage_2_1](https://github.com/user-attachments/assets/bdd7b2ea-747d-407e-8804-1df772c28421)
![SettingsPage_2_2](https://github.com/user-attachments/assets/705060fc-4025-4cb2-a0ce-e3d314fd0c28)

### Logs

![LogsMainPage](https://github.com/user-attachments/assets/11b31434-366c-42e2-b7f1-82f044831403)
![Logs_2_Page](https://github.com/user-attachments/assets/f9ca7961-e558-4bd3-9a8b-5aa06956bc97)
![Logs_3_Page](https://github.com/user-attachments/assets/047426d0-55dd-43e2-82a6-99bfeca6252e)
![Logs_DeleteAll_Page](https://github.com/user-attachments/assets/b572309d-07f4-4069-a62a-e8ab53112d65)
![Logs_3_SendEmail_Android_Page](https://github.com/user-attachments/assets/6fd7a63a-b87d-478e-8e4b-4825af9c6e4b)
![Logs_3_GMail_Entry](https://github.com/user-attachments/assets/b84271c1-a1de-4fd8-8012-129e72cf7215)


## 🌟 Contributing

Contributions to the Daisy Pets project are welcome! If you encounter any issues or have suggestions for improvement, please open an issue on the GitHub repository: https://github.com/fauxtix/DaisyPets/issues

Fork the project (https://github.com/fauxtix/DaisyPets/fork)

Create a branch for your modification (git checkout -b fauxtix/DaisyPets)

Commit (git commit -am 'Add some fooBar')

Push_ (git push origin fauxtix/DaisyPets)

Create a new Pull Request

When contributing code, please follow the existing code style and submit a pull request with your changes.

## ⚖ License

The DaisyPets project is licensed under the MIT License. You can find more information in the [LICENSE](https://github.com/fauxtix/DaisyPets/blob/master/MauiPetsApp/LICENSE.md) file.

## 📞 Contact

If you have any questions or need further assistance, you can reach out to the project maintainer:

- 👨‍💻 Maintainer: Fausto Luís
- ✉ Email: fauxtix.luix@hotmail.com

Feel free to contact me with any feedback or inquiries.

