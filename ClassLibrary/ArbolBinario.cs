using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ClassLibrary;

namespace ClassLibrary
{
    public class ArbolBinario<T> : InterfazArbol<T> where T :IComparable
    {
        private NodoArbol<T> padre;

        public ArbolBinario(){}
        //AGREGAR
        protected override void AgregarArbol(T farmaco, Delegate delegates, NodoArbol<T> padre) 
        {
            if (padre == null)
            {
                padre.DatosArbol = farmaco;
                padre.izquierdo = null;
                padre.derecho = null;
            }
            else
            {
                if (Convert.ToInt32(delegates.DynamicInvoke(padre.DatosArbol, farmaco)) == -1)
                {
                    //izquierda
                    AgregarArbol(farmaco, delegates, padre.izquierdo);
                }
                else if (Convert.ToInt32(delegates.DynamicInvoke(padre.DatosArbol, farmaco)) == 1)
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

        //BUSQUEDA
       
    }
}
