
# Trainee Management API

## Technology Used
- dotnet - to create all the apis
- mysql - to store trainees data 
- ef core - to connect to mysql database

## requirements
- Must have dotnet (version 10) installed on the system to run this project
- Must have mysql (preferably version 8 onwards) installed 

## How to Run (Backend Setup)
- Download the project folder
- Open this project folder in any IDE
- Update the mysql database connection string and jwt configuration details in .env. If there is no .env file, create it under project root folder and add/update the following values :

        - Jwt__Key=
        - Jwt__Issuer=
        - Jwt__Audience=
        - Jwt__ExpiryMinutes=
        - ConnectionStrings__DefaultConnection=
        - AdminUser__Password=
        - AdminUser__Email=

- Open terminal, go to root project folder path and run this command 'dotnet restore' to restore all the required packages 
- Run 'dotnet ef migrations add InitialCreate' and 'dotnet ef database update' command for database migrations
- Run 'dotnet run' command to run the project
- To test the apis, go to this url http://localhost:port/swagger/index.html
- By default one admin data is seeded after running the project for the first time, so login as an admin to access all the protected apis with these credentials : {username : 'admin', password : 'admin@123'}
- Add the jwt token to http request header in this format : {Bearer <jwt_token>} to access protected api routes


## Database Setup Steps
 
- First import required packages and make sure all the packages are of the same version so that we do not get any version mismatch error.
- Update the .env file regarding the connection strings
- Run dotnet build to make sure there are no errors.
- Run the migration command => dotnet ef migrations add InitialCreate
- Once the migration is completed run this command to make tables in the database => dotnet ef database update
- Once ran successfully, the code and the database are in sync. We can test the connection by using swagger UI, try adding one entry using POST end point and see if it is shown in the datase or not


## EF Core migration commands
 
- dotnet ef migrations add "MigrationName"
- dotnet ef database update

## Login Credentials for testing
 
- username = "admin"
- password = "admin@123"

### JWT Usage Instructions
- In the .env file, change the Key, Issuer and Audience values to change the JWT settings.
 

## Notes 
- This application uses 'PBKDF2 with HMAC-SHA256' to hash the password which is by default provided by ASP.NET core. No external library is being used to hash the password

## Features Completed
Working API endpoints:
- GET /api/health

- GET /api/trainees
- GET /api/trainees/{id}
- POST /api/trainees
- PUT /api/trainees/{id}
- DELETE /api/trainees/{id}

- POST /api/auth/login

- GET /api/mentors
- GET /api/mentors/{id} 
- POST /api/mentors
- PUT /api/mentors/{id}
- DELETE /api/mentors/{id}

- GET /api/learning-tasks
- GET /api/learning-tasks/{id} 
- POST /api/learning-tasks
- PUT /api/learning-tasks/{id}
- DELETE /api/learning-tasks/{id}

- POST   /api/task-assignments
- GET    /api/task-assignments
- GET    /api/task-assignments/{id}
- PUT    /api/task-assignments/{id}/status
 
- POST   /api/submissions
- GET    /api/submissions
- GET    /api/submissions/{id}
 
- POST   /api/reviews
- GET    /api/reviews
- GET    /api/reviews/{id}



## api endpoints and expected response for each api

- POST /api/auth/login
    - Valid login credentials 200 OK
    - Invalid credentials 400 Bad Request

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

- GET /api/mentors
    - 200 OK

- GET /api/mentors/{id} 
    - Valid ID 200 OK
    - Invalid ID 404 Not Found

- POST /api/mentors
    - Valid data 201 Created
    - Invalid data 400 Bad Request

- PUT /api/mentors/{id}
    - Valid ID 200 OK
    - Invalid ID 404 Not Found
    - Invalid request 400 Bad Request

- DELETE /api/mentors/{id}
    - Valid ID 204 No Content
    - Invalid ID 404 Not Found

- GET /api/learning-tasks
    - 200 OK

- GET /api/learning-tasks/{id}
    - Valid ID 200 OK
    - Invalid ID 404 Not Found

- POST /api/learning-tasks
    - Valid data 201 Created
    - Invalid data 400 Bad Request

- PUT /api/learning-tasks/{id}
    - Valid ID 200 OK
    - Invalid ID 404 Not Found
    - Invalid request 400 Bad Request

- DELETE /api/learning-tasks/{id}
    - Valid ID 204 No Content
    - Invalid ID 404 Not Found


- POST  /api/task-assignments
    - Valid data 201 Created
    - Invalid data 400 Bad Request

- GET    /api/task-assignments
    - 200 OK

