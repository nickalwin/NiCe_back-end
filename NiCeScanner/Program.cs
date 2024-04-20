using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NiCeScanner.Data;
using NiCeScanner.Controllers;

var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// development database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DevDatabase")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
	options.AddPolicy("RequireManagerRole", policy => policy.RequireRole("Admin", "Manager"));
	options.AddPolicy("RequireResearcherRole", policy => policy.RequireRole("Admin", "Manager", "Researcher"));
	options.AddPolicy("RequireStudentRole", policy => policy.RequireRole("Admin", "Manager", "Researcher", "Student"));
	options.AddPolicy("RequireMemberRole", policy => policy.RequireRole("Admin", "Manager", "Researcher", "Student", "Member"));
});

builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new() { Title = "NiCeScanner", Version = "v1" });
});

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll",
		builder =>
		{
			builder.AllowAnyOrigin()
				   .AllowAnyMethod()
				   .AllowAnyHeader();
		});
});

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
	dbContext.Database.Migrate();
}

ApplicationDbInitializer.Seed(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NiCeScanner v1"));
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("AllowAll");


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	var roles = new List<string> { "Admin", "Manager", "Researcher", "Student", "Member" };

	foreach (var role in roles)
	{
		if (!await roleManager.RoleExistsAsync(role))
		{
			roleManager.CreateAsync(new IdentityRole(role)).Wait();
		}
	}
}
using (var scope = app.Services.CreateScope())
{
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
	string name = "Admin";
	string email = "admin@admin.com";
	string password = "Admin@123";
	if (await userManager.FindByEmailAsync(email) == null)
	{
		var user = new IdentityUser { UserName = email, Email = email };
		await userManager.CreateAsync(user, password);
		userManager.AddToRoleAsync(user, "Admin");
	}

	userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
	name = "Manager";
	email = "manager@manager.com";
	password = "Manager@123";
	if (await userManager.FindByEmailAsync(email) == null)
	{
		var user = new IdentityUser { UserName = email, Email = email };
		await userManager.CreateAsync(user, password);
		userManager.AddToRoleAsync(user, "Manager");
	}

	userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
	name = "Researcher";
	email = "researcher@researcher.com";
	password = "Researcher@123";
	if (await userManager.FindByEmailAsync(email) == null)
	{
		var user = new IdentityUser { UserName = email, Email = email };
		await userManager.CreateAsync(user, password);
		userManager.AddToRoleAsync(user, "Researcher");
	}

	userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
	name = "Student";
	email = "student@student.com";
	password = "Student@123";
	if (await userManager.FindByEmailAsync(email) == null)
	{
		var user = new IdentityUser { UserName = email, Email = email };
		await userManager.CreateAsync(user, password);
		userManager.AddToRoleAsync(user, "Student");
	}

	userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
	name = "Member";
	email = "member@member.com";
	password = "Member@123";
	if (await userManager.FindByEmailAsync(email) == null)
	{
		var user = new IdentityUser { UserName = email, Email = email };
		await userManager.CreateAsync(user, password);
		userManager.AddToRoleAsync(user, "Member");
	}
}

app.Run();
