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

docker run -d --hostname my-rabbitmq-server --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management

https://code-maze.com/masstransit-rabbitmq-aspnetcore/
https://kylegenebrown.medium.com/gitarchitecture-a-better-way-to-capture-architectural-decisions-b3574a3d604
https://saranyansenthivel.medium.com/draw-io-diagrams-into-markdown-files-using-vscode-extension-bcb28575f682
https://masstransit-project.com/quick-starts/rabbitmq.html

https://github.com/MassTransit/Sample-JobConsumers/blob/master/docker-compose.yml

sudo lsof -i :5432
sudo kill -9 <PID>

curl http://localhost:5123/api/v1/payment-gateway/vendor-account -H 'Content-Type: application/json' -H 'X-MEMBER-ID: 600010' -H 'X-CORRELATION-ID: ffae0601-15b1-452e-8f6b-76c33b030176' -H 'X-METAMEMBER-ID: 600010'

curl -X POST http://localhost:5123/api/v1/payment-gateway/vendor-account -H 'Content-Type: application/json' -H 'X-MEMBER-ID: 600010' -H 'X-CORRELATION-ID: ffae0601-15b1-452e-8f6b-76c33b030176' -H 'X-METAMEMBER-ID: 600010' -d '{"MemberId":600010,"MetaMemberId":600150,"ApiSecretKey":"sk_test_dvyisdgf9ebweusdf983DSFDS3udu","ApiPublicKey":"sk_test_dvyisdgf9ebweusdf983DSFDS3udu","IsActivated":true}'

{"MemberId":600010,"MetaMemberId":600150,"ApiSecretKey":"sk_test_dvyisdgf9ebweusdf983DSFDS3udu","ApiPublicKey":"sk_test_dvyisdgf9ebweusdf983DSFDS3udu","IsActivated":true,"DateCreated":"2023-01-24T08:35:07.8909963+00:00","DateUpdatedted":"2023-01-24T08:35:07.9005876+00:00"}