using Microsoft.EntityFrameworkCore;
using CompanySystem.DAL;
using CompanySystem.BL;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("defaultAuthen").AddJwtBearer("defaultAuthen",options=>
{
    #region Key
    var SecretKey = builder.Configuration.GetValue<string>("SecretKey");
    var SecretKeyInByte = Encoding.ASCII.GetBytes(SecretKey);
    var key = new SymmetricSecurityKey(SecretKeyInByte);
    #endregion
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey=key,
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

#region Configuration
//configuration read from 3 places:
//1-appsettings.json
//2-environment variables -> using in production
//3-User Secrets ->most recommended for credintial data according to the security  -> using in deployment
#endregion
var connectionString =builder.Configuration.GetConnectionString("CompanySystem");  
//b3ml register ll context 3ndi fe al program.cs as a service 3shan ay controller y2dar y3melo injection   
builder.Services.AddDbContext<SystemContext>(options => options.UseSqlServer(connectionString));
//builder.Services.AddDbContext<StudentContext>(options => options.UseSqlServer(connectionString));

#region Manager & Repos sercices
builder.Services.AddScoped<IInstructorRepo, InstructorRepo>();
builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
builder.Services.AddScoped<IInstructorManager, InstructorManager>();
builder.Services.AddScoped<IDepartmentManager, DepartmentManager>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
#region MiddleWares pipeLine
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(
async (context, next) =>
//1-logic on request before passing the context to the next middleware
//2-passing context to the next middleware
await next(context)
//3-logic on response from the next middleware
);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
#endregion

//determine  witch controller and action the request have to be direct to
app.MapControllers();


#region filter pipeline
//1-model binding filter
//convert from json object to c# object
//2-model validation filter  -> fill model state
//3-automatic bad request filter (Apis only, could be disapled)

#endregion

//running pipeline
app.Run();
