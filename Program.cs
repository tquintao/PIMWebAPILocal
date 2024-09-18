var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços ao contêiner de dependências
builder.Services.AddControllers();

// Adiciona a configuração para injeção de dependência do ClienteRepository
builder.Services.AddScoped<PIMWebAPILocal.Repositories.ClienteRepository>();

// Adiciona o serviço de documentação Swagger para visualização dos endpoints da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do middleware do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware para roteamento
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
