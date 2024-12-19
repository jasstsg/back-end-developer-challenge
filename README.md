# DDB Back End Developer Challenge

## Overview
This repository implements the task outlined in the [back-end-developer-challenge](https://github.com/DnDBeyond/back-end-developer-challenge) repository.  It provides an API for managing a Dungeons and Dragons player character's Hit Points (HP).  The API provides operations for dealing damage, healing, and add temporary hit points to the character.

## Documentation
- [API Specification](https://github.com/jasstsg/back-end-developer-challenge/wiki/API-Specification)
- [Architecture](https://github.com/jasstsg/back-end-developer-challenge/wiki/Architecture)
- [Unit Testing](https://github.com/jasstsg/back-end-developer-challenge/wiki/Unit-Testing)

## Usage

### Dependencies
Other versions may work, however listed below is what was used to develop the project (or test it on other machines).
- SQL Server 2019 (or 2022)
- .NET 8 SDK
- Node v20.18.0 (or v20.18.1)

### Getting Started
1. Ensure you have the dependencies installed
2. Clone this repository and open the root folder
3. Navigate to the `<root folder>\scripts` folder
4. Double click the `run-backend.bat` file or run it in a command prompt.  This executes the following command for you (if you have issues run it manually):
```CMD
cd <root folder>\DDB.HPApi
dotnet run
```
When its done you should see that is mentions the application is running on `http://localhost:5011`.  For example:
```CMD
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5011
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\repo\back-end-developer-challenge\DDB.HPApi
```
5. Now that the backend is running double click the `run-frontend.bat` file or run it in a command prompt.  This executes the following commands for you (if you have issues run them manually):
```CMD
cd <root folder>\ddb-hp-ui
npm install
npm run build
npm run start
```
6. When its done you should see that it mentions the application is running on `http://localhost:3000`.  For example:
```CMD
   ▲ Next.js 15.1.0
   - Local:        http://localhost:3000
   - Network:      http://10.0.0.178:3000

 ✓ Starting...
 ✓ Ready in 558ms
```

7. Open http://localhost:3000 in your browser and have fun :)
