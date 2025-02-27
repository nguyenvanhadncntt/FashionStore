# FashionStore Web Application

FashionStore is a web application built using ASP.NET Core and Blazor WebAssembly. It allows users to browse products, manage orders, and perform administrative tasks such as managing brands and products. The application uses ASP.NET Identity for authentication and authorization, and it integrates with SQL Server for data storage.

---

## Table of Contents

1. [Features](#features)
2. [Technologies Used](#technologies-used)
3. [Project Structure](#project-structure)
4. [Setup Instructions](#setup-instructions)
5. [Database Configuration](#database-configuration)
6. [Running the Application](#running-the-application)
7. [Testing](#testing)
8. [Contributing](#contributing)
9. [License](#license)

---

## Features

- **User Authentication**:
  - Secure login using ASP.NET Identity.
  - JWT-based authentication with cookies for API requests.

- **Product Management**:
  - Admin can add, update, and delete products.
  - Users can view product details and place orders.

- **Order Management**:
  - Users can view their orders.
  - Admin can view all orders and update order statuses.

- **Brand Management**:
  - Admin can manage brands (add, update, delete).

- **User Management**:
  - Admin can manage users (add, update, delete).

- **Category Management**:
  - Admin can manage category (add, update, delete).

- **Role-Based Authorization**:
  - Roles: `Admin` and `User`.
  - Admin has access to backend management features.

---

## Technologies Used

- **Backend**:
  - ASP.NET Core
  - Entity Framework Core
  - SQL Server

- **Frontend**:
  - Blazor WebAssembly
  - Bootstrap for UI components

- **Authentication**:
  - ASP.NET Identity
  - JWT (JSON Web Tokens)

- **Testing**:
  - xUnit for integration tests
  - Moq for mocking dependencies

- **Other Tools**:
  - Swagger for API documentation
  - EF Core Migrations for database schema management

---

## Project Structure

The project is divided into the following layers:

1. **FashionStore.WebApp**:
   - Frontend application built with Blazor WebAssembly.
   - Contains Razor components, services, and HTTP clients.

2. **FashionStore.Api**:
   - Backend API built with ASP.NET Core.
   - Handles business logic, database interactions, and authentication.

3. **FashionStore.Data**:
   - Contains the `ApplicationDbContext` and Entity Framework models.
   - Includes migrations and seeding logic.

4. **FashionStore.Tests**:
   - Integration tests for controllers and services.
   - Uses `WebApplicationFactory` for test setup.

---

## Setup Instructions

### Prerequisites

- [.NET SDK 9 or later](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB or full SQL Server instance)
- Visual Studio or Visual Studio Code

### Clone the Repository

```bash
git clone https://github.com/nguyenvanhadncntt/FashionStore.git
cd FashionStore
```

### Database Configuration
1. Update the connection string in `appsettings.json`

```
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=FashionStoreDB;Trusted_Connection=True;"
}
```

2. Apply migrations to create the database schema:
```
dotnet ef database update
```
