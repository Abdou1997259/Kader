using Kader_System.Api.Helpers;
using Kader_System.Api.Helpers.SwaggerHelper;
using Kader_System.DataAccess.DbMiddlewares;
using Kader_System.DataAccess.Models;
using Kader_System.DataAccess.Repositories;
using Kader_System.DataAccess.Repositories.EmployeeRequests;
using Kader_System.DataAccess.Repositories.HR;
using Kader_System.DataAccess.Repositories.Logging;
using Kader_System.DataAccess.Repositories.StaticDataRepository;
using Kader_System.DataAccess.Repositories.Trans;
using Kader_System.DataAccesss.Context;
using Kader_System.Domain;
using Kader_System.Domain.Customization.Middleware;
using Kader_System.Domain.Interfaces;
using Kader_System.Domain.Interfaces.EmployeeRequest;
using Kader_System.Domain.Interfaces.HR;
using Kader_System.Domain.Interfaces.Logging;
using Kader_System.Domain.Interfaces.StaticDataRepository;
using Kader_System.Domain.Interfaces.Trans;
using Kader_System.Domain.Options;
using Kader_System.Domain.SwaggerFilter;
using Kader_System.Services.IServices;
using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.EmployeeRequests.PermessionRequests;
using Kader_System.Services.IServices.EmployeeRequests.Requests;
using Kader_System.Services.IServices.HTTP;
using Kader_System.Services.IServices.InterviewServices;
using Kader_System.Services.IServices.Trans;
using Kader_System.Services.Services;
using Kader_System.Services.Services.AppServices;
using Kader_System.Services.Services.Auth;
using Kader_System.Services.Services.EmployeeRequests.PermessionRequests;
using Kader_System.Services.Services.EmployeeRequests.Requests;
using Kader_System.Services.Services.HR;
using Kader_System.Services.Services.HTTP;
using Kader_System.Services.Services.InterviewServices;
using Kader_System.Services.Services.Setting;
using Kader_System.Services.Services.Trans;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(Shared.CorsPolicy,
//        builder => builder
//            .AllowAnyOrigin()
//            .AllowAnyMethod()
//            .AllowAnyHeader());
//});
builder.Services.AddCors();
builder.Services.AddControllers(op =>
{
    op.Filters.Add<PermissionFilter>();
    // op.Filters.Add<DeflateCompressionAttribute>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
}).AddEntityFrameworkStores<KaderDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    //x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    //x.JsonSerializerOptions.Converters.Add(new DateTimeConverter("yyyy-MM-dd"));
}
    );




builder.Services.AddDbContext<KaderAuthorizationContext>(options =>
 options.UseSqlServer(builder.Configuration.
 GetConnectionString("KaderAuthorizationConnection"))
 );
builder.Services.AddDbContext<KaderDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString(Shared.KaderSystemConnection),
     b => b.MigrationsAssembly(typeof(KaderDbContext).Assembly.FullName)));

#region JWT config

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(Shared.JwtSettings));

var jwtSettings = new JwtSettings();
builder.Configuration.Bind(nameof(jwtSettings), jwtSettings);
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = false;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecretKey)),
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidIssuer = jwtSettings.Issuer,
        ClockSkew = TimeSpan.Zero
    };

    o.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query[Shared.AccessToken];

            // If the request is for our hub...
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments(Shared.Notify))
            {
                // Read the token out of the query string
                context.Request.Headers.Authorization = accessToken;
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});
builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.Zero;
});

#endregion

#region Localization config

builder.Services.AddLocalization(options => options.ResourcesPath = Shared.Resources);

var supportedCultures = Shared.Cultures;
var localizationOptions =
    new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[1])
.AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

builder.Services.AddControllers().AddDataAnnotationsLocalization(options =>
{
    options.DataAnnotationLocalizerProvider = (type, factory) =>
        factory.Create(typeof(DataAnnotationValidation));
});


builder.Services.Configure<RequestLocalizationOptions>(options =>
{

    var cultures = new[]
    {
        new CultureInfo("en"),
        new CultureInfo("ar")
    };

    options.DefaultRequestCulture = new RequestCulture(uiCulture: "en", culture: "en");
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});
builder.Services.AddMvcCore().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);


#endregion

#region Swagger config

builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc(Modules.Auth, new OpenApiInfo
    {
        Title = $"{Shared.KaderSystem} {Modules.Auth}",
        Version = Modules.V1
    });
    x.SwaggerDoc(Modules.Setting, new OpenApiInfo
    {
        Title = $"{Shared.KaderSystem} {Modules.Setting}",
        Version = Modules.V1
    });
    x.SwaggerDoc(Modules.HR, new OpenApiInfo
    {
        Title = $"{Shared.KaderSystem} {Modules.HR}",
        Version = Modules.V1
    });
    x.SwaggerDoc(Modules.Trans, new OpenApiInfo
    {
        Title = $"{Shared.KaderSystem} {Modules.Trans}",
        Version = Modules.V1
    });
    x.SwaggerDoc(Modules.EmployeeRequest, new OpenApiInfo
    {
        Title = $"{Shared.KaderSystem} {Modules.EmployeeRequest}",
        Version = Modules.V1
    });
    x.SwaggerDoc(Modules.Interview, new OpenApiInfo
    {
        Title = $"{Shared.KaderSystem} {Modules.Interview}",
        Version = Modules.V1

    });
    x.AddSecurityDefinition(Modules.Bearer, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field Like => Bearer yourToken....",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = Modules.Bearer,
        BearerFormat = "Bearer",

    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
     {
       Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id =  Modules.Bearer,

       },
        In = ParameterLocation.Header,
                Name=  Modules.Bearer,
            },
            Array.Empty<string>()
        }
  });
    x.SchemaFilter<SwaggerTest>();
    x.OperationFilter<AddHeadersOperationFilter>();
});


#endregion

#region DI

builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandlerService>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProviderService>();
builder.Services.AddScoped<ILoanRequestService, LoanRequesService>();
builder.Services.AddSingleton<IStaticDataRepository, StaticDataRepository>();
builder.Services.AddScoped<IStructureMangement, StructureMangement>();
builder.Services.AddScoped<IScreenService, ScreenService>();
builder.Services.AddScoped<IResignationRequestService, ResignationRequestService>();
builder.Services.AddScoped<IFileServer, FileServer>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbInitSeedsService, DbInitSeedsService>();
builder.Services.AddScoped<IPermService, PermService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ILoggingRepository, LoggingRepository>();
builder.Services.AddScoped<IScreenCategoryService, ScreenCategoryService>();
builder.Services.AddScoped<IMainScreenService, MainScreenService>();
builder.Services.AddScoped<ISubScreenService, SubScreenService>();

builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IAllowanceService, AllowanceService>();
builder.Services.AddScoped<IBenefitService, BenefitService>();
builder.Services.AddScoped<IDeductionService, DeductionService>();
builder.Services.AddScoped<IQualificationService, QualificationService>();
builder.Services.AddScoped<IShiftService, ShiftService>();
builder.Services.AddScoped<IHrJobService, HrJobService>();
builder.Services.AddScoped<IVacationService, VacationService>();
builder.Services.AddScoped<IManagementService, ManagementService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<IFingerPrintDeviceService, FingerPrintDeviceService>();
builder.Services.AddScoped<ITransAllowanceService, TransAllowanceService>();
builder.Services.AddScoped<ITransBenefitService, TransBenefitService>();
builder.Services.AddScoped<ITransDeductionService, TransDeductionService>();
builder.Services.AddScoped<ITransVacationService, TransVacationService>();
builder.Services.AddScoped<ITransCovenantService, TransCovenantService>();
builder.Services.AddScoped<ITransLoanService, TransLoanService>();
builder.Services.AddScoped<ITransSalaryIncreaseService, TransSalaryIncreaseService>();
builder.Services.AddScoped<ITransCalcluateSalaryService, TransCalcluateSalaryService>();
builder.Services.AddScoped<ISalaryIncreaseTypeRepository, SalaryIncreaseTypeRepository>();
builder.Services.AddScoped<ITransSalaryIncreaseRepository, TransSalaryIncreaseRepository>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<ITitleService, TitleService>();
builder.Services.AddScoped<IPermessionStructureService, PermessionStructureService>();
builder.Services.AddScoped<IUserPermessionService, UserPermessionService>();
builder.Services.AddScoped<ITitlePermessionService, TitlePermessionService>();
builder.Services.AddScoped<IGetAllScreensService, GetAllScreensService>();
builder.Services.AddScoped<IHttpContextService, HttpContextService>();
builder.Services.AddScoped<IEmployeeNotesServices, EmployeeNotesServices>();
builder.Services.AddScoped<IApplicantServices, ApplicantServices>();
builder.Services.AddScoped<IInterJobServices, InterJobServices>();
builder.Services.AddScoped<IUserContextService, UserContextService>();

