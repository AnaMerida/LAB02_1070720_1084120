using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using LAB02_1070720_1084120.Models;
using System.IO;
using System.Web;
using System.Text;
using ClassLibrary;
using System.Diagnostics;
using Microsoft.Extensions.Hosting;
using System.Text.RegularExpressions;

namespace LAB02_1070720_1084120.Controllers
{
    public class FarmacosController : Controller
    {
        public static ArbolBinario<DatosArbol> ArbolBinario = new ArbolBinario<DatosArbol>();
        public static ListaArtesanal<Farmacos> ListaArtesanalFarmacos = new ListaArtesanal<Farmacos>();
        public static ListaArtesanal<Farmacos> SinExistencia = new ListaArtesanal<Farmacos>();
        public static ListaArtesanal<DatosPedidos> ListaPedidos = new ListaArtesanal<DatosPedidos>();
        public static DatosPedidos DatosPedidoNuevo = new DatosPedidos();
        public ActionResult ImportarCSV()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult ImportarCSV(IFormFile ArchivoCSV)
        {
            if (ArchivoCSV != null)
            {
                if (ArchivoCSV.FileName.Contains(".csv"))
                {
                    using (var reader = new StreamReader(ArchivoCSV.OpenReadStream()))
                    {
                        string archivolineas = reader.ReadToEnd().Remove(0, 57);
                        foreach (string linea in archivolineas.Split("\n"))
                        {
                            if (!string.IsNullOrEmpty(linea))
                            {
                                Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                                String[] Fields = CSVParser.Split(linea);
                                for (int i = 0; i < Fields.Length; i++)
                                {
                                    Fields[i] = Fields[i].TrimStart(' ', '"');
                                    Fields[i] = Fields[i].TrimEnd('"');
                                }
                                if (Fields[4].Contains("$"))
                                {
                                    Fields[4] = Fields[4].TrimStart('$', ' ');
                                }
                                if (Fields[5].Contains("\r"))
                                {
                                    Fields[5] = Fields[5].Remove(Fields[5].Length - 1, 1);
                                }
                                Farmacos Farmaco = new Farmacos();
                                DatosArbol FarmacoArbol = new DatosArbol();
                                FarmacoArbol.IdA = Convert.ToInt32(Fields[0]);
                                FarmacoArbol.NombreA = Fields[1];
                                Farmaco.Id = Convert.ToInt32(Fields[0]);
                                Farmaco.Nombre = Fields[1];
                                Farmaco.Descripcion = Fields[2];
                                Farmaco.Casa_productora = Fields[3];
                                Farmaco.Precio = Convert.ToDouble(Fields[4]);
                                Farmaco.Existencia = Convert.ToInt32(Fields[5]);
                                ListaArtesanalFarmacos.AddArtesanal(Farmaco);
                                ArbolBinario.AddArbol(FarmacoArbol, FarmacoArbol.OrdenarPorNombreArbol);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    return RedirectToAction(nameof(MostrarIndice));
                }
                else
                {
                    return View("ImportarCSV");
                }
            }
            else {
                
                return View("ImportarCSV");
                
            }
        }

        public ActionResult BuscarProducto(string TextoBusqueda)
        {
            DatosArbol buscarfarmaco = new DatosArbol();
            buscarfarmaco.NombreA = TextoBusqueda;
            buscarfarmaco = ArbolBinario.SearchFarmaco(buscarfarmaco, buscarfarmaco.OrdenarPorNombreArbol);
           
            if (buscarfarmaco != null)
            {
                //Encontro el producto, para agregarlo

                BuscarF(buscarfarmaco.IdA);
                return View("ProductoAComprar");
            }
            else
            {
                //Lista artesanal
                ViewBag.Farmacos = DatosPedidoNuevo.ListaPedido;
                return View("RealizarPedido");
            }
        }

        public ActionResult BuscarF(int Id)
        {
            
            Farmacos producto = new Farmacos();
            producto.Id = Id;
            producto = ListaArtesanalFarmacos.BuscarIdArtesanal(producto.OrdenarPorId, producto);

            return View(producto);
        }

        [HttpPost]
        public ActionResult BuscarF(int Id, IFormCollection collection) 
        {
            Farmacos producto = new Farmacos();
            producto.Id = Id;
            producto.Nombre = collection["Nombre"];
            producto.Descripcion = collection["Descripcion"];
            producto.Casa_productora = collection["Casa_productora"];
            producto.Precio = Convert.ToDouble(collection["Precio"]);
            producto.Existencia = Convert.ToInt32(collection["Existencia"]);

            return View();
        }

            public ActionResult MostrarIndice()
        {
            return View(ListaArtesanalFarmacos);
        }

        // GET: FarmacosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FarmacosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FarmacosController/Create
        public ActionResult FinPedido()
        {
            DatosPedidoNuevo = new DatosPedidos();
            return View("FinPedido");
        }

        // GET: FarmacosController/Edit/5
        public ActionResult Edit(int id)
        {
            Farmacos producto = new Farmacos();
            producto.Id = id;
            producto = ListaArtesanalFarmacos.BuscarIdArtesanal(producto.OrdenarPorId, producto);

            return View();
        }

        // POST: FarmacosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            DatosArbol editar = new DatosArbol();
            editar.NombreA = collection["Nombre"];
            editar = ArbolBinario.SearchFarmaco(editar, editar.OrdenarPorNombreArbol);
            int cantidad = Convert.ToInt32(collection["Existencia"]);
            Farmacos editar_lista = new Farmacos();
            editar_lista.Existencia = Convert.ToInt32(collection["Existencia"]);
            //BuscarF(editar.IdA);
            editar_lista.Id = editar.IdA;
            editar_lista = ListaArtesanalFarmacos.BuscarIdArtesanal(editar_lista.OrdenarPorId, editar_lista);
            double preciopagar = 0;
            if (editar != null)
            {
                if (cantidad <= editar_lista.Existencia && cantidad >= 0)
                {
                    editar_lista.Existencia = editar_lista.Existencia - cantidad;
                    preciopagar += (editar_lista.Precio * cantidad);
                    DatosPedidoNuevo.Preciototal = DatosPedidoNuevo.Preciototal + (editar_lista.Precio * cantidad);
                    DatosPedidoNuevo.ListaPedido.AddArtesanal(editar_lista);
                    ViewBag.Farmacos = DatosPedidoNuevo.ListaPedido;

                    if (editar_lista.Existencia == 0)
                    {
                        ArbolBinario.EliminarFarmaco(editar, editar.OrdenarPorNombreArbol);
                        SinExistencia.AddArtesanal(editar_lista);
                    }

                }
                else
                {
                    //mensaje de error
                    DatosPedidoNuevo.Preciototal = preciopagar;
                    return View("CompraNegada");
                }
            }
            DatosPedidoNuevo.NombreCliente = collection["NombreCliente"];
            DatosPedidoNuevo.Nit = Convert.ToInt32(collection["Nit"]);
            DatosPedidoNuevo.Direccion = collection["Direccion"];
            DatosPedidoNuevo.Preciototal =Math.Round(preciopagar, 2, MidpointRounding.AwayFromZero);
            return View("RealizarPedido", DatosPedidoNuevo);
        }

        // GET: FarmacosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FarmacosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult RealizarPedido()
        {
            ViewBag.Farmacos = DatosPedidoNuevo.ListaPedido;
            return View(DatosPedidoNuevo);
        }

        //REABASTECER
        public ActionResult Reabastecer()
        {
            foreach (var item in SinExistencia)
            {
                int numero;
                Farmacos producto = new Farmacos();
                int id = item.Id;
                producto.Id = id;
                producto = ListaArtesanalFarmacos.BuscarIdArtesanal(producto.OrdenarPorId, producto);
                Random rdn = new Random();
                numero = rdn.Next(1, 15);
                producto.Existencia = numero;
                DatosArbol ProductoArbol = new DatosArbol();
                ProductoArbol.IdA = item.Id;
                ProductoArbol.NombreA = item.Nombre;
                ArbolBinario.AddArbol(ProductoArbol, ProductoArbol.OrdenarPorNombreArbol);
                
            }
            return View(SinExistencia);
        }
    }
}
