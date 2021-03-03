using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ClassLibrary;

namespace ClassLibrary
{
    public class ArbolBinario<T> : InterfazArbol<T> where T :IComparable
    {
        NodoArbol<T> padre = new NodoArbol<T>();
        public int contador = 0;
        public ArbolBinario(){}
        //AGREGAR
        protected override void AgregarArbol(T farmaco, Delegate delegates, NodoArbol<T> padre) 
        {
            if (padre.DatoNodoArbol == null)
            {
                padre.DatoNodoArbol = farmaco;
                padre.izquierdo = new NodoArbol<T>();
                padre.derecho = new NodoArbol<T>();
                contador++;
            }
            else
            {
                if (Convert.ToInt32(delegates.DynamicInvoke(padre.DatoNodoArbol, farmaco)) == 1)
                {
                    //izquierda
                    AgregarArbol(farmaco, delegates, padre.izquierdo);
                }
                else if (Convert.ToInt32(delegates.DynamicInvoke(padre.DatoNodoArbol, farmaco)) == -1)
                {
                    //derecho
                    AgregarArbol(farmaco, delegates, padre.derecho);
                }
            }
        }
        public void AddArbol(T farmaco, Delegate delegates)
        {
            AgregarArbol(farmaco,  delegates, padre);
        }

        //ELIMINAR
        //protected override void EliminarArbol (T farmaco, Delegate delegates, Nodo<T> padre)
        //{
        //    if (padre != null)
        //    {
        //        //if ()
        //        //{

        //        //}
        //    }
        //}

        //BUSQUEDA
       
    }
}
