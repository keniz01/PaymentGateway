#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/external/PaymentGateway.Consumers/PaymentGateway.Consumers.csproj", "src/external/PaymentGateway.Consumers/"]
COPY ["src/core/PaymentGateway.Application/PaymentGateway.Application.csproj", "src/core/PaymentGateway.Application/"]
COPY ["src/core/PaymentGateway.Domain/PaymentGateway.Domain.csproj", "src/core/PaymentGateway.Domain/"]
RUN dotnet restore "src/external/PaymentGateway.Consumers/PaymentGateway.Consumers.csproj"
COPY . .
WORKDIR "/src/src/external/PaymentGateway.Consumers"
RUN dotnet build "PaymentGateway.Consumers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentGateway.Consumers.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentGateway.Consumers.dll"]
