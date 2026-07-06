FROM mcr.microsoft.com/dotnet/sdk:10.0 AS builder
WORKDIR /app
COPY DeveloperCore.WoodRoute.Platform/*.csproj DeveloperCore.WoodRoute.Platform/
RUN dotnet restore ./DeveloperCore.WoodRoute.Platform
COPY . .
RUN dotnet publish ./DeveloperCore.WoodRoute.Platform -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=builder /app/out .
# The .NET 10 aspnet image binds Kestrel to port 8080 by default; Render auto-detects it.
EXPOSE 8080
ENTRYPOINT ["dotnet", "DeveloperCore.WoodRoute.Platform.dll"]
