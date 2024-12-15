@echo off

REM Change directory to <Current Path>\scripts
cd %~dp0\scripts

REM Execute the script to start the backend API application
start cmd /k run-backend.bat

REM Give the API a few seconds to start before launchging the UI
REM Ideally we would replace this with a proper request made to the API to check if its running and healthy
timeout 3

REM Execute the script to start the frontend UI application
start cmd /k run-frontend.bat

REM Give the UI a few seconds to start before opening the browser
REM Ideally we would replace this with a proper request made to the UI to check if its running and healthy
timeout 3

REM Open the UI app in browser
start "https://localhost:3000"