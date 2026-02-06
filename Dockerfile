FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["src/StarterKit.Api/StarterKit.Api.csproj", "src/StarterKit.Api/"]
RUN dotnet restore "src/StarterKit.Api/StarterKit.Api.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/src/StarterKit.Api"
RUN dotnet build "StarterKit.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StarterKit.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Install curl for health checks
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StarterKit.Api.dll"]
