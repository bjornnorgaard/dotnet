# .NET Service Template
Service template for .NET services.

## Requirements and progress
Planned and unplanned features of new service template.

### Diagnostics and logging
- [ ] Logging middleware
- [ ] Strongly typed configuration
- [ ] Configuration validation on startup (compile time maybe?)
- [ ] Bind configuration by options
- [ ] Automatic correlation IDs on incoming
- [ ] CORS setup
- [ ] Elastic stack setup
- [ ] Sample benchmark setup
- [ ] Test datadog
 
### Internal application structure
- [ ] Mediator based handlers
- [ ] Separate command model validation

### Database and schema
- [ ] Support for entity framework + migrations
- [ ] Strongly typed configuration of schema
- [ ] Dapper support for hot path queries

### Error handling
- [ ] Global exception filter
- [ ] Custom error types

### Communication
- [ ] Controller endpoints
- [ ] OpenAPI contracts 
- [ ] GraphQL contracts for frontend 
- [ ] gRPC for inter-service communication

### Async message handling
- [ ] RabbitMQ support for messages
- [ ] Deferred messages with RabbitMQ

### Authentication and authorization
- [ ] JWT support and validation

### Unit and integration testing
- [ ] Database supported testing
- [ ] Easy to mock external dependencies regardless of protocol
- [ ] Fast execution and elegant rollback
- [ ] Fake data generator

### Deployment and hosting
- [ ] Dockerfile
- [ ] Docker Compose for local dev and testing
- [ ] Easy overview of environment variables

### Utilities
- [ ] In-memory cache available 
- [ ] EF supported sorting by request
- [ ] Mapper lib? Consider usage
