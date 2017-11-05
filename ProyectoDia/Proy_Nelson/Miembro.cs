using System;
namespace Proy_Nelson
{
	public class Miembro
	{
		private string dni { get; set; }
		private string nombre { get; set; }
		private string apellidos { get; set; }
		private string telefono { get; set; }
		private string email { get; set; }
		private string direccion { get; set; }

		public Miembro(string dni, string nombre, string apellidos, string telefono, string email, string direccion)
		{
			this.dni = dni;
			this.nombre = nombre;
			this.apellidos = apellidos;
			this.telefono = telefono;
			this.email = email;
			this.direccion = direccion;
		}

	}
}
