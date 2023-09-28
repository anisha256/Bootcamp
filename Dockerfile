# Use the appropriate .NET 7.0 base image for your application
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

# Set the MSBuild version explicitly
ENV MSBUILD_VERSION=17.7.3

# Use the appropriate .NET 7.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
# Copy the entire project into the container
COPY . .

# Navigate to the project directory
WORKDIR "/src/Bootcamp.WebAPI"

# Explicitly specify the project file and build it
RUN dotnet build "Bootcamp.WebAPI.csproj" -c Release -o /app/build

# Create the publish image
FROM build AS publish
RUN dotnet publish "Bootcamp.WebAPI.csproj" -c Release -o /app/publish

# Create the final image with the published output
FROM base AS final
WORKDIR /app

# Copy the published output from the published image
COPY --from=publish /app/publish .

# Set the entry point for your application
ENTRYPOINT ["dotnet", "Bootcamp.WebAPI.dll"]
