﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WS.Models.OUT;

namespace UI.Areas.Commandes.Controllers
{
    public class CommandesController : Controller
    {

        //backoffice
        [Route("Commandes")]
        public ActionResult Commandes()
        {

            if (Constants.BACKOFFICE_SECURITY)
            {
                //connexion
                if (Session["www.cavalier-roi.fr"] == null)
                {
                    Response.Redirect("/");
                }
                else if ((Session["www.cavalier-roi.fr"] != null) && ((Session["www.cavalier-roi.fr"] as Eleve).Administration == false))
                {
                    Response.Redirect("/");
                }
            }

            return View("~/Areas/Commandes/Views/Commandes.cshtml");
        }

    }
}