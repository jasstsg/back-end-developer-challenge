# DDB Back End Developer Challenge

## Overview
This repository implments the task outlined in the [back-end-developer-challenge](https://github.com/DnDBeyond/back-end-developer-challenge) repository.  It provides an API for managing a Dungeons and Dragons player character's Hit Points (HP).  The API provides operations for dealing damage, healing, and add temporary hit points to the character.

## Architecture
The application consists of three major parts.  A NextJS app for the UI, a .NET Core Web API, and a SQL database.  Below is a digram dipicting different layers of the overall application, followed by more details regarding each layer.

![Architecture Diagram](https://github.com/jasstsg/back-end-developer-challenge/blob/master/diagrams/Architecture.drawio.png)

### NextJS Frontend 

#### React UI
React + TypeScript component front end interface providing controls for performing the API operations.  This UI leverages components from [MaterialUI](https://mui.com/material-ui/).

![UI Image]()

#### Axios Service
A service that is invoked by the controls in the React UI.  It makes requests to the .NET Core Web API using [Axios](https://axios-http.com/docs/intro) and provides response data back to the UI so it can reflect updates in the page.

### .NET Core Backend

#### Character Controller
This controller is the exposed API, it has actions for each of the required API Operations (Deal Damage, Heal, Add Temporary Hit Points).  These actions pass data from requests to the service layer.

#### Character Service
The service layer houses the business logic that processes the given data.  For example, when a request is made to 'deal damage' it will consider the character's resistances and immunities (halving or negating the damage) and apply the resulting damage to the temporary and actualy hit points appropriately.  It then provides new character data to the repository layer.

#### Character Respository
The repository layer is unaware of the API operations.  It abstracts EF Core methods and exposes its own simple methods (to be used by the service layer) that retrieve or update character data.

#### EF Core
Microsoft's EntityFramework Core is used to read and write data to the SQL database.  Its methods are called by the repository layer, and during application initiliazation in order to seed the database.

### SQL CharacterDB
SQL is used to house a database called "CharacterDB".  The tables are broken down to match each model that was used in code, and the models are based on the JSON objects in the [provided character JSON file](https://github.com/jasstsg/back-end-developer-challenge/blob/master/DDB.HPApi/briv.json).  Below is a diagram showing how the models were identified in the JSON structure (left) and how the tables are related to each other as a result of their respective models navigation properties (right).

![Models & SQL Tables](https://github.com/jasstsg/back-end-developer-challenge/blob/master/diagrams/Models%20%26%20SQL%20Structure.PNG)


