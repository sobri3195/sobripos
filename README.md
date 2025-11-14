# SOBRIPOS - Modern Point of Sale System

SOBRIPOS adalah aplikasi Point of Sale (POS) modern yang dikembangkan menggunakan .NET dengan arsitektur modular dan scalable. Sistem ini dirancang untuk membantu usaha retail mengelola transaksi penjualan, inventori, laporan, hingga manajemen pengguna secara cepat dan efisien.

Dibangun menggunakan ASP.NET Core, Entity Framework Core, dan Blazor/WPF/MAUI (sesuai implementasi), SOBRIPOS mampu berjalan pada environment desktop maupun web dengan performa tinggi serta mudah untuk dikembangkan lebih lanjut.

## 🚀 Fitur Utama

### 🛒 1. Manajemen Produk
- CRUD produk dan kategori
- Harga, diskon, dan variasi produk
- Manajemen stok otomatis

### 📦 2. Inventori & Stok
- Update stok real-time
- Notifikasi stok menipis
- Laporan persediaan

### 💳 3. Transaksi Penjualan
- Input manual atau scan barcode
- Pembayaran tunai/non-tunai
- Cetak struk (printer ESC/POS)
- Riwayat transaksi

### 👥 4. Manajemen Kasir & Pengguna
- Login kasir dengan role-based access
- Hak akses admin/kasir/supervisor
- Log aktivitas pengguna

### 📊 5. Laporan Lengkap
- Penjualan harian/bulanan
- Analisis pendapatan dan laba
- Ekspor PDF/Excel

### ☁️ 6. Sinkronisasi Cloud (Opsional)
- Mode offline menggunakan SQLite
- Auto-sync ke server pusat
- API backend berbasis ASP.NET Core

### 🙍 7. Manajemen Pelanggan
- Data pelanggan
- Riwayat pembelian
- Poin loyalty

### 📑 8. Pembelian & Supplier
- Input pembelian dari supplier
- Update stok otomatis
- Nota pembelian

## 🏛 Arsitektur Proyek

Proyek mengikuti pola **Clean Architecture / Layered Architecture**.

```
/SOBRIPOS
 ├── src/
 │   ├── Core/
 │   │   ├── SOBRIPOS.Core           → Domain entities & enums
 │   │   └── SOBRIPOS.Application    → Business logic & interfaces
 │   ├── Infrastructure/
 │   │   └── SOBRIPOS.Data           → EF Core, DbContext & repositories
 │   └── Presentation/
 │       ├── SOBRIPOS.API            → ASP.NET Core Web API
 │       ├── SOBRIPOS.Web            → Blazor Web Admin Dashboard
 │       └── SOBRIPOS.Desktop        → Desktop POS Client
 └── tests/                          → Unit & Integration tests
```

### Penjelasan Layer

#### Core Layer
- **SOBRIPOS.Core**: Berisi domain entities, enums, dan business rules
- **SOBRIPOS.Application**: Berisi interfaces, DTOs, dan service contracts

#### Infrastructure Layer
- **SOBRIPOS.Data**: Implementasi repository pattern, DbContext, dan migrations

#### Presentation Layer
- **SOBRIPOS.API**: REST API untuk backend services
- **SOBRIPOS.Web**: Blazor web application untuk admin dashboard
- **SOBRIPOS.Desktop**: Desktop application untuk kasir POS

## 🧰 Teknologi yang Digunakan

- **.NET 8.0**
- **ASP.NET Core** (Web API)
- **Entity Framework Core** 8.0
- **SQL Server / PostgreSQL / SQLite**
- **Blazor** (Web UI)
- **iText/QuestPDF** untuk PDF
- **EPPlus** untuk Excel
- **Barcode scanner integration**
- **ESC/POS printer support**

## 📋 Prerequisites

- .NET 8.0 SDK atau lebih tinggi
- SQL Server 2019+ / PostgreSQL 13+ / SQLite (untuk development)
- Visual Studio 2022 atau VS Code
- (Opsional) Docker untuk containerization

## 🚀 Getting Started

### 1. Clone Repository

