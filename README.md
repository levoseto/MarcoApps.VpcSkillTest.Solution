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
