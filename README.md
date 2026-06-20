# WoodRoute Platform

## Project Overview

The `WoodRoute Platform` is the backend application for **WoodRoute**, a SaaS solution that helps
independent carpenters and small carpentry workshops manage custom furniture orders, quotes,
payments, production planning, inventory, and customer communication.

Built with **.NET 10** and **ASP.NET Core**, it adheres to **Domain-Driven Design (DDD)** principles
and implements **Command Query Responsibility Segregation (CQRS)**, promoting modularity,
maintainability, and testability. The platform is structured around distinct **Bounded Contexts**,
ensuring a clear separation of concerns and enabling independent development of core functionalities.

## Table of Contents

1. Architecture Overview
2. Domain-Driven Design (DDD) Concepts
3. Key Features & Best Practices Implemented
4. Bounded Contexts
   * Sales (Cotización y Venta)
   * Inventory (Inventario)
   * Onboarding — IAM & Profiles
   * Manufacturing (Producción)
   * Engagement (Seguimiento y Comunicación)
5. Technologies Used
6. Getting Started
   * Prerequisites
   * Setup Instructions
7. Project Structure
8. API Endpoints
9. Versioning & Git Flow
10. Documentation
11. License

## Architecture Overview

The architecture is driven by **Domain-Driven Design (DDD)** and implements **CQRS**, organizing the
codebase to align closely with the business domain and separating operations that change state
(Commands) from operations that read state (Queries).

* **Domain Layer**: Contains the core business logic — entities, aggregates, value objects, domain
  events, and domain errors. It is the heart of the application and has no dependencies on other layers.
* **Application Layer**: Orchestrates domain objects to fulfill use cases. It defines command services,
  query services, and event handlers. It depends on the Domain layer.
* **Infrastructure Layer**: Provides implementations for the contracts defined in the Domain and
  Application layers (e.g. Entity Framework Core repositories, the database context, interceptors).
* **Interfaces Layer (Presentation)**: Handles external communication through REST APIs. It defines
  controllers, API resources (DTOs), and assemblers that transform between API models and
  application/domain models.

## Domain-Driven Design (DDD) Concepts

* **Bounded Contexts**: The application is explicitly divided into `Sales`, `Inventory`, `Iam`,
  `Profiles`, `Manufacturing`, and `Engagement`, each with its own ubiquitous language and domain model.
* **Aggregates**: Key domain objects such as `Order` (in Sales) act as aggregate roots, encapsulating a
  cluster of entities and value objects and guaranteeing transactional consistency within their
  boundaries. The `Order` aggregate, for example, owns its `Quote` and `Payment` entities.
* **Entities**: Objects with a distinct identity that runs through time (e.g. `Quote`, `Payment`).
* **Value Objects**: Immutable objects compared by their values, not their identity (e.g.
  `FurnitureDetails`, and enum value objects like `EOrderStatus`, `EQuoteStatus`, `EPaymentStatus`).
* **Domain Events**: Aggregates raise domain events (via the Shared kernel `IDomainEvent` /
  `IDomainEventHandler` contracts) to communicate meaningful changes without coupling contexts.

## Key Features & Best Practices Implemented

* **CQRS**: Commands update data; Queries read data. This separation enables independent optimization of
  read and write models.
* **Cancellation Tokens**: Propagated across all asynchronous operations (application services,
  repositories, controllers) for graceful cancellation and better resource management.
* **`Result<T>` Pattern**: Application services return an explicit success/failure result instead of
  throwing exceptions for control flow.
* **Domain-Specific Error Catalog**: Strongly-typed error definitions (e.g. `SalesErrors`) provide
  clear, categorized error codes for business-rule violations.
* **RFC 7807 Problem Details**: API error responses adhere to this standard via dedicated
  `ActionResultAssembler` classes that map domain errors to the correct HTTP status code.
