# خطوات نشر المشروع على ASP Monster

## 1. تحديث إعدادات الإنتاج

قم بتحديث ملف `appsettings.Production.json` بـ:
- معلومات قاعدة البيانات من ASP Monster
- URL الإنتاج الخاص بك
- JWT Secret Key آمن

## 2. بناء المشروع للإنتاج

```powershell
cd c:\Users\Rocket\Desktop\Store\Store.Fares
dotnet clean
dotnet restore
dotnet build -c Release
```

## 3. نشر المشروع

### الطريقة الأولى: استخدام dotnet publish

```powershell
dotnet publish -c Release -o ./publish
```

هذا سيُنشئ مجلد `publish` يحتوي على جميع الملفات المطلوبة.

### الطريقة الثانية: استخدام Visual Studio

1. افتح المشروع في Visual Studio
2. انقر بزر الماوس الأيمن على `Store.Fares.Api`
3. اختر `Publish`
4. اختر `New Profile` ثم `FTP/SFTP`
5. أدخل تفاصيل الخادم من ASP Monster

## 4. رفع الملفات إلى ASP Monster

### خيار أ: استخدام FTP
استخدم برنامج FTP (مثل FileZilla) لرفع ملفات من مجلد `publish` إلى خادم ASP Monster

### خيار ب: استخدام Web Deploy
```powershell
dotnet publish -c Release -p:PublishProfile=FolderProfile
```

## 5. تكوين قاعدة البيانات

1. تأكد من إنشاء قاعدة البيانات على الخادم
2. قم بتشغيل Migrations:

```powershell
dotnet ef database update -c StoreDbContext
dotnet ef database update -c StoreIdentityDbContext
```

## 6. متطلبات الخادم

- .NET 10 Runtime أو أحدث
- SQL Server أو MySQL
- Redis (اختياري - للـ Caching و Basket)

## 7. متطلبات إضافية في ASP Monster

- تأكد من أن البوابات مفتوحة:
  - Port 5136 (أو المنفذ المُحدد)
  - HTTPS مفعل
- ربط الـ Domain
- تكوين CORS إذا لزم الأمر

---

**ملاحظات مهمة:**
- غير `appsettings.Production.json` بـ values الفعلية قبل النشر
- تأكد من تفعيل HTTPS
- استخدم environment variables لـ secrets بدلاً من تخزينها في الملفات
