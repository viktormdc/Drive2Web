#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Microservices/Analytics/Analytics.Microservice/Analytics.Microservice.csproj", "Microservices/Analytics/Analytics.Microservice/"]
COPY ["Microservices/Analytics/Analytics.Service/Analytics.Service.csproj", "Microservices/Analytics/Analytics.Service/"]
COPY ["Microservices/Analytics/Analytics.Domain/Analytics.Domain.csproj", "Microservices/Analytics/Analytics.Domain/"]
COPY ["Rabbit.Domain.Core/Rabbit.Domain.Core.csproj", "Rabbit.Domain.Core/"]
COPY ["Microservices/Analytics/Analytics.Data/Analytics.Data.csproj", "Microservices/Analytics/Analytics.Data/"]
COPY ["Rabbit.Infrastructure.IoC/Rabbit.Infrastructure.IoC.csproj", "Rabbit.Infrastructure.IoC/"]
COPY ["Microservices/Administration/Administration.Service/Administration.Service.csproj", "Microservices/Administration/Administration.Service/"]
COPY ["Microservices/Administration/Administration.Data/Administration.Data.csproj", "Microservices/Administration/Administration.Data/"]
COPY ["Microservices/Administration/Administration.Domain/Administration.Domain.csproj", "Microservices/Administration/Administration.Domain/"]
COPY ["Rabbit.Infrastructure.Bus/Rabbit.Infrastructure.Bus.csproj", "Rabbit.Infrastructure.Bus/"]
RUN dotnet restore "Microservices/Analytics/Analytics.Microservice/Analytics.Microservice.csproj"
COPY . .
WORKDIR "/src/Microservices/Analytics/Analytics.Microservice"
RUN dotnet build "Analytics.Microservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Analytics.Microservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "Analytics.Microservice.dll"]

RUN	apt-get update 