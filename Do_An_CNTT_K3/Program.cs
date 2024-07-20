using Do_An_CNTT_K3.Data;
using Do_An_CNTT_K3.Models.Interfaces;
using Do_An_CNTT_K3.Models.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ILienLacRepository, LienLacRepository>();
builder.Services.AddScoped<IBanHang, BanHang>();
builder.Services.AddScoped<IThongTinSanPham, ThongTinSanPham>();
builder.Services.AddScoped<IShoppingProduct>(sp => ShoppingProduct.GetCart(sp));
builder.Services.AddDbContext<FoodDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FoodDbContextConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // Thêm dòng này ?? b?t tính n?ng vai trò
    .AddEntityFrameworkStores<FoodDbContext>();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();

var app = builder.Build();

// Kh?i t?o vai trò m?c ??nh
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    var roles = new[] { "Admin", "User", "Seller"}; // Thêm các vai trò c?n thi?t

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // T?o tài kho?n Admin m?c ??nh n?u ch?a có
    var adminEmail = "admin@example.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
        await userManager.CreateAsync(adminUser, "Admin@1234");
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}

app.UseSession();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SanPham}/{action=TrangChu}/{id?}");

app.Run();
