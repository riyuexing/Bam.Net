FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Bam.Net/_web/netCore_2_2/bamapps.net/bamapps.net.csproj", "Bam.Net/_web/netCore_2_2/bamapps.net/"]
RUN dotnet restore "Bam.Net/_web/netCore_2_2/bamapps.net/bamapps.net.csproj"
COPY . .
WORKDIR "/src/Bam.Net/_web/netCore_2_2/bamapps.net"
RUN dotnet build "bamapps.net.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "bamapps.net.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "bamapps.net.dll"]