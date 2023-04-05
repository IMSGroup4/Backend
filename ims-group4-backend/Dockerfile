#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ims-group4-backend/ims-group4-backend.csproj", "ims-group4-backend/"]
RUN dotnet restore "ims-group4-backend/ims-group4-backend.csproj"
COPY . .
WORKDIR "/src/ims-group4-backend"
RUN dotnet build "ims-group4-backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ims-group4-backend.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ims-group4-backend.dll"]