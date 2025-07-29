# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["IM.Auth/IM.Auth.csproj", "IM.Auth/"]
COPY ["Auth.Data/Auth.Data.csproj", "Auth.Data/"]
COPY ["Auth.Domain/Auth.Domain.csproj", "Auth.Domain/"]
COPY ["Auth.Service/Auth.Service.csproj", "Auth.Service/"]
COPY ["Auth.Dto/Auth.Dto.csproj", "Auth.Dto/"]
COPY ["Helper/Helper.csproj", "Helper/"]
COPY ["ConfigurationModel/ConfigurationModel.csproj", "ConfigurationModel/"]
COPY ["Middleware/Middleware.csproj", "Middleware/"]
COPY ["ResponseWrapper/ResponseWrapper.csproj", "ResponseWrapper/"]
RUN dotnet restore "./IM.Auth/IM.Auth.csproj"
COPY . .
WORKDIR "/src/IM.Auth"
RUN dotnet build "./IM.Auth.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./IM.Auth.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IM.Auth.dll"]