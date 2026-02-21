# NZWalks.API

A production-style RESTful Web API built using **ASP.NET Core** and **Entity Framework Core**, demonstrating clean architecture principles, scalable API design, and structured backend development practices.

This project manages walking tracks (“Walks”) and Regions, serving as a real-world backend implementation example suitable for enterprise-grade applications.

---

## 🚀 Features

- RESTful API endpoints (CRUD operations)
- Clean Architecture & layered project structure
- Entity Framework Core with migrations
- DTO pattern & AutoMapper (if implemented)
- Asynchronous programming (async/await)
- Swagger / OpenAPI documentation
- SQL Server integration
- Proper status codes & validation handling

---

## 🏗 Architecture Overview

The project follows a structured, maintainable architecture:

- **Controllers** → Handle HTTP requests & responses
- **Repositories** → Data access abstraction layer
- **Models** → Domain entities
- **DTOs** → Data transfer objects
- **DbContext** → EF Core database configuration

This approach ensures:
- Separation of concerns
- Testability
- Maintainability
- Scalability for production systems

---

## 🛠 Technologies Used

- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI
- Visual Studio

---

## 📦 Installation & Setup

### 1️⃣ Clone the Repository

```bash
git clone https://github.com/roh-anku/NZWalks.API.git
cd NZWalks.API
```

---

### 2️⃣ Configure Database Connection

Update the connection string inside:

```
appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "NZWalksConnectionString": "Server=YOUR_SERVER;Database=NZWalksDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

---

### 3️⃣ Apply Migrations

If migrations already exist:

```bash
dotnet ef database update
```

If creating fresh migration:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

### 4️⃣ Run the Application

```bash
dotnet run
```

Or run directly from Visual Studio.

---

### 5️⃣ Access Swagger UI

Once running, open:

```
https://localhost:{port}/swagger
```

Swagger provides interactive API documentation.

---

# 📡 API Endpoints

## 📍 Regions

### GET All Regions

**Request:**
```
GET /api/regions
```

**Response (200 OK):**
```json
[
  {
    "id": "1",
    "code": "AKL",
    "name": "Auckland",
    "regionImageUrl": "https://example.com/image.jpg"
  }
]
```

---

### GET Region by ID

**Request:**
```
GET /api/regions/{id}
```

**Response (200 OK):**
```json
{
  "id": "1",
  "code": "AKL",
  "name": "Auckland",
  "regionImageUrl": "https://example.com/image.jpg"
}
```

---

### POST Create Region

**Request:**
```
POST /api/regions
Content-Type: application/json
```

**Body:**
```json
{
  "code": "WGN",
  "name": "Wellington",
  "regionImageUrl": "https://example.com/wellington.jpg"
}
```

**Response (201 Created):**
```json
{
  "id": "2",
  "code": "WGN",
  "name": "Wellington",
  "regionImageUrl": "https://example.com/wellington.jpg"
}
```

---

## 🥾 Walks

### GET All Walks

```
GET /api/walks
```

### POST Create Walk

```json
{
  "name": "Mount Eden Walk",
  "description": "Scenic city walk",
  "lengthInKm": 5,
  "regionId": "1",
  "walkDifficultyId": "2"
}
```

---

# 🔐 HTTP Status Codes Used

- 200 OK
- 201 Created
- 204 No Content
- 400 Bad Request
- 404 Not Found

---

# 📈 Engineering Highlights

- Implemented repository pattern for abstraction
- Used DTO mapping to prevent domain exposure
- Applied async programming for non-blocking I/O
- Structured project for scalability and maintainability
- Implemented proper error handling and validation responses

---

# 📌 Future Improvements

- JWT Authentication & Authorization
- Role-based access control
- Global exception handling middleware
- Logging using Serilog
- Unit & Integration testing
- Docker containerization
- CI/CD pipeline integration

---

# 👨‍💻 Author

**Rohit Shyam Tumma**  
Senior .NET Full Stack Engineer  
Pune, India  

LinkedIn: https://linkedin.com/in/rohit-tumma-676992198  
GitHub: https://github.com/roh-anku  

---

## ⭐ If you found this helpful, consider giving it a star!
