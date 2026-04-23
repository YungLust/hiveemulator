# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копіюємо .sln і всі .csproj для кешування залежностей
COPY src/CommunicationControl/ .
RUN dotnet restore DevOpsProject/DevOpsProject.CommunicationControl.API.csproj

RUN dotnet publish DevOpsProject/DevOpsProject.CommunicationControl.API.csproj \
    -c Release -o /app/published

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
RUN groupadd --gid 1000 appuser && \
    useradd --uid 1000 --gid appuser --shell /bin/bash --create-home appuser
COPY --from=build --chown=appuser:appuser /app/published ./
USER appuser
EXPOSE 8080
ENTRYPOINT ["dotnet", "DevOpsProject.CommunicationControl.API.dll"]
