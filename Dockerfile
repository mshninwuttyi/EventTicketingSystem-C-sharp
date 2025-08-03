# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy solution and project files
COPY EventTicketingSystem.CSharp.sln ./

COPY EventTicketingSystem.CSharp.Api ./EventTicketingSystem.CSharp.Api

COPY EventTicketingSystem.CSharp.Database ./EventTicketingSystem.CSharp.Database

COPY EventTicketingSystem.CSharp.Domain ./EventTicketingSystem.CSharp.Domain

COPY EventTicketingSystem.CSharp.Shared ./EventTicketingSystem.CSharp.Shared

COPY DropExistingTables.sql ./DropExistingTables.sql

COPY EventTicketingSystem.sql ./EventTicketingSystem.sql

# Restore dependencies
RUN dotnet restore EventTicketingSystem.CSharp.sln


# Publish the application
RUN dotnet publish EventTicketingSystem.CSharp.Api/EventTicketingSystem.CSharp.Api.csproj -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/out ./

EXPOSE 80

ENTRYPOINT ["dotnet", "EventTicketingSystem.CSharp.Api.dll"]    