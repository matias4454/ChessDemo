using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChessDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FigureController : ControllerBase
    {
        // GET: api/Game
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //string[] figures = new string[FigureFactory.Figures.Length];
            
            return FigureFactory.Figures;
        }
    }
}