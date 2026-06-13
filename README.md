# RecruitProApp – ATS (Applicant Tracking System)

**Plateforme de gestion de recrutement moderne** pour les RH avec Une API modulaire .NET 9 respectant les principes SOLID, l'architecture Clean (DDD + CQRS + MediatR), avec observabilité via OpenTelemetry et logging via Serilog et visualisation des logs et métriques via Azure Application Insight.

---

## Sommaire

- [Objectif](#-objectif)
- [Architecture](#-architecture)
- [Lancer le projet (dev)](#-lancer-le-projet-dev)
- [Arborescence](#-arborescence)
- [Technologies](#️-technologies)
- [API & Swagger](#-api--swagger)
- [Fonctionnalités RH](#-fonctionnalités-rh)
- [Build & Déploiement](#-build--déploiement)
- [Auteur](#️-auteur)

---

# Objectif

Développer une application de recrutement RH moderne, modulaire et évolutive avec :
- Automatisation du processus RH
- Gestion des offres, candidats, entretiens, scoring
- Dashboard en temps réel via **SignalR**

---

# Architecture

> Basée sur une architecture **Clean Architecture** (CQRS + DDD)

```bash
RecruitProApp/
├── src/
│   ├── RecruitProApp.Application/       # Handlers CQRS, DTOs, logique métier
│   ├── RecruitProApp.Domain/            # Entités, enums, règles métier pures (DDD)
│   ├── RecruitProApp.Infrastructure/    # EF Core, SQL Server, emails, etc.
│   ├── RecruitProApp.WebAPI/            # API REST .NET 9 + Swagger (Contrôleurs, Startup, Swagger)
│   └── RecruitProApp.WebClient/         # Angular 19 + Angular Material
├── tests/
│   ├── RecruitProApp.Tests/             # Tests unitaires avec xUnit
```

# Lancer le projet (dev)
## 1. API .NET
```bash
cd src/RecruitProApp.WebAPI
dotnet ef database update
dotnet run
```
Swagger disponible sur : https://localhost:7039/swagger

## 2. Frontend Angular
```bash
cd src/RecruitProApp.WebClient
npm install
npm run start
```
Interface accessible sur : http://localhost:4200

# Fonctionnalités RH
- Gestion des offres, candidatures, entretiens
- Automatisation du processus (email, statut, scoring)
- Dashboard RH temps réel (SignalR)
- Planification & annulation d’entretiens
- Notification par email (SendGrid)
- Scoring automatique des candidatures (bonus)

## Fonctionnalités Tech
- Couverture de tests unitaires (xUnit, AutoFixture, NSubstitute)
- Logging structuré via Serilog
- Traces distribuées avec OpenTelemetry
- Visualisation des logs et métrics via Aure Monitor (Application Insight)
- Déploiement via Docker

# Technologies
| Backend (.NET)         | Frontend (Angular)       |
| ---------------------- | ------------------------ |
| ASP.NET Core 9         | Angular 19               |
| EF Core 9 (SQL Server) | Angular Material         |
| MediatR (CQRS)         | Standalone Components    |
| FluentValidation       | RxJS                     |
| SignalR (temps réel)   | ApexCharts, FullCalendar |
| xUnit / NSubstitute    |                          |

---

## Tests

```bash
cd tests/RecruitProApp.Tests
dotnet test
```
xUnit
AutoFixture
NSubstitute

## Docker
### 1. Build & run
```bash
docker build -t recruitproapp-api .
docker run -d -p 8080:80 --name recruitpro recruitproapp-api
```

### 2. Swagger disponible
http://localhost:8080/swagger

## Observabilité Azure (OpenTelemetry + Serilog)
Les logs sont visibles dans Azure Monitor > Logs

Les traces distribuées dans Application Insights > Live Metrics

Export via OTLP + AzureMonitorLogExporter

!!! Pensez à créer une Application Insights Resource dans Azure et copier la Connection String.

## À venir
- Gestion des candidatures
- Ajout CV (upload)
- Notification SignalR
- Authentification JW

# Auteur 
>    __Abdel Rahmane__
>    contact : [in/abdel-rahmane](https://www.linkedin.com/in/abdel-rahmane/)