# DDB Back End Developer Challenge

## Overview
This repository implements the task outlined in the [back-end-developer-challenge](https://github.com/DnDBeyond/back-end-developer-challenge) repository.  It provides an API for managing a Dungeons and Dragons player character's Hit Points (HP).  The API provides operations for dealing damage, healing, and add temporary hit points to the character.

## Usage

### Dependencies
- SQL Server 2019
- .NET Core 8
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


## Architecture
The application consists of three major parts. A SQL database, a .NET Core Web API, and a NextJS app for the UI.  The SQL database stores character data.  The API is responsible for updating the character data in the database and for serving incoming requests for character data.  The UI displays character data and provides controls on page for interacting with the API.  Below is a digram depicting different layers of the overall application, followed by more details regarding each layer.

![Architecture Diagram](https://github.com/jasstsg/back-end-developer-challenge/blob/master/diagrams/Architecture.drawio.png)

### SQL CharacterDB
SQL is used to house a database called "CharacterDB".  The tables are broken down to match each model that was used in code, and the models are based on the JSON objects in the [provided character JSON file](https://github.com/jasstsg/back-end-developer-challenge/blob/master/DDB.HPApi/briv.json).  Below is a diagram showing how the models were identified in the JSON structure (left) and how the tables are related to each other as a result of their respective models navigation properties (right).

![Models & SQL Tables](https://github.com/jasstsg/back-end-developer-challenge/blob/master/diagrams/Models%20%26%20SQL%20Structure.PNG)

### .NET Core Backend

#### Character Controller
This controller is the exposed API, it has actions for each of the required API Operations (Deal Damage, Heal, Add Temporary Hit Points).  These actions pass data from requests to the service layer.

#### Character Service
The service layer houses the business logic that processes the given data.  For example, when a request is made to 'deal damage' it will consider the character's resistances and immunities (halving or negating the damage) and apply the resulting damage to the temporary and actualy hit points appropriately.  It then provides new character data to the repository layer.

#### Character Respository
The repository layer is unaware of the API operations.  It abstracts EF Core methods and exposes its own simple methods (to be used by the service layer) that retrieve or update character data.

#### EF Core
Microsoft's EntityFramework Core is used to read and write data to the SQL database.  Its methods are called by the repository layer, and during application initiliazation in order to seed the database.

### NextJS Frontend 

#### React UI
React + TypeScript component front end interface providing controls for performing the API operations.  This UI leverages components from [MaterialUI](https://mui.com/material-ui/).

![UI Image](https://github.com/jasstsg/back-end-developer-challenge/blob/master/diagrams/UI.PNG)

#### Axios Service
A service that is invoked by the controls in the React UI.  It makes requests to the .NET Core Web API using [Axios](https://axios-http.com/docs/intro) and provides response data back to the UI so it can reflect updates in the page.