- GET    /api/task-assignments/{id}
    - Valid ID 200 OK
    - Invalid ID 404 Not Found

- PUT    /api/task-assignments/{id}/status
    - Valid ID 200 OK
    - Invalid ID 404 Not Found
    - Invalid request 400 Bad Request
 
- POST   /api/submissions
    - Valid data 201 Created
    - Invalid data 400 Bad Request

- GET    /api/submissions
    - 200 OK

- GET    /api/submissions/{id}
    - Valid ID 200 OK
    - Invalid ID 404 Not Found
 
- POST   /api/reviews
    - Valid data 201 Created
    - Invalid data 400 Bad Request

- GET    /api/reviews
    - 200 OK

- GET    /api/reviews/{id}
    - Valid ID 200 OK
    - Invalid ID 404 Not Found






## Sample Request JSON

Sample POST /api/auth/login request :

    {
        "username": "admin",
        "password": "admin@123"
    }


Sample POST /api/trainees request:

    {
        "firstName": "john",
        "lastName": "joe",
        "email": "john.joe@training.com",
        "techStack": "HTML, CSS, JavaScript",
        "status": "Active"
    }
 
Sample PUT /api/trainees/{id} request:

    {  
        "firstName": "john",
        "lastName": "cena",
        "email": "john.cena@training.com",
        "techStack": "Java",
        "status": "InActive"
    }


Sample POST /api/mentors request:

    {
        "firstName": "Hiroshi",
        "lastName": "Tanaka",
        "email": "h.tanaka@systems.jp",
        "expertise": "DevOps Engineering",
        "status": "Active"
    }


Sample PUT /api/mentors/{id} request:

    {
        "firstName": "Hiroshi",
        "lastName": "Tanaka",
        "email": "h.tanaka@systems.jp",
        "expertise": "Java Fullstack",
        "status": "Inactive"
    }

 
Sample POST /api/learning-tasks request:

    {
        "title": "frontend ",
        "description": "to complete frontend design",
        "expectedTechStack": "react",
        "dueDate": "2026-06-12",
        "status": "Draft"
    }


Sample PUT /api/learning-tasks/{id} request:

    {
        "title": "frontend",
        "description": "to complete frontend design",
        "expectedTechStack": "react",
        "dueDate": "2026-06-12",
        "status": "Closed"
    }


Sample POST   /api/task-assignments request : 

    {
        "traineeId": 1,
        "mentorId": 1,
        "learningTaskId": 7,
        "assignedDate": "2026-06-16",
        "dueDate": "2026-06-23",
        "status": "Assigned",
        "remarks": "Complete modules 1 through 3 and submit the GitHub repository link before the review meeting."
    }

Sample PUT    /api/task-assignments/{id}/status request :

    {
        "traineeId": 1,
        "mentorId": 1,
        "learningTaskId": 7,
        "assignedDate": "2026-06-16",
        "dueDate": "2026-06-23",
        "status": "Completed",
        "remarks": "Complete modules 1 through 3 and submit the GitHub repository link before the review meeting."
    }
 
Sample POST   /api/submissions request :

    {
        "taskAssignmentId": 5,
        "submissionUrl": "drive",
        "notes": "Finished all primary requirements and included the optional optimization scripts in the repository.",
        "submissionDate": "2026-06-16",
        "status": "Submitted"
    }
 
Sample POST   /api/reviews request :

    {
        "submissionId": 3,
        "mentorId": 7,
        "feedback": "Excellent work on the database indexing. The query performance improved significantly.",
        "score": "95/100",
        "status": "Accepted",
        "reviewedDate": "2026-06-16"
    }




 
## Sample Response JSON

Sample POST /api/auth/login response :

    {
        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJUcmFpbmVlTWFuYWdlbWVudENsaWVudCIsImlzcyI6IlRyYWluZWVNYW5hZ2VtZW50QXBpIiwiZXhwIjoxNzgxMTgwMjM5LCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImlhdCI6MTc4MTE3NjYzOSwibmJmIjoxNzgxMTc2NjM5fQ.30SeusEa2rkDd5-EueaMwXn4uixhwKW4Z4rIADf5Mck",
        "expiresIn": "60",
        "user": {
            "id": 1,
            "username": "admin",
            "role": "Admin"
        }
    }


