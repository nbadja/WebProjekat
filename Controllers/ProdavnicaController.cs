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
    public class ProdavnicaController : ControllerBase
    {
       public ProdavnicaContext Context{get;set;}
       public ProdavnicaController(ProdavnicaContext context)
       {
           Context = context;
       }

       [Route("PreuzmiProdavnicu/{Naziv}")]
       [HttpGet]
       public async Task<ActionResult> PreuzmiProdavnicu(string Naziv)
       {
            if (Context.Prodavnice.Where(p => p.Naziv == Naziv).FirstOrDefault() == null)
                return BadRequest("Uneti naziv prodavnice nije validan !");

             var RetVal = Context.Prodavnice
             .Where(p => p.Naziv == Naziv);

            try
            {
                return Ok(await RetVal.Select( p => new {p.Naziv,p.Mesto}).ToListAsync());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
       }

       [Route("PreuzmiProdavnice")]
       [HttpGet]
       public async Task<ActionResult> PreuzmiProdavnice()
       {
           try{
               return Ok(await Context.Prodavnice.Select(p => new {p.Naziv, p.Mesto}).ToListAsync());
           }
           catch(Exception e)
           {
                return BadRequest(e.Message);
           }
       }

        [Route("DodajProdavnicu")]
        [HttpPost]
        public async Task<ActionResult> DodajProdavnicu([FromBody] Prodavnica prodavnica)
        {
            bool isDigitIme = prodavnica.Naziv.Any(c => char.IsDigit(c));

            if (string.IsNullOrWhiteSpace(prodavnica.Naziv)|| prodavnica.Naziv.Length < 1 || isDigitIme == true)
                return BadRequest("Uneti naziv prodavnice nije validan !");

            try
            {
                Prodavnica novaProd = new Prodavnica
                {
                    ID = prodavnica.ID,
                    Naziv = prodavnica.Naziv,
                    Mesto = prodavnica.Mesto,
                };

                Context.Prodavnice.Add(novaProd);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno dodata nova prodavnica sa nazivom: {novaProd.Naziv}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

       [Route("DodajProdavnicu/{Naziv}/{Mesto}/{Ulica}")]
       [HttpPost]
       public async Task<ActionResult> DodajProdavnicu(string Naziv, string Mesto, string Ulica)
        {
            bool isDigitIme = Naziv.Any(c => char.IsDigit(c));

            if (string.IsNullOrWhiteSpace(Naziv) || Naziv.Length < 1 || isDigitIme == true)
                return BadRequest("Uneti naziv prodavnice nije validan !");
            try
            {
                Lokacija lok = new Lokacija
                {
                    Naziv = Mesto,
                    Ulica = Ulica
                };
                Context.Lokacije.Add(lok);

                Prodavnica novaProd = new Prodavnica
                {
                    Naziv = Naziv,
                    Mesto = lok,
                };

                Context.Prodavnice.Add(novaProd);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno dodata nova prodavnica sa nazivom: {novaProd.Naziv}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("UpdateProdavnica/{Naziv}/{NoviNaziv}")]
        [HttpPut]
        public async Task<ActionResult> UpdateProdavnica(string Naziv, string NoviNaziv)
        {
            bool isDigitIme = Naziv.Any(c => char.IsDigit(c));

            if (string.IsNullOrWhiteSpace(Naziv)|| Naziv.Length < 1 || isDigitIme == true)
                return BadRequest("Uneti naziv prodavnice nije validan !");

            try
            {
                var prodavnica = Context.Prodavnice.Where(p => p.Naziv == Naziv).FirstOrDefault();
                prodavnica.Naziv = NoviNaziv;

                Context.Prodavnice.Update(prodavnica);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno updejtovana nova prodavnica sa nazivom: {Naziv}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("DeleteProdavnica/{Naziv}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteProdavnica(string Naziv)
        {
             if(Context.Prodavnice.Where( p =>p.Naziv  == Naziv).FirstOrDefault() == null)
                return BadRequest("Uneta prodavnica nije validan !");
             try
             {
                var Dev = Context.Prodavnice.Where(p =>p.Naziv  == Naziv).FirstOrDefault();
                Context.Prodavnice.Remove(Dev);
                await Context.SaveChangesAsync();
                return Ok($"Delete prodavnice sa nazivom: {Naziv} je uspesan");
             }
             catch(Exception e)
             {
                return BadRequest(e.Message);
             }
        }
    }
}