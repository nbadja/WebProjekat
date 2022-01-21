using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LokacijaController : ControllerBase
    {
        public ProdavnicaContext Context{get;set;}
        public LokacijaController(ProdavnicaContext context)
        {
            Context = context;
        }


        [Route("DodajLokaciju")]
        [HttpPost]
        public async Task<ActionResult> DodajLokaciju([FromBody] Lokacija lokacija)
        {
            bool isDigitIme = lokacija.Naziv.Any(c => char.IsDigit(c));

            if (string.IsNullOrWhiteSpace(lokacija.Naziv)|| lokacija.Naziv.Length < 1 || isDigitIme == true)
                return BadRequest("Uneta lokacija nije validna!");

            try
            {
                Lokacija lok = new Lokacija
                {
                    ID = lokacija.ID,
                    Naziv = lokacija.Naziv,
                    Ulica = lokacija.Ulica
                };

                Context.Lokacije.Add(lok);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno dodata nova lokacija sa nazivom: {lok.Naziv}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DodajLokaciju/{Naziv}/{Ulica}/")]
        [HttpPost]
        public async Task<ActionResult> DodajLokaciju(string Naziv, string Ulica)
        {
            bool isDigitIme = Naziv.Any(c => char.IsDigit(c));

            if (string.IsNullOrWhiteSpace(Naziv)|| Naziv.Length < 1 || isDigitIme == true)
                return BadRequest("Uneta lokacija nije validna!");

            try
            {
                Lokacija lok = new Lokacija
                {
                    Naziv = Naziv,
                    Ulica = Ulica
                };

                Context.Lokacije.Add(lok);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno dodata nova lokacija sa nazivom: {lok.Naziv}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}