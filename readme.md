# Trainee Management API

## Technology Used
- dotnet - to create all apis

## Features Completed
Working API endpoints:
- GET /api/health
- GET /api/trainees
- GET /api/trainees/{id}
- POST /api/trainees
- PUT /api/trainees/{id}
- DELETE /api/trainees/{id}

## requirements
- Must have dotnet installed on the system to run this project

## How to Run
- Download the project folder (Trainee.api)
- Open the project folder in any IDE
- Open terminal, go to root project folder path and run this command 'dotnet run'
- To test the apis, go to this url http://localhost:port/swagger/index.html

## expected response for each api

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

## Challanges faced
- While installing dotnet packages and setting up initial web project in dotnet
- While installing and setting up swagger in the current project for testing apis

## Improvements Planned
Day 3 Goal
- Replace manual List<T> storage with EF Core InMemory Database.
- Introduce DbContext, DbSet, async/await, LINQ filtering, and search query parameters.
- Prepare trainees for Phase 2, where the same project will move to EF Core Code First with a real database.

