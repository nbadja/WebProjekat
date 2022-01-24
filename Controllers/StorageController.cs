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

        [Route("DodajStoregu/{ProdavnicaID}/{PredmetID}")]
        [HttpPost]
        public async Task<ActionResult> DodajStoregu(int ProdavnicaID, int PredmetID)
        {
            try
            {
                Storage st = new Storage{
                    Prodavnica = Context.Prodavnice.Where(p => p.ID == ProdavnicaID).FirstOrDefault(),
                    Predmet = Context.Predmeti.Where(p => p.ID == PredmetID).FirstOrDefault(),
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


        [Route("DeleteStorage/{ProdavnicaID}/{PredmetID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteStorage(int ProdavnicaID, int PredmetID)
        {
            if(Context.Prodavnice.Where( p =>p.ID  == ProdavnicaID).FirstOrDefault() == null)
                return BadRequest("Uneta prodavnica nije validan !");
            if(Context.Predmeti.Where( p =>p.ID  == PredmetID).FirstOrDefault() == null)
                return BadRequest("Uneti predmet nije validan !");
            try
             {
                var prodavnica = Context.Prodavnice.Where( p =>p.ID  == ProdavnicaID).FirstOrDefault();
                var predmet = Context.Predmeti.Where( p =>p.ID  == PredmetID).FirstOrDefault();

                var storage = Context.Storages.Where(p => p.Prodavnica == prodavnica && p.Predmet == predmet).FirstOrDefault();

                Context.Storages.Remove(storage);
                await Context.SaveChangesAsync();
                return Ok($"Delete predmeta iz storega sa nazivom: {prodavnica.Naziv} {predmet.Naziv} je uspesan");
             }
             catch(Exception e)
             {
                return BadRequest(e.Message);
             }
        }


        [Route("DeleteAllFromStorage/{ProdavnicaID}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAllFromStorage(int ProdavnicaID)
        {
             if(Context.Prodavnice.Where(p =>p.ID  == ProdavnicaID).FirstOrDefault() == null)
                return BadRequest("Uneta prodavnica nije validan !");
             if(Context.Storages.Where(p => p.Prodavnica.ID == ProdavnicaID).FirstOrDefault() == null)
                return Ok("Nema sta da brise");

             try
             {

                var prodavnica = Context.Prodavnice.Where(p =>p.ID  == ProdavnicaID).FirstOrDefault();

                var storage = Context.Storages.Where(p => p.Prodavnica == prodavnica);

                foreach(var store in storage)
                    Context.Storages.Remove(store);

                await Context.SaveChangesAsync();
                return Ok($"Delete storega sa ID prodavnice: {ProdavnicaID} je uspesan");
             }
             catch(Exception e)
             {
                return BadRequest(e.Message);
             }
        }

       [Route("PreuzmiStorage/{ProdavnicaID}")]
       [HttpGet]
       public async Task<ActionResult> PreuzmiStorage(int ProdavnicaID)
       {
            if (Context.Prodavnice.Where(p => p.ID == ProdavnicaID).FirstOrDefault() == null)
                return BadRequest("Uneti naziv prodavnice nije validan !");

             var RetVal = Context.Storages
             .Where(p => p.Prodavnica.ID == ProdavnicaID);

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