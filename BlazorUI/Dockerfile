# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy project files
COPY ["BlazorUI/BlazorUI.csproj", "BlazorUI/"]
COPY ["Shared/Shared.csproj", "Shared/"]

# Restore dependencies
RUN dotnet restore "BlazorUI/BlazorUI.csproj"

# Copy all source files
COPY . .

# Build the project
RUN dotnet build "BlazorUI/BlazorUI.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "BlazorUI/BlazorUI.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=publish /app/publish .

EXPOSE 8080
EXPOSE 8443

ENV ASPNETCORE_URLS=http://+:8080;https://+:8443
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "BlazorUI.dll"]
