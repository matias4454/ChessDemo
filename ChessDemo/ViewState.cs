using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessDemo
{
    public class ViewState
    {
        public Dictionary<string, Field> OccupiedFields { get; set; }
        public string ErrorMsg { get; set; }
    }
    
}
