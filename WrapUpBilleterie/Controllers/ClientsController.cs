﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Principal;
using WrapUpBilleterie.Data;
using WrapUpBilleterie.ViewModels;
using WrapUpBilleterie.Models;

namespace WrapUpBilleterie.Controllers
{
    public class ClientsController : Controller
    {
        readonly R22_BilleterieContext _context;
        public ClientsController(R22_BilleterieContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["PrenomNom"] = "visiteur";
            IIdentity? identite = HttpContext.User.Identity;
            if(identite != null && identite.IsAuthenticated)
            {
                string courriel = HttpContext.User.FindFirstValue(ClaimTypes.Name);
                Client? client = await _context.Clients.FirstOrDefaultAsync(x => x.Courriel == courriel);
                if (client != null)
                {
                    // Pour dire "Bonjour X" sur l'index
                    ViewData["PrenomNom"] = client.Prenom + " " + client.Nom;
                }
            }
            return View();
        }

        // Inscription en requête get
        public IActionResult Inscription()
        {
            return View();
        }

        // Inscription en requête post
        [HttpPost]
        public async Task<IActionResult> Inscription(InscriptionViewModel ivm)
        {
            // A COMPLETER LORS DE L'ETAPE 1

            return RedirectToAction("Index");
        }

        public IActionResult Connexion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Connexion(ConnexionViewModel cvm)
        {
            // A COMPLETER LORS DE L'ÉTAPE 1

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Deconnexion()
        {
            // Cette ligne mange le cookie 🍪 Slurp
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

    }
}
