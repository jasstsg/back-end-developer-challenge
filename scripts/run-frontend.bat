@echo off
cd ..\ddb-hp-ui

echo installing npm modules...
call npm install

echo building the application...
call npm run build

echo starting the fronent ui app...
call npm run start