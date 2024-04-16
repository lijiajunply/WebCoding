FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM ubuntu:latest AS installer
RUN apt-get update && apt-get install -y \
    gcc\
    g++ \
    python3 \
    java-11-openjdk-headless \
    dotnet-sdk:8.0 \
    && dotnet tool update -g dotnet-execute\
    && pip3 install --upgrade pip \
    && pip3 install \
      numpy \
      scipy \
      pandas \
      matplotlib \
      scikit-learn \
    && rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebCoding.Api/WebCoding.Api.csproj", "WebCoding.Api/"]
RUN dotnet restore "WebCoding.Api/WebCoding.Api.csproj"
COPY . .
WORKDIR "/src/WebCoding.Api"
RUN dotnet build "WebCoding.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WebCoding.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebCoding.Api.dll"]
