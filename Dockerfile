FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["Simple Billing API.csproj", "./"]
RUN dotnet restore "Simple Billing API.csproj"

# Copy everything else and build
COPY . .
RUN dotnet publish "Simple Billing API.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Simple Billing API.dll"]
