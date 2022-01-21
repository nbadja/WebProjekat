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
    public class PredmetController : ControllerBase
    {
       public ProdavnicaContext Context{get;set;}
       public PredmetController(ProdavnicaContext context)
       {
           Context = context;
       }

        [Route("DodajPredmet")]
        [HttpPost]
        public async Task<ActionResult> DodajPredmet([FromBody] Predmet predmet)
        {
            bool isDigitIme = predmet.Naziv.Any(c => char.IsDigit(c));

            if (string.IsNullOrWhiteSpace(predmet.Naziv)|| predmet.Naziv.Length < 1 || isDigitIme == true)
                return BadRequest("Uneti naziv predmeta nije validan !");

            try
            {
                Predmet novaProd = new Predmet
                {
                    ID = predmet.ID,
                    Naziv = predmet.Naziv,
                    BarCode = predmet.BarCode,
                    Cena = predmet.Cena
                };

                Context.Predmeti.Add(novaProd);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno dodata nova prodavnica sa nazivom: {novaProd.Naziv}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [Route("DodajPredmet/{Naziv}/{BarCode}/{Cena}")]
        [HttpPost]
        public async Task<ActionResult> DodajPredmet(string Naziv, string BarCode, int Cena)
        {
            bool isDigitIme = Naziv.Any(c => char.IsDigit(c));

            if (string.IsNullOrWhiteSpace(Naziv)|| Naziv.Length < 1 || isDigitIme == true)
                return BadRequest("Uneti naziv predmeta nije validan !");

            try
            {
                Predmet novaProd = new Predmet
                {
                    Naziv = Naziv,
                    BarCode = BarCode,
                    Cena = Cena
                };

                Context.Predmeti.Add(novaProd);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno dodata nova prodavnica sa nazivom: {novaProd.Naziv}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("UpdatePredmet/{Naziv}/{BarCode}/{Cena}")]
        [HttpPut]
        public async Task<ActionResult> UpdatePredmet(string Naziv, string BarCode, int Cena)
        {
            bool isDigitIme = Naziv.Any(c => char.IsDigit(c));

            if (string.IsNullOrWhiteSpace(Naziv)|| Naziv.Length < 1 || isDigitIme == true)
                return BadRequest("Uneti naziv predmeta nije validan !");

            try
            {
                var predmet = Context.Predmeti.Where(p => p.Naziv == Naziv).FirstOrDefault();
                predmet.Naziv = Naziv;
                predmet.BarCode = BarCode;
                predmet.Cena = Cena;

                Context.Predmeti.Update(predmet);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno dodata nova prodavnica sa nazivom: {Naziv}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("DeletePredmet/{Naziv}")]
        [HttpDelete]
        public async Task<IActionResult> DeletePredmet(string Naziv)
        {
             if(Context.Predmeti.Where( p =>p.Naziv  == Naziv).FirstOrDefault() == null)
                return BadRequest("Uneti predmet nije validan !");
             try
             {
                var Dev = Context.Predmeti.Where(p =>p.Naziv  == Naziv).FirstOrDefault();
                Context.Predmeti.Remove(Dev);
                await Context.SaveChangesAsync();
                return Ok($"Delete predmeta sa nadimkom: {Naziv} je uspesan");
             }
             catch(Exception e)
             {
                return BadRequest(e.Message);
             }
        }



       [Route("PreuzmiPredmet/{Naziv}")]
       [HttpGet]
       public async Task<ActionResult> PreuzmiPredmet(string Naziv)
       {
            if (Context.Predmeti.Where(p => p.Naziv == Naziv).FirstOrDefault() == null)
                return BadRequest("Uneti naziv prodavnice nije validan !");

             var RetVal = Context.Predmeti
             .Where(p => p.Naziv == Naziv);

            try
            {
                return Ok(await RetVal.Select( p => new {p.Naziv,p.BarCode, p.Cena}).ToListAsync());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
       }

       [Route("PreuzmiPredmet")]
       [HttpGet]
       public async Task<ActionResult> PreuzmiProdavnice()
       {
           try{
               return Ok(await Context.Predmeti.Select(p => new {p.Naziv , p.BarCode, p.Cena}).ToListAsync());
           }
           catch(Exception e)
           {
                return BadRequest(e.Message);
           }
       }



    }
}