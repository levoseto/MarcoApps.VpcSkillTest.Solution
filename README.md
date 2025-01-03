# Sistema de Gestión de Refacciones entre Talleres

## Planteamiento del Problema

### Contexto General
La empresa "Talleres Unidos" cuenta con una red de talleres mecánicos que comparten refacciones y piezas para la reparación de vehículos. Sin embargo, las piezas no siempre están disponibles en todos los talleres. Por lo tanto, surge la necesidad de un sistema que permita coordinar la solicitud, envío y registro de instalación de refacciones entre talleres, optimizando los tiempos y asegurando la trazabilidad de las piezas.

## Objetivo del Sistema

Desarrollar una solución que permita:

- Consultar un catálogo de refacciones disponibles en la red de talleres a partir del VIN del vehículo.
- Gestionar la solicitud de refacciones entre talleres, incluyendo:
  - Geolocalización del taller solicitante.
  - Registro del mecánico que realiza la solicitud.
  - Registro del mecánico que realiza el envío desde el taller proveedor.
- Registrar la instalación de la refacción en el taller solicitante una vez que esta ha sido enviada y recibida.

## Requerimientos Funcionales

### Catálogo de Refacciones y Talleres

- Cada taller tendrá un catálogo de refacciones con sus existencias.
- Se podrá buscar una refacción a partir del VIN del vehículo.
- Si una refacción no está disponible en un taller, se podrá consultar su existencia en otros talleres.

### Gestión de Solicitudes de Refacciones

El mecánico del taller solicitante deberá:

- Seleccionar el VIN del vehículo.
- Elegir la pieza necesaria.
- Registrar la solicitud especificando el taller proveedor.
- Incluir la geolocalización del taller solicitante.

### Gestión de Envíos de Refacciones

El mecánico del taller proveedor podrá:

- Ver las solicitudes pendientes.
- Confirmar el envío de la pieza.
- Registrar los datos del envío (fecha, hora y refacción enviada).

### Registro de Instalación de Refacciones

Una vez recibida la pieza, el mecánico del taller solicitante podrá:

- Registrar la instalación de la pieza.
- Confirmar que la refacción fue correctamente instalada en el vehículo.

------------------------------------------------------------------
# Sistema de Gestión para Talleres Mecánicos

## Planteamiento del Ejercicio

Desarrolla una aplicación móvil utilizando .NET MAUI y un backend en ASP.NET Core Web API para gestionar las operaciones de un taller mecánico. El objetivo principal es permitir la solicitud, envío e instalación de refacciones entre diferentes talleres, además de proporcionar información clara y accesible sobre las actividades realizadas.

## Requisitos

### Módulo de Autenticación

- Implementa un inicio de sesión funcional en la aplicación móvil.
- Al autenticar, obtén los datos del mecánico (nombre, taller al que pertenece) y utiliza esta información globalmente en la aplicación.
- Almacena de forma segura los datos de sesión con persistencia.

### Gestión de Solicitudes

- Permitir que un taller realice solicitudes de refacciones a otro taller, indicando:
  - Vehículo (mediante su VIN).
  - Refacción requerida.
  - Ubicación del taller solicitante (latitud y longitud).
  - Mecánico que realiza la solicitud.
- Mostrar las solicitudes realizadas en un listado y permitir editar o eliminar las solicitudes existentes.

### Gestión de Envíos

- Registrar el envío de refacciones de un taller a otro.
- Actualizar automáticamente el inventario de refacciones en ambos talleres:
  - Restar la pieza enviada del taller proveedor.
  - Sumar la pieza en el taller solicitante.
- Cambiar el estado de la solicitud a "Completado" tras realizar el envío.

### Gestión de Instalaciones

- Registrar la instalación de refacciones en los vehículos.
- Seleccionar la refacción desde el inventario local del taller o desde una solicitud previa.
- Capturar la ubicación (latitud y longitud) donde se realiza la instalación.

### Pantalla Principal (HomePage)

- Mostrar un saludo personalizado con el nombre del mecánico autenticado y el taller.
- Resumen informativo con indicadores rápidos:
  - Total de solicitudes realizadas.
  - Total de solicitudes recibidas pendientes.
  - Total de instalaciones completadas.
- Accesos directos a funcionalidades clave:
  - Solicitudes realizadas.
  - Solicitudes recibidas.
  - Registro de instalaciones.

### Integración Visual

- Diseñar un flujo limpio y moderno para la aplicación móvil.
- Incorporar íconos personalizados para las secciones principales: Home, Solicitudes Realizadas, Solicitudes Recibidas, Instalaciones, y Logout.
- Hacer uso de imágenes dinámicas que cambien según el tema (claro u oscuro).

### Backend con ASP.NET Core Web API

- Crear un backend que permita realizar las siguientes operaciones:
  - CRUD de solicitudes, envíos e instalaciones.
  - Gestión del inventario de refacciones.
  - Autenticación y validación de usuarios (mecánicos).
- Base de datos SQL para almacenar:
  - Talleres, mecánicos, vehículos, refacciones, inventario, solicitudes, envíos e instalaciones.
- Exponer endpoints RESTful para la integración con la aplicación móvil.

