# WebPruebaEcop

Aplicación web desarrollada con Razor Pages para consumir la API del sistema de gestión.

## Tecnologías utilizadas

- **.NET 8** - Framework principal
- **ASP.NET Core Razor Pages** - Para la interfaz de usuario
- **C#** - Lenguaje de programación

## Patrón de diseño

El proyecto implementa el patrón **MVC** y **Repository** para la comunicación con el backend, permitiendo una separación clara entre la lógica de presentación y el acceso a datos a través de la API REST.

## Configuración inicial

Antes de ejecutar el proyecto, es necesario configurar la URL base del backend en el archivo `appsettings.json`:
Cambia el valor de `BaseUrl` según la dirección donde tengas corriendo tu API.
