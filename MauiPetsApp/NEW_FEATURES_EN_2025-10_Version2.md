# New Features – MauiPets

This document summarizes the main features recently added (Oct/2025).

---

## 📸 Pet Photo Gallery

- **Per-Pet Photo Gallery**
  - Each pet now has an associated photo gallery.
  - Features include:
    - Add photos (using device camera or gallery).
    - View photos in a gallery/carousel mode.
    - Delete individual photos.
    - Enlarge/view a photo in a popup.
  - Photos are stored locally in the app and linked to the respective pet.

- **UI/UX Integration**
  - Access the gallery directly from the pet profile.
  - Visual confirmation and toast messages for user actions (e.g., photo deletion).
  - Easy navigation between the gallery and pet details.

---

## 🔐 Data Backup and Restore (`BackupPage`)

- **Manual Backup**
  - Ability to create backups of the app's local database via the interface.
  - Users can see the name, date, and location of the last backup.
  - Backup is saved as a local file, with visual indication of success/error.
  - Protection against accidental overwriting: confirmation before replacing existing backups.

- **Secure Restore**
  - Restore the local database from an existing backup.
  - Mandatory confirmation before replacing current data.
  - Visual information about differences between the current state and the backup.
  - Restore process with user feedback and clear success/failure messages.

---

## 📄 Pet Profile PDF Export & Sharing (`PetFichaPdfService`)

- **Generate Detailed Pet Profile PDF**
  - Create a comprehensive PDF file for any pet, including:
    - Main data (name, species, breed, age, chip, etc.)
    - Vaccination, deworming, food, and vet consultation history.
  
- **Easy Sharing**
  - The PDF can be shared directly through the device’s native share options (email, WhatsApp, etc.).  

---

## Security and Privacy

- **Validation and Confirmation for Critical Actions**
  - Backup/restore and photo deletion actions require user confirmation.
  - Clear messages and visual feedback for all sensitive operations.

- **Local Data Management**
  - Photos and backup files are managed locally, respecting user privacy.
  - No sensitive data is sent to external servers without user action.

---

*For more details on each feature, explore the app interface.*
