using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAB02_1070720_1084120.Models;
using System.ComponentModel.DataAnnotations;

namespace LAB02_1070720_1084120.Models
{
    public class DatosArbol : IComparable
    {
        [Required]
        public int IdA { get; set; }
        [Required]
        public string NombreA { get; set; }

        public int CompareTo(object obj)
        {
            var comparer = ((DatosArbol)obj).IdA;
            return comparer < IdA ? 1 : comparer == IdA ? 0 : -1;
        }
        public Comparison<DatosArbol> OrdenarPorIdArbol = delegate (DatosArbol f1, DatosArbol f2)
        {
            return f1.IdA.CompareTo(f2.IdA);
        };
        public Comparison<DatosArbol> OrdenarPorNombreArbol = delegate (DatosArbol f1, DatosArbol f2)
        {
            return f1.NombreA.CompareTo(f2.NombreA);
        };
    }
}