Sample GET /api/trainees response :

    {
        "pageNumber": 1,
        "pageSize": 5,
        "totalRecords": 17,
        "data": [
            {
                "id": 1,
                "firstName": "akash",
                "lastName": "prajapati",
                "email": "akash@gmail.com",
                "techStack": "java",
                "status": "Active",
                "createdAt": "2026-06-10T11:23:25.692974",
                "updatedAt": "2026-06-10T11:23:25.69299"
            },
            {
                "id": 2,
                "firstName": "Suraj",
                "lastName": "Prajapati",
                "email": "suraj@gmail.com",
                "techStack": "MERN",
                "status": "Inactive",
                "createdAt": "2026-06-11T08:56:56.104701",
                "updatedAt": "2026-06-11T10:35:35.153876"
            },
                {
                "id": 3,
                "firstName": "Priya",
                "lastName": "Sharma",
                "email": "priya.s@example.com",
                "techStack": "DotNet",
                "status": "Active",
                "createdAt": "2026-06-11T08:57:09.270786",
                "updatedAt": "2026-06-11T08:57:09.270787"
            },
            {
                "id": 4,
                "firstName": "Amit",
                "lastName": "Verma",
                "email": "amit.v@example.com",
                "techStack": "Python",
                "status": "Inactive",
                "createdAt": "2026-06-11T08:57:19.07379",
                "updatedAt": "2026-06-11T08:57:19.07379"
            },
            {
                "id": 5,
                "firstName": "Sonia",
                "lastName": "Bose",
                "email": "sonia.b@example.com",
                "techStack": "AWS",
                "status": "Inactive",
                "createdAt": "2026-06-11T08:57:36.226802",
                "updatedAt": "2026-06-11T08:57:36.226803"
            }
        ]
    }

 
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



Sample GET /api/learning-tasks response :

    [
        {
            "id": 1,
            "title": "frontend ",
            "description": "to complete frontend design",
            "expectedTechStack": "react",
            "dueDate": "2026-06-12T00:00:00",
            "status": "Closed",
            "createdDate": "2026-06-12T10:19:58.705764",
            "updatedDate": "2026-06-12T10:26:07.749961"
        },
        {
            "id": 2,
            "title": "frontend design",
            "description": "to complete frontend design",
            "expectedTechStack": "java",
            "dueDate": "2026-06-12T00:00:00",
            "status": "Draft",
            "createdDate": "2026-06-12T10:20:35.893474",
            "updatedDate": "2026-06-12T10:20:35.893474"
        }
    ]


Sample GET /api/learning-tasks/{id} response : 

    {
        "id": 2,
        "title": "frontend design",
        "description": "to complete frontend design",
        "expectedTechStack": "java",
        "dueDate": "2026-06-12T00:00:00",
        "status": "Draft",
        "createdDate": "2026-06-12T10:20:35.893474",
        "updatedDate": "2026-06-12T10:20:35.893474"
    }


Sample POST /api/learning-tasks response :

    {
        "id": 2,
        "title": "frontend design",
        "description": "to complete frontend design",
        "expectedTechStack": "java",
        "dueDate": "2026-06-12T00:00:00",
        "status": "Draft",
        "createdDate": "2026-06-12T10:20:35.893474",
        "updatedDate": "2026-06-12T10:20:35.893474"
    }

Sample /api/learning-tasks/{id} response :

    {
        "id": 2,
        "title": "frontend design",
        "description": "to complete frontend design",
        "expectedTechStack": "java",
        "dueDate": "2026-06-12T00:00:00",
        "status": "Closed",
        "createdDate": "2026-06-12T10:20:35.893474",
        "updatedDate": "2026-06-12T10:20:35.893474"
    }


Sample GET /api/mentors response :

    [
        {
            "id": 8,
            "firstName": "Marcus",
            "lastName": "Chen",
            "email": "m.chen@techcorp.com",
            "expertise": "Cloud Architecture",
            "status": "Active",
            "createdDate": "2026-06-12T08:51:02.126754",
            "updatedDate": "2026-06-12T08:51:02.126754"
        },
        {
            "id": 9,
            "firstName": "Elena",
            "lastName": "Rodriguez",
            "email": "elena.rodriguez@designhub.io",
            "expertise": "UI/UX Design",
            "status": "Active",
            "createdDate": "2026-06-12T08:51:11.363087",
            "updatedDate": "2026-06-12T08:51:11.363087"
        }
    ]


Sample GET /api/mentors/{id} response : 

    {
        "id": 9,
        "firstName": "Elena",
        "lastName": "Rodriguez",
        "email": "elena.rodriguez@designhub.io",
        "expertise": "UI/UX Design",
        "status": "Active",
        "createdDate": "2026-06-12T08:51:11.363087",
        "updatedDate": "2026-06-12T08:51:11.363087"
    }

