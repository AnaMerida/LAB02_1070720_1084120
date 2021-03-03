using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ClassLibrary;


namespace ClassLibrary
{
    public class ListaArtesanal<T> : Interfaz<T>, IEnumerable<T>
    {
        private Nodo<T> inicio { get; set; }
        private Nodo<T> fin { get; set; }
        public int contador = 0;

        public ListaArtesanal() { }

        protected override void Agregar(T farmaco)
        {
            Nodo<T> nuevo = new Nodo<T>();
            nuevo.DatosFarmacos = farmaco;
            
            if (inicio == null)
            {
                inicio = nuevo;
                fin = nuevo;
            }
            else
            {
                fin.siguiente = nuevo;
                nuevo.anterior = fin;
                fin = nuevo;
            }
            contador++;
        }
        public void AddArtesanal(T farmaco)
        {
            Agregar(farmaco);
        }
        protected override T BuscarId(Delegate delegates, T DatosFarmacos)
        {
            Nodo<T> NodoBuscar = inicio;
            while (Convert.ToInt32(delegates.DynamicInvoke(NodoBuscar.DatosFarmacos, DatosFarmacos)) != 0)
            {
                NodoBuscar = NodoBuscar.siguiente;
            }
            return NodoBuscar.DatosFarmacos;
        }
        public T BuscarIdArtesanal(Delegate delegates, T DatosFarmacos)
        {
            return BuscarId(delegates, DatosFarmacos);
        }

        protected override T BuscarNombre(Delegate delegates, T DatosFarmacos)
        {
            Nodo<T> NodoBuscar1 = inicio;
            while (Convert.ToInt32(delegates.DynamicInvoke(NodoBuscar1.DatosFarmacos, DatosFarmacos)) != 0)
            {
                NodoBuscar1 = NodoBuscar1.siguiente;
            }
            return NodoBuscar1.DatosFarmacos;
        }

        public T BuscarNombreArtesanal(Delegate delegates, T DatosFarmacos)
        {
            return BuscarNombre(delegates, DatosFarmacos);
        }
        //ELIMINAR EN LA LISTA?
        protected override void Eliminar(Delegate delegates, T DatosFarmacos)
        {
            Nodo<T> NodoFarmacos = new Nodo<T>();
            NodoFarmacos = inicio.siguiente;
            if (!Vacia())
            {
                if (Convert.ToInt32(delegates.DynamicInvoke(inicio.DatosFarmacos, DatosFarmacos)) == 0)
                {
                    if (contador == 1)
                    {
                        inicio = null;
                        fin = inicio;
                    }
                    else
                    {
                        inicio = inicio.siguiente;
                        inicio.siguiente = null;
                    }
                }
                else if (Convert.ToInt32(delegates.DynamicInvoke(fin.DatosFarmacos, DatosFarmacos)) == 0)
                {
                    if (contador == 1)
                    {
                        fin = fin.siguiente;
                        inicio = fin;
                    }
                    else
                    {
                        fin = fin.anterior;
                        fin.siguiente = null;
                    }
                }
                else
                {
                    while (NodoFarmacos != fin)
                    {
                        if (Convert.ToInt32(delegates.DynamicInvoke(NodoFarmacos.DatosFarmacos, DatosFarmacos)) == 0)
                        {
                            NodoFarmacos.siguiente.anterior = NodoFarmacos.anterior;
                            NodoFarmacos.anterior.siguiente = NodoFarmacos.siguiente;
                            NodoFarmacos = fin;
                        }
                        else
                        {
                            NodoFarmacos = NodoFarmacos.Siguiente;
                        }
                    }
                }
            }
        }
        public void EliminarArtesanal(Delegate delegates, T DatosFarmacos)
        {
            Eliminar(delegates, DatosFarmacos);
        }
        public bool Vacia()
        {
            bool vacio;
            if (contador > 0)
            {
                vacio = false;
            }
            else
            {
                vacio = true;
            }
            return vacio;
        }
        public ListaArtesanal<T> FindAllArtesanal(Delegate delegates, T DatosFarmacos, ListaArtesanal<T> L)
        {
            Nodo<T> NodoFarmacos = L.inicio;
            ListaArtesanal<T> ListaBusquedaA = new ListaArtesanal<T>();
            while (NodoFarmacos != L.fin.siguiente)
            {
                if (Convert.ToInt32(delegates.DynamicInvoke(NodoFarmacos.DatosFarmacos, DatosFarmacos)) == 0)
                {
                    ListaBusquedaA.AddArtesanal(NodoFarmacos.DatosFarmacos);
                    NodoFarmacos = NodoFarmacos.Siguiente;
                }
                else
                {
                    NodoFarmacos = NodoFarmacos.Siguiente;
                }
            }
            return ListaBusquedaA;
        }
        public IEnumerator<T> GetEnumerator()
        {
            var nodo = inicio;
            while (nodo != null)
            {
                yield return nodo.DatosFarmacos;
                nodo = nodo.siguiente;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
