# 🔐 PasswordManagerService - Microservicio de Generación de Contraseñas

Este es un microservicio desarrollado en **.NET 8** bajo el patrón de arquitectura basada en microservicios. Forma parte del proyecto `PasswordManagerService`, cuyo objetivo es ofrecer una solución moderna, escalable y segura para la **gestión de contraseñas**.

Este microservicio en particular se encarga de generar contraseñas seguras, aleatorias o personalizadas, sin requerir autenticación para su uso básico. Se diseñó para integrarse fácilmente con otros servicios como almacenamiento y recuperación de contraseñas, y una pasarela de autenticación mediante JWT y MFA.

---

## 🚀 Tecnologías Utilizadas

- .NET 8 (ASP.NET Core Web API)
- C#
- Arquitectura MVC
- Docker (pendiente)
- Visual Studio Code + .NET CLI
- Estilo Clean Code simplificado

---

## 🧠 Funcionalidad actual

- Generación de contraseñas seguras:
  - Longitud personalizada
  - Inclusión de mayúsculas, minúsculas, números y símbolos
- Si el usuario desea guardar la contraseña pero **no está autenticado**, se muestra un mensaje solicitando inicio de sesión.
- Preparado para integración futura con Gateway API y otros servicios.

---

## 🧩 Estructura del Proyecto

```plaintext
PasswordGeneratorService/
├── PasswordGeneratorService.API/        ← Proyecto principal
│   ├── Controllers/                     ← Controlador REST
│   ├── Services/                        ← Lógica de generación
│   ├── DTOs/                            ← Clases de entrada/salida
│   └── Program.cs                       ← Configuración general
├── PasswordGeneratorService.Tests/      ← Pruebas unitarias (en progreso)
└── PasswordGeneratorService.sln         ← Solución .NET
```

## Como ejecutar el proyecto

```bash
# Restaurar dependencias
dotnet restore

# Compilar la solución
dotnet build

# Ejecutar la API (desde la raíz del proyecto API)
dotnet run --project PasswordGeneratorService.API

```