using System;
namespace Proy_ImaTor
{
    public class Excepcion : Exception
    {
        public Excepcion(string error): base(error)
        {
            
        }
    }
}
