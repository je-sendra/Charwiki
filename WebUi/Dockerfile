# Use the official ASP.NET SDK image for the build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project
COPY . ./

# Copy the ClassLib project files
COPY ./ClassLib/ ./ClassLib/

# Restore and build the application in Release mode
RUN dotnet restore && dotnet publish -c Release -o out

# Use the official ASP.NET runtime image for the production stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the built application from the build stage
COPY --from=build /app/out ./

# Set the environment variable to ASP.NET Core environment
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose the port the app runs on
EXPOSE 8080

# Start the application
ENTRYPOINT ["dotnet", "WebUi.dll"]