Sample POST /api/mentors response :

    {
        "id": 9,
        "firstName": "Elena",
        "lastName": "Rodriguez",
        "email": "elena.rodriguez@designhub.io",
        "expertise": "UI/UX Design",
        "status": "Active",
        "createdDate": "2026-06-12T08:51:11.363087",
        "updatedDate": "2026-06-12T08:51:11.363087"
    }



Sample PUT /api/mentors/{id} response :

    {
        "id": 9,
        "firstName": "Elena",
        "lastName": "Rodriguez",
        "email": "elena.rodriguez@designhub.io",
        "expertise": "UI/UX Design",
        "status": "Inactive",
        "createdDate": "2026-06-12T08:51:11.363087",
        "updatedDate": "2026-06-12T08:51:11.363087"
    }



 
Sample GET /api/task-assignment response:

    [
        {
            "id": 1,
            "traineeId": 1,
            "mentorId": 1,
            "learningTaskId": 1,
            "assignedDate": "2026-06-15T10:08:57.407",
            "dueDate": "2026-06-15T10:08:57.407",
            "status": "InProgress",
            "remarks": ""
        }
    ]
 
Sample GET /api/task-assignment/{id} response:

    {
        "id": 1,
        "traineeId": 1,
        "mentorId": 1,
        "learningTaskId": 1,
        "assignedDate": "2026-06-15T10:08:57.407",
        "dueDate": "2026-06-15T10:08:57.407",
        "status": "InProgress",
        "remarks": ""
    }

Sample POST /api/task-assignment response:

    {
        "id": 3,
        "traineeId": 1,
        "mentorId": 9,
        "learningTaskId": 1,
        "assignedDate": "2026-06-16T00:00:00",
        "dueDate": "2026-06-16T00:00:00",
        "status": "InProgress",
        "remarks": "string"
    }
 
Sample PUT /api/task-assignment/{id}/status response:

    {
        "id": 3,
        "traineeId": 1,
        "mentorId": 9,
        "learningTaskId": 1,
        "assignedDate": "2026-06-16T00:00:00",
        "dueDate": "2026-06-16T00:00:00",
        "status": "Completed",
        "remarks": "string"
    }
 
Sample GET /api/submissions response:

    [
        {
            "id": 1,
            "taskAssignmentId": 2,
            "submissionUrl": "drive",
            "notes": "task submitted",
            "submissionDate": "2026-06-15T00:00:00",
            "status": "Submitted"
        },
        {
            "id": 2,
            "taskAssignmentId": 2,
            "submissionUrl": "drive",
            "notes": "task submitted",
            "submissionDate": "2026-06-16T00:00:00",
            "status": "Submitted"
        }
    ]
 
Sample GET /api/submissions/{id} response:

    {
        "id": 1,
        "taskAssignmentId": 2,
        "submissionUrl": "drive",
        "notes": "task submitted",
        "submissionDate": "2026-06-15T00:00:00",
        "status": "Submitted"
    }
 
Sample POST /api/submissions response:

    {
        "id": 1,
        "taskAssignmentId": 2,
        "submissionUrl": "drive",
        "notes": "task submitted",
        "submissionDate": "2026-06-15T00:00:00",
        "status": "Submitted"
    }
 
Sample GET /api/reviews response:

    [
        {
            "id": 1,
            "submissionId": 1,
            "mentorId": 10,
            "feedback": "good",
            "score": "A+",
            "status": "Accepted",
            "reviewedDate": "2026-06-15T00:00:00"
        },
        {
            "id": 2,
            "submissionId": 1,
            "mentorId": 9,
            "feedback": "good",
            "score": "A+",
            "status": "Accepted",
            "reviewedDate": "2026-06-15T00:00:00"
        }
    ]
 
Sample GET /api/reviews/{id} response:

    {
        "id": 2,
        "submissionId": 1,
        "mentorId": 9,
        "feedback": "good",
        "score": "A+",
        "status": "Accepted",
        "reviewedDate": "2026-06-15T00:00:00"
    }
 
Sample POST /api/reviews response:

    {
        "id": 2,
        "submissionId": 1,
        "mentorId": 9,
        "feedback": "good",
        "score": "A+",
        "status": "Accepted",
        "reviewedDate": "2026-06-15T00:00:00"
    }
 




## Challanges faced
- While installing dotnet packages and setting up initial web project in dotnet
- While installing and setting up swagger in the current project for testing apis
- While establishing connection to mysql database and running database migration command


## Limitations
- Absence of role based authorisation 
- No email verification
- No file upload support for submissions
 
## Security Checklist
 
 
## Next Improvement areas
- Integration with frontend