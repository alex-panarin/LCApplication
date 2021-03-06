#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["LC.Backend.Api/LC.Backend.Api.csproj", "LC.Backend.Api/"]
COPY ["LC.Backend.Common/LC.Backend.Common.csproj", "LC.Backend.Common/"]
RUN dotnet restore "LC.Backend.Api/LC.Backend.Api.csproj"
COPY . .
WORKDIR "/src/LC.Backend.Api"
RUN dotnet build "LC.Backend.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LC.Backend.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "LC.Backend.Api.dll"]