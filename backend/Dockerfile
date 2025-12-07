FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY "MeuProjeto.Api/MeuProjeto.Api.csproj" "MeuProjeto.Api/"
RUN dotnet restore "MeuProjeto.Api/MeuProjeto.Api.csproj"
COPY . .
WORKDIR "/src/MeuProjeto.Api"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./
EXPOSE 5000
ENV ASPNETCORE_URLS=https://+;http://+
ENTRYPOINT ["dotnet", "MeuProjeto.Api.dll"]
