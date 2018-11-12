FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 50391
EXPOSE 44348

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["cavitt.net/cavitt.net.csproj", "cavitt.net/"]
RUN dotnet restore "cavitt.net/cavitt.net.csproj"
COPY . .
WORKDIR "/src/cavitt.net"
RUN dotnet build "cavitt.net.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "cavitt.net.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "cavitt.net.dll"]