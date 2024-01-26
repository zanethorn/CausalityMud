FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/server/Causality.Mud.Server/Causality.Mud.Server.csproj", "src/server/Causality.Mud.Server/"]
RUN dotnet restore "src/server/Causality.Mud.Server/Causality.Mud.Server.csproj"
COPY . .
WORKDIR "/src/src/server/Causality.Mud.Server"
RUN dotnet build "Causality.Mud.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Causality.Mud.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Causality.Mud.Server.dll"]
