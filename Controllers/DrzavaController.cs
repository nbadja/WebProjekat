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
    public class DrzavaController : ControllerBase
    {
        public ProdavnicaContext Context{get;set;}
        public DrzavaController(ProdavnicaContext context)
        {
            Context = context;
        }


        [Route("PreuzmiDrzave")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiDrzave()
        {
            try{
                return Ok(await Context.Drzave.Select(p => new {p.ID, p.Naziv}).ToListAsync());
            }
            catch(Exception e)
            {
                    return BadRequest(e.Message);
            }
        }

        [Route("DodajDrzavu/{DrzavaIme}")]
        [HttpPost]
        public async Task<ActionResult> DodajDrzavu(string DrzavaIme)
        {
            bool isDigitIme = DrzavaIme.Any(c => char.IsDigit(c));

            if (string.IsNullOrWhiteSpace(DrzavaIme)|| DrzavaIme.Length < 1 || isDigitIme == true)
                return BadRequest("Uneta Drzava nije validna!");

            try
            {
                Drzava drzava = new Drzava{
                    Naziv = DrzavaIme
                };
                Context.Drzave.Add(drzava);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno dodata nova Drzava sa nazivom: {drzava.Naziv}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}