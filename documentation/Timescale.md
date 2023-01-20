1) Command/query interfaces [Done]
2) Masstransit for inserts
3) Thin read only repository [Done]
4) Presentation layer [Done]
5) IPipeline behaviour - validation, correlation id, metric (time span), logging [Done]
6) Pass writes through domain.


http://localhost:15672
AMQ 5672

CreateVendorAccountRequest
CreateVendorAccountEvent
VendorAccountCreatedEvent (past)


Consumer
MassTransit
MassTransit.RabbitMQ
https://code-maze.com/masstransit-rabbitmq-aspnetcore/
