# AspSwagger

AspSwagger es una aplicación desarrollada en C# que expone una API REST JSON para administración de países de acuerdo al [ISO 3166-1](https://en.m.wikipedia.org/wiki/ISO_3166-1). Se utilizó ASP Core 5.0 basandose en las especificaciones de OpenAPI 3.0 y las herramientas de Swagger incorporadas en *Visual Studio for Mac versión 8.9 Community Edition*.

Se incluye una SPA autogenerada para consumir la API al ejecutar el proyecto:

`https://localhost:5001/swagger/index.html`

CORS está habilitado para que la API se pueda coNsumir por aplicaciones cliente externas. Para configurar una url de origen específica, se debe agregar al archivo Startup.cs como se muestra a continuación:

```
public class Startup
{

    private const string CorsConfiguration = "_corsConfiguration";
    private const string CorsOriginUrl = "https://localhost:5000";
    
    ...
}
```
