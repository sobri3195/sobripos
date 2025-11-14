# SOBRIPOS Architecture Documentation

## Overview

SOBRIPOS menggunakan **Clean Architecture** (juga dikenal sebagai Onion Architecture atau Hexagonal Architecture) untuk memastikan:
- Separation of Concerns
- Dependency Inversion
- Testability
- Maintainability
- Independence from frameworks, UI, database

## Clean Architecture Layers

```
┌─────────────────────────────────────────────────┐
│                                                 │
│             Presentation Layer                  │
│    (API, Web, Desktop - User Interface)        │
│                                                 │
└────────────────┬────────────────────────────────┘
                 │
                 ▼
┌─────────────────────────────────────────────────┐
│                                                 │
│            Application Layer                    │
│   (Interfaces, DTOs, Business Logic)           │
│                                                 │
└────────────────┬────────────────────────────────┘
                 │
                 ▼
┌─────────────────────────────────────────────────┐
│                                                 │
│               Core Layer                        │
│         (Entities, Enums, Rules)               │
│                                                 │
└─────────────────────────────────────────────────┘
                 ▲
                 │
┌────────────────┴────────────────────────────────┐
│                                                 │
│          Infrastructure Layer                   │
│   (Data Access, EF Core, Repositories)         │
│                                                 │
└─────────────────────────────────────────────────┘
```

## Project Dependencies

```
┌─────────────────────────────────────────────────────────────┐
│                    Dependency Flow                          │
└─────────────────────────────────────────────────────────────┘

SOBRIPOS.API ──────────┐
                       │
SOBRIPOS.Web ──────────┼───────► SOBRIPOS.Data ─────┐
                       │                             │
SOBRIPOS.Desktop ──────┘                             │
                                                     ▼
                                          SOBRIPOS.Application
                                                     │
                                                     ▼
                                             SOBRIPOS.Core
```

### Dependency Rules

1. **Core Layer** tidak bergantung pada layer lain (fully independent)
2. **Application Layer** hanya bergantung pada Core Layer
3. **Infrastructure Layer** bergantung pada Core dan Application Layer
4. **Presentation Layer** bergantung pada Application dan Infrastructure Layer

## Layer Details

### 1. Core Layer (Domain)

#### SOBRIPOS.Core
- **Location**: `src/Core/SOBRIPOS.Core`
- **Purpose**: Berisi business entities dan domain logic
- **Dependencies**: None (fully independent)
- **Contents**:
  - `Entities/`: Domain entities (Product, Category, Transaction, User, etc.)
  - `Enums/`: Business enums (UserRole, PaymentMethod, TransactionStatus, etc.)

**Karakteristik**:
- Tidak bergantung pada framework atau library eksternal
- Pure C# classes
- Business rules dan validations
- Paling stabil, jarang berubah

### 2. Application Layer

#### SOBRIPOS.Application
- **Location**: `src/Core/SOBRIPOS.Application`
- **Purpose**: Use cases, business logic, dan interfaces
- **Dependencies**: SOBRIPOS.Core
- **Contents**:
  - `Interfaces/`: Repository interfaces, service interfaces
  - `DTOs/`: Data Transfer Objects
  - `Services/`: Business logic services
  - `Validators/`: Input validation

**Karakteristik**:
- Defines contracts (interfaces) untuk infrastructure
- Use case orchestration
- Business logic yang tidak masuk ke entities
- Dependency Inversion: defines interfaces, infrastructure implements

### 3. Infrastructure Layer

#### SOBRIPOS.Data
- **Location**: `src/Infrastructure/SOBRIPOS.Data`
- **Purpose**: Data access dan persistence
- **Dependencies**: SOBRIPOS.Core, SOBRIPOS.Application
- **Contents**:
  - `Contexts/`: DbContext (Entity Framework Core)
  - `Repositories/`: Repository implementations
  - `Configurations/`: EF Core entity configurations
  - `Migrations/`: Database migrations
  - `UnitOfWork.cs`: Unit of Work pattern implementation

**Karakteristik**:
- Implements interfaces dari Application layer
- Database-specific code
- ORM configurations
- External service integrations

### 4. Presentation Layer

#### SOBRIPOS.API
- **Location**: `src/Presentation/SOBRIPOS.API`
- **Purpose**: RESTful API backend
- **Dependencies**: SOBRIPOS.Core, SOBRIPOS.Application, SOBRIPOS.Data
- **Contents**:
  - `Controllers/`: API endpoints
  - `Program.cs`: Application startup & DI configuration
  - `appsettings.json`: Configuration

**Features**:
- RESTful endpoints
- Swagger/OpenAPI documentation
- CORS support
- JWT authentication (planned)

#### SOBRIPOS.Web
- **Location**: `src/Presentation/SOBRIPOS.Web`
- **Purpose**: Blazor web admin dashboard
- **Dependencies**: SOBRIPOS.Core, SOBRIPOS.Application, SOBRIPOS.Data
- **Contents**:
  - `Components/`: Blazor components
  - `Pages/`: Blazor pages
  - `Services/`: UI services

**Features**:
- Admin dashboard
- Reporting interface
- User management
- Configuration

#### SOBRIPOS.Desktop
- **Location**: `src/Presentation/SOBRIPOS.Desktop`
- **Purpose**: Desktop POS client
- **Dependencies**: SOBRIPOS.Core, SOBRIPOS.Application, SOBRIPOS.Data
- **Target**: Console app (future: WPF/MAUI)

**Features** (Planned):
- Cashier interface
- Quick sales processing
- Barcode scanning
- Receipt printing
- Offline mode

## Design Patterns

