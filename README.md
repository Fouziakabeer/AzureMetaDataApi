# Azure Metadata API

A .NET 8 Web API to fetch and serve Azure Instance Metadata Service (IMDS) data.  
This project demonstrates clean architecture with separation of concerns and includes unit tests using Moq.

---

## 🌐 Features

- Fetch all metadata from Azure IMDS
- Fetch specific metadata using a key path
- Caching with `IMemoryCache`
- Resilience using Polly retry policies
- Swagger UI for testing endpoints
- Unit tests with Moq

---

## 📁 Project Structure

```
AzureMetadataApi/
│
├── AzureMetadataApi.Domain/             # Domain contracts (interfaces, models)
├── AzureMetadataApi.Application/        # Application interfaces
├── AzureMetadataApi.Infrastructure/     # Service implementations (HttpClient, caching)
├── AzureMetadataApi.Web/                # ASP.NET Core Web API (controllers, DI, Swagger)
├── AzureMetadataApi.Tests/              # Unit tests (xUnit, Moq)
└── README.md                            # Project documentation
```

---

## ⚙️ Setup Instructions

### 1. Clone the repository

```bash
git clone https://github.com/your-username/AzureMetadataApi.git
cd AzureMetadataApi
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Build the solution

```bash
dotnet build
```

### 4. Run the Web API

```bash
cd AzureMetadataApi.Web
dotnet run
```

### 5. Open Swagger

Visit [https://localhost:7210/swagger](https://localhost:5001/swagger) (or check the console output for the correct port).

---

## 🔪 Running Tests

```bash
cd AzureMetadataApi.Tests
dotnet test
```

Make sure packages like `Moq`, `Microsoft.Extensions.Logging.Abstractions`, etc., are installed.

---

## 🔑 API Endpoints

| Method | Route                            | Description                         |
|--------|----------------------------------|-------------------------------------|
| GET    | `/api/metadata`                  | Returns all metadata as JSON        |
| GET    | `/api/metadata/by-path?keyPath=...` | Returns metadata by specific path   |

---

## ✅ Example Usage

```http
GET /api/metadata/by-path?keyPath=compute/location
```

Returns:

```json
{
  "value": "eastus"
}
```

---

## 📦 Dependencies

- .NET 8 SDK
- Microsoft.Extensions.Http.Polly
- Moq
- Swashbuckle.AspNetCore
- Microsoft.Extensions.Caching.Memory

---

## 🙌 Author

**Fouzia Kabeer**  
Email: [fouziakabeer@gmail.com](mailto:fouziakabeer@gmail.com)