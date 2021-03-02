using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class NodoArbol<T>
    {
        public string Nombre { get; set;}
        public int Id { get; set;}

        public NodoArbol<T> izquierdo;
        public NodoArbol<T> derecho;
        public NodoArbol<T> padre;
        public T DatosArbol { get; set; }
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
        //public NodoArbol<T> Padre
        //{
        //    get { return padre; }
        //    set { padre = value; }
        //}
    }
}
