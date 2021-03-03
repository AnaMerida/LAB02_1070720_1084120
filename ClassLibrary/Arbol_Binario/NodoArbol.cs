using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class NodoArbol<T>
    {
        public NodoArbol<T> izquierdo;
        public NodoArbol<T> derecho;
        public T DatoNodoArbol { get; set; }
        public NodoArbol<T> Izquierdo
        {
            get { return izquierdo; }
            set { izquierdo = value; }
        }
        public NodoArbol<T> Derecho
        {
            get { return derecho; }
            set { derecho = value; }
        }
    }
}
