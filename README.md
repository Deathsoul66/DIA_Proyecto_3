# DIA_Proyecto_3

Un departamento universitario necesita llevar la cuenta de la producción científica de sus miembros. Para ello necesita una aplicación con los siguientes módulos:

Gestión de miembros del departamento (altas, bajas, modificaciones), salvaguarda y recuperación. (L. Gerardo R. G.)

  - DNI  -> string
  - Nombre -> string  
  - Teléfono -> int 
  - Email -> string
  - Dirección postal -> int 
  
  
Gestión de méritos científicos (altas, bajas, modificaciones), salvaguarda y recuperación. Pueden ser: libros, artículos o comunicación   (en congreso). (Victor e Imanol Cobian)

  - Código DOI o autoasignado si no está disponible el DOI -> string 	(DOI = 10.1000/182 (los num antes de la / indenfican al autor, los de despues al objeto o publicacion) 
  - Título  -> string
  - Editorial  -> string
  - Nombre del Congreso (si es aplicable) -> string  
  - Ciudad de celebración del Congreso (si es aplicable) -> string  
  - Fechas de celebración del Congreso (si es aplicable)  -> date
  - Fecha de publicación  -> datetime
  - Página inicial y página final -> int 
  - Autor(es) (pueden ser miembros del departamento (al menos uno), o no). -> string  
  
  
Informes de producción científica. (Nelson Araujo)
  - Mensual: Mostrará un gráfico con el número de méritos científicos publicados por cada miembro, para los meses del año.
  - Anual: Mostrará el número de méritos de todo el departamento, por mes.
  - Anual: por escrito, todos los méritos de ese año.

Búsquedas. (Marco Aurelio)
  - Permitirá hacer búsquedas filtrando por los siguientes campos (uno o varios a la vez):
    - Miembro del departamento: Mostrará todos los méritos aplicaples para el miembro.
    - Anual: Mostrará el número de méritos de todo el departamento, por año.
    - Tipo de publicación: Mostrará los méritos aplicables según el tipo: artículo, comunicación, libro.

# Especificaciones Proyecto 
- Diseño de la interfaz: GTK
- Diseño Clases:
	- Una clase base con los atributos (Miembros, Publicaciones)
	- Una clase para las fuciones Añadir, Eliminar, Modificar, Guardar y Recuperar (ListaMiembros, ListaPublicaciones)

