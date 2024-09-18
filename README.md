# MyTestVueApp

## Required IDE Setup

[Node.JS](https://nodejs.org/en)

[Visual Studio](https://visualstudio.microsoft.com/)

[VS Code](https://code.visualstudio.com/)

[GIT](https://git-scm.com/downloads)

[Docker Desktop](https://www.docker.com/get-started/)

## Essential VS Code Extensions

Prettier - Code formatter <- *Set as default formatter*

Path Intellisense

vscode-icons

## Setup for local development

### Frontend setup

1. Open the .Client project folder in VS Code
1. Run the following commands:
	```
	npm install
	```

### Database Setup
1. CD to return to the root directory and run the following commands:
	```
	docker-compose up -d
	```
	*This will run the database in a docker container and create the schema defined in the init.sql file*
1. Open Microsoft SQL Server Management Studio and connect to the database using the following credentials:
	```
	Server: localhost,1433
	Login: sa
	Password: PASSWORD_HERE
	Encryption: Optional
	Trust Server Certificate: Yes
	```
	*Use the same password as defined in your .env file*

### Backend setup

1. Open the .sln file in the root directory of the project in Visual Studio
1. Run the project with the Start button in Visual Studio

## Testing the application

WIP