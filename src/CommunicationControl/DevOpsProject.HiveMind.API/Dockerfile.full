FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY src/CommunicationControl/ .
RUN dotnet restore DevOpsProject.HiveMind.API/DevOpsProject.HiveMind.API.csproj
RUN dotnet publish DevOpsProject.HiveMind.API/DevOpsProject.HiveMind.API.csproj \
    -c Release -o /app/published

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
RUN groupadd --gid 1000 appuser && \
    useradd --uid 1000 --gid appuser --shell /bin/bash appuser
COPY --from=build --chown=appuser:appuser /app/published ./
USER appuser
EXPOSE 5149
ENTRYPOINT ["dotnet", "DevOpsProject.HiveMind.API.dll"]
