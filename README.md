# .NET Service Template
Service template for .NET services.

## Requirements and progress
Planned and unplanned features of new service template.

### Diagnostics and logging
- [ ] Logging middleware
- [X] Strongly typed configuration
- [ ] Configuration validation on startup (compile time maybe?)
- [X] Bind configuration by options
- [ ] Automatic correlation IDs on incoming
- [ ] CORS setup
- [ ] Elastic stack setup
- [ ] Sample benchmark setup
- [ ] Test datadog
- [x] Healthchecks
 
### Internal application structure
- [X] Mediator based handlers
- [X] Separate command model validation

### Database and schema
- [X] Support for entity framework + migrations
- [X] Strongly typed configuration of schema
- [ ] Dapper support for hot path queries
- [ ] Event sourcing (eventstore)

### Error handling
- [X] Global exception filter
- [X] Custom error types

### Communication
- [X] Controller endpoints
- [ ] OpenAPI contracts 
- [ ] GraphQL contracts for frontend 
- [ ] gRPC for inter-service communication
- [x] OpenAPI swagger specfication

### Async message handling
- [ ] RabbitMQ support for messages
- [ ] Deferred messages with RabbitMQ
- [ ] Rabbit vs. Redis?

### Authentication and authorization
- [ ] JWT support and validation
- [ ] Refresh token support + invalidation

### Unit and integration testing
- [ ] Database supported testing
- [ ] Easy to mock external dependencies regardless of protocol
- [ ] Fast execution and elegant rollback
- [ ] Fake data generator

### Deployment and hosting
- [X] Dockerfile
- [X] Docker Compose for local dev and testing
- [ ] Easy overview of environment variables

### Utilities
- [ ] In-memory cache available 
- [ ] EF supported sorting by request
- [ ] Mapper lib? Consider usage
