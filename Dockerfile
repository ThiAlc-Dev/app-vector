FROM mcr.microsoft.com/dotnet/sdk:5.0 AS builder

WORKDIR /source
COPY ./src .
RUN dotnet restore
RUN dotnet publish ./Api.Application --output /app/

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=builder /app .
EXPOSE 5000
ENTRYPOINT ["dotnet", "application.dll"]