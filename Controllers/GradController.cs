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
    public class GradController : ControllerBase
    {
        public ProdavnicaContext Context{get;set;}
        public GradController(ProdavnicaContext context)
        {
            Context = context;
        }


        [Route("PreuzmiGradove")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiGradove()
        {
            try{
                return Ok(await Context.Gradovi.Select(p => new {p.ID, p.Naziv}).ToListAsync());
            }
            catch(Exception e)
            {
                    return BadRequest(e.Message);
            }
        }


        [Route("PreuzmiGradove/{DrzavaID}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiGradove(int DrzavaID)
        {
            var RetVal = Context.Gradovi
             .Where(p => p.Drzava.ID == DrzavaID);

            try{
                return Ok(await RetVal.Select(p => new {p.ID, p.Naziv}).ToListAsync());
            }
            catch(Exception e)
            {
                    return BadRequest(e.Message);
            }
        }


        [Route("DodajGrad/{GradIme}/{DrzavaID}")]
        [HttpPost]
        public async Task<ActionResult> DodajGrad(string GradIme, int DrzavaID)
        {
            bool isDigitIme = GradIme.Any(c => char.IsDigit(c));

            if (string.IsNullOrWhiteSpace(GradIme)|| GradIme.Length < 1 || isDigitIme == true)
                return BadRequest("Uneta Grad nije validna!");

            if(Context.Drzave.Where(p => p.ID == DrzavaID).FirstOrDefault() == null)
                 return BadRequest("Uneta Drzava ne postoji!");

            try
            {
                Grad grad = new Grad
                {
                    Naziv = GradIme,
                    Drzava = Context.Drzave.Where(p => p.ID == DrzavaID).FirstOrDefault()
                };
                Context.Gradovi.Add(grad);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno dodata novi Grad sa nazivom: {grad.Naziv}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}