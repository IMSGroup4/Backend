﻿using ims_group4_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ims_group4_backend.Controllers
{
    [ApiController]
    [Route("api/hw")]
    public class TodoController : ControllerBase
    {
        private readonly HelloWorld hw = new HelloWorld();

        [HttpGet("{id}")]
        public ActionResult<HelloWorld> Get(int id)
        {
            string? lang = hw.Get(id);
            if (lang == null)
            {
                return BadRequest();
            }
            return Ok(lang);
        }

        [HttpPost]
        public ActionResult<HelloWorld> Set(string newLang)
        {
            string newAddedLang = hw.Set(newLang);
            return CreatedAtAction(nameof(Get), new { lang = newAddedLang}, new { Language = newAddedLang });
        }

    }
}