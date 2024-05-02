# ModularMonolithCourse

The modular monolith course from Milan Jovanovic

## What is a modular monolith?

A modular monolith is an architectural pattern that structures the application into independent modules or components with well-defined boundaries. The modules are split based on logical boundaries, grouping together related functionalities. This approach significantly improves the cohesion of the system.

The modules are loosely coupled, which further promotes modularity and separation of concerns. Modules communicate through a public API.

### Benefits

- High cohesion
- Loose coupling
- Simplified deployment
- Improved performance
- Lower operational complexity
- Easier transition to microservices

### How to achieve

- Must be independent and interchangeable
- Must be able to provide the required functionality
- Must have a well-defined interface exposed to other modules

### Constraints

- Logical isolation between modules
- Modules must talk through a public API
- Module data must be isolated in the database

## Module communication

- Method calls
- Messaging

### Method calls

- Speed of in-memory calls
- Easy to implement
- No indirection

### Messaging

- High availability
- Loose coupling

## Data isolation

- Level 1: Separate tables
- Level 2: Separate schemas
- Level 3: Separate databases
- Level 4: Separate servers

NEVER directly call another module's database!

If you need data from another module:

- Use the public API.
- Messaging (fat events)
- Data copy

## Domain Driven Design

How to define your domain model?

1. Discover
2. Decompose
3. Connect
4. Code

### Discover

- Event storming
- Bounded contexts

#### Event storming

1. Write up the domain events using sticky notes.
2. Create a timeline
3.