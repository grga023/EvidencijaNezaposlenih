# Use the official .NET SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project files and restore dependencies
COPY *.sln .
COPY Evidencijanezaposlenih.Interface/*.csproj Evidencijanezaposlenih.Interface/
COPY EvidencijaNezaposlenih.PoslovnaLogika/*.csproj EvidencijaNezaposlenih.PoslovnaLogika/
COPY EvidencijaNezaposlenih.Repozitorijum/*.csproj EvidencijaNezaposlenih.Repozitorijum/
COPY EvidencijaNezaposlenih.Servisi/*.csproj EvidencijaNezaposlenih.Servisi/
COPY EvidencijaNezaposlenih.modeliPodataka/*.csproj EvidencijaNezaposlenih.modeliPodataka/

RUN dotnet restore

# Copy the rest of the application code
COPY . .

# Build the app
RUN dotnet publish Evidencijanezaposlenih.Interface/Evidencijanezaposlenih.Interface.csproj -c Release -o /out

# Use the runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

# Expose the port the app runs on
EXPOSE 5000
EXPOSE 5001

# Set the entry point to run the app
ENTRYPOINT ["dotnet", "Evidencijanezaposlenih.Interface.dll"]
