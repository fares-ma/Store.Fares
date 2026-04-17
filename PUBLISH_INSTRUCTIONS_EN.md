# Store.Fares Deployment Guide to ASP Monster

## 📦 Published Files

All necessary files are ready in the `publish` folder:
- Application executable (.exe, .dll files)
- Configuration files (appsettings.*.json)
- All required libraries and dependencies
- IIS configuration (web.config)

## 🚀 Deployment Steps

### Step 1: Database Setup

1. **Create Database on ASP Monster:**
   - Database Name: `Store.Fares.App`
   - Get connection credentials from ASP Monster Control Panel

2. **Note Down Connection Details:**
   ```
   Server: [ASP Monster provided]
   Database: Store.Fares.App
   User ID: [your username]
   Password: [your password]
   ```

### Step 2: Update Configuration

Edit `appsettings.Production.json` in the `publish` folder:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=Store.Fares.App;User Id=YOUR_USER;Password=YOUR_PASSWORD;Encrypt=true;TrustServerCertificate=false;",
    "Redis": "YOUR_REDIS_CONNECTION_STRING"
  },
  "BaseUrl": "https://your-domain.com",
  "jwtOptions": {
    "SecretKey": "SECURE_KEY_AT_LEAST_32_CHARACTERS_LONG_CHANGE_THIS!",
    "Audience": "StoreFaresApp",
    "Issuer": "StoreFares",
    "DurationInDays": 7
  }
}
```

### Step 3: Upload Files

#### Option A: Using FTP (Recommended)

1. Use FTP client (FileZilla, WinSCP, etc.)
2. Connect with credentials from ASP Monster
3. Navigate to web root (`/httpdocs` or `/public_html`)
4. Upload all files from `publish` folder

#### Option B: Using Web Deploy

```powershell
dotnet publish -c Release
# Follow the publish profile instructions
```

### Step 4: Run Database Migrations

Execute migrations to create tables:

```powershell
# Navigate to project root
cd c:\Users\Rocket\Desktop\Store\Store.Fares

# Create and update databases
dotnet ef database update -c StoreDbContext
dotnet ef database update -c StoreIdentityDbContext
```

### Step 5: Configure Website

In ASP Monster Control Panel:

1. **Bind Domain:**
   - Point domain to application folder

2. **Enable HTTPS:**
   - Get free SSL certificate (Let's Encrypt)

3. **Verify Requirements:**
   - .NET 10 Runtime installed
   - IIS configured for ASP.NET Core
   - AspNetCoreModuleV2 enabled

## ✅ Pre-Deployment Checklist

- [ ] Database created and credentials saved
- [ ] appsettings.Production.json updated with real values
- [ ] JWT Secret Key changed to secure value (32+ characters)
- [ ] Connection strings verified
- [ ] Redis connection (if needed) configured
- [ ] Domain ready to point to server
- [ ] SSL certificate obtained
- [ ] .NET 10 Runtime available on server

## 📋 Server Requirements

| Component | Minimum | Recommended |
|-----------|---------|-------------|
| .NET Runtime | 10.0 | 10.0+ |
| SQL Server | 2016 | 2019+ |
| RAM | 2 GB | 4+ GB |
| Disk Space | 1 GB | 5+ GB |
| IIS Version | 10 | 10+ |

## 🔒 Security Checklist

Before going live:

- [ ] Change JWT Secret Key
- [ ] Use HTTPS/SSL everywhere
- [ ] Configure CORS properly
- [ ] Enable logging and monitoring
- [ ] Use environment variables for secrets
- [ ] Implement rate limiting
- [ ] Enable authentication on sensitive endpoints
- [ ] Regular database backups

## 🐛 Troubleshooting

| Issue | Solution |
|-------|----------|
| Database connection fails | Verify connection string and firewall rules |
| .NET Runtime not found | Install .NET 10 Runtime on server |
| 500 Internal Server Error | Check application logs and event viewer |
| Static files not loading | Verify wwwroot folder uploaded |
| CORS errors | Check CORS configuration in appsettings |

## 📍 Application URLs

Once deployed:

- **API Base:** `https://your-domain.com`
- **Swagger UI:** `https://your-domain.com/swagger/index.html`
- **API Documentation:** `https://your-domain.com/swagger/swagger.json`

## 📞 Support

For ASP Monster support, contact:
- Website: [ASP Monster website]
- Support Email: [ASP Monster support email]
- Documentation: [ASP Monster docs]

## 📚 Additional Resources

- [ASP.NET Core Deployment Guide](https://docs.microsoft.com/aspnet/core/host-and-deploy)
- [IIS Configuration for ASP.NET Core](https://docs.microsoft.com/aspnet/core/host-and-deploy/iis)
- [Entity Framework Core Migrations](https://docs.microsoft.com/ef/core/managing-schemas/migrations)

---

**Your application is ready for deployment!** 🎉

All files in the `publish` folder are production-ready. Follow the steps above to deploy to ASP Monster.
