
# Trainee Management API

## Technology Used
- dotnet - to create all the apis
- mysql - to store trainees data 

## requirements
- Must have dotnet (version 10) installed on the system to run this project
- Must have mysql (preferably version 8 onwards) installed 

## How to Run
- Download the project folder
- Open this project folder in any IDE
- Update the mysql database connection string details in appsettings.json
- Open terminal, go to root project folder path and run this command 'dotnet restore' to restore all the required packages 
- Run 'dotnet ef migrations add InitialCreate' and 'dotnet ef database update' command for database migrations
- Run 'dotnet run' command to run the project
- To test the apis, go to this url http://localhost:port/swagger/index.html

## Features Completed
Working API endpoints:
- GET /api/health
- GET /api/trainees
- GET /api/trainees/{id}
- POST /api/trainees
- PUT /api/trainees/{id}
- DELETE /api/trainees/{id}


## api endpoints and expected response for each api

- PUT /api/trainees/{id}
    - Valid trainee ID 200 OK
    - Invalid trainee ID 404 Not Found
    - Invalid request 400 Bad Request

- DELETE /api/trainees/{id}
    - Valid trainee ID 204 No Content
    - Invalid trainee ID 404 Not Found

- POST /api/trainees
    - Valid data 201 Created
    - Invalid data 400 Bad Request  

- GET /api/health
    - 200 OK

- GET /api/trainees
    - 200 OK

- GET /api/trainees/{id}
    - Valid ID 200 OK
    - Invalid ID 404 Not Found




## Sample Request JSON
Sample POST /api/trainees request:

    {
        "firstName": "john",
        "lastName": "joe",
        "email": "john.joe@training.com",
        "techStack": "HTML, CSS, JavaScript",
        "status": "Active"
    }
 
Sample PUT /api/trainees/1 request:

    {  
        "firstName": "john",
        "lastName": "cena",
        "email": "john.cena@training.com",
        "techStack": "Java",
        "status": "InActive"
    }
 
## Sample Response JSON
Sample GET /api/trainees response:

    [
        {
            "id": 1,
            "firstName": "john",
            "lastName": "doe",
            "email": "john.doe@training.com",
            "techStack": "HTML, CSS, JavaScript",
            "status": "Active",
            "createdDate": "2026-06-08T10:55:05.7288647+00:00",
            "updatedDate": "2026-06-08T10:55:05.7294876+00:00"
        }
    ]
 
Sample POST /api/trainees response:

    {
    
        "id": 1,
        "firstName": "john",
        "lastName": "joe",
        "email": "john.doe@training.com",
        "techStack": "HTML, CSS, JavaScript",
        "status": "Active"
        "createdDate": "2026-06-08T10:55:05.7288647+00:00",
        "updatedDate": "2026-06-08T10:55:05.7294876+00:00"
    }
    
Sample GET /api/trainees/{id} response:

    {
        "id": 1,
        "firstName": "john",
        "lastName": "joe",
        "email": "john.doe@training.com",
        "techStack": "HTML, CSS, JavaScript",
        "status": "Active",
        "createdDate": "2026-06-08T10:55:05.7288647+00:00",
        "updatedDate": "2026-06-08T10:55:05.7294876+00:00"
    }
 
Sample PUT /api/trainees/{id} response:

    {
        "id": 1,
        "firstName": "john",
        "lastName": "joe",
        "email": "john.doe@training.com",
        "techStack": "HTML, CSS, JavaScript",
        "status": "Inactive",
        "createdDate": "2026-06-08T10:55:05.7288647+00:00",
        "updatedDate": "2026-06-08T10:57:22.9859447+00:00"
    }

## Challanges faced
- While installing dotnet packages and setting up initial web project in dotnet
- While installing and setting up swagger in the current project for testing apis

## Limitations
- Absence of authentication and authorisation