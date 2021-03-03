using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public abstract class Interfaz<T>
    {
        protected abstract void Agregar(T farmaco);
        protected abstract T BuscarId(Delegate delegates, T farmacos);
        protected abstract void Eliminar(Delegate delegates, T farmacos);
        protected abstract T BuscarNombre(Delegate delegates, T farmacos);
        
    }
}
