# EOP Project

## Overview

This project leverages the Clean Architecture and CQRS (Command Query Responsibility Segregation) patterns to ensure a scalable, maintainable, and robust solution.

## Table of Contents

- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Features](#features)
- [Getting Started](#getting-started)
- [Running the Application](#running-the-application)
- [Testing](#testing)
- [Contributing](#contributing)
- [License](#license)

## Tech Stack

- **Backend:** .NET 6 API
- **Design Patterns:** Clean Architecture, CQRS

## Architecture

### Clean Architecture

The project follows the Clean Architecture principles to separate concerns and ensure a decoupled, testable, and maintainable codebase. The key layers in this architecture are:

- **API Layer:** Contains the API controllers and models.
- **Application Layer:** Contains the business logic, use cases, and CQRS handlers.
- **Domain Layer:** Contains the core entities and interfaces.
- **Infrastructure Layer:** Contains implementations for database access, external services, and other infrastructure concerns.

### CQRS (Command Query Responsibility Segregation)

CQRS is used to separate read and write operations, providing a clear distinction between commands (which modify state) and queries (which read state).

## Features

- **Modular Design:** The project is designed with modularity in mind, allowing easy addition and modification of features.
- **Scalability:** Clean Architecture and CQRS ensure that the system can scale horizontally and vertically as needed.
- **Maintainability:** The separation of concerns makes the codebase easy to maintain and extend.

## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/jlawas06/eop-clean-architecture.git
    cd EOP
    ```

2. Set up the database:

    - Create a new database in SQL Server.
    - Update the connection string in `appsettings.json`.

3. Restore dependencies and build the solution:

    ```bash
    dotnet restore
    dotnet build
    ```

## Running the Application

1. Start the application:

    ```bash
    dotnet run --project EOP.Api/EOP.Api.csproj
    ```

2. The API will be available at `http://localhost:5282`.

## Testing

Run the tests using the following command:

```bash
dotnet test
