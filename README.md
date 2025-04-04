# 🔐 PasswordManagerService - Gestor de Contraseñas con Microservicios (.NET 8)

Este proyecto es un sistema completo de gestión de contraseñas desarrollado como ejercicio práctico para aprender e implementar **arquitectura de microservicios**, **patrones de diseño**, **Docker**, **CQRS**, **JWT**, y despliegue en **Microsoft Azure**.

La aplicación está dividida en microservicios independientes construidos en **.NET 8**, utilizando bases de datos como **MySQL** y **MongoDB**, y empaquetados con Docker para facilitar su orquestación y despliegue.

---

## 🧩 Microservicios

| Microservicio | Descripción | Requiere JWT |
|---------------|-------------|---------------|
| `AuthService` | Registro, inicio de sesión, emisión de JWT | ✅ |
| `PasswordGeneratorService` | Generación de contraseñas aleatorias o personalizadas | ❌ |
| `PasswordStorageCommandService` | Almacenamiento de contraseñas (solo si usuario está autenticado) | ✅ |
| `PasswordQueryService` | Recuperación de contraseñas guardadas por el usuario | ✅ + MFA |
| `GatewayAPI` | Punto único de entrada, enrutamiento y seguridad | ✅ |

---

## 🎯 Objetivos de aprendizaje

- Aplicar arquitectura de microservicios con .NET 8
- Diseñar APIs REST limpias y seguras
- Implementar CQRS: separación de comandos y consultas
- Usar JWT para autenticación y autorización
- Almacenar datos en **MySQL** (comandos) y **MongoDB** (consultas)
- Dockerizar los servicios con `Dockerfile` y `docker-compose`
- Preparar despliegue en Microsoft Azure

---

## ⚙️ Tecnologías y herramientas

- `.NET 8`, `C#`, `ASP.NET Core`
- `MySQL` y `MongoDB`
- `JWT` para autenticación
- `Docker` y `docker-compose`
- `Visual Studio Code`, `dotnet CLI`
- `Swagger` para documentación de API
- `Azure` (objetivo de despliegue futuro)

---

## 📦 Estructura general del proyecto

```plaintext
PasswordManagerService/
├── docker/                      ← Dockerfiles y docker-compose
├── docs/                        ← Documentación técnica
├── src/                         ← Código fuente de todos los microservicios
│   ├── AuthService/
│   ├── PasswordGeneratorService/
│   ├── PasswordStorageCommandService/
│   ├── PasswordQueryService/
│   └── GatewayAPI/
└── README.md                    ← Este archivo
```

## Como clonar el repositorio

```bash
git clone https://github.com/leomarqz/PasswordManagerService.git
cd PasswordManagerService
```

## Como levantar los microservicios

```bash
docker-compose up --build
```