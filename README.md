# WorkNet Job Portal API

WorkNet is a job portal API designed to streamline the process of connecting employers with job seekers. The API is built using .NET 8, emphasizing clean architecture and scalability.

---

## Features

- **Layered Architecture**: Follows a structured, modular approach with distinct layers (Main, BLL, DAL) for separation of concerns.
- **Repository Pattern**: Implements the repository pattern for better maintainability and testability of data access logic.
- **Asynchronous Programming**: Utilizes `async`/`await` to ensure optimal performance and scalability.
- **JWT Authentication**: Securely authenticates and authorizes users with JSON Web Tokens (JWT), including role-based authorization.
- **Secure Password Hashing**: Passwords are hashed using `bcrypt` to ensure secure storage.
- **Global Exception Handling**: Centralized exception management using `IExceptionHandler` for consistent error responses. (need improvements)
- **Logging**: Integrated `ILogger` for application monitoring and debugging.

---

## Technologies Used

- **Framework**: .NET 8
- **Database**: SQL Server (Data-first approach)
- **Authentication**: JWT (JSON Web Tokens)
- **Hashing**: BCrypt
- **Dependency Injection**: Built-in .NET DI
- **Mapping**: AutoMapper
- **Containerization**: Docker and Docker Compose

---

## Prerequisites

- .NET 8 SDK
- Docker (if running in containers)
- SQL Server
- Postman or similar API testing tool (optional)

---

## Using or Testing the API from Docker Hub

You can quickly test the WorkNet API by pulling the pre-built Docker image from Docker Hub and running it with the provided `docker-compose.yml` file.

---

### 1. Download the `docker-compose.yml` File
- Obtain the `docker-compose.yml` file from this repository or download it directly:

### 2. Pull and Run the Image from Docker Hub
- docker-compose up

### 3. Access the API
- Base URL: http://localhost:8001 (or the port specified in the docker-compose.yml file)
- Use tools like Postman or Swagger to interact with the API.
