#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/external/PaymentGateway.Api/PaymentGateway.Api.csproj", "src/external/PaymentGateway.Api/"]
COPY ["src/external/PaymentGateway.Persistence/PaymentGateway.Persistence.csproj", "src/external/PaymentGateway.Persistence/"]
COPY ["src/core/PaymentGateway.Application/PaymentGateway.Application.csproj", "src/core/PaymentGateway.Application/"]
COPY ["src/core/PaymentGateway.Domain/PaymentGateway.Domain.csproj", "src/core/PaymentGateway.Domain/"]
COPY ["src/external/PaymentGateway.Presentation/PaymentGateway.Presentation.csproj", "src/external/PaymentGateway.Presentation/"]
RUN dotnet restore "src/external/PaymentGateway.Api/PaymentGateway.Api.csproj"
COPY . .
WORKDIR "/src/src/external/PaymentGateway.Api"
RUN dotnet build "PaymentGateway.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentGateway.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentGateway.Api.dll"]
