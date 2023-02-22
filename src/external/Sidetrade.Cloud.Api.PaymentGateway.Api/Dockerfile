#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/external/Sidetrade.Cloud.Api.PaymentGateway.Api/Sidetrade.Cloud.Api.PaymentGateway.Api.csproj", "src/external/Sidetrade.Cloud.Api.PaymentGateway.Api/"]
COPY ["src/external/Sidetrade.Cloud.Api.PaymentGateway.Persistence/Sidetrade.Cloud.Api.PaymentGateway.Persistence.csproj", "src/external/Sidetrade.Cloud.Api.PaymentGateway.Persistence/"]
COPY ["src/core/Sidetrade.Cloud.Api.PaymentGateway.Application/Sidetrade.Cloud.Api.PaymentGateway.Application.csproj", "src/core/Sidetrade.Cloud.Api.PaymentGateway.Application/"]
COPY ["src/core/Sidetrade.Cloud.Api.PaymentGateway.Domain/Sidetrade.Cloud.Api.PaymentGateway.Domain.csproj", "src/core/Sidetrade.Cloud.Api.PaymentGateway.Domain/"]
COPY ["src/external/Sidetrade.Cloud.Api.PaymentGateway.Presentation/Sidetrade.Cloud.Api.PaymentGateway.Presentation.csproj", "src/external/Sidetrade.Cloud.Api.PaymentGateway.Presentation/"]
RUN dotnet restore "src/external/Sidetrade.Cloud.Api.PaymentGateway.Api/Sidetrade.Cloud.Api.PaymentGateway.Api.csproj"
COPY . .
WORKDIR "/src/src/external/Sidetrade.Cloud.Api.PaymentGateway.Api"
RUN dotnet build "Sidetrade.Cloud.Api.PaymentGateway.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sidetrade.Cloud.Api.PaymentGateway.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sidetrade.Cloud.Api.PaymentGateway.Api.dll"]
