// using Microsoft.EntityFrameworkCore;
// using ComparacaoPrecos.Data;
// using ComparacaoPrecos.Service;
// using ComparacaoPrecos.Repository;
// using Microsoft.AspNetCore.Identity;
// using ComparacaoPrecos.Areas.Identity.Data;
// using ComparacaoPrecos.Models; // Added for Loja, Produto, ProdutoLoja models

// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddScoped<ProdutoRepository>();
// builder.Services.AddScoped<CategoriaRepository>();
// builder.Services.AddScoped<ProdutoLojaRepository>(); // Manter este nome para o serviço/repositório
// builder.Services.AddScoped<LojaRepository>();
// builder.Services.AddScoped<UserRepository>();

// builder.Services.AddScoped<ProdutoService>();
// builder.Services.AddScoped<CategoriaService>();
// builder.Services.AddScoped<ProdutoLojaService>(); // Manter este nome para o serviço
// builder.Services.AddScoped<LojaService>();
// builder.Services.AddScoped<UserService>();


// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// builder.Services.AddControllers();


// // Add services to the container.
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseNpgsql(connectionString));
// builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
// {
//     options.Password.RequireDigit = false; 
//     options.Password.RequireLowercase = false;
//     options.Password.RequireUppercase = false;
//     options.Password.RequireNonAlphanumeric = false;
//     options.Password.RequiredLength = 6;
// })
//     .AddRoles<IdentityRole>() 
//     .AddEntityFrameworkStores<ApplicationDbContext>();

// var app = builder.Build();

// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     // OBTER O DB CONTEXT UMA ÚNICA VEZ AQUI
//     var context = services.GetRequiredService<ApplicationDbContext>(); 
//     try
//     {
//         // SeedAdminAsync ainda precisa do serviceProvider para UserManager/RoleManager
//         await SeedAdminAsync(services); 
//         // Agora, passar a mesma instância de 'context' para os métodos de seeding
//         await SeedLojasAsync(context); 
//         await SeedProdutosAsync(context);
//         await SeedProdutoLojasAsync(context);
//     }
//     catch (Exception ex)
//     {
//         var logger = services.GetRequiredService<ILogger<Program>>();
//         logger.LogError(ex, "Erro durante a seed de inicialização");
//     }
// }

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseMigrationsEndPoint();
// }
// else
// {
//     app.UseExceptionHandler("/Error");
//     app.UseHsts();
// }

// app.UseHttpsRedirection();
// app.UseStaticFiles();
// app.UseRouting();
// app.UseAuthentication();
// app.UseAuthorization();

// app.MapControllers();
// app.MapRazorPages();

// app.Run();

// async Task SeedAdminAsync(IServiceProvider serviceProvider)
// {
//     var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
//     var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

//     string adminEmail = "admin@gmail.com";
//     string adminPassword = "Admin@123";

//     // Create "Admin" and "User" roles if they don't exist
//     string[] roles = { "Admin", "User" };
//     foreach (var roleName in roles)
//     {
//         if (!await roleManager.RoleExistsAsync(roleName))
//         {
//             await roleManager.CreateAsync(new IdentityRole(roleName));
//         }
//     }

//     // Check if admin user already exists
//     var adminUser = await userManager.FindByEmailAsync(adminEmail);
//     if (adminUser == null)
//     {
//         adminUser = new ApplicationUser
//         {
//             UserName = adminEmail,
//             Email = adminEmail,
//             EmailConfirmed = true
//         };

//         var result = await userManager.CreateAsync(adminUser, adminPassword);
//         if (result.Succeeded)
//         {
//             await userManager.AddToRoleAsync(adminUser, "Admin");
//             Console.WriteLine("Administrador criado com sucesso!");
//         }
//         else
//         {
//             Console.WriteLine("Erro ao criar administrador: " + string.Join(", ", result.Errors));
//         }
//     }
//     else
//     {
//         Console.WriteLine("Administrador já existe.");
//     }
// }

