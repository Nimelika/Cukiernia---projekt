using MakeAWishDB.Context;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Services.Shopping;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<SharedData_Entities>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SharedData_Entities")
    )
);

// MVC configuration
builder.Services
    .AddControllersWithViews(options =>
    {
        // Prevent implicit [Required] on non-nullable reference types
        options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    })
    .AddViewOptions(options =>
    {
        // Disable client-side validation (Materialize compatibility)
        options.HtmlHelperOptions.ClientValidationEnabled = false;
    });

// Services
builder.Services.AddScoped<QuoteRequestService>();
builder.Services.AddScoped<CatalogOrderService>();

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();

