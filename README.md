# SyncGuardian Console App

**SyncGuardian** is a console application designed to provide file backup functionalities with additional features.

## Table of Contents

- [Introduction](#introduction)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Features](#features)
- [Project Structure](#project-structure)
- [Dependencies](#dependencies)
- [Contributing](#contributing)
- [Download](#download)
- [License](#license)

## Introduction

**SyncGuardian** is a console application written in C# that offers a simple and efficient way to create backups of files and folders. It provides a menu-driven interface for users to navigate through various options such as starting a backup, accessing instructions, learning about the application, and quitting the app.

## Getting Started

To get started with **SyncGuardian**, follow these steps:

1. Clone the repository to your local machine:

    ```bash
    git clone https://github.com/your-username/SyncGuardian.git
    ```

2. Open the project in your preferred C# development environment.

3. Build and run the project to start the **SyncGuardian** console application.

## Usage

Once the application is running, the user is presented with a menu containing different options:

- **Start Backup:** Initiates the process to start file backup.
- **Instructions:** Provides instructions on how to use the application.
- **About:** Displays information about the application.
- **Quit:** Exits the application.

Follow the on-screen prompts to navigate through the menu and perform desired actions.

## Features

- Menu-driven interface for easy navigation.
- File backup with options for source and backup directories, time intervals, and more.
- Logging of backup actions to a log file.
- Ability to delete files only present in the backup folder.


## Project Structure

The project is structured as follows:

- **Program.cs:** Main entry point for the console application.
- **ConsoleMenu.cs:** Manages the console menu and user interactions.
- **Helper.cs:** Contains helper methods for console interactions and input validation.
- **ActionMethods.cs:** Defines methods associated with different menu actions.
- **BackupController.cs:** Handles the backup process and file operations.
- **Services:** Folder containing services used in the application.
  - **LogService.cs:** Service for registering messages on a log file.
  - **FileComparer.cs:** Service for file comparison, verifying existence, and length checking.
  - **FileHashComparer.cs:** Extends FileComparer to include hash-based file comparison.

## Dependencies

The project uses the following dependencies:

- System.Runtime.InteropServices
- System.Timers
- System.IO
- System.Security.Cryptography

Ensure these dependencies are available in your C# development environment.

## Contributing

Contributions to this project are welcome! Feel free to open issues or pull requests.


## Download

- [SyncGuardian.zip](https://drive.google.com/file/d/1PBztduThoQBjWGWctiY60nh5UfDvjjzj/view?usp=drive_link) Download the executable file.

   SyncGuardian.zip contains the executable of the application.


## License

This project is licensed under the MIT License.
