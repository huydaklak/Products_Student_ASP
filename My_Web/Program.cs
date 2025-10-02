using Microsoft.EntityFrameworkCore;
using My_Web.Interfaces;
using My_Web.Models;
using My_Web.Services;

var builder = WebApplication.CreateBuilder(args);

#region Cấu hình dịch vụ (Services)

// Thêm dịch vụ MVC (Controller + View)
builder.Services.AddControllersWithViews();

// Thêm Session (dùng để lưu giỏ hàng, thông tin đăng nhập,...)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // thời gian hết hạn session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Đăng ký các Service nghiệp vụ (DI - Dependency Injection)
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Cấu hình Entity Framework Core với MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 30)) // version MySQL
    ));

#endregion

var app = builder.Build();

#region Cấu hình Middleware (Pipeline)

// Nếu môi trường không phải Development thì bật trang lỗi và HSTS
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/User/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection(); // ép HTTPS
app.UseStaticFiles();      // cho phép đọc file tĩnh (css, js, img)

app.UseRouting();          // bật định tuyến
app.UseSession();          // bật session
app.UseAuthorization();    // bật phân quyền (nếu có)

#endregion

#region Cấu hình Route (Endpoints)

// ✅ Route cho Admin Area
app.MapControllerRoute(
    name: "admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "Admin" }
);

// ✅ Route cho User Area
app.MapControllerRoute(
    name: "user",
    pattern: "User/{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "User" }
);

// ✅ Route mặc định → trỏ về User Area
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "User" }
);

#endregion

app.Run();
