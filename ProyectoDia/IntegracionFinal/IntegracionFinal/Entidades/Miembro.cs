using System;
namespace IntegracionFinal
{
    /***
     * Esta clase contiene la definición del objeto Miembro que recoge los atributos para cada miembro del departamento
     */
	public class Miembro
	{
		public string dni { get; set; }
		public string nombre { get; set; }
		public string apellidos { get; set; }
		public string telefono { get; set; }
		public string email { get; set; }
		public string direccion { get; set; }

		public Miembro() { }
        //Constructor
		public Miembro(string dni, string nombre, string apellidos, string telefono, string email, string direccion)
		{
			this.dni = dni;
			this.nombre = nombre;
			this.apellidos = apellidos;
			this.telefono = telefono;
			this.email = email;
			this.direccion = direccion;
		}
        //Sobrecarga del método ToString para devolver un Miembro en texto.
		public override string ToString()
		{
			return string.Format("=== Miembro ===\n" +
								 "DNI= " + dni + "\n" +
								 "NOMBRE= " + nombre + "\n" +
								 "APELLIDOS= " + apellidos + "\n" +
								 "TELEFONO= " + telefono + "\n" +
								 "EMAIL= " + email + "\n" +
								 "DIRECCION= " + direccion + "\n");
		}
        /***
         * checkMiembro comprueba que el string recibido coincide con el miembro dado un formato específico
         * Devuelve true en caso de que el string concuerde
         * False en el otro caso
         */
		public bool checkMiembro(String namePointApellido)
		{
			return string.Format(this.nombre[0] + "." + this.apellidos.Split(' ')[0]).Equals(namePointApellido) ? true : false;
		}
	}
}
