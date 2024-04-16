FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

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
RUN apt install -y python3.10 \
    && apt install -y openjdk-17-jre-headless \
    && apt install -y gcc \
    && apt install -y g++ \
    && apt install -y go \
    && apt install -y python3-pip \
    && pip3 install requests \
    && pip3 install numpy \
    && pip3 install pandas \