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
    public class StorageController : ControllerBase
    {
        public ProdavnicaContext Context{get;set;}
        public StorageController(ProdavnicaContext context)
        {
            Context = context;
        }

        [Route("DodajStoregu/{Prodavnica}/{Predmet}")]
        [HttpPost]
        public async Task<ActionResult> DodajStoregu(string Prodavnica, string Predmet)
        {
            try
            {
                Storage st = new Storage{
                    Prodavnica = Context.Prodavnice.Where(p => p.Naziv == Prodavnica).FirstOrDefault(),
                    Predmet = Context.Predmeti.Where(p => p.Naziv == Predmet).FirstOrDefault(),
                };

                Context.Storages.Add(st);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno dodato u storage: {st.ID}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("DeleteStorage/{Prodavnica}/{Predmet}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteStorage(string Prodavnica, string Predmet)
        {
             if(Context.Prodavnice.Where( p =>p.Naziv  == Prodavnica).FirstOrDefault() == null)
                return BadRequest("Uneta prodavnica nije validan !");
             try
             {
                var prodavnica = Context.Prodavnice.Where(p =>p.Naziv  == Prodavnica).FirstOrDefault();
                var predmet = Context.Predmeti.Where(p => p.Naziv == Predmet).FirstOrDefault();

                var storage = Context.Storages.Where(p => p.Prodavnica == prodavnica && p.Predmet == predmet).FirstOrDefault();

                Context.Storages.Remove(storage);
                await Context.SaveChangesAsync();
                return Ok($"Delete predmeta iz storega sa nazivom: {Prodavnica} {Predmet} je uspesan");
             }
             catch(Exception e)
             {
                return BadRequest(e.Message);
             }
        }


        [Route("DeleteAllFromStorage/{Prodavnica}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAllFromStorage(string Prodavnica)
        {
             if(Context.Prodavnice.Where( p =>p.Naziv  == Prodavnica).FirstOrDefault() == null)
                return BadRequest("Uneta prodavnica nije validan !");
             try
             {
                var prodavnica = Context.Prodavnice.Where(p =>p.Naziv  == Prodavnica).FirstOrDefault();

                var storage = Context.Storages.Where(p => p.Prodavnica == prodavnica);

                foreach(var store in storage)
                    Context.Storages.Remove(store);

                await Context.SaveChangesAsync();
                return Ok($"Delete storega sa nazivom: {Prodavnica} je uspesan");
             }
             catch(Exception e)
             {
                return BadRequest(e.Message);
             }
        }

       [Route("PreuzmiStorage/{Prodavnica}")]
       [HttpGet]
       public async Task<ActionResult> PreuzmiStorage(string Prodavnica)
       {

            if (Context.Prodavnice.Where(p => p.Naziv == Prodavnica).FirstOrDefault() == null)
                return BadRequest("Uneti naziv prodavnice nije validan !");

             var RetVal = Context.Storages
             .Where(p => p.Prodavnica.Naziv == Prodavnica);

            try
            {
                return Ok(await RetVal.Select( p => new {p.Predmet}).ToListAsync());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
       }

       [Route("PreuzmiStorage")]
       [HttpGet]
       public async Task<ActionResult> PreuzmiStorage()
       {
           try{
               return Ok(await Context.Storages.Select(p => new {p.Predmet}).ToListAsync());
           }
           catch(Exception e)
           {
                return BadRequest(e.Message);
           }
       }

    }
}