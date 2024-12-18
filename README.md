# DDB Back End Developer Challenge

## Overview
This repository implements the task outlined in the [back-end-developer-challenge](https://github.com/DnDBeyond/back-end-developer-challenge) repository.  It provides an API for managing a Dungeons and Dragons player character's Hit Points (HP).  The API provides operations for dealing damage, healing, and add temporary hit points to the character.

## Documentation
- [API Specification](https://github.com/jasstsg/back-end-developer-challenge/wiki/API-Specification)
- [Architecture](https://github.com/jasstsg/back-end-developer-challenge/wiki/Architecture)
- [Unit Testing](https://github.com/jasstsg/back-end-developer-challenge/wiki/Unit-Testing)

## Usage

### Dependencies
- SQL Server 2019
- .NET 8
- Node v20.18.0

### Getting Started
1. Ensure you have the dependencies installed
2. Clone this repository and open the root folder
3. Double click the `run.bat` file or run it in a command prompt.  This executes the following commands for you (if you have issues run these manually):
  - Run the backend:
     ```CMD
      cd <root folder>\DDB.HPApi
      dotnet run
    ```
  - Run the frontend:
    ```CMD
      cd <root folder>\ddb-hp-ui
      npm install
      npm run build
      npm run start
    ```
4. When the scripts are done the backend should be running on http://localhost:5011 and the frontend should be running on http://localhost:3000.  Open the frontend in your browser and have fun :)
