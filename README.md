# VS Example

## Introduction
This is a simple project showing how a modular monolithic .NET MVC Application can be built using various layers.  It also makes use of vertical slices to implement functionality  The projects in the solution are as follows:

## src

Contains all source code for the application.

### Application
The application layer.  This project is arranged by Features/Modules.  We use a CQRS-like pattern to perform:
- **Queries** - queries into the domain logic to obtain data but not change the state of the system.
- **Commands** - commands into the domain logic to mutate/change/create data.
- **Notifications** - notifications/events for when something in the domain has changed.

This project depends upon the **Infrastructure** and **Domain** projects and the following nuget packages:

- [FluentResults](https://github.com/altmann/FluentResults) - for its `Result` object.
- [FluentValidation](https://docs.fluentvalidation.net/en/latest/installation.html) for validation of queries and commands
- [MediatR](https://github.com/jbogard/MediatR) - for Queries, Commannds and Notifications

### Domain

Houses our entity, event and value objects.  This project does not depend upon any other projects.

This project depends upon:

- [MediatR](https://github.com/jbogard/MediatR) - for Notifications 

### Infrastructure

Serves as the infrastructure layer of the application.  This houses:

- **Persistence** - how our entities are created, retrieved and stored
- **Logging** - how our application logs messages.

plus any other technical capabilities we may need (caching, clients to external apis, telemetry).  This project only depends upon our Domain layer.

This project makes use of:

- [Entity Framework](https://learn.microsoft.com/en-us/ef/) for domain entity persistence
- [Serilog](https://serilog.net/) for application logging.

### Web
A .NET MVC application that depends upon the Application and Infrastructure projects.

## test

### Unit

A project where we can implement unit tests.   This project makes use of:

- [xUnit](https://xunit.net/)
- [FluentAssertions](https://fluentassertions.com/)
- [Moq](https://github.com/devlooped/moq)

### Integration

This project would house Integration level tests and likely make use of:

- [TestContainers](https://testcontainers.com/) (e.g. for a throwaway database)