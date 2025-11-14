# SOBRIPOS Quick Start Guide

Panduan cepat untuk memulai dengan SOBRIPOS dalam 5 menit!

## 📋 Prerequisites

Pastikan Anda sudah menginstall:
- ✅ [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- ✅ Git

## 🚀 Getting Started

### 1. Clone Repository

```bash
git clone <repository-url>
cd SOBRIPOS
```

### 2. Build Solution

```bash
dotnet build
```

Output yang diharapkan:
```
Build succeeded.
    0 Warning(s)
    0 Error(s)
```

### 3. Run Database Migrations

```bash
cd src/Infrastructure/SOBRIPOS.Data
dotnet ef migrations add InitialCreate --startup-project ../../Presentation/SOBRIPOS.API
dotnet ef database update --startup-project ../../Presentation/SOBRIPOS.API
```

**Note**: Migrations akan membuat file database SQLite `sobripos.db` di folder API.

### 4. Run the API

```bash
cd ../../Presentation/SOBRIPOS.API
dotnet run
```

API akan berjalan di:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`

### 5. Test the API

Buka browser dan akses Swagger UI:
```
https://localhost:5001/swagger
```

## 🧪 Test API Endpoints

### Create a Category

```bash
curl -X POST "https://localhost:5001/api/categories" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Electronics",
    "description": "Electronic products"
  }'
```

### Get All Categories

```bash
curl -X GET "https://localhost:5001/api/categories"
```

### Create a Product

```bash
curl -X POST "https://localhost:5001/api/products" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Laptop Dell XPS 15",
    "description": "High-performance laptop",
    "barcode": "123456789",
    "sku": "DELL-XPS15-001",
    "price": 15000000,
    "costPrice": 12000000,
    "stockQuantity": 10,
    "minimumStock": 5,
    "categoryId": "<paste-category-id-here>"
  }'
```

### Get Low Stock Products

```bash
curl -X GET "https://localhost:5001/api/products/low-stock"
```

### Search Product by Barcode

```bash
curl -X GET "https://localhost:5001/api/products/barcode/123456789"
```

## 📱 Run Web Dashboard (Optional)

```bash
cd src/Presentation/SOBRIPOS.Web
dotnet run
```

Web dashboard akan berjalan di:
- HTTPS: `https://localhost:5002`
- HTTP: `http://localhost:5003`

## 🖥️ Run Desktop Client (Optional)

```bash
cd src/Presentation/SOBRIPOS.Desktop
dotnet run
```

## 📊 Project Structure

```
SOBRIPOS/
├── src/
│   ├── Core/
│   │   ├── SOBRIPOS.Core              # Domain entities
│   │   └── SOBRIPOS.Application       # Business logic interfaces
│   ├── Infrastructure/
│   │   └── SOBRIPOS.Data              # Database & repositories
│   └── Presentation/
│       ├── SOBRIPOS.API               # REST API ✨
│       ├── SOBRIPOS.Web               # Blazor dashboard
│       └── SOBRIPOS.Desktop           # Desktop client
├── README.md
├── ARCHITECTURE.md                     # Architecture docs
├── CONTRIBUTING.md                     # Contribution guide
└── SOBRIPOS.sln                       # Solution file
```

## 🎯 Next Steps

### 1. Explore the Architecture

Baca [ARCHITECTURE.md](ARCHITECTURE.md) untuk memahami struktur proyek.

### 2. Add More Controllers

Tambahkan controller untuk:
- Users (`UsersController.cs`)
- Transactions (`TransactionsController.cs`)
- Customers (`CustomersController.cs`)
- Suppliers (`SuppliersController.cs`)
- Purchases (`PurchasesController.cs`)

### 3. Implement Authentication

Tambahkan JWT authentication untuk secure API endpoints.

### 4. Add Validation

Gunakan FluentValidation untuk validasi input.

### 5. Write Tests

Buat unit tests dan integration tests.

## 🔧 Common Commands

### Build
```bash
dotnet build
```

### Run Tests
```bash
dotnet test
```

### Clean Build Artifacts
```bash
dotnet clean
```

### Restore Dependencies
```bash
dotnet restore
```

### Create Migration
```bash
cd src/Infrastructure/SOBRIPOS.Data
dotnet ef migrations add MigrationName --startup-project ../../Presentation/SOBRIPOS.API
```

### Update Database
```bash
cd src/Infrastructure/SOBRIPOS.Data
dotnet ef database update --startup-project ../../Presentation/SOBRIPOS.API
```

### Remove Last Migration
```bash
cd src/Infrastructure/SOBRIPOS.Data
dotnet ef migrations remove --startup-project ../../Presentation/SOBRIPOS.API
```

## 🐛 Troubleshooting

### Build Errors

**Problem**: `The type or namespace name 'X' could not be found`

**Solution**: 
```bash
dotnet restore
dotnet clean
dotnet build
```

### Database Errors

**Problem**: `A network-related or instance-specific error occurred`

**Solution**: Check `appsettings.json` connection string. Default menggunakan SQLite, pastikan folder writable.

### Port Already in Use

**Problem**: `Unable to bind to https://localhost:5001`

**Solution**: Edit `Properties/launchSettings.json` dan ganti port:
```json
{
  "applicationUrl": "https://localhost:5011;http://localhost:5010"
}
```

## 📚 Useful Resources

- [.NET Documentation](https://docs.microsoft.com/dotnet)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [ASP.NET Core](https://docs.microsoft.com/aspnet/core)
- [Blazor](https://docs.microsoft.com/aspnet/core/blazor)

## 💡 Tips

1. **Use Swagger** - Swagger UI di `/swagger` sangat helpful untuk test API
2. **Database Seeding** - Tambahkan sample data untuk development
3. **Hot Reload** - Use `dotnet watch run` untuk auto-reload saat development
4. **Environment Variables** - Gunakan `appsettings.Development.json` untuk local config
5. **Logging** - Check console logs untuk troubleshooting

## 🆘 Need Help?

- 📖 Read [README.md](README.md) untuk overview lengkap
- 🏗️ Read [ARCHITECTURE.md](ARCHITECTURE.md) untuk detail arsitektur
- 🤝 Read [CONTRIBUTING.md](CONTRIBUTING.md) untuk contribution guidelines
- 🐛 Report bugs di [Issues](https://github.com/yourusername/SOBRIPOS/issues)

## ✅ Checklist

Setelah mengikuti quick start guide:

- [ ] ✅ Build berhasil tanpa error
- [ ] ✅ Database migrations berhasil
- [ ] ✅ API berjalan di localhost
- [ ] ✅ Swagger UI accessible
- [ ] ✅ Berhasil create category via API
- [ ] ✅ Berhasil create product via API
- [ ] ✅ Berhasil get data via API

---

**Selamat! Anda siap untuk develop dengan SOBRIPOS! 🎉**