### 1. Repository Pattern
```csharp
// Interface di Application Layer
public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    // ... more methods
}

// Implementation di Infrastructure Layer
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    // EF Core implementation
}
```

### 2. Unit of Work Pattern
```csharp
public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    ICategoryRepository Categories { get; }
    // ... other repositories
    Task<int> SaveChangesAsync();
}
```

### 3. Dependency Injection
```csharp
// Program.cs
builder.Services.AddDbContext<ApplicationDbContext>(options => ...);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
```

## Data Flow Example

### Creating a Product

```
┌──────────┐      ┌────────────┐      ┌─────────────┐      ┌──────────┐
│  Client  │      │    API     │      │  Repository │      │ Database │
└────┬─────┘      └─────┬──────┘      └──────┬──────┘      └────┬─────┘
     │                  │                     │                   │
     │ POST /products   │                     │                   │
     ├─────────────────►│                     │                   │
     │                  │  AddAsync()         │                   │
     │                  ├────────────────────►│                   │
     │                  │                     │  INSERT           │
     │                  │                     ├──────────────────►│
     │                  │                     │                   │
     │                  │  SaveChangesAsync() │                   │
     │                  ├────────────────────►│                   │
     │                  │                     │  COMMIT           │
     │                  │                     ├──────────────────►│
     │                  │                     │                   │
     │     201 Created  │                     │                   │
     │◄─────────────────┤                     │                   │
     │                  │                     │                   │
```

## Database Schema

### Entity Relationships

```
┌──────────────┐       ┌──────────────┐
│   Category   │       │   Supplier   │
└──────┬───────┘       └──────┬───────┘
       │                      │
       │ 1:N                  │ 1:N
       │                      │
┌──────▼───────┐       ┌──────▼───────┐
│   Product    │       │   Purchase   │
└──────┬───────┘       └──────┬───────┘
       │                      │
       │ 1:N                  │ 1:N
       │                      │
┌──────▼───────┐       ┌──────▼────────┐
│TransactionItem│      │ PurchaseItem  │
└──────┬───────┘       └───────────────┘
       │
       │ N:1
       │
┌──────▼───────┐
│ Transaction  │
└──────┬───────┘
       │
       ├─────────────┐
       │ N:1         │ N:1
       │             │
┌──────▼───────┐ ┌───▼────────┐
│     User     │ │  Customer  │
│  (Cashier)   │ │            │
└──────────────┘ └────────────┘
```

## Security Considerations

### 1. Authentication & Authorization
- JWT tokens untuk API authentication
- Role-based access control (Admin, Supervisor, Cashier)
- Password hashing dengan BCrypt

### 2. Data Protection
- Soft delete untuk semua entities (IsDeleted flag)
- Audit fields (CreatedAt, UpdatedAt)
- User activity logging

### 3. Input Validation
- DTOs untuk request validation
- FluentValidation (planned)
- SQL injection protection via EF Core

## Testing Strategy

```
┌────────────────────────────────────────┐
│           Unit Tests                   │
│  - Domain entities                     │
│  - Business logic                      │
│  - Repositories (with InMemory DB)     │
└────────────────────────────────────────┘

┌────────────────────────────────────────┐
│        Integration Tests               │
│  - API endpoints                       │
│  - Database operations                 │
│  - Service layer                       │
└────────────────────────────────────────┘

┌────────────────────────────────────────┐
│          E2E Tests                     │
│  - Complete user workflows             │
│  - UI interactions                     │
└────────────────────────────────────────┘
```

## Deployment Architecture

### Option 1: Monolithic Deployment
```
┌────────────────────────────┐
│      Single Server         │
│                            │
│  ┌──────────────────────┐  │
│  │  SOBRIPOS.API        │  │
│  │  (+ Static Files)    │  │
│  └──────────────────────┘  │
│                            │
│  ┌──────────────────────┐  │
│  │   SQL Server/        │  │
│  │   PostgreSQL         │  │
│  └──────────────────────┘  │
└────────────────────────────┘
```

### Option 2: Distributed Deployment
```
┌──────────────┐    ┌──────────────┐    ┌──────────────┐
│  Web Server  │    │  API Server  │    │   Database   │
│              │    │              │    │              │
│ SOBRIPOS.Web │───►│ SOBRIPOS.API │───►│  SQL Server  │
│              │    │              │    │              │
└──────────────┘    └──────────────┘    └──────────────┘
       ▲
       │
┌──────┴───────┐
│   Desktop    │
│   Clients    │
└──────────────┘
```

## Best Practices

1. **Follow SOLID Principles**
   - Single Responsibility
   - Open/Closed
   - Liskov Substitution
   - Interface Segregation
   - Dependency Inversion

2. **Keep Core Clean**
   - No external dependencies
   - Pure domain logic
   - Framework agnostic

3. **Use Dependency Injection**
   - Constructor injection
   - Interface-based dependencies
   - Scoped lifetimes for repositories

4. **Async All The Way**
   - Use async/await pattern
   - Avoid blocking calls
   - Proper cancellation token usage

5. **Error Handling**
   - Use exceptions for exceptional cases
   - Return null/empty for "not found"
   - Proper logging

## Future Enhancements

1. **CQRS Pattern** - Separate read and write operations
2. **Event Sourcing** - Track all state changes
3. **Mediator Pattern** - Decouple requests from handlers
4. **Message Queue** - Async processing with RabbitMQ/Azure Service Bus
5. **Microservices** - Split into smaller services
6. **API Gateway** - Centralized entry point
7. **Caching** - Redis for performance
8. **CDN** - Static file delivery

---

**Document Version**: 1.0  
**Last Updated**: 2025-11-14
