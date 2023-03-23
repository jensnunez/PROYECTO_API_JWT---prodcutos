# PROYECTO_API_JWT---prodcutos

la api productos genera un crud sencillo el cual consta de un modelo que es una clase con los atributos, la logica de la conexion a la base de datos se genera en el controlador ProductoController que se encarga de practicamente todo, no maneja token porque no se solicito en el ejemplo, el controlados genera 5 metodos 
1. http://localhost:5124/Lista ejecuta un procedimiento almacenado llamado sp_lista_productos
2. http://localhost:5124/Obtener/1 busca por id ejecuta el mismo procedimiento sp_lista_productos pero al final de la ejecucion aplica un where para filtrar el producto por el id
3. http://localhost:5124/Guardar ejecuta un procedimiento almacenado sp_guardar_producto el json que se debe enviar es el siguiente por el verbo post
{
  "id": "2",
  "nombre": "papas",
  "precio": "1230"
}
4. http://localhost:5124/Editar ejecuta un procedimiento almacenado sp_editar_producto el json que se debe enviar es el siguiente por el verbo post
{
4. 
{
  "id": "2",
  "nombre": "papas fritas",
  "precio": "1230"
}
5. http://localhost:5124/Eliminar/2 ejecuta un procedimiento almacenado sp_eliminar_producto se envia el id despues del /

los datos de conexion a la base de datos se deben colocar en appsetting.json

deben crear una base de datos y le ponen el nombre que quieran , una vez creada la base de datos correr el archivo api.sql
