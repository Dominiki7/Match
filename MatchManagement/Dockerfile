FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MatchManagement/MatchManagement.csproj", "MatchManagement/"]
RUN dotnet restore "MatchManagement/MatchManagement.csproj"
COPY . .
WORKDIR "/src/MatchManagement"
RUN dotnet build "MatchManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MatchManagement.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MatchManagement.dll"]