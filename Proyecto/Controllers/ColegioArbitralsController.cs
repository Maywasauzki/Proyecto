using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class ColegioArbitralsController : Controller
    {
        //Objeto de tipo contexto
        private ProyectoContext db = new ProyectoContext();

        // GET: Patrocinadores
        public ActionResult Index()
        {
            return View(db.ColegioArbitrals.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ColegioArbitral colegioArbitral)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.ColegioArbitrals.Add(colegioArbitral);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "¡Colegio Arbitral creado exitosamente ....!";
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexIdentificacion"))
                    {
                        ViewBag.Error = "Error, ya hay registrado un Colegio Arbitral con esta identificacion";

                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }

                    return View(colegioArbitral);

                }

            }
            else
            {
                return View(colegioArbitral);
            }

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColegioArbitral colegioArbitral = db.ColegioArbitrals.Find(id);
            if (colegioArbitral.Equals(null))
            {
                return HttpNotFound();
            }


            return View(colegioArbitral);
        }

        [HttpPost]
        public ActionResult Edit(ColegioArbitral colegioArbitral)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(colegioArbitral).State = EntityState.Modified;// update
                    db.SaveChanges();
                    TempData["AlertMessage"] = "¡Colegio Arbitral modificado exitosamente ....!";
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null
                        && ex.InnerException.InnerException.Message.Contains("IndexIdentificacion"))
                    {
                        ViewBag.Error = "Error, ya hay registrado un colegio arbitral con esta identificacion";

                    }
                    else
                    {
                        ViewBag.Error = ex.Message;

                    }

                    return View(colegioArbitral);
                }
            }
            else
            {
                return View(colegioArbitral);
            }

        }


        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColegioArbitral colegioArbitral = db.ColegioArbitrals.Find(id);
            if (colegioArbitral.Equals(null))
            {
                return HttpNotFound();
            }


            return View(colegioArbitral);

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColegioArbitral colegioArbitral = db.ColegioArbitrals.Find(id);// select o from Municipios where MunicipioId=id
            if (colegioArbitral.Equals(null))
            {
                return HttpNotFound();
            }


            return View(colegioArbitral);

        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            ColegioArbitral colegioArbitral = db.ColegioArbitrals.Find(id);
            if (colegioArbitral.Equals(null))
            {
                return HttpNotFound();
            }
            else
            {
                try
                {
                    db.ColegioArbitrals.Remove(colegioArbitral);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "¡Colegio Arbitral eliminado exitosamente ....!";

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null &&
                        ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                    {
                        ViewBag.Error = "No se permite la eliminación de registros con integridad referencial";
                    }
                    else
                    {
                        ViewBag.Error = ex.Message;
                    }
                    return View(colegioArbitral);
                }

            }
            return RedirectToAction("Index");

        }

        //Metodo para cerrar la conexion con la base de datos

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}