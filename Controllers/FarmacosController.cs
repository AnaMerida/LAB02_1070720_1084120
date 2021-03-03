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
        public ActionResult ImportarCSV()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult ImportarCSV(IFormFile ArchivoCSV)
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
                            Farmaco.precio = Convert.ToDouble(Fields[4]);
                            Farmaco.existencia = Convert.ToInt32(Fields[5]);
                            ListaArtesanalFarmacos.AddArtesanal(Farmaco);
                            ArbolBinario.AddArbol(FarmacoArbol, FarmacoArbol.OrdenarPorNombreArbol);  
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                return View("Index");    
            }
            else
            {
                return View("ImportarCSV");
            }
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: FarmacosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: FarmacosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FarmacosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
    }
}
