"# Biblioteca" 
Crear un Libro (POST)
URL: /libro/
Método: POST
Descripción: Crea un nuevo libro. Este endpoint requiere que pases un cuerpo JSON con los detalles del libro a crear.
Campos:
libro: Un objeto JSON con los detalles del libro (por ejemplo, título, autor, etc.).
Ejemplo de JSON para el cuerpo:
json
Copiar código
{
    "codigo": "001"
    "titulo": "El Gran Libro",
    "autor": "Juan Pérez",
    "urlimagen": ""
}
Respuesta esperada:
200 OK: Si el libro fue creado exitosamente.
400 Bad Request: Si hubo un error al crear el libro.
Obtener Todos los Libros (GET)
URL: /libro/


Método: GET
Descripción: Obtiene todos los libros. Este endpoint permite filtrar y ordenar los resultados utilizando parámetros opcionales.
Parámetros:
sortField: El campo por el cual ordenar los libros.
algorithm: Algoritmo para ordenar (si aplica). 
keysearch: Palabra clave para búsqueda.
search: Parámetro de búsqueda.
Ejemplo de consulta en Swagger:
sortField=autor&keysearch=Juan
Respuesta esperada:
200 OK: Si se obtienen los libros correctamente.
400 Bad Request: Si hubo un error en la consulta.
Obtener un Libro por ID (GET)
URL: /libro/{id}


Método: GET
Descripción: Obtiene un libro según su ID.
Parámetros:
id: El ID del libro que se desea obtener.
Ejemplo de solicitud:
Para obtener un libro con ID 1, usa el endpoint /libro/1.
Respuesta esperada:
200 OK: Si se obtiene el libro correctamente.
400 Bad Request: Si el ID no existe o hay algún error.
Actualizar un Libro (PUT)
URL: /libro/{id}

Método: PUT
Descripción: Actualiza un libro existente utilizando su ID. Debes enviar un objeto JSON con los nuevos datos.
Campos:
libro: Un objeto JSON con los detalles actualizados del libro (por ejemplo, título, autor, etc.).
Ejemplo de JSON:
json
Copiar código
{
    "codigo": "001"
    "titulo": "El Libro",
    "autor": "Juan Pérez",
    "urlimagen": ""
}
Respuesta esperada:
200 OK: Si el libro fue actualizado exitosamente.
400 Bad Request: Si hubo un error al actualizar el libro.
Eliminar un Libro (DELETE)
URL: /libro/{id}

Método: DELETE

Descripción: Elimina un libro según su ID.

Parámetros:

id: El ID del libro que deseas eliminar.
Ejemplo de solicitud:

Para eliminar el libro con ID 1, usa el endpoint /libro/1.
Respuesta esperada:

200 OK: Si el libro fue eliminado correctamente.
400 Bad Request: Si hubo un error al eliminar el libro.
Contar los Libros (GET)
URL: /libro/count


Método: GET

Descripción: Devuelve el número total de libros en la base de datos.

Respuesta esperada:

200 OK: Si se obtiene el número total de libros.
400 Bad Request: Si hubo un error.
Buscar un Libro por un Campo Específico (GET)
URL: /libro/search

Método: GET

Descripción: Busca un libro según un campo específico y su valor.

Parámetros:

key: El campo de búsqueda (por ejemplo, "autor", "titulo").
value: El valor a buscar en el campo especificado.
Ejemplo de consulta:

key=autor&value=Juan Pérez
Respuesta esperada:

200 OK: Si se obtiene el libro que coincide con los parámetros de búsqueda.
400 Bad Request: Si hubo un error en la consulta.
