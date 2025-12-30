# vMarket: Fullstack Hexagonal Marketplace

vMarket is a robust, scalable, and production-ready full-stack application built with a **Dual-Hexagonal Architecture**. It combines a high-performance .NET 10 backend with a modern Next.js 15 frontend, focusing on maintainability, testability, and a premium user experience.

![Marketplace Preview](Videogames.Web/public/preview.png) *(Note: Add your preview image here)*

## 🏗 Architecture

This project implements **Hexagonal Architecture** (Ports and Adapters) on both the backend and frontend to ensure strict separation of concerns and business logic independence.

### Backend (.NET)
- **Domain Layer**: Pure logic, entities (Videogames, Users), and domain services.
- **Application Layer**: Use cases, DTOs, and Ports (Interfaces).
- **Infrastructure Layer**: Persistence (PostgreSQL + EF Core), Auth (BCrypt), and Adapters.
- **API Layer**: ASP.NET Core controllers and DI configuration.

### Frontend (Next.js)
- **Domain**: Models and interface definitions for business logic.
- **Infrastructure**: API services (Axios) and storage adapters.
- **Presentation**: React components and Next.js App Router pages.
- **Context/State**: Clean state management via React Context.

## ✨ Features

- **Full Marketplace Flow**: Browse videogames by categories with an eBay-inspired design.
- **User Authentication**: Secure registration and login with JWT and BCrypt hashing.
- **Inventory Management**: Sell and list items with detailed forms (pricing, condition, categories).
- **Image Upload System**: 
  - Multiple cover images with drag-and-drop reordering
  - Individual uploads for 6 product sides (Front, Back, Right, Left, Top, Bottom)
  - Real-time preview thumbnails
  - MinIO/S3-compatible storage integration
- **Dual-Hexagonal Pattern**: Decoupled layers for maximum testability.
- **Responsive Design**: Premium UI with Dark Mode support and micro-animations.
- **Clean CI/CD**: Automated GitHub Actions pipeline for backend and frontend with E2E tests.

## 🛠 Tech Stack

### Backend
- **Framework**: .NET 10.0
- **Database**: PostgreSQL 17
- **ORM**: Entity Framework Core 10.0
- **Security**: 
  - JWT Authentication (Microsoft.AspNetCore.Authentication.JwtBearer 10.0.0)
  - BCrypt.Net-Next 4.0.3
- **Storage**: MinIO (S3-compatible) via AWSSDK.S3 3.7.407
- **Logging**: Serilog.AspNetCore 9.0.0
- **Testing**: xUnit 2.9.2, Moq 4.20.72
- **API Documentation**: Swashbuckle.AspNetCore 6.6.2

### Frontend
- **Framework**: Next.js 16.1.0 (App Router)
- **Runtime**: React 19.2.0
- **Language**: TypeScript 5.x
- **Styling**: Tailwind CSS 4.x (Vanilla CSS with modern patterns)
- **Icons**: Heroicons 2.2.0, Iconoir React 7.11.0
- **API Client**: Axios 1.13.2 with interceptors
- **E2E Testing**: Playwright 1.57.0

### Infrastructure
- **Containerization**: Docker & Docker Compose
- **CI/CD**: GitHub Actions
- **Node.js**: 20.x
- **Package Manager**: npm

## 🚀 Getting Started

### Prerequisites
- [.NET SDK 10.0+](https://dotnet.microsoft.com/download)
- [Node.js 20+](https://nodejs.org/)
- [Docker](https://www.docker.com/) (for PostgreSQL)

### Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/Alountk/Boilerplate.git
   cd Boilerplate
   ```

2. **Start Infrastructure (PostgreSQL)**
   ```bash
   docker-compose up -d
   ```

3. **Backend Setup**
   ```bash
   # Run migrations
   cd Videogames.Infrastructure
   dotnet ef database update -s ../Videogames.API
   
   # Run API
   cd ../Videogames.API
   dotnet run
   ```
   The API will be available at `http://localhost:5017`.

4. **Frontend Setup**
   ```bash
   cd Videogames.Web
   npm install
   npm run dev
   ```
   The Marketplace will be available at `http://localhost:3000`.

## 🧪 Testing

### Backend
```bash
dotnet test
```

### Frontend E2E
```bash
cd Videogames.Web
npx playwright install
npm run test:e2e
```

## 📂 Project Structure

```
├── Videogames.API             # .NET REST entry point
├── Videogames.Application     # Backend Use Cases & Ports
├── Videogames.Domain          # Backend Entities & Domain Logic
├── Videogames.Infrastructure  # DB, Migrations, Auth Implementations
├── Videogames.Web             # Next.js 15 Frontend
│   ├── src/app                # App Router (Pages)
│   ├── src/components         # UI Components
│   ├── src/context            # Auth & State Contexts
│   ├── src/domain             # Frontend Models & Ports
│   ├── src/infrastructure     # API Services & Axios Setup
│   └── tests/                 # Playwright E2E Tests
└── .github/workflows          # CI/CD (GitHub Actions)
```

## 🗺 Roadmap

- [x] **Full-stack Foundation** (Next.js + .NET)
- [x] **Authentication System** (JWT + BCrypt)
- [x] **Marketplace Discovery** (Home + Categories)
- [x] **Sell Item Flow** (Forms + API integration)
- [x] **CI/CD Pipeline** (Automated Linters + Tests)
- [x] **Image Upload System**: MinIO/S3 integration with multi-image support and drag-to-reorder
- [ ] **Messaging System**: Real-time chat between buyers and sellers.
- [ ] **Advanced Filtering**: Full-text search and faceted navigation.
- [ ] **Payment Integration**: Stripe or PayPal checkout.

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📄 License

This project is licensed under the MIT License.
