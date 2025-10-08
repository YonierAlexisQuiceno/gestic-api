# GesticApi – API para el catálogo de servicios TIC

Este proyecto contiene una API REST básica construida con **ASP.NET Core 8** y **Entity Framework Core** para gestionar el catálogo de servicios TIC, las solicitudes de servicio y las métricas de cumplimiento de ANS. La API está diseñada para funcionar con una base de datos PostgreSQL preexistente llamada `gestic_db`, que se puede crear con el script SQL que se proporcionó previamente.

## Requisitos previos

- [.NET 8 SDK](https://dotnet.microsoft.com/). Puedes comprobar tu versión con `dotnet --version`.
- PostgreSQL con un usuario y contraseña válidos.
- La base de datos `gestic_db` creada mediante pgAdmin o la línea de comandos, y las tablas `servicios`, `solicitudes` y `metricas_sla` creadas ejecutando el script `postgres_db_script.sql`.

## Configuración

1. Clona o copia el directorio `gestic_api` en tu máquina local.
2. Ajusta la cadena de conexión en `appsettings.json` para que coincida con tus credenciales y el puerto de PostgreSQL. La cadena predeterminada utiliza:
   
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=gestic_db;Username=postgres;Password=root"
     }
   }
   ```
   
   Asegúrate de reemplazar `postgres` y `root` por tu usuario y contraseña reales si son diferentes.

3. Desde una terminal, navega al directorio `gestic_api` y restaura los paquetes NuGet:
   ```bash
   dotnet restore
   ```

4. Ejecuta la API:
   ```bash
   dotnet run
   ```
   Por defecto la aplicación escucha en `http://localhost:5000` y `https://localhost:5001`. En el navegador podrás acceder a la documentación Swagger en `https://localhost:5001/swagger` para probar los endpoints.

## Endpoints principales

| Método | Ruta | Descripción |
|-------:|------|-------------|
| `GET`  | `/api/services` | Obtiene todos los servicios. |
| `GET`  | `/api/services/{id}` | Obtiene un servicio por su ID. |
| `POST` | `/api/services` | Crea un nuevo servicio (envía JSON con los campos). |
| `PUT`  | `/api/services/{id}` | Actualiza un servicio existente. |
| `DELETE` | `/api/services/{id}` | Elimina un servicio. |
| `GET`  | `/api/solicitudes` | Obtiene todas las solicitudes. |
| `POST` | `/api/solicitudes` | Crea una nueva solicitud. |
| `GET`  | `/api/solicitudes/{id}` | Obtiene una solicitud por ID. |
| `PUT`  | `/api/solicitudes/{id}` | Actualiza una solicitud. |
| `DELETE` | `/api/solicitudes/{id}` | Elimina una solicitud. |
| `GET`  | `/api/metricas` | Obtiene todas las métricas SLA. |
| `POST` | `/api/metricas` | Crea una nueva métrica. |
| `GET`  | `/api/metricas/{id}` | Obtiene una métrica por ID. |
| `PUT`  | `/api/metricas/{id}` | Actualiza una métrica existente. |
| `DELETE` | `/api/metricas/{id}` | Elimina una métrica. |

## Migraciones y cambios en el esquema

Esta API está configurada para mapear las entidades a las tablas existentes definidas en tu base de datos. Como ya tienes la base creada manualmente, **no es necesario ejecutar migraciones**. Si en un futuro quieres gestionar el esquema con Entity Framework, puedes añadir los paquetes necesarios y ejecutar `dotnet ef migrations add Inicial` y `dotnet ef database update`. Ten en cuenta que esto sobrescribirá o modificará tu esquema actual.

## Seguridad y despliegue

Esta API es solo una base de partida. Para un entorno de producción se recomienda:

- Configurar variables de entorno para la cadena de conexión en lugar de dejar la contraseña en `appsettings.json`.
- Añadir autenticación y autorización para proteger los endpoints sensibles.
- Implementar validaciones más robustas y manejo de errores.

Con esta estructura deberías poder empezar a trabajar con tu base de datos `gestic_db` desde una API REST .NET minimalista.# gestic-api
