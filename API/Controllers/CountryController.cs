using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.Countries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class CountryController : BaseApiController
    {
        [HttpGet]   // api/studentinfo
        public async Task<ActionResult<List<Country>>> GetCountryList()
        {
            return await Mediator.Send(new List.Query());
        }
    }
}