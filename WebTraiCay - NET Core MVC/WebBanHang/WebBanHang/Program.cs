using Microsoft.EntityFrameworkCore;
using WebBanHang.Models;
using WebBanHang.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Lấy chuỗi kết nối từ cấu hình QlbanValiContext 
var connectionString = builder.Configuration.GetConnectionString("QlbanValiContext");
// đăng ký lớp QlbanValiContext với dịch vụ của ứng dụng
builder.Services.AddDbContext<QlbanValiContext>(x => x.UseSqlServer(connectionString));
// đăng ký một dịch vụ ILoaiSpRepository và cung cấp triển khai cụ thể của nó là LoaiSpRepository với phạm vi dịch vụ là Scoped
builder.Services.AddScoped<ILoaiSpRepository, LoaiSpRepository>();

// đăng ký Session
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Sử dụng Session
app.UseSession();

app.MapControllerRoute(
    name: "default",
    // pattern : cấu hình Controller đầu tiên trong App muốn chạy
    pattern: "{controller=Home}/{action=Index}/{id?}");
    //pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();