#region Employee_Requests
builder.Services.AddScoped<IEmployeeRequestsRepository, EmployeeRequestsRepository>();
builder.Services.AddScoped<IVacationRequestService, VacationRequestService>();
builder.Services.AddScoped<ILeavePermissionRequestService, LeavePermissionRequestService>();
builder.Services.AddScoped<IDelayPermissionService, DelayPermissionService>();
builder.Services.AddScoped<IAllowanceRequestService, AllowanceRequestService>();
builder.Services.AddScoped<ISalaryIncreaseRequestService, SalaryIncreaseRequestService>();
builder.Services.AddScoped<IContractTerminationRequestService, ContractTerminationRequestService>();
#endregion
#endregion
var httpPort = builder.Configuration.GetValue<int>("KestrelServer:Http.Port");
var httpsPort = builder.Configuration.GetValue<int>("KestrelServer:Https.Port");
var httpsCertificateFilePath = builder.Configuration.GetValue<string>("KestrelServer:Https.CertificationFilePath");
var httpsCertificatePassword = builder.Configuration.GetValue<string>("KestrelServer:Https.CertificationPassword");
builder.WebHost.UseIIS();
builder.WebHost.UseIISIntegration();

////Support Self-Host by Kestrel Server
//builder.WebHost.UseKestrel(kestrelServerOptions =>
//{
//    if (string.IsNullOrEmpty(httpsCertificateFilePath) || string.IsNullOrEmpty(httpsCertificatePassword))
//    {
//        //if no certification then no ssl then run use http
//        kestrelServerOptions.Limits.MaxConcurrentConnections = 100;
//        kestrelServerOptions.Limits.MaxConcurrentUpgradedConnections = 100;
//        kestrelServerOptions.Limits.MaxRequestBodySize = 10 * 1024;
//        kestrelServerOptions.Limits.MinRequestBodyDataRate =
//            new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
//        kestrelServerOptions.Limits.MinResponseDataRate =
//            new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
//        kestrelServerOptions.ListenAnyIP(httpPort);

//    }
//    else
//{
//    //if exist certificate then run as HTTPS
//    kestrelServerOptions.ListenAnyIP(httpsPort, (listenOptions) =>
//    {
//        listenOptions.UseHttps(httpsCertificateFilePath, httpsCertificatePassword);
//    });
//}

//});

var app = builder.Build();

#region To take an instance from specific repository

var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
using var scope = scopedFactory!.CreateScope();
ILoggingRepository loggingRepository = scope.ServiceProvider.GetService<ILoggingRepository>()!;

#endregion

#region Seed cliams

try
{
    SeedData(app);
}
catch (Exception ex)
{
    Log.Error(ex, "Error When Seedding Data");
}


static async void SeedData(IHost app) //can be placed at the very bottom under app.Run()
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using var scope = scopedFactory!.CreateScope();
    var dbInitializer = scope.ServiceProvider.GetService<IDbInitSeedsService>();
    //loggingRepository = scope.ServiceProvider.GetService<ILoggingRepository>()!;
    await dbInitializer!.SeedClaimsForSuperAdmin();
}
#endregion



app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint($"/swagger/{Modules.Auth}/swagger.json", "Auth_Management v1");
    x.SwaggerEndpoint($"/swagger/{Modules.Setting}/swagger.json", "Setting_Management v1");
    x.SwaggerEndpoint($"/swagger/{Modules.HR}/swagger.json", "HR_Management v1");
    x.SwaggerEndpoint($"/swagger/{Modules.Trans}/swagger.json", "Transaction_Management v1");
    x.SwaggerEndpoint($"/swagger/{Modules.EmployeeRequest}/swagger.json", "Employee_Request v1");
    x.SwaggerEndpoint($"/swagger/{Modules.Interview}/swagger.json", "Interview");
});
//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment() || app.Environment.IsProduction() || app.Environment.IsEnvironment(Shared.Local))
// {


//     Log.Information("end of swagger");
//}
app.UseMiddleware<ExceptionMiddleware>();
app.ConfigureExceptionHandler(loggingRepository);    // custom as a global exception
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRequestLocalization(localizationOptions);
app.LocalizationMiddleWaresHandler();
app.UseRouting();

//app.UseExceptionHandler();
/*app.ConfigureStaticFilesHandler();       */            // custom as Static files

app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseMiddleware<HeadersValidationMiddleware>();
app.UseMiddleware<PathMiddleware>();
app.UseMiddleware<ClientDatabaseMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSerilogRequestLogging();
app.MapGet(string.Empty, (context) =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});

app.MapGet("env", async (context) => await context.Response.WriteAsync(app.Environment.EnvironmentName));

try
{
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application Error When Building");
}
finally
{
    Log.CloseAndFlush();
}