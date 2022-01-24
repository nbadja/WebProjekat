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

       [Route("PreuzmiProdavnicu/{ID}")]
       [HttpGet]
       public async Task<ActionResult> PreuzmiProdavnicu(int ID)
       {
            if (Context.Prodavnice.Where(p => p.ID == ID).FirstOrDefault() == null)
                return BadRequest("Uneta prodavnice ne postoji !");

             var RetVal = Context.Prodavnice
             .Where(p => p.ID == ID);

            try
            {
                return Ok(await RetVal.Select( p => new {p.ID, p.Naziv, p.Drzava, p.Grad, p.Adresa}).ToListAsync());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
       }


       [Route("PreuzmiProdavnicuNaziv/{Naziv}")]
       [HttpGet]
       public async Task<ActionResult> PreuzmiProdavnicuNaziv(string Naziv)
       {
            if (Context.Prodavnice.Where(p => p.Naziv == Naziv).FirstOrDefault() == null)
                return BadRequest("Uneta prodavnice ne postoji !");

             var RetVal = Context.Prodavnice
             .Where(p => p.Naziv == Naziv);

            try
            {
                return Ok(await RetVal.Select( p => new {p.ID, p.Naziv, p.Drzava, p.Grad, p.Adresa}).ToListAsync());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
       }


       [Route("PreuzmiProdavniceDrzava/{DrzavaID}")]
       [HttpGet]
       public async Task<ActionResult> PreuzmiProdavniceDrzava(int DrzavaID)
       {
            if (Context.Drzave.Where(p => p.ID == DrzavaID).FirstOrDefault() == null)
                return BadRequest("Uneta drzava ne postoji !");

             var RetVal = Context.Prodavnice
             .Where(p => p.Drzava.ID == DrzavaID);

            try
            {
                return Ok(await RetVal.Select( p => new {p.ID, p.Naziv, p.Drzava, p.Grad, p.Adresa}).ToListAsync());
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
               return Ok(await Context.Prodavnice.Select(p => new {p.ID, p.Naziv, p.Drzava, p.Grad, p.Adresa}).ToListAsync());
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
                    Drzava = prodavnica.Drzava,
                    Grad = prodavnica.Grad,
                    Adresa = prodavnica.Adresa
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

        [Route("DodajProdavnicu/{Naziv}/{DrzavaID}/{GradID}/{Adresa}")]
        [HttpPost]
        public async Task<ActionResult> DodajProdavnicu(string Naziv, int DrzavaID, int GradID, string Adresa)
        {
            bool isDigitIme = Naziv.Any(c => char.IsDigit(c));

            if (string.IsNullOrWhiteSpace(Naziv) || Naziv.Length < 1 || isDigitIme == true)
                return BadRequest("Uneti naziv prodavnice nije validan !");
            if(Context.Drzave.Where(p => p.ID == DrzavaID).FirstOrDefault() == null)
                 return BadRequest("Uneta drzava ne postoji!");
            if(Context.Gradovi.Where(p => p.ID == GradID).FirstOrDefault() == null)
                 return BadRequest("Uneti grad ne postoji!");
            try
            {
                Prodavnica novaProd = new Prodavnica
                {
                    Naziv = Naziv,
                    Drzava = Context.Drzave.Where(p => p.ID == DrzavaID).FirstOrDefault(),
                    Grad = Context.Gradovi.Where(p => p.ID == GradID).FirstOrDefault(),
                    Adresa = Adresa,
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


        [Route("UpdateProdavnica/{ID}/{Naziv}/{Adresa}")]
        [HttpPut]
        public async Task<ActionResult> UpdateProdavnica(int id, string Naziv, string Adresa)
        {
            bool isDigitIme = Naziv.Any(c => char.IsDigit(c));

            if (string.IsNullOrWhiteSpace(Naziv)|| Naziv.Length < 1 || isDigitIme == true)
                return BadRequest("Uneti naziv prodavnice nije validan !");

            try
            {
                var updejtovanaProdavnica = Context.Prodavnice.Where(p => p.ID == id).FirstOrDefault();
                updejtovanaProdavnica.Naziv = Naziv;
                updejtovanaProdavnica.Adresa = Adresa;

                Context.Prodavnice.Update(updejtovanaProdavnica);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno updejtovana nova prodavnica sa nazivom: {Naziv}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("DeleteProdavnica/{ID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteProdavnica(int ID)
        {
             if(Context.Prodavnice.Where( p =>p.ID  == ID).FirstOrDefault() == null)
                return BadRequest("Uneta prodavnica ne postoji!");
             try
             {
                var Dev = Context.Prodavnice.Where(p =>p.ID  == ID).FirstOrDefault();
                Context.Prodavnice.Remove(Dev);
                await Context.SaveChangesAsync();
                return Ok($"Delete prodavnice sa ID: {ID} je uspesan");
             }
             catch(Exception e)
             {
                return BadRequest(e.Message);
             }
        }
    }
}