// // Assinatura alterada para receber ApplicationDbContext diretamente
// async Task SeedLojasAsync(ApplicationDbContext context)
// {
//     // Removido o 'using (var context = ...)' interno
//     if (!context.Loja.Any())
//     {
//         var lojas = new List<Loja>
//         {
//             // Casts explícitos para double
//             new Loja { LojaID = 1, Nome = "Continente", Latitude = (double)41.14961m, Longitude = (double)-8.61099m, Deleted = false },
//             new Loja { LojaID = 2, Nome = "Pingo Doce", Latitude = (double)40.6389m, Longitude = (double)-8.6553m, Deleted = false },
//             new Loja { LojaID = 3, Nome = "Lidl", Latitude = (double)38.7223m, Longitude = (double)-9.1393m, Deleted = false },
//             new Loja { LojaID = 4, Nome = "Minipreço", Latitude = (double)39.7445m, Longitude = (double)-8.4150m, Deleted = false },
//             new Loja { LojaID = 5, Nome = "Mercadona", Latitude = (double)40.6389m, Longitude = (double)-8.6553m, Deleted = false }
//         };
//         await context.Loja.AddRangeAsync(lojas);
//         await context.SaveChangesAsync();
//         Console.WriteLine("Dados de Lojas teste criados com sucesso!");
//     }
//     else
//     {
//         Console.WriteLine("Dados de Lojas já existem.");
//     }
// }

// // Assinatura alterada para receber ApplicationDbContext diretamente
// async Task SeedProdutosAsync(ApplicationDbContext context)
// {
//     // Removido o 'using (var context = ...)' interno
//     if (!context.Produto.Any())
//     {
//         // Certificar que as categorias existem antes de semear produtos que dependem delas
//         // Assumindo que Categoria tem um campo 'Nome' baseado noutros testes/modelos que forneceu
//         var categoriaFrutas = new Categoria { CategoriaID = "FRUTAS", Deleted = false };
//         var categoriaEletronicos = new Categoria { CategoriaID = "ELETRONICOS",  Deleted = false };
//         var categoriaLaticinios = new Categoria { CategoriaID = "LATICINIOS",  Deleted = false };

//         if (!context.Categoria.Any(c => c.CategoriaID == categoriaFrutas.CategoriaID)) context.Categoria.Add(categoriaFrutas);
//         if (!context.Categoria.Any(c => c.CategoriaID == categoriaEletronicos.CategoriaID)) context.Categoria.Add(categoriaEletronicos);
//         if (!context.Categoria.Any(c => c.CategoriaID == categoriaLaticinios.CategoriaID)) context.Categoria.Add(categoriaLaticinios);
//         await context.SaveChangesAsync();


//         var produtos = new List<Produto>
//         {
//             // Assumindo que Produto tem um campo 'PrecoMedio' baseado noutros testes/modelos que forneceu
//             new Produto { ProdutoID = 1, Nome = "Banana",  CategoriaID = "FRUTAS", Deleted = false },
//             new Produto { ProdutoID = 2, Nome = "Maçã",  CategoriaID = "FRUTAS", Deleted = false },
//             new Produto { ProdutoID = 3, Nome = "Leite",  CategoriaID = "LATICINIOS", Deleted = false },
//             new Produto { ProdutoID = 4, Nome = "Televisão",CategoriaID = "ELETRONICOS", Deleted = false }
//         };
//         await context.Produto.AddRangeAsync(produtos);
//         await context.SaveChangesAsync();
//         Console.WriteLine("Dados de Produtos teste criados com sucesso!");
//     }
//     else
//     {
//         Console.WriteLine("Dados de Produtos já existem.");
//     }
// }

// // Assinatura alterada para receber ApplicationDbContext diretamente
// async Task SeedProdutoLojasAsync(ApplicationDbContext context)
// {
//     // Removido o 'using (var context = ...)' interno
//     // Alterado para 'Produto_Loja' e removido o campo 'Preco'
//     if (!context.Produto_Loja.Any()) // Corrigido o nome do DbSet para Produto_Loja
//     {
//         // Certificar que os produtos e lojas existem antes de criar associações
//         var banana = await context.Produto.SingleOrDefaultAsync(p => p.Nome == "Banana");
//         var maca = await context.Produto.SingleOrDefaultAsync(p => p.Nome == "Maçã");
//         var leite = await context.Produto.SingleOrDefaultAsync(p => p.Nome == "Leite");
//         var televisao = await context.Produto.SingleOrDefaultAsync(p => p.Nome == "Televisão");

