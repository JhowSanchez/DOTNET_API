using Microsoft.EntityFrameworkCore;
using ProjetoAPI.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Por aqui a gente acessa a conexao e enviar ela para o context para fazer a conexao
builder.Services.AddDbContext<AgendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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


//Criar Classe que iremos trabalhar por exemplo Contato
//Conectar ao banco e criar o migration : add-migration [Nome da Migration]
//Conectado ao banco iremos dar um update-database para upar ao banco
//criar o Controller onde iremos ter os metodos CRUD/Http e os endpoints
