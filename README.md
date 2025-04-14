--Restore DB

--Use this to locate files
RESTORE FILELISTONLY 
FROM DISK = 'c:\Users\Admin\Downloads\AdventureWorks2022.bak' 

--use the path from above result below
RESTORE DATABASE [AdventureWorks2022]
FROM DISK = 'c:\Users\Admin\Downloads\AdventureWorks2022.bak'
WITH 
    MOVE 'AdventureWorks2022' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\AdventureWorks2022.mdf',
    MOVE 'AdventureWorks2022_Log' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\AdventureWorks2022_log.ldf',
    REPLACE;
GO

--To sign out all users
USE master;
GO
ALTER DATABASE <DatabaseName> SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

----------------------------------------------------------------------------------------------------

@workspace 
I want to create a project that will be a combination of Vertical slice and Microservice architecture I have already created base solution with .net aspire concept I am trying to use the above mentioned architecture concepts. I want the following things How should I define my project structure to support that architecture I want to use SQL Server as backend and .net core entity framework for database connections I want to use CQRS pattern as well I need to use Redis cache (hybrid cache if possible) I want to support OAuth 2.0 authentication I would like to use Outbox design pattern for event handling I would like to use Fluent validation for validations Suggest me a project structure and base code for that

/Adventure.sln
|
|-- src/
|   |-- Adventure.AppHost/            # Aspire Orchestrator (You already have this)
|   |   |-- Program.cs
|   |   |-- Adventure.AppHost.csproj
|   |
|   |-- Adventure.ServiceDefaults/    # Common Aspire configurations (Health checks, Telemetry, etc.)
|   |   |-- Extensions.cs
|   |   |-- Adventure.ServiceDefaults.csproj
|   |
|   |-- BuildingBlocks/               # Shared libraries (use sparingly!)
|   |   |-- Adventure.BuildingBlocks.Core/          # Core abstractions (IUnitOfWork, Domain Events, Base Entity, etc.)
|   |   |-- Adventure.BuildingBlocks.Persistence.EfCore/ # EF Core helpers, Outbox implementation, Base Repository
|   |   |-- Adventure.BuildingBlocks.Messaging/       # Message bus abstractions (for Outbox publisher)
|   |   |-- Adventure.BuildingBlocks.Caching/         # Caching abstractions (IHybridCache)
|   |   |-- Adventure.BuildingBlocks.Authentication/  # Common Auth helpers (e.g., JWT configuration extensions)
|   |
|   |-- Services/                     # Microservices root
|   |   |-- Ordering/                 # Example: Ordering Microservice
|   |   |   |-- Adventure.Ordering.Api/ # The API project for Ordering
|   |   |   |   |-- Program.cs
|   |   |   |   |-- appsettings.json
|   |   |   |   |-- Features/         # VERTICAL SLICES live here
|   |   |   |   |   |-- CreateOrder/
|   |   |   |   |   |   |-- CreateOrderCommand.cs
|   |   |   |   |   |   |-- CreateOrderHandler.cs
|   |   |   |   |   |   |-- CreateOrderValidator.cs
|   |   |   |   |   |   |-- CreateOrderEndpoint.cs  # (Minimal API endpoint definition)
|   |   |   |   |   |-- GetOrderById/
|   |   |   |   |   |   |-- GetOrderByIdQuery.cs
|   |   |   |   |   |   |-- GetOrderByIdHandler.cs
|   |   |   |   |   |   |-- OrderDto.cs
|   |   |   |   |   |   |-- GetOrderByIdEndpoint.cs
|   |   |   |   |   |-- ... (other features)
|   |   |   |   |-- Domain/           # Ordering specific domain models
|   |   |   |   |   |-- Aggregates/
|   |   |   |   |   |   |-- Order.cs
|   |   |   |   |   |   |-- OrderItem.cs
|   |   |   |   |   |-- Events/
|   |   |   |   |   |   |-- OrderCreatedDomainEvent.cs
|   |   |   |   |-- Infrastructure/   # Implementation details for Ordering
|   |   |   |   |   |-- Persistence/
|   |   |   |   |   |   |-- OrderingDbContext.cs
|   |   |   |   |   |   |-- Migrations/
|   |   |   |   |   |   |-- Configurations/ # EF Core Entity Configurations
|   |   |   |   |   |   |   |-- OrderConfiguration.cs
|   |   |   |   |   |   |-- Repositories/   # (Optional if using generic repository)
|   |   |   |   |   |-- Caching/        # Specific caching logic/keys for Ordering
|   |   |   |   |   |-- IntegrationEventHandlers/ # Handlers for events from OTHER services
|   |   |   |   |-- Configuration/    # Service registration extensions
|   |   |   |   |   |-- DependencyInjection.cs
|   |   |   |   |-- Adventure.Ordering.Api.csproj
|   |   |
|   |   |-- Catalog/                  # Example: Catalog Microservice (similar structure)
|   |   |   |-- Adventure.Catalog.Api/
|   |   |   |   |-- ... (Features, Domain, Infrastructure, etc.)
|   |   |   |   |-- Adventure.Catalog.Api.csproj
|   |   |
|   |   |-- Identity/                 # Example: Identity Microservice (could host IdentityServer/OpenIddict)
|   |   |   |-- Adventure.Identity.Api/
|   |   |   |   |-- ...
|   |   |   |   |-- Adventure.Identity.Api.csproj
|   |
|-- tests/
|   |-- Adventure.Ordering.UnitTests/
|   |-- Adventure.Ordering.IntegrationTests/
|   |-- Adventure.Catalog.UnitTests/
|   |-- Adventure.Catalog.IntegrationTests/
|   |-- Adventure.BuildingBlocks.UnitTests/ # (If applicable)

----------------------------------------------------------------------------------------------------
Service Discovery

Add Adventure.ServiceDefaults reference to targeted api projects
Then, in the api project, do the following
builder.AddServiceDefaults();
app.MapDefaultEndPoints();
----------------------------------------------------------------------------------------------------
Person
Production
Purchase
Sales
Warehouse
Finance
HumanResource

----------------------------------------------------------------------------------------------------
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools

dotnet ef dbcontext scaffold "data source=CIQLAP139\SQL2022_INSTANCE;initial catalog=AdventureWorks2022;user id=sa;password=cloudiq@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Entities --context-dir Persistence --context AdventureWorksDbContext --no-onconfiguring
----------------------------------------------------------------------------------------------------