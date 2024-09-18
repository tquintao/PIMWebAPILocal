var builder = WebApplication.CreateBuilder(args);

// Adiciona os servi�os ao cont�iner de depend�ncias
builder.Services.AddControllers();

// Adiciona a configura��o para inje��o de depend�ncia do ClienteRepository
builder.Services.AddScoped<PIMWebAPILocal.Repositories.ClienteRepository>();

// Adiciona o servi�o de documenta��o Swagger para visualiza��o dos endpoints da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura��o do middleware do Swagger
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
