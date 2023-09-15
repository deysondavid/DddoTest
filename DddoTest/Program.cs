using DddoTest.Models;
using Microsoft.EntityFrameworkCore;

using DddoTest.Services.Contrato;
using DddoTest.Services.Implementacion;
using DddoTest.DTOs;
using DddoTest.Utilidades;

using AutoMapper;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TestDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

builder.Services.AddScoped<IMonedaServices, MonedaService>();
builder.Services.AddScoped<ISucursalServices, SucursalServices>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


#region PETICIONES API REST
app.MapGet("/moneda/lista", async (
    IMonedaServices _monedaServices,
    IMapper _mapper
    ) =>
{
    var listaMoneda = await _monedaServices.GetList();
    var listaMonedaDTO = _mapper.Map<List<MonedaDTO>>(listaMoneda);
    if (listaMonedaDTO.Count > 0)
    {
        return Results.Ok(listaMonedaDTO);
    }
    else
    {
        return Results.NotFound();
    }

});

app.MapGet("/sucursal/lista", async (
    ISucursalServices _sucursalServices,
    IMapper _mapper
    ) =>
{
    var listaSucursal = await _sucursalServices.GetList();
    var listaSucursalDTO = _mapper.Map<List<SucursalDTO>>(listaSucursal);
    if (listaSucursalDTO.Count > 0)
    {
        return Results.Ok(listaSucursalDTO);
    }
    else
    {
        return Results.BadRequest();
    }

});

app.MapPost("/sucursal/guardar", async (
    SucursalDTO modelo,
    ISucursalServices _sucursalServicio,
    IMapper _mapper
    ) =>
{
    var _sucursal = _mapper.Map<SucursalesDdo>(modelo);
    var _sucursalCreado = await _sucursalServicio.Add(_sucursal);

    if (_sucursalCreado.IdSucursal != 0)
        return Results.Ok(_mapper.Map<SucursalDTO>(_sucursalCreado));
    else
        return Results.StatusCode(StatusCodes.Status500InternalServerError);

});

app.MapPut("/sucursal/editar/{idSucursal}", async (
    int idSucursal,
    SucursalDTO modelo,
    ISucursalServices _sucursalServicio,
    IMapper _mapper
    ) => {
        var _encontrado = await _sucursalServicio.Get(idSucursal);
        if(_encontrado is null)return Results.NotFound();

        var _sucursal = _mapper.Map<SucursalesDdo>(modelo);

        _encontrado.IdMoneda = _sucursal.IdMoneda;
        _encontrado.Identificacion = _sucursal.Identificacion;
        _encontrado.Direccion = _sucursal.Direccion;
        _encontrado.Descripcion = _sucursal.Descripcion;
        _encontrado.Codigo = _sucursal.Codigo;
        _encontrado.FechaCreacion = _sucursal.FechaCreacion;

        var respuesta = await _sucursalServicio.Update(_encontrado);
        if (respuesta)
            return Results.Ok(_mapper.Map<SucursalDTO>(_encontrado));
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);

    });

app.MapDelete("/sucursal/eliminar/{idSucursal}", async (
    int idSucursal,
    ISucursalServices _sucursalServicio
    ) => {
        var _encontrado = await _sucursalServicio.Get(idSucursal);

        if (_encontrado is null) return Results.NotFound();

        var respuesta = await _sucursalServicio.Delete(_encontrado);

        if (respuesta)
            return Results.Ok();
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
    });

#endregion


app.UseCors("NuevaPolitica");

app.Run();

