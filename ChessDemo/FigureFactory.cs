using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessDemo
{
    public abstract class FigureFactory
    {
        //private static string[] figures =  { };
        public static readonly string[] Figures = 
            { 
                "",
                typeof(King).Name,
                typeof(Bishop).Name,
                typeof(Rook).Name
            };
        public static Figure GetFigure(string typeName, int posX, int posY) 
        {
            Figure newObj;

            switch (typeName)
            {
                case "King":
                    newObj = new King(posX, posY);                                
                    break;

                case "Bishop":
                    newObj = new Bishop(posX, posY);
                    break;
                
                case "Rook":
                    newObj = new Rook(posX, posY);
                    break;

              /*  case "Knight":

                    break;*/

                default:
                    throw new Exception("Invalid type of chess figure");
                    
            }
            return newObj;
        }
    }
    public abstract class Figure
    {
        protected string name;
        protected int positionX;
        protected int positionY;

        protected Figure(int currentX, int currentY)
        {
            positionX = currentX;
            positionY = currentY;
        }

        //public abstract bool CanMove(int nextX, int nextY);

        public abstract List<Field> GetFieldsToMove();

    }
    public class King : Figure
    {
        public King(int currentX, int currentY) : base(currentX, currentY)
        { }

        public override List<Field> GetFieldsToMove()
        {
            List<Field> fields = new List<Field>();

            int nextX = positionX - 1;
            int nextY = positionY - 1;
            fields.Add( new Field { FigureName = typeof(King).Name, X = nextX, Y = nextY });

            nextX = positionX;
            nextY = positionY - 1;
            fields.Add(new Field { FigureName = typeof(King).Name, X = nextX, Y = nextY });

            nextX = positionX + 1;
            nextY = positionY - 1;
            fields.Add(new Field { FigureName = typeof(King).Name, X = nextX, Y = nextY });

            nextX = positionX + 1;
            nextY = positionY;
            fields.Add(new Field { FigureName = typeof(King).Name, X = nextX, Y = nextY });

            nextX = positionX + 1;
            nextY = positionY + 1;
            fields.Add(new Field { FigureName = typeof(King).Name, X = nextX, Y = nextY });

            nextX = positionX;
            nextY = positionY + 1;
            fields.Add(new Field { FigureName = typeof(King).Name, X = nextX, Y = nextY });

            nextX = positionX - 1;
            nextY = positionY + 1;
            fields.Add(new Field { FigureName = typeof(King).Name, X = nextX, Y = nextY });

            nextX = positionX - 1;
            nextY = positionY;
            fields.Add(new Field { FigureName = typeof(King).Name, X = nextX, Y = nextY });

            return fields;
        }
    }
    public class Bishop : Figure 
    {
        public Bishop(int currentX, int currentY) : base(currentX, currentY) 
        { }

        public override List<Field> GetFieldsToMove()
        {
            List<Field> fields = new List<Field>();

            for (int i = 1; i < 8; i++)
            {
                int nextX = positionX + i;
                int nextY = positionY + i;
                fields.Add(new Field { FigureName = typeof(Bishop).Name, X = nextX, Y = nextY});

                nextX = positionX - i;
                nextY = positionY + i;
                fields.Add(new Field { FigureName = typeof(Bishop).Name, X = nextX, Y = nextY });

                nextX = positionX - i;
                nextY = positionY - i;
                fields.Add(new Field { FigureName = typeof(Bishop).Name, X = nextX, Y = nextY });

                nextX = positionX + i;
                nextY = positionY - i;
                fields.Add(new Field { FigureName = typeof(Bishop).Name, X = nextX, Y = nextY });
            }
            return fields;
        }
    }
    public class Rook : Figure 
    {
        public Rook(int currentX, int currentY) : base(currentX, currentY)
        { }

        public override List<Field> GetFieldsToMove()
        {
            List<Field> fields = new List<Field>();

            for (int i = 1; i < 8; i++)
            {
                int nextX = positionX;
                int nextY = positionY - i;
                fields.Add(new Field { FigureName = typeof(Bishop).Name, X = nextX, Y = nextY });

                nextX = positionX + i;
                nextY = positionY;
                fields.Add(new Field { FigureName = typeof(Bishop).Name, X = nextX, Y = nextY });

                nextX = positionX;
                nextY = positionY + i;
                fields.Add(new Field { FigureName = typeof(Bishop).Name, X = nextX, Y = nextY });

                nextX = positionX - i;
                nextY = positionY;
                fields.Add(new Field { FigureName = typeof(Bishop).Name, X = nextX, Y = nextY });
            }
            return fields;
        }
    }
}
