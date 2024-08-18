using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TransactionService.Application.Command;
using TransactionService.Application.Queries;
using TransactionService.Persistence;
using TransactionService.Persistence.BackgroundJob;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Shared:Authority"];
        options.Audience = builder.Configuration["Shared:Audience"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Shared:Issuer"],
            ValidAudience = builder.Configuration["Shared:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Shared:SigningKey"]))
        };
    });


//builder.Services.AddGrpc();
builder.Services.AddDbContext<TransactionContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHostedService<RecurrentTransactionService>();

// Register MediatR and its handlers
builder.Services.AddMediatR(typeof(CreateRecurrentTransactionHandler).Assembly);
builder.Services.AddMediatR(typeof(GetTransactionHandler).Assembly);
builder.Services.AddMediatR(typeof(CreateTransactionHandler).Assembly);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();