* **Global Exception Handling Middleware**: Catches unhandled exceptions and returns standardized
  `ProblemDetails` responses, preventing sensitive information leakage.
* **Thin Controllers + Assemblers**: Controllers stay lean; static assemblers transform resources to
  commands and entities to resources, centralizing mapping and response logic.
* **Auditable Entities**: An EF Core interceptor automatically maintains `createdAt` / `updatedAt`
  timestamps for entities implementing `IAuditableEntity`.
* **Persistence**: Entity Framework Core over **PostgreSQL**, with snake_case + pluralized table naming
  conventions applied through `Humanizer`.

## Bounded Contexts

The application is logically divided into the following Bounded Contexts. The current implementation
status is indicated for each.

### Sales (Cotización y Venta) — ✅ Implemented

Manages the full lifecycle of a custom furniture order, from creation through quotation, acceptance,
payment, and final resolution.

* **Scope**: Order management, quote generation and acceptance, and payment registration/validation.
* **Aggregate**: `Order` (owns `Quote` and `Payment` entities).
* **Value Objects**: `FurnitureDetails`, `EOrderStatus`, `EQuoteStatus`, `EPaymentStatus`, `EPaymentType`.
* **Key Features**:
  * Order creation with furniture details and public tracking identifier.
  * Order lifecycle transitions: accept, reject, cancel, modify.
  * Quote generation and acceptance.
  * Payment registration and validation (confirm / reject).
  * Queries by id, public tracking id, customer, and carpenter.
* **API Endpoints**: `/api/v1/orders`

### Inventory (Inventario) — 🟡 In Progress

Material and stock management, low-stock detection, and automatic purchase-order generation to
suppliers.

* **Scope**: Materials catalog, stock control with a minimum threshold, and replenishment.
* **Aggregate**: `InventoryMaterial`.
* **Status**: Domain layer in progress; application, infrastructure, and REST layers pending.

### Onboarding — IAM & Profiles — ⏳ Planned

* **IAM**: User authentication, registration, JWT issuance/validation, and password hashing.
* **Profiles**: Personal and contact information for customers and carpenters.

### Manufacturing (Producción) — ⏳ Planned

Production-stage planning, progress tracking, and material-consumption logging linked to Inventory.

### Engagement (Seguimiento y Comunicación) — ⏳ Planned

In-app messaging with customers, post-delivery reviews, and notification orchestration.

## Technologies Used

* **.NET 10**: Core framework for the application.
* **ASP.NET Core**: For building RESTful APIs.
* **Entity Framework Core 10**: ORM for database interactions.
* **PostgreSQL**: Relational database (via `Npgsql.EntityFrameworkCore.PostgreSQL`).
* **Swashbuckle.AspNetCore**: For OpenAPI/Swagger documentation.
* **Humanizer**: For naming conventions (snake_case and pluralized table names).

## Getting Started

### Prerequisites

* .NET 10 SDK
* PostgreSQL Server (or Docker for local development)
* Git

### Setup Instructions

1. **Clone the repository:**
   ```bash
   git clone git@github.com:Developer-Core/wood-route-platform.git
   cd wood-route-platform
   ```

2. **Restore NuGet packages:**
   ```bash
   dotnet restore
   ```

3. **Configure the database:**
   * Ensure your PostgreSQL server is running.
   * Update the `DefaultConnection` string in
     `DeveloperCore.WoodRoute.Platform/appsettings.json`
     (and `appsettings.Development.json`) to point to your PostgreSQL instance.
   * Apply the migrations:
     ```bash
     dotnet ef database update --project DeveloperCore.WoodRoute.Platform
     ```

4. **Run the application:**
   ```bash
   dotnet run --project DeveloperCore.WoodRoute.Platform
   ```

5. **Access Swagger UI:**
   Open your browser and navigate to `https://localhost:7000/swagger` (or the port reported on
   startup) to explore the API endpoints.

## Project Structure

