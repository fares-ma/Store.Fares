# شرح نشر Store.Fares على ASP Monster

## 📦 محتويات مجلد النشر (publish)

جميع الملفات المطلوبة للتشغيل موجودة في مجلد `publish`:
- `.exe` و `.dll` و `.pdb` ملفات التطبيق
- ملفات الإعدادات (`appsettings.*.json`)
- جميع المكتبات المطلوبة
- ملف `web.config` لخادم IIS

## 🚀 خطوات النشر على ASP Monster

### الخطوة 1: تجهيز قاعدة البيانات

قبل رفع الملفات، تأكد من:

1. **إنشاء قاعدة البيانات:**
   - اسم قاعدة البيانات: `Store.Fares.App`
   - حفظ بيانات الاتصال

2. **الحصول على بيانات الاتصال من ASP Monster:**
   ```
   Server: [من لوحة التحكم]
   Database: [اسم قاعدتك]
   User ID: [اسم المستخدم]
   Password: [كلمة المرور]
   ```

### الخطوة 2: تحديث ملف الإعدادات

حدّث ملف `appsettings.Production.json` في مجلد `publish`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=Store.Fares.App;User Id=YOUR_USER;Password=YOUR_PASSWORD;Encrypt=true;TrustServerCertificate=false;",
    "Redis": "YOUR_REDIS_CONNECTION"
  },
  "BaseUrl": "https://YOUR_DOMAIN.com",
  "jwtOptions": {
    "SecretKey": "CHANGE_TO_SECURE_SECRET_KEY_AT_LEAST_32_CHARS!",
    "Audience": "StoreFaresApp",
    "Issuer": "StoreFares",
    "DurationInDays": 7
  }
}
```

### الخطوة 3: رفع الملفات

#### الطريقة 1: استخدام FTP (الأسهل)

1. افتح برنامج FTP (مثل FileZilla)
2. اتصل بخادم ASP Monster باستخدام:
   - Host: [من ASP Monster]
   - Username: [اسم المستخدم]
   - Password: [كلمة المرور]
   - Port: 21
3. اذهب إلى مجلد التطبيق (عادة `/httpdocs` أو `/public_html`)
4. رفع جميع ملفات من مجلد `publish`

#### الطريقة 2: استخدام Web Deploy

```powershell
# من PowerShell في مجلد المشروع
dotnet publish -c Release -p:PublishProfile=FTP
```

### الخطوة 4: تشغيل Migrations

بعد رفع الملفات، شغّل migrations من خلال:

1. **عبر سطر الأوامر (إن أمكن):**
   ```powershell
   cd c:\Users\Rocket\Desktop\Store\Store.Fares
   dotnet ef database update -c StoreDbContext
   dotnet ef database update -c StoreIdentityDbContext
   ```

2. **أو من خلال ASP Monster Control Panel:**
   - تأكد من أن المتطلبات موجودة على الخادم

### الخطوة 5: تكوين الموقع

في لوحة تحكم ASP Monster:

1. **ربط Domain:**
   - أضف domain واشر إلى مجلد التطبيق

2. **تفعيل HTTPS:**
   - استخدم SSL Certificate المجاني من Let's Encrypt

3. **متطلبات التشغيل:**
   - .NET 10 Runtime
   - IIS Module: `AspNetCoreModuleV2`

## ⚙️ متطلبات الخادم

- **.NET Runtime:** 10.0 أو أحدث
- **قاعدة البيانات:** SQL Server (محلي أو سحابي)
- **Redis** (اختياري): للـ Caching والـ Basket
- **IIS:** الإصدار 10 أو أحدث

## 📝 ملفات الإعدادات

```
publish/
├── appsettings.json              # إعدادات عامة
├── appsettings.Development.json  # إعدادات التطوير (تجاهل)
└── appsettings.Production.json   # إعدادات الإنتاج (حدّث هذا!)
```

## 🔐 نصائح الأمان

⚠️ **قبل النشر تأكد من:**

1. ✅ تغيير `JWT Secret Key` إلى قيمة آمنة
2. ✅ استخدام متغيرات البيئة للـ Secrets
3. ✅ تفعيل HTTPS/SSL
4. ✅ تحديد قواعد الـ CORS بشكل صحيح
5. ✅ تفعيل Logging في الإنتاج
6. ✅ عدم حفظ Passwords في ملفات الإعدادات

## 🆘 استكشاف الأخطاء

### خطأ: "Database connection failed"
- تحقق من Connection String
- تأكد من أن قاعدة البيانات مُنشأة

### خطأ: ".NET Runtime not found"
- ثبت .NET 10 Runtime على الخادم

### خطأ: "Port already in use"
- غيّر Port في `Program.cs` أو البيئة

## 📞 معلومات مفيدة

- **Swagger API:** `https://YOUR_DOMAIN.com/swagger/index.html`
- **Health Check:** `https://YOUR_DOMAIN.com/health`
- **Logs:** تتوفر في مجلد التطبيق أو Windows Event Viewer

---

**تم إعداد المشروع للنشر بنجاح!** ✨

بمجرد اتمام الخطوات أعلاه، سيكون تطبيقك متاحاً على الإنترنت.
