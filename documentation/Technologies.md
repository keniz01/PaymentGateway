Design

Api -> Application -> Domain | Infrastructure
Api - MediatR, CorrelationId, Logging, Correlation, Global exceptions, validation, 
    rate limiter, metrics
Application -> Validation, logging, IPipelineBehavior, IRequestPreProcessor, IRequestPostProcessor,
    IRequestHandler, IRequest

https://garywoodfine.com/how-to-use-mediatr-pipeline-behaviours/
https://code-maze.com/cqrs-mediatr-fluentvalidation/
https://github.com/CodeMazeBlog/cqrs-validation-mediatr-fluentvalidation/tree/main/CQRS_Validation_FluentValidator/Web
https://codewithmukesh.com/blog/using-entity-framework-core-and-dapper/

Technologies
------------
FluentValidation
MediatR
Mapster (AutoMapper)
Dapper/EFCore
Bogus/AutoFixture
Masstransit/RabbitMQ (Saga pattern)


Patterns
--------
1) Specification (Domain-level)
2) CQRS (Architecture)
3) CQS (Design)