```bash
git clone https://github.com/yourusername/SOBRIPOS.git
cd SOBRIPOS
```

### 2. Konfigurasi Database

Edit `appsettings.json` di `src/Presentation/SOBRIPOS.API/`:

**Untuk SQLite (Development):**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=sobripos.db"
  }
}
```

**Untuk SQL Server:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SOBRIPOS;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 3. Jalankan Migrations

```bash
cd src/Infrastructure/SOBRIPOS.Data
dotnet ef migrations add InitialCreate --startup-project ../../Presentation/SOBRIPOS.API
dotnet ef database update --startup-project ../../Presentation/SOBRIPOS.API
```

### 4. Jalankan Aplikasi

**API Backend:**
```bash
cd src/Presentation/SOBRIPOS.API
dotnet run
```

API akan berjalan di `https://localhost:5001` dan Swagger UI di `https://localhost:5001/swagger`

**Web Dashboard:**
```bash
cd src/Presentation/SOBRIPOS.Web
dotnet run
```

**Desktop Client:**
```bash
cd src/Presentation/SOBRIPOS.Desktop
dotnet run
```

## 🔧 Development

### Build Solution

```bash
dotnet build
```

### Run Tests

```bash
dotnet test
```

### Create New Migration

```bash
cd src/Infrastructure/SOBRIPOS.Data
dotnet ef migrations add MigrationName --startup-project ../../Presentation/SOBRIPOS.API
```

## 📚 API Documentation

Setelah menjalankan API, akses dokumentasi Swagger di:
- Development: `https://localhost:5001/swagger`
- Production: `https://your-domain.com/swagger`

### Endpoint Utama

- **Products**: `/api/products`
- **Categories**: `/api/categories`
- **Transactions**: `/api/transactions`
- **Users**: `/api/users`
- **Customers**: `/api/customers`
- **Suppliers**: `/api/suppliers`
- **Purchases**: `/api/purchases`

## 🏗️ Struktur Database

### Tabel Utama

1. **Products** - Produk dan item yang dijual
2. **Categories** - Kategori produk
3. **Transactions** - Transaksi penjualan
4. **TransactionItems** - Detail item transaksi
5. **Users** - Pengguna sistem (Admin, Kasir, Supervisor)
6. **Customers** - Data pelanggan
7. **Suppliers** - Data supplier
8. **Purchases** - Pembelian dari supplier
9. **PurchaseItems** - Detail item pembelian
10. **UserActivityLogs** - Log aktivitas pengguna

## 🔒 Security

- Password hashing menggunakan BCrypt
- JWT Token untuk autentikasi API
- Role-based authorization
- SQL injection protection via EF Core
- CORS configuration

## 🎯 Roadmap

- [ ] Implementasi autentikasi JWT
- [ ] Integration dengan payment gateway
- [ ] Mobile app (MAUI)
- [ ] Real-time notifications dengan SignalR
- [ ] Multi-tenant support
- [ ] Advanced reporting dengan charts
- [ ] Integration dengan barcode scanner hardware
- [ ] Integration dengan ESC/POS printer
- [ ] Backup dan restore database
- [ ] Multi-language support

## 📥 Kontribusi

Kontribusi selalu terbuka! Silakan:
1. Fork repository ini
2. Buat branch fitur baru (`git checkout -b feature/AmazingFeature`)
3. Commit perubahan (`git commit -m 'Add some AmazingFeature'`)
4. Push ke branch (`git push origin feature/AmazingFeature`)
5. Buat Pull Request

## 📄 Lisensi

Proyek ini dirilis dengan lisensi **MIT License**. Lihat file [LICENSE](LICENSE) untuk detail.

## 👨‍💻 Author

**Your Name**
- GitHub: [@yourusername](https://github.com/yourusername)
- Email: your.email@example.com

## 🙏 Acknowledgments

- Terima kasih kepada komunitas .NET
- Entity Framework Core team
- Blazor community

## 📞 Support

Jika Anda memiliki pertanyaan atau menemukan bug:
- Buka [Issue](https://github.com/yourusername/SOBRIPOS/issues)
- Atau hubungi via email

---

**Made with ❤️ using .NET**