//         var continente = await context.Loja.SingleOrDefaultAsync(l => l.Nome == "Continente");
//         var pingoDoce = await context.Loja.SingleOrDefaultAsync(l => l.Nome == "Pingo Doce");
//         var lidl = await context.Loja.SingleOrDefaultAsync(l => l.Nome == "Lidl");
//         var minipreco = await context.Loja.SingleOrDefaultAsync(l => l.Nome == "Minipreço");
//         var mercadona = await context.Loja.SingleOrDefaultAsync(l => l.Nome == "Mercadona");

//         var produtoLojas = new List<Produto_Loja>(); // Corrigido o nome da lista para Produto_Loja

//         if (banana != null && continente != null)
//             produtoLojas.Add(new Produto_Loja { ProdutoID = banana.ProdutoID, LojaID = continente.LojaID, DataHora = DateTime.UtcNow }); // Removido Preco
//         if (banana != null && pingoDoce != null)
//             produtoLojas.Add(new Produto_Loja { ProdutoID = banana.ProdutoID, LojaID = pingoDoce.LojaID, DataHora = DateTime.UtcNow }); // Removido Preco
//         if (banana != null && lidl != null)
//             produtoLojas.Add(new Produto_Loja { ProdutoID = banana.ProdutoID, LojaID = lidl.LojaID, DataHora = DateTime.UtcNow }); // Removido Preco

//         if (maca != null && continente != null)
//             produtoLojas.Add(new Produto_Loja { ProdutoID = maca.ProdutoID, LojaID = continente.LojaID, DataHora = DateTime.UtcNow }); // Removido Preco
//         if (maca != null && pingoDoce != null)
//             produtoLojas.Add(new Produto_Loja { ProdutoID = maca.ProdutoID, LojaID = pingoDoce.LojaID, DataHora = DateTime.UtcNow }); // Removido Preco

//         if (leite != null && lidl != null)
//             produtoLojas.Add(new Produto_Loja { ProdutoID = leite.ProdutoID, LojaID = lidl.LojaID, DataHora = DateTime.UtcNow }); // Removido Preco
//         if (leite != null && minipreco != null)
//             produtoLojas.Add(new Produto_Loja { ProdutoID = leite.ProdutoID, LojaID = minipreco.LojaID, DataHora = DateTime.UtcNow }); // Removido Preco

//         if (televisao != null && mercadona != null)
//             produtoLojas.Add(new Produto_Loja { ProdutoID = televisao.ProdutoID, LojaID = mercadona.LojaID, DataHora = DateTime.UtcNow }); // Removido Preco
//         if (televisao != null && continente != null)
//             produtoLojas.Add(new Produto_Loja { ProdutoID = televisao.ProdutoID, LojaID = continente.LojaID, DataHora = DateTime.UtcNow }); // Removido Preco

//         await context.Produto_Loja.AddRangeAsync(produtoLojas); // Corrigido o nome do DbSet para Produto_Loja
//         await context.SaveChangesAsync();
//         Console.WriteLine("Dados de ProdutoLoja teste criados com sucesso!"); // Mantido o texto da mensagem
//     }
//     else
//     {
//         Console.WriteLine("Dados de ProdutoLoja já existem."); // Mantido o texto da mensagem
//     }
// }
using Microsoft.EntityFrameworkCore;
using ComparacaoPrecos.Data;
using ComparacaoPrecos.Service;
using ComparacaoPrecos.Repository;
using Microsoft.AspNetCore.Identity;
using ComparacaoPrecos.Areas.Identity.Data;
using ComparacaoPrecos.Models; // Added for Loja, Produto, Produto_Loja models

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddScoped<CategoriaRepository>();
builder.Services.AddScoped<ProdutoLojaRepository>(); // Manter este nome para o serviço/repositório
builder.Services.AddScoped<LojaRepository>();
builder.Services.AddScoped<UserRepository>();

builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ProdutoLojaService>(); // Manter este nome para o serviço
builder.Services.AddScoped<LojaService>();
builder.Services.AddScoped<UserService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();


