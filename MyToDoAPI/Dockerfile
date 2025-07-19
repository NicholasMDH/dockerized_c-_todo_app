# MyToDoApi/Dockerfile

# --- STAGE 1: Build Stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything
COPY . ./

# Restore as distinct layers
RUN dotnet restore

# Build and publish a release
RUN dotnet publish -c Release -o out

# --- STAGE 2: Runtime Stage ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy published application from the build stage
COPY --from=build /app/out .

# Set environment variables for containerized deployment
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80

# Expose the port your application listens on
EXPOSE 80

# Define the entry point for the container
ENTRYPOINT ["dotnet", "MyToDoAPI.dll"]
