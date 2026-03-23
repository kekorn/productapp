# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy project files
COPY ["AuthAPI/AuthAPI.csproj", "AuthAPI/"]
COPY ["Shared/Shared.csproj", "Shared/"]

# Restore dependencies
RUN dotnet restore "AuthAPI/AuthAPI.csproj"

# Copy all source files
COPY . .

# Build the project
RUN dotnet build "AuthAPI/AuthAPI.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "AuthAPI/AuthAPI.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=publish /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_HTTPS_PORT=

ENTRYPOINT ["dotnet", "AuthAPI.dll"]
