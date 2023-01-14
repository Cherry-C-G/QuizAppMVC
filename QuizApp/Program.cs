using Microsoft.AspNetCore.Authentication.Cookies;
using QuizApp.DAO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<UsersDAO>();
builder.Services.AddScoped<QuizDAO>();
builder.Services.AddScoped<QuestionDAO>();
builder.Services.AddScoped<CategoryDAO>();
builder.Services.AddScoped<AnswerDAO>();
builder.Services.AddScoped<ResultDAO>();
builder.Services.AddScoped<FeedbackDAO>();
builder.Services.AddScoped<QuizQuestionDAO>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Users/Login";
                });


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
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
