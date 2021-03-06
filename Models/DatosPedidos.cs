using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ClassLibrary;

namespace LAB02_1070720_1084120.Models
{
    public class DatosPedidos
    {
        [Required]
        public string NombreCliente { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public int Nit { get; set; }

        public ListaArtesanal<Farmacos> ListaPedido = new ListaArtesanal<Farmacos>();
        public double Preciototal { get; set; }

    }
}
