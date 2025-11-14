# Contributing to SOBRIPOS

Terima kasih atas minat Anda untuk berkontribusi pada SOBRIPOS! 🎉

Dokumen ini berisi panduan untuk berkontribusi pada proyek SOBRIPOS.

## 📋 Daftar Isi

- [Code of Conduct](#code-of-conduct)
- [Cara Berkontribusi](#cara-berkontribusi)
- [Development Setup](#development-setup)
- [Coding Standards](#coding-standards)
- [Commit Guidelines](#commit-guidelines)
- [Pull Request Process](#pull-request-process)
- [Reporting Bugs](#reporting-bugs)
- [Suggesting Features](#suggesting-features)

## Code of Conduct

Dengan berkontribusi pada proyek ini, Anda setuju untuk menjaga lingkungan yang ramah dan profesional.

### Our Standards

- Bersikap hormat terhadap sudut pandang yang berbeda
- Menerima kritik konstruktif dengan baik
- Fokus pada yang terbaik untuk komunitas
- Menunjukkan empati terhadap anggota komunitas lainnya

## Cara Berkontribusi

### 1. Fork Repository

```bash
# Fork repository melalui GitHub UI, kemudian clone
git clone https://github.com/your-username/SOBRIPOS.git
cd SOBRIPOS
```

### 2. Buat Branch Baru

```bash
git checkout -b feature/nama-fitur-anda
# atau
git checkout -b fix/nama-bug-fix
```

### 3. Lakukan Perubahan

- Tulis kode yang bersih dan mudah dipahami
- Ikuti coding standards yang ada
- Tambahkan tests jika memungkinkan
- Update dokumentasi jika diperlukan

### 4. Commit Perubahan

```bash
git add .
git commit -m "feat: deskripsi singkat perubahan"
```

### 5. Push ke Fork Anda

```bash
git push origin feature/nama-fitur-anda
```

### 6. Buat Pull Request

Buka GitHub dan buat Pull Request dari branch Anda ke `main` branch di repository utama.

## Development Setup

### Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022 atau VS Code
- SQL Server / PostgreSQL / SQLite
- Git

### Setup Steps

1. **Clone Repository**
   ```bash
   git clone https://github.com/yourusername/SOBRIPOS.git
   cd SOBRIPOS
   ```

2. **Restore Dependencies**
   ```bash
   dotnet restore
   ```

3. **Build Solution**
   ```bash
   dotnet build
   ```

4. **Run Database Migrations**
   ```bash
   cd src/Infrastructure/SOBRIPOS.Data
   dotnet ef database update --startup-project ../../Presentation/SOBRIPOS.API
   ```

5. **Run Tests**
   ```bash
   dotnet test
   ```

6. **Run API**
   ```bash
   cd src/Presentation/SOBRIPOS.API
   dotnet run
   ```

## Coding Standards

### C# Style Guide

Kami mengikuti [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions) dari Microsoft.

#### Naming Conventions

```csharp
// Classes dan Interfaces: PascalCase
public class ProductService { }
public interface IProductRepository { }

// Methods: PascalCase
public async Task<Product> GetProductAsync(Guid id) { }

// Parameters dan local variables: camelCase
public void ProcessOrder(string orderNumber, int quantity) 
{
    var totalAmount = 0;
}

// Constants: PascalCase
public const int MaxRetryCount = 3;

// Private fields: _camelCase dengan underscore
private readonly IUnitOfWork _unitOfWork;
```

#### Code Organization

```csharp
public class ExampleClass
{
    // 1. Fields
    private readonly IService _service;
    
    // 2. Constructor
    public ExampleClass(IService service)
    {
        _service = service;
    }
    
    // 3. Properties
    public string Name { get; set; }
    
    // 4. Methods
    public void DoSomething() { }
}
```

#### Async/Await

```csharp
// ✅ Good
public async Task<Product> GetProductAsync(Guid id)
{
    return await _repository.GetByIdAsync(id);
}

// ❌ Bad
public Product GetProduct(Guid id)
{
    return _repository.GetByIdAsync(id).Result; // Don't use .Result or .Wait()
}
```

#### Error Handling

```csharp
// ✅ Good
public async Task<Product?> GetProductAsync(Guid id)
{
    try
    {
        return await _repository.GetByIdAsync(id);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error getting product {ProductId}", id);
        throw;
    }
}

// ❌ Bad
public async Task<Product> GetProductAsync(Guid id)
{
    try
    {
        return await _repository.GetByIdAsync(id);
    }
    catch
    {
        return null; // Don't swallow exceptions
    }
}
```

### Architecture Guidelines

#### Layer Responsibilities

1. **Core Layer** - Pure domain logic, no dependencies
2. **Application Layer** - Business logic, interfaces
3. **Infrastructure Layer** - Data access, external services
4. **Presentation Layer** - UI, API controllers

#### Dependency Rules

```csharp
// ✅ Good - Application depends on Core
namespace SOBRIPOS.Application
{
    using SOBRIPOS.Core.Entities;
}

// ❌ Bad - Core should NOT depend on Application
namespace SOBRIPOS.Core
{
    using SOBRIPOS.Application.Services; // DON'T DO THIS
}
```

## Commit Guidelines

Kami menggunakan [Conventional Commits](https://www.conventionalcommits.org/) format.

### Commit Message Format

```
<type>(<scope>): <subject>

<body>

<footer>
```

### Types

- `feat`: Fitur baru
- `fix`: Bug fix
- `docs`: Perubahan dokumentasi
- `style`: Format kode (tidak mengubah logic)
- `refactor`: Refactoring kode
- `test`: Menambah atau mengubah tests
- `chore`: Maintenance tasks

### Examples

```bash
# Feature
git commit -m "feat(products): add barcode scanning support"

# Bug fix
git commit -m "fix(api): resolve null reference exception in products controller"

# Documentation
git commit -m "docs(readme): update installation instructions"

# Refactoring
git commit -m "refactor(repositories): simplify query logic"
```

## Pull Request Process

### Before Submitting

1. ✅ Pastikan kode Anda build tanpa error
2. ✅ Jalankan semua tests dan pastikan pass
3. ✅ Update dokumentasi jika diperlukan
4. ✅ Ikuti coding standards
5. ✅ Rebase dengan branch `main` terbaru

### PR Description Template

```markdown
## Description
Deskripsi singkat tentang perubahan

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## How Has This Been Tested?
Jelaskan test yang sudah dilakukan

## Checklist
- [ ] Code follows style guidelines
- [ ] Self-review completed
- [ ] Comments added for complex code
- [ ] Documentation updated
- [ ] No new warnings generated
- [ ] Tests added/updated
- [ ] All tests passing
```

### Review Process

1. Minimal 1 reviewer harus approve
2. All CI checks harus pass
3. Resolve semua comments
4. Squash commits sebelum merge (optional)

## Reporting Bugs

### Before Submitting

- Cek apakah bug sudah dilaporkan di [Issues](https://github.com/yourusername/SOBRIPOS/issues)
- Pastikan menggunakan versi terbaru
- Kumpulkan informasi debugging

### Bug Report Template

```markdown
**Describe the bug**
Deskripsi jelas tentang bug

**To Reproduce**
Steps untuk reproduce:
1. Go to '...'
2. Click on '...'
3. See error

**Expected behavior**
Apa yang seharusnya terjadi

**Screenshots**
Jika applicable, tambahkan screenshots

**Environment:**
 - OS: [e.g. Windows 10]
 - .NET Version: [e.g. .NET 8.0]
 - Browser: [e.g. Chrome 120]
 - Version: [e.g. 1.0.0]

**Additional context**
Informasi tambahan
```

## Suggesting Features

### Feature Request Template

```markdown
**Is your feature request related to a problem?**
Deskripsi masalah

**Describe the solution you'd like**
Deskripsi solusi yang diinginkan

**Describe alternatives you've considered**
Alternatif lain yang sudah dipertimbangkan

**Additional context**
Screenshots, mockups, atau informasi tambahan
```

## Code Review Guidelines

### For Reviewers

- Review dengan konstruktif dan helpful
- Fokus pada kode, bukan author
- Jelaskan reasoning di balik suggestions
- Approve jika changes sudah sesuai standards

### For Authors

- Terima feedback dengan terbuka
- Respond to all comments
- Jangan defensive
- Update code berdasarkan feedback

## Development Workflow

```
main (production-ready)
  │
  ├── develop (integration branch)
  │     │
  │     ├── feature/user-authentication
  │     ├── feature/barcode-scanner
  │     └── fix/transaction-bug
  │
  └── release/v1.0.0
```

### Branch Naming

- `feature/feature-name` - untuk fitur baru
- `fix/bug-name` - untuk bug fixes
- `docs/doc-name` - untuk dokumentasi
- `refactor/refactor-name` - untuk refactoring
- `test/test-name` - untuk tests

## Getting Help

Jika Anda memerlukan bantuan:

1. Cek [Documentation](README.md)
2. Baca [Architecture Guide](ARCHITECTURE.md)
3. Search di [Issues](https://github.com/yourusername/SOBRIPOS/issues)
4. Buat Issue baru dengan label `question`
5. Join Discord/Slack community (jika ada)

## License

Dengan berkontribusi ke SOBRIPOS, Anda setuju bahwa kontribusi Anda akan dilisensikan di bawah MIT License.

---

**Terima kasih atas kontribusi Anda! 🙏**
