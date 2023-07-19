using Explorer.Application.Services;
using Explorer.DAL;
using Explorer.Domain.Interfaces;
using Explorer.Helpers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IFolderService, FolderService>();
builder.Services.AddTransient<IFileTypeService, FileTypeService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllersWithViews();

var app = builder.Build();

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
    pattern: "{controller=Explorer}/{action=Index}/{id?}");

app.Run();