The project is organized by **Bounded Contexts** at the top level, with each context further
decomposed into architectural layers:

```
DeveloperCore.WoodRoute.Platform/
├── Sales/                   # Sales Bounded Context (Order, Quote, Payment)
│   ├── Application/         # Command/Query Services, contracts and internal implementations
│   ├── Domain/              # Aggregates, Entities, Value Objects, Commands, Queries, Events, Errors
│   ├── Infrastructure/      # EF Core repositories and persistence configuration
│   └── Interfaces/          # REST Controllers, Resources, Assemblers
├── Inventory/               # Inventory Bounded Context (Materials, stock, purchase orders)
├── Iam/                     # Identity & Access Management (planned)
├── Profiles/                # User profiles — customers and carpenters (planned)
├── Manufacturing/           # Production planning and tracking (planned)
├── Engagement/              # Messaging, reviews, notifications (planned)
├── Shared/                  # Cross-cutting concerns
│   ├── Application/         # Generic Result<T>, common application models
│   ├── Domain/              # AggregateRoot, ValueObject, IDomainEvent, base repositories
│   └── Infrastructure/      # AppDbContext, base repository, UnitOfWork, interceptors, middleware
├── Program.cs               # Application startup and DI configuration
├── appsettings.json         # Configuration files
└── DeveloperCore.WoodRoute.Platform.csproj
```

## API Endpoints

The **Sales** context currently exposes the following endpoints under `/api/v1/orders`:

| Method  | Route                                          | Description                       |
| ------- | ---------------------------------------------- | --------------------------------- |
| `POST`  | `/api/v1/orders`                               | Create a new order                |
| `GET`   | `/api/v1/orders`                               | List orders (filterable)          |
| `GET`   | `/api/v1/orders/{orderId}`                     | Get an order by id                |
| `PATCH` | `/api/v1/orders/{orderId}`                     | Modify an order                   |
| `PATCH` | `/api/v1/orders/{orderId}/accept`              | Accept an order                   |
| `PATCH` | `/api/v1/orders/{orderId}/reject`              | Reject an order                   |
| `PATCH` | `/api/v1/orders/{orderId}/cancel`              | Cancel an order                   |
| `POST`  | `/api/v1/orders/{orderId}/quote`               | Generate a quote for an order     |
| `PATCH` | `/api/v1/orders/{orderId}/quote/accept`        | Accept the order's quote          |
| `POST`  | `/api/v1/orders/{orderId}/payments`            | Register a payment                |
| `PATCH` | `/api/v1/orders/{orderId}/payments/{paymentId}/validate` | Validate (confirm/reject) a payment |

## Versioning & Git Flow

This repository follows **Git Flow** and **Semantic Versioning (SemVer 2.0.0)**.

* **`main`**: Production-ready, tagged releases only.
* **`develop`**: Integration branch for completed features.
* **`feature/*`**: One branch per change; merged into `develop` through a Pull Request.

Each delivered feature is merged to `develop`, promoted to `main`, and tagged with its version.
Commits follow the **Conventional Commits** specification (`feat`, `fix`, `build`, `refactor`,
`docs`, `chore`).

| Tag     | Milestone                                                     |
| ------- | ------------------------------------------------------------ |
| `0.1.0` | DDD base structure and Shared kernel                          |
| `0.1.1` | Inventory domain (initial)                                    |
| `0.2.0` | Migration to .NET 10 and Sales order management API           |
| `0.3.0` | Sales quote and payment endpoints (Sales bounded context complete) |

## Documentation

Architecture and design artifacts (C4 model, class diagrams per bounded context, EventStorming, and
the product specification) are maintained in the project documentation repository, including:

* **System Architecture (C4)**: `architecture/workspace.dsl`
* **Class Diagrams**: `architecture/class-diagrams/`
* **Product Specification & Sprints**: `README.md`

## License

This project is licensed under the **MIT License**.
