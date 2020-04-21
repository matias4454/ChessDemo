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
    public class GameController : ControllerBase
    {
        // GET: api/Game
        [HttpGet]
        public string[] GetFieldsToMove(string figure, int posX, int posY)
        {            
            Figure fig;
            List<string> results = new List<string>();
            try
            {
                fig = FigureFactory.GetFigure(figure, posX, posY);
                List<Field> fields = fig.GetFieldsToMove()
                    .Where(p => (p.X < 8) && (p.Y < 8))
                    .Where(p => (p.X >= 0) && (p.Y >= 0))
                    .ToList();
                
                foreach (Field item in fields)
                {
                    string key = string.Format("{0},{1}", item.X, item.Y);
                    results.Add(key);
                }

            }
            catch (Exception)
            {
                ;
            }
            return results.ToArray();
        }

        
        // GET: api/Game/posX|posY|nextX|nextY|figure
        [HttpGet("{dataStr}", Name = "Get")]
        public bool CheckMove(string dataStr)
        {
            string[] splitTab = dataStr.Split("|");
            int posX = Convert.ToInt32(splitTab[0]);
            int posY = Convert.ToInt32(splitTab[1]);
            int nextX = Convert.ToInt32(splitTab[2]);
            int nextY = Convert.ToInt32(splitTab[3]);
            string figure = splitTab[4];
            
            Figure fig;
            List<Field> fields = new List<Field>();
            try
            {
                fig = FigureFactory.GetFigure(figure, posX, posY);
                fields = fig.GetFieldsToMove()
                    .Where(p => (p.X < 8) && (p.Y < 8))
                    .Where(p => (p.X >= 0) && (p.Y >= 0))
                    .ToList();                               
            }
            catch (Exception)
            {
                ;
            }
            return fields.Where(p => p.X.Equals(nextX) && p.Y.Equals(nextY)).Count() > 0;
        }

        // POST: api/Game
        [HttpPost]
        public bool Post([FromBody] string val)
        {

            return true;
        }

    }
}
