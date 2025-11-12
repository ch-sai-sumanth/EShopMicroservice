# 🧩 EShop Microservices — .NET 8 Clean Architecture Implementation

### 🛒 A Cloud-Ready Microservices-based E-Commerce Application built with ASP.NET Core, Docker, and modern .NET 8 tools

This repository implements a real-world **e-commerce application** using **Microservices Architecture** with **Domain-Driven Design (DDD)**, **CQRS**, and **Event-Driven Communication** patterns — completely built by **CH Sai Sumanth** as a learning and showcase project inspired by industry best practices.

---

## 🧠 Overview

The solution simulates an end-to-end **E-Commerce Platform** consisting of multiple **.NET 8 microservices** communicating through **gRPC**, **RabbitMQ**, and **HTTP** via a **YARP API Gateway**.

Each service is containerized with Docker and can be orchestrated together using Docker Compose.

---

## 🧱 Microservices Implemented

| Service | Description | Tech Stack |
|---------|-------------|------------|
| **Catalog.API** | Manages product catalog operations | ASP.NET Core Minimal APIs, PostgreSQL (Marten), CQRS with MediatR |
| **Basket.API** | Manages user basket with caching and event publishing | ASP.NET Core Web API, Redis, RabbitMQ, gRPC, MassTransit |
| **Discount.GRPC** | Provides product discounts to Basket service via gRPC | gRPC Server, SQLite, EF Core |
| **Ordering.API** | Handles order creation and payment after basket checkout | ASP.NET Core Web API, SQL Server, DDD, CQRS, MediatR, RabbitMQ |
| **Yarp.ApiGateway** | Centralized API Gateway using YARP Reverse Proxy | ASP.NET Core YARP, Rate Limiting |
| **Shopping.Web** | Razor UI client consuming APIs via Gateway | ASP.NET Core Razor Pages, Refit HttpClientFactory |

---

## 🧩 Architecture Highlights

- 🧱 **Clean Architecture & DDD principles**
- ⚙️ **CQRS Pattern** with **MediatR** and **FluentValidation**
- 💬 **Event-Driven Communication** via **RabbitMQ + MassTransit**
- 🌐 **gRPC-based inter-service communication**
- 🧠 **Resilient communication** with **HTTP clients + Refit**
- 🧰 **Containerized with Docker Compose**
- 🪣 **Polyglot Persistence:**
    - PostgreSQL (Marten)
    - Redis (Distributed Cache)
    - SQLite (Discounts)
    - SQL Server (Ordering)

---

## 🛠️ Tools & Technologies Used

