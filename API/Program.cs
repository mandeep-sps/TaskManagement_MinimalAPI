

var builder = WebApplication.CreateBuilder(args);
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
            builder =>
            {
                builder.WithOrigins("*")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
            });
});
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    

}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWTKey"])),
        ValidateIssuer = false,
        ValidateLifetime = false,
        ValidateAudience = false

    };
});
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task Management API's", Version = "v1", Description = "For Internal use only." });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer",
                            }
                        },
                        new string[]{}
                    }
                });
});


builder.Services.Configure<ConnectionString>(builder.Configuration.GetSection("ConnectionString"));
builder.Services.Configure<string>(builder.Configuration.GetSection("JWTKey"));
builder.Services.AddScoped<IRepository, RepositoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseSwagger();
app.UseSwaggerUI(c => c.DocumentTitle = ".Net 6 Minimal API");

app.UseAuthentication();
app.UseAuthorization();



app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.MapProjectRoutes();

app.MapApplicationUserRoutes(builder.Configuration);

await app.RunAsync();