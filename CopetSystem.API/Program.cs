using CopetSystem.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);
{
    // Adição dos Controllers
    builder.Services.AddControllers();

    // Realiza comunicação com os demais Projetos.
    builder.Services.AddInfrastructure(builder.Configuration);

    // Configuração do Swagger
    builder.Services.AddInfrastructureSwagger();

    // Permite que rotas sejam acessíveis em lowercase
    builder.Services.AddRouting(options => options.LowercaseUrls = true);
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHsts();
    app.UseHttpsRedirection();

    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}