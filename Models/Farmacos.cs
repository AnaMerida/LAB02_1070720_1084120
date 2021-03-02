using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LAB02_1070720_1084120.Models
{
    public class Farmacos : IComparable
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string Casa_productora { get; set; }
        [Required]
        public double precio { get; set; }
        [Required]
        public int existencia { get; set; }

        public int CompareTo(object obj)
        {
            var comparer = ((Farmacos)obj).Id;
            return comparer < Id ? 1 : comparer == Id ? 0 : -1;
        }
        public Comparison<Farmacos> OrdenarPorId = delegate (Farmacos f1, Farmacos f2)
        {
            return f1.Id.CompareTo(f2.Id);
        };
        public Comparison<Farmacos> OrdenarPorNombre = delegate (Farmacos f1, Farmacos f2)
        {
            return f1.Nombre.CompareTo(f2.Nombre);
        };
         
    }
}