![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet)
![Docker](https://img.shields.io/badge/Docker-Enabled-blue)
![RabbitMQ](https://img.shields.io/badge/RabbitMQ-Message%20Broker-orange)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Database-blue)
![Redis](https://img.shields.io/badge/Redis-Cache-red)
![YARP](https://img.shields.io/badge/YARP-API%20Gateway-green)

- **.NET 8 / C# 12**
- **ASP.NET Core Minimal APIs / Razor Pages**
- **Entity Framework Core / Marten (DocumentDB)**
- **PostgreSQL, Redis, SQL Server, SQLite**
- **RabbitMQ + MassTransit**
- **gRPC**
- **YARP API Gateway**
- **Docker & Docker Compose**
- **Refit (Typed HttpClient)**
- **FluentValidation**
- **MediatR**
- **Mapster (Object Mapping)**
- **Health Checks, Logging, Global Exception Middleware**

---

## ⚙️ System Design Overview

```mermaid
graph TD
    A[Shopping.Web (Razor App)] -->|HTTP (Refit)| B[Yarp.ApiGateway]
    B -->|REST / gRPC| C[Catalog.API]
    B -->|REST / gRPC| D[Basket.API]
    D -->|gRPC| E[Discount.GRPC]
    D -->|RabbitMQ Event| F[Ordering.API]
    C -->|PostgreSQL| G[(CatalogDB)]
    D -->|Redis| H[(BasketDB)]
    E -->|SQLite| I[(DiscountDB)]
    F -->|SQL Server| J[(OrderDB)]
```

---

# 🐳 Docker Compose Setup

This guide explains how to build, run, and manage the **.NET 8 EShop Microservices** solution using **Docker Compose**. It includes all microservices, databases, and supporting infrastructure required for a complete e-commerce system.

---

## 🧩 Services in the Docker Compose Stack

| Service | Description | Ports |
|---------|-------------|-------|
| **Catalog.API** | Manages product catalog, uses PostgreSQL | 6000 (HTTP), 6060 (HTTPS) |
| **Basket.API** | Manages user basket with Redis & RabbitMQ | 6001 (HTTP), 6061 (HTTPS) |
| **Discount.GRPC** | Provides gRPC discount service (SQLite) | 6002 (HTTP), 6062 (HTTPS) |
| **Ordering.API** | Handles order workflow using SQL Server & RabbitMQ | 6003 (HTTP), 6063 (HTTPS) |
| **Yarp.ApiGateway** | Routes traffic between frontend & backend services | 6004 (HTTP), 6064 (HTTPS) |
| **Shopping.Web** | Razor frontend UI calling APIs via Gateway | 6005 (HTTP), 6065 (HTTPS) |
| **PostgreSQL (CatalogDb & BasketDb)** | Databases for Catalog and Basket | 5432, 5433 |
| **SQL Server (OrderDb)** | Database for Ordering microservice | 1433 |
| **Redis** | Distributed cache for Basket service | 6379 |
| **RabbitMQ** | Message broker for async communication | 5672 (AMQP), 15672 (Dashboard) |

---

## ⚙️ Prerequisites

You'll need the following tools installed before running the stack:

- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [.NET SDK 8.0+](https://dotnet.microsoft.com/download/dotnet/8.0)
- At least **4 GB RAM** and **2 CPUs** allocated to Docker

> 💡 **Tip:** Open Docker Desktop → Settings → Resources → set CPU = 2, Memory = 4 GB

---

## 🏗️ Build and Run the Stack

At the root of your repository, run the following command:

```bash
docker compose up --build
```

This will:
- Build all .NET microservices from their Dockerfiles
- Start all containers (APIs, DBs, RabbitMQ, Redis, etc.)
- Establish internal Docker networking

You can monitor container startup logs in real-time:

```bash
docker compose logs -f
```

To stop all containers:

```bash
docker compose down
```

---

## 🧾 Health Check and Readiness

Each microservice and database includes built-in healthcheck configurations.

You can verify all containers are healthy using:

```bash
docker ps
```

Look for the **STATUS** column — it should show `healthy`.

---

## 🌐 Accessing the Services

| Service | URL | Notes |
|---------|-----|-------|
| 🛍️ Shopping Web UI | https://localhost:6065 | Main entry point (Razor UI) |
| 🧭 YARP API Gateway | https://localhost:6064 | Routes API calls to backend services |
| 🐇 RabbitMQ Dashboard | http://localhost:15672 | Username: `guest`, Password: `guest` |
| 🐘 CatalogDb (Postgres) | localhost:5432 | Username: `postgres`, Password: `postgres` |
| 🧺 BasketDb (Postgres) | localhost:5433 | Username: `postgres`, Password: `postgres` |
| 🗄️ OrderingDb (SQL Server) | localhost:1433 | Username: `sa`, Password: `SwN12345678` |
| 💾 Redis | localhost:6379 | Used for Basket caching |

---

## 🔁 Communication Flow

### ✅ Synchronous (gRPC & HTTP)
- Basket.API ↔ Discount.GRPC (gRPC)
- Shopping.Web ↔ YARP Gateway ↔ APIs (HTTP)

### ⚡ Asynchronous (RabbitMQ)
- Basket.API → RabbitMQ → Ordering.API

You can view messages and queues via RabbitMQ dashboard at http://localhost:15672

---

## 🧰 Environment Variables Overview

Each service uses environment variables defined in `docker-compose.yml`.

**Example (Basket.API):**

```yaml
environment:
  - ConnectionStrings__BasketDb=Host=basketdb;Database=BasketDb;Username=postgres;Password=postgres
  - Redis__ConnectionString=distributedcache:6379,abortConnect=false
  - MessageBroker__Host=amqp://ecommerce-mq:5672
  - MessageBroker__UserName=guest
  - MessageBroker__Password=guest
```

⚙️ These variables are automatically injected when the containers start — no manual configuration is needed.

---

## 🧱 Container Volumes

Persistent storage is used for databases:

```yaml
volumes:
  postgres_catalog:
  postgres_basket:
  mssql_data:
```

This ensures data is preserved even when containers are restarted.

To reset data, simply remove volumes:

```bash
docker compose down -v
```



## 🧾 Summary

✅ **Single Command Startup:**
```bash
docker compose up --build
```

✅ **Services Included:**
- Catalog.API, Basket.API, Discount.GRPC, Ordering.API
- Yarp.ApiGateway, Shopping.Web
- PostgreSQL, SQL Server, Redis, RabbitMQ

✅ **Technologies:**
- .NET 8, Docker, RabbitMQ, Redis, PostgreSQL, SQL Server, YARP

✅ **Purpose:**
- To demonstrate a real-world microservices architecture with full container orchestration

---

## 📦 Project Information

**Author:** CH Sai Sumanth  
**Project:** EShop Microservices (.NET 8)  
**Repository:** https://github.com/ch-sai-sumanth/EShopMicroservice

---
