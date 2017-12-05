using System;
namespace IntegracionFinal
{
	public class Miembro
	{
		public string dni { get; set; }
		public string nombre { get; set; }
		public string apellidos { get; set; }
		public string telefono { get; set; }
		public string email { get; set; }
		public string direccion { get; set; }

		public Miembro() { }

		public Miembro(string dni, string nombre, string apellidos, string telefono, string email, string direccion)
		{
			this.dni = dni;
			this.nombre = nombre;
			this.apellidos = apellidos;
			this.telefono = telefono;
			this.email = email;
			this.direccion = direccion;
		}

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

		public bool checkMiembro(String namePointApellido)
		{
			return string.Format(this.nombre[0] + "." + this.apellidos.Split(' ')[0]).Equals(namePointApellido) ? true : false;
		}
	}
}
