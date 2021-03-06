using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ClassLibrary;

namespace ClassLibrary
{
    public class ArbolBinario<T> : InterfazArbol<T> where T : IComparable
    {
       
        NodoArbol<T> padre = new NodoArbol<T>();
        public int contador = 0;
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
        
        //protected override void EliminarArbol(T farmaco, Delegate delegates, NodoArbol<T> padre)
        //{
        //    if (padre.DatoNodoArbol != null)
        //    {
        //        if (Convert.ToInt32(delegates.DynamicInvoke(padre.DatoNodoArbol, farmaco)) == 1)//menor
        //        {
        //            EliminarArbol(farmaco, delegates, padre.izquierdo);
        //        }
        //        else if (Convert.ToInt32(delegates.DynamicInvoke(padre.DatoNodoArbol, farmaco)) == -1)//mayor
        //        {
        //            EliminarArbol(farmaco, delegates, padre.derecho);
        //        }
        //        else if (Convert.ToInt32(delegates.DynamicInvoke(padre.DatoNodoArbol, farmaco)) == 0)
        //        {
        //            NodoArbol<T> NodoEliminar = padre;
        //            NodoArbol<T> NodoTemp = new NodoArbol<T>();

        //            EliminarNodoArbol(padre);
        //        }
        //    }
        //}
        
        //protected override void EliminarNodoArbol(NodoArbol<T> padre)
        //{
        //    //tiene dos hijos
        //    if (padre.izquierdo.DatoNodoArbol != null && padre.derecho.DatoNodoArbol != null)
        //    {
        //        NodoArbol<T> minimo = minimo_izquierdo(padre.derecho);
        //        padre.DatoNodoArbol = minimo.DatoNodoArbol;
        //        EliminarNodoArbol(minimo);
        //    }
        //    //tiene un hijo izquierdo pero no derecho
        //    else if (padre.izquierdo.DatoNodoArbol == null && padre.derecho.DatoNodoArbol != null)
        //    {

        //        NodoArbol<T> NodoEliminar = padre; 
        //        NodoArbol<T> NodoTemp = new NodoArbol<T>();
        //        padre = padre.izquierdo;
        //        while(padre.derecho.DatoNodoArbol != null)
        //        {
        //            NodoTemp = padre;
        //            padre = padre.derecho;
        //        }
        //        NodoEliminar.DatoNodoArbol = padre.DatoNodoArbol;

        //    }
        //    //tiene un hijo derecho pero no izquierdo
        //    else if (padre.derecho == null && padre.izquierdo != null)
        //    {

        //    }
        //}
        //public void reemplazar_nodos(NodoArbol<T> padre, NodoArbol<T> nuevo)
        //{

        //}

        //public NodoArbol<T> minimo_izquierdo(NodoArbol<T> padre)
        //{
        //    if (padre.DatoNodoArbol == null)
        //    {
        //        return null;
        //    }
        //    if (padre.izquierdo != null)
        //    {
        //        return minimo_izquierdo(padre.izquierdo);
        //    }
        //    else
        //    {
        //        return padre;
        //    }
        //}

        //ELIMINAR 2
        public void EliminarFarmaco(T farmaco, Delegate delegates) 
        { 
            padre = Eliminar(padre, farmaco, delegates); 
        }

        public NodoArbol<T> Eliminar(NodoArbol<T> padre, T farmaco, Delegate delegates)
        {
            if (padre.DatoNodoArbol == null)
            {
                return padre;
            }
            if (Convert.ToInt32(delegates.DynamicInvoke(padre.DatoNodoArbol, farmaco)) == 1)
            {
                padre.izquierdo = Eliminar(padre.izquierdo, farmaco, delegates);
            }
            else if (Convert.ToInt32(delegates.DynamicInvoke(padre.DatoNodoArbol, farmaco)) == -1)
            {
                padre.derecho = Eliminar(padre.derecho, farmaco, delegates);
            }
            else
            {
                if (padre.izquierdo.DatoNodoArbol == null)
                {
                    return padre.derecho;
                }
                else if (padre.derecho.DatoNodoArbol == null)
                {
                    return padre.izquierdo;
                }
                else
                {
                    padre.DatoNodoArbol = ValorMinimo(padre.derecho);
                    padre.derecho = Eliminar(padre.derecho, padre.DatoNodoArbol, delegates);
                }
            }
            return padre;
        }

        public T ValorMinimo(NodoArbol<T> padre)
        {
            T minv;
            minv = padre.DatoNodoArbol;
            while (padre.izquierdo.DatoNodoArbol != null)
            {
                minv = padre.izquierdo.DatoNodoArbol;
                padre = padre.izquierdo;
            }
            return minv;
        }

        //BUSQUEDA

        protected override T BuscarFarmaco(T farmaco, Delegate delegates, NodoArbol<T> padre)
        {
            if (padre.DatoNodoArbol != null)
            {
                if (Convert.ToInt32(delegates.DynamicInvoke(padre.DatoNodoArbol, farmaco)) == 1)//menor
                {
                    return BuscarFarmaco(farmaco, delegates, padre.izquierdo);
                }
                else if (Convert.ToInt32(delegates.DynamicInvoke(padre.DatoNodoArbol, farmaco)) == -1)//mayor
                {
                   return BuscarFarmaco(farmaco, delegates, padre.derecho);
                }
                else if (padre.DatoNodoArbol == null)
                {
                    padre.DatoNodoArbol = farmaco;
                    return padre.DatoNodoArbol;
                }
                return padre.DatoNodoArbol;
            }
            return padre.DatoNodoArbol;
        }

            public T SearchFarmaco(T farmaco, Delegate delegates)
        {
            return BuscarFarmaco(farmaco, delegates, padre);
        }

    }
}
