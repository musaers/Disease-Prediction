using HeartDiseaseAnalysis.Data;
using Microsoft.EntityFrameworkCore;
using HeartDiseaseAnalysis.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

IServiceCollection serviceCollection = builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddScoped<AgeGroupAnalysisService>(); 
builder.Services.AddScoped<BmiAnalysisService>(); 
builder.Services.AddScoped<SleepTimeAnalysisService>();
builder.Services.AddScoped<RaceAnalysisService>();
builder.Services.AddScoped<SmokeAnalysisService>();
builder.Services.AddScoped<DiabetesAnalysisService>();
builder.Services.AddScoped<GenderAnalysisService>();
builder.Services.AddScoped<TensionAnalysisService>();
builder.Services.AddScoped<MarriageAnalysisService>();
builder.Services.AddScoped<WorkAnalysisService>();
builder.Services.AddScoped<BloodSugarAnalysisService>();
builder.Services.AddScoped<AgeAnalysisService>();
builder.Services.AddScoped<SGenderAnalysisService>();
builder.Services.AddScoped<DeathRateAnalysisServiceByYear>();
builder.Services.AddScoped<DeathRateAnalysisServiceByMonth>();
builder.Services.AddScoped<RiskAnalysisService>();






var app = builder.Build();


// Configure the HTTP request pipeline.
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=HeartDiseaseResult}/{id?}"
);








    

app.Run();
