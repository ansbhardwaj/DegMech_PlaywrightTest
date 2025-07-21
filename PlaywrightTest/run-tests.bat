@echo off
echo Building project...
dotnet build

echo Running tests...
dotnet test

echo Generating Allure Report...
allure generate --clean

echo Opening Allure Report in browser...
start "" "%cd%\allure-report\index.html"

pause
