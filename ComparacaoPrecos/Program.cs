using Microsoft.EntityFrameworkCore;
using ComparacaoPrecos.Data;
using ComparacaoPrecos.Service;
using ComparacaoPrecos.Repository;
using Microsoft.AspNetCore.Identity;
using ComparacaoPrecos.Areas.Identity.Data;
using ComparacaoPrecos.Models; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddScoped<CategoriaRepository>();
builder.Services.AddScoped<ProdutoLojaRepository>();
builder.Services.AddScoped<LojaRepository>();
builder.Services.AddScoped<UserRepository>();

builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ProdutoLojaService>();
builder.Services.AddScoped<LojaService>();
builder.Services.AddScoped<UserService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.Password.RequireDigit = false; 
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
    .AddRoles<IdentityRole>() 
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await SeedAdminAsync(services);
        await SeedLojasAsync(services); 
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erro durante a seed de inicialização");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();

async Task SeedAdminAsync(IServiceProvider serviceProvider)
{
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string adminEmail = "admin@gmail.com";
    string adminPassword = "Admin@123";

    string[] roles = { "Admin", "User" };
    foreach (var roleName in roles)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
            Console.WriteLine("Administrador criado com sucesso!");
        }
        else
        {
            Console.WriteLine("Erro ao criar administrador: " + string.Join(", ", result.Errors));
        }
    }
    else
    {
        Console.WriteLine("Administrador já existe.");
    }
}

async Task SeedLojasAsync(IServiceProvider serviceProvider)
{
    using (var context = serviceProvider.GetRequiredService<ApplicationDbContext>())
    {
        if (!context.Loja.Any())
        {
            var lojas = new List<Loja>
            {
                new Loja { Nome = "Continente", Latitude = (double)41.14961m, Longitude = (double)-8.61099m, Deleted = false },
                new Loja { Nome = "Pingo Doce", Latitude = (double)40.6389m, Longitude = (double)-8.6553m, Deleted = false },
                new Loja { Nome = "Lidl", Latitude = (double)38.7223m, Longitude = (double)-9.1393m, Deleted = false },
                new Loja { Nome = "Minipreço", Latitude = (double)39.7445m, Longitude = (double)-8.4150m, Deleted = false }
            };
            await context.Loja.AddRangeAsync(lojas);
            await context.SaveChangesAsync();
            Console.WriteLine("Dados de Lojas teste criados com sucesso!");
        }
        else
        {
            Console.WriteLine("Dados de Lojas já existem.");
        }
    }
}
