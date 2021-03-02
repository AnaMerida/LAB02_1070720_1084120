using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public abstract class InterfazArbol<T>
    {
        protected abstract void AgregarArbol(T farmaco, Delegate delegates, NodoArbol<T> padre);
       

    }
}
