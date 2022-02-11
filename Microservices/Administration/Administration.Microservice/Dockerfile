#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Microservices/Administration/Administration.Microservice/Administration.Microservice.csproj", "Microservices/Administration/Administration.Microservice/"]
COPY ["Rabbit.Infrastructure.IoC/Rabbit.Infrastructure.IoC.csproj", "Rabbit.Infrastructure.IoC/"]
COPY ["Rabbit.Domain.Core/Rabbit.Domain.Core.csproj", "Rabbit.Domain.Core/"]
COPY ["Rabbit.Infrastructure.Bus/Rabbit.Infrastructure.Bus.csproj", "Rabbit.Infrastructure.Bus/"]
RUN dotnet restore "Microservices/Administration/Administration.Microservice/Administration.Microservice.csproj"
COPY . .
WORKDIR "/src/Microservices/Administration/Administration.Microservice"
RUN dotnet build "Administration.Microservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Administration.Microservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Administration.Microservice.dll"]