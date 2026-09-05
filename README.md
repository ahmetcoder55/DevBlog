### 📝 DevBlog - N-Katmanlı Mimari MVC Blog Projesi

**DevBlog**, kurumsal yazılım standartlarına uygun olarak **N-Tier Architecture (N-Katmanlı Mimari)** ve **ASP.NET Core MVC** kullanılarak geliştirilmiş, modern ve güvenli bir blog yönetim sistemidir. Proje, güçlü bir üyelik sistemi (**Identity**) ve gelişmiş bir **Admin Paneli / Dashboard** altyapısına sahiptir. 

### 🏗️ Mimari ve Katman Yapısı

Proje, kodun sürdürülebilirliği ve test edilebilirliği için **4 ana katman** üzerine inşa edilmiştir: 

* **DevBlog.WebUI:** Kullanıcı arayüzü, Admin Paneli, Dashboard, Controller ve View bileşenlerinin yer aldığı sunum katmanı.
* **DevBlog.Business:** İş kurallarının (Validation, Business Logic) ve servis kayıtlarının (Extension Methods) yönetildiği katman.
* **DevBlog.DataAccess:** Veritabanı entegrasyonu, AppDbContext, Entity Framework Core konfigürasyonları ve Migrations dosyalarının bulunduğu katman.
* **DevBlog.Core:** Tüm Katmanlardan ortak alınan yapılar buraya entegre edilmiştir ayriyeten Entities Katmanıda buradadır.
### 🔒 Güvenlik ve Kimlik Yönetimi (Identity)

Uygulama, modern web güvenliği standartları (Production-Ready) dikkate alınarak yapılandırılmıştır: 

* **Sıkı Şifre Politikası:** Brute-force saldırılarını önlemek amacıyla şifrelerde büyük/küçük harf, rakam, özel karakter zorunluluğu ve minimum 8 karakter limiti aktif edilmiştir.
* **Hesap Kilitleme (Lockout):** Üst üste 5 kez hatalı giriş denemesinde hesap **15 dakika** boyunca otomatik olarak kilitlenir.
* **Benzersiz E-posta:** Sistemde aynı e-posta adresiyle mükerrer kayıt açılması engellenmiştir.
* **Gelişmiş Cookie Güvenliği:** 

  * HttpOnly = true ile XSS saldırılarına karşı çerez koruması sağlanmıştır.
  * SameSite.Strict ve SecurePolicy.Always ile CSRF (Siteler Arası İstek Sahtekarlığı) engellenmiştir.
  * SlidingExpiration özelliği ile kullanıcı aktif olduğu sürece 2 saatlik oturum süresi otomatik yenilenir.
* **Rol Bazlı Yetkilendirme:** /Admin/* yolları ve Dashboard paneli, sadece Admin rolüne sahip kullanıcıların erişimine [Authorize(Roles = "Admin")] korumasıyla açılmıştır.

### 🛠️ Kullanılan Teknolojiler

* **Backend:** .NET 10.0 + / C#
* **UI Architecture:** ASP.NET Core MVC (Model-View-Controller)
* **ORM:** Entity Framework Core
* **Database:** MS SQL Server
* **Authentication:** ASP.NET Core Identity
* **Lisans:** MIT License


