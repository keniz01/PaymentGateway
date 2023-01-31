#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/external/Sidetrade.Cloud.Api.PaymentGateway.Consumers/Sidetrade.Cloud.Api.PaymentGateway.Consumers.csproj", "src/external/Sidetrade.Cloud.Api.PaymentGateway.Consumers/"]
COPY ["src/core/Sidetrade.Cloud.Api.PaymentGateway.Application/Sidetrade.Cloud.Api.PaymentGateway.Application.csproj", "src/core/Sidetrade.Cloud.Api.PaymentGateway.Application/"]
COPY ["src/core/Sidetrade.Cloud.Api.PaymentGateway.Domain/Sidetrade.Cloud.Api.PaymentGateway.Domain.csproj", "src/core/Sidetrade.Cloud.Api.PaymentGateway.Domain/"]
RUN dotnet restore "src/external/Sidetrade.Cloud.Api.PaymentGateway.Consumers/Sidetrade.Cloud.Api.PaymentGateway.Consumers.csproj"
COPY . .
WORKDIR "/src/src/external/Sidetrade.Cloud.Api.PaymentGateway.Consumers"
RUN dotnet build "Sidetrade.Cloud.Api.PaymentGateway.Consumers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sidetrade.Cloud.Api.PaymentGateway.Consumers.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sidetrade.Cloud.Api.PaymentGateway.Consumers.dll"]
