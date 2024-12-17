@echo off

REM Change directory to <Current Path>\scripts
cd %~dp0\scripts

REM Execute the script to start the backend API application
start cmd /k run-backend.bat

REM Execute the script to start the frontend UI application
start cmd /k run-frontend.bat

REM Give the front end app some time install modules, build, and start, before opening the browser
REM Ideally we would replace this with a proper request made to the URL to check if its running and healthy
timeout 30

REM Open the UI app in browser
start "" http://localhost:3000