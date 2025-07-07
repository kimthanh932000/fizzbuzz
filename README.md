# 🧠 FizzBuzz App

A full-stack FizzBuzz quiz web app built with:

- **Frontend**: React (Vite)
- **Backend**: ASP.NET Core Web API with Entity Framework Core
- **Database**: SQL Server 2022
- **Containerized**: Using Docker Compose

---

## 📁 Project Structure

```
.
├── client/              # React frontend (Vite)
│   └── Dockerfile       # Frontend Dockerfile
├── Backend/             # ASP.NET Core Web API
│   └── Dockerfile       # Backend Dockerfile
├── docker-compose.yml   # Orchestration for all services
└── README.md            # This file
```

---

## ⚙️ Prerequisites

- ✅ [Docker](https://www.docker.com/products/docker-desktop)
- ✅ [Docker Compose](https://docs.docker.com/compose/)
- ⬛ Optional: [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download) (for local development)

---

## 🚀 Quick Start (with Docker)

### 1. Clone the repository

```bash
git clone https://github.com/your-username/fizzbuzz-app.git
cd fizzbuzz
```

### 2. Build and run the app

```bash
docker-compose up --build
```

- `--build`: Forces Docker to rebuild images (useful after code changes)
- Brings up the frontend, backend, and database

### 3. Access the app

- **Frontend**: http://localhost:5173
- **Backend API**: http://localhost:8080
- **SQL Server**: Accessible on `localhost:1433` with:

  ```
  Server: localhost
  Username: sa
  Password: YourStrong!Passw0rd
  ```

---

## 🔄 Rebuilding and Stopping

### Stop and remove all containers

```bash
docker-compose down
```

### Remove all containers + database volume (⚠️ deletes DB data)

```bash
docker-compose down -v
```

---

## 🧩 Environment Configuration

### Database connection string (docker-compose.yml)

```yaml
ConnectionStrings__DefaultConnection=Server=db;Database=fizzbuzz;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;
```

This overrides `appsettings.json` using ASP.NET Core's environment variable mapping.

### appsettings.json (local use only)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=fizzbuzz;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

---

## 🧃 EF Core Migrations

### Automatic Migration on Container Start

In your `Program.cs`:

```csharp
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}
```

### Manual Migration (requires .NET SDK)

```bash
# Add a migration (from root)
Add-Migration InitialCreate -StartupProject Backend -OutputDir Data\Migrations -Context ApplicationDbContext

# Update the database
Update-Database -StartupProject Backend -Context ApplicationDbContext

# Drop the database
Drop-Database -StartupProject Backend -Context ApplicationDbContext
```

---

## 🧪 API Testing

```bash
curl http://localhost:8080/api/game
```

Or view Swagger UI:

```bash
http://localhost:8080/swagger
```

---

## 🗃️ Persistent Database

SQL Server data is stored using a named volume:

```yaml
volumes:
  db_data:/var/opt/mssql
```

To delete all data:

```bash
docker-compose down -v
```

---

## 🧹 Clean Up

```bash
docker-compose down           # Stop containers
docker-compose down -v        # Stop + remove volumes
docker image prune            # Remove unused images
```

---

## 🧑‍💻 Development (Local)

### Frontend (Vite)

```bash
cd client
npm install
npm run dev
```

### Backend (ASP.NET)

```bash
cd Backend
dotnet restore
dotnet run
```

---

## 📄 License

MIT License