// Add services to the container.
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
    // OBTER O DB CONTEXT UMA ÚNICA VEZ AQUI
    var context = services.GetRequiredService<ApplicationDbContext>(); 
    try
    {
        // SeedAdminAsync ainda precisa do serviceProvider para UserManager/RoleManager
        await SeedAdminAsync(services); 
        // Agora, passar a mesma instância de 'context' para os métodos de seeding
        await SeedLojasAsync(context); 
        await SeedProdutosAsync(context);
        await SeedProdutoLojasAsync(context, services); // Passar 'services' para obter UserManager para o ID do admin
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erro durante a seed de inicialização");
    }
}

// Configure the HTTP request pipeline.
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

    // Create "Admin" and "User" roles if they don't exist
    string[] roles = { "Admin", "User" };
    foreach (var roleName in roles)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Check if admin user already exists
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

// Assinatura alterada para receber ApplicationDbContext diretamente
async Task SeedLojasAsync(ApplicationDbContext context)
{
    // Removido o 'using (var context = ...)' interno
    if (!context.Loja.Any())
    {
        var lojas = new List<Loja>
        {
            // Latitude e Longitude como double (literais double 'd' ou 'm' com cast explícito)
            new Loja { LojaID = 1, Nome = "Continente", Latitude = 41.14961d, Longitude = -8.61099d, Deleted = false },
            new Loja { LojaID = 2, Nome = "Pingo Doce", Latitude = 40.6389d, Longitude = -8.6553d, Deleted = false },
            new Loja { LojaID = 3, Nome = "Lidl", Latitude = 38.7223d, Longitude = -9.1393d, Deleted = false },
            new Loja { LojaID = 4, Nome = "Minipreço", Latitude = 39.7445d, Longitude = -8.4150d, Deleted = false },
            new Loja { LojaID = 5, Nome = "Mercadona", Latitude = 40.6389d, Longitude = -8.6553d, Deleted = false }
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

// Assinatura alterada para receber ApplicationDbContext diretamente
async Task SeedProdutosAsync(ApplicationDbContext context)
{
    // Removido o 'using (var context = ...)' interno
    if (!context.Produto.Any())
    {
        // Certificar que as categorias existem antes de semear produtos que dependem delas
        // Categoria AGORA NÃO TEM CAMPO 'Nome' no seu modelo, apenas CategoriaID e Deleted
        var categoriaFrutas = new Categoria { CategoriaID = "FRUTAS", Deleted = false };
        var categoriaEletronicos = new Categoria { CategoriaID = "ELETRONICOS", Deleted = false };
        var categoriaLaticinios = new Categoria { CategoriaID = "LATICINIOS", Deleted = false };

        // Adicionar apenas se não existirem
        if (!context.Categoria.Any(c => c.CategoriaID == categoriaFrutas.CategoriaID)) context.Categoria.Add(categoriaFrutas);
        if (!context.Categoria.Any(c => c.CategoriaID == categoriaEletronicos.CategoriaID)) context.Categoria.Add(categoriaEletronicos);
        if (!context.Categoria.Any(c => c.CategoriaID == categoriaLaticinios.CategoriaID)) context.Categoria.Add(categoriaLaticinios);
        await context.SaveChangesAsync();


        var produtos = new List<Produto>
        {
            // Removido o campo 'PrecoMedio' de Produto, conforme o seu modelo
            new Produto { ProdutoID = 1, Nome = "Banana", CategoriaID = "FRUTAS", Deleted = false },
            new Produto { ProdutoID = 2, Nome = "Maçã", CategoriaID = "FRUTAS", Deleted = false },
            new Produto { ProdutoID = 3, Nome = "Leite", CategoriaID = "LATICINIOS", Deleted = false },
            new Produto { ProdutoID = 4, Nome = "Televisão", CategoriaID = "ELETRONICOS", Deleted = false }
        };
        await context.Produto.AddRangeAsync(produtos);
        await context.SaveChangesAsync();
        Console.WriteLine("Dados de Produtos teste criados com sucesso!");
    }
    else
    {
        Console.WriteLine("Dados de Produtos já existem.");
    }
}

// Assinatura alterada para receber ApplicationDbContext e IServiceProvider
async Task SeedProdutoLojasAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
{
    // Removido o 'using (var context = ...)' interno
    // Alterado o nome do DbSet para Produto_Loja
    if (!context.Produto_Loja.Any()) 
    {
        // Certificar que os produtos e lojas existem antes de criar associações
        var banana = await context.Produto.SingleOrDefaultAsync(p => p.Nome == "Banana");
        var maca = await context.Produto.SingleOrDefaultAsync(p => p.Nome == "Maçã");
        var leite = await context.Produto.SingleOrDefaultAsync(p => p.Nome == "Leite");
        var televisao = await context.Produto.SingleOrDefaultAsync(p => p.Nome == "Televisão");

        var continente = await context.Loja.SingleOrDefaultAsync(l => l.Nome == "Continente");
        var pingoDoce = await context.Loja.SingleOrDefaultAsync(l => l.Nome == "Pingo Doce");
        var lidl = await context.Loja.SingleOrDefaultAsync(l => l.Nome == "Lidl");
        var minipreco = await context.Loja.SingleOrDefaultAsync(l => l.Nome == "Minipreço");
        var mercadona = await context.Loja.SingleOrDefaultAsync(l => l.Nome == "Mercadona");

        // Obter o ID do utilizador administrador para a FK em Produto_Loja
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var adminUser = await userManager.FindByEmailAsync("admin@gmail.com");
        string adminUserId = adminUser?.Id;

        var produtoLojas = new List<Produto_Loja>(); 

        // Adicionar apenas se os produtos/lojas e o ID do admin existirem
        if (banana != null && continente != null && adminUserId != null)
            produtoLojas.Add(new Produto_Loja { ProdutoID = banana.ProdutoID, LojaID = continente.LojaID, DataHora = DateTime.UtcNow, preco = 1.60, credibilidade = 5, Id = adminUserId });
        if (banana != null && pingoDoce != null && adminUserId != null)
            produtoLojas.Add(new Produto_Loja { ProdutoID = banana.ProdutoID, LojaID = pingoDoce.LojaID, DataHora = DateTime.UtcNow, preco = 1.45, credibilidade = 4, Id = adminUserId });
        if (banana != null && lidl != null && adminUserId != null)
            produtoLojas.Add(new Produto_Loja { ProdutoID = banana.ProdutoID, LojaID = lidl.LojaID, DataHora = DateTime.UtcNow, preco = 1.55, credibilidade = 5, Id = adminUserId });

        if (maca != null && continente != null && adminUserId != null)
            produtoLojas.Add(new Produto_Loja { ProdutoID = maca.ProdutoID, LojaID = continente.LojaID, DataHora = DateTime.UtcNow, preco = 2.10, credibilidade = 4, Id = adminUserId });
        if (maca != null && pingoDoce != null && adminUserId != null)
            produtoLojas.Add(new Produto_Loja { ProdutoID = maca.ProdutoID, LojaID = pingoDoce.LojaID, DataHora = DateTime.UtcNow, preco = 1.95, credibilidade = 5, Id = adminUserId });

        if (leite != null && lidl != null && adminUserId != null)
            produtoLojas.Add(new Produto_Loja { ProdutoID = leite.ProdutoID, LojaID = lidl.LojaID, DataHora = DateTime.UtcNow, preco = 0.85, credibilidade = 3, Id = adminUserId });
        if (leite != null && minipreco != null && adminUserId != null)
            produtoLojas.Add(new Produto_Loja { ProdutoID = leite.ProdutoID, LojaID = minipreco.LojaID, DataHora = DateTime.UtcNow, preco = 0.95, credibilidade = 5, Id = adminUserId });

        if (televisao != null && mercadona != null && adminUserId != null)
            produtoLojas.Add(new Produto_Loja { ProdutoID = televisao.ProdutoID, LojaID = mercadona.LojaID, DataHora = DateTime.UtcNow, preco = 449.99, credibilidade = 4, Id = adminUserId });
        if (televisao != null && continente != null && adminUserId != null)
            produtoLojas.Add(new Produto_Loja { ProdutoID = televisao.ProdutoID, LojaID = continente.LojaID, DataHora = DateTime.UtcNow, preco = 460.00, credibilidade = 5, Id = adminUserId });

        await context.Produto_Loja.AddRangeAsync(produtoLojas); 
        await context.SaveChangesAsync();
        Console.WriteLine("Dados de ProdutoLoja teste criados com sucesso!"); 
    }
    else
    {
        Console.WriteLine("Dados de ProdutoLoja já existem."); 
    }
}

