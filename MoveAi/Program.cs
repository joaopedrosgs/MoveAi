using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveAi
{
    enum Coord
    {
        Y = 0,
        X = 1
    }

    class Program
    {


        static void Main(string[] args)
        {
            var Ai = new MovementAi();
            int a = 0;
            while (Ai.playerPosition != Ai.objetivePosition)
            {
                Console.Clear();
                Ai.PrintMap();
                Ai.Go();
                Console.Write(a);
                a++;
                Console.ReadKey();


            }
        }
    }
    class MovementAi {
        public  int[] playerPosition;
        public  int[] objetivePosition;
        public  int[,] map;
        public  List<int[]> walked;

        public MovementAi()
        {

            playerPosition = new int[2] { 1, 1 };
            objetivePosition = new int[2] { 9, 8 };
            walked = new List<int[]>();
            


            map = new[,]
            {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, // 2 = Entity
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                {1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                {1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1},
                {1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1},
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                {1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                {1, 0, 0, 0, 0, 0, 0, 0, 9, 0, 0, 0, 0, 1}, // 9 = Objective
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
            };

        }
        public void PrintMap()
        {
            for (int y = 0; y < map.GetUpperBound((int) Coord.Y); y++)
            {
                for (int x = 0; x < map.GetUpperBound((int) Coord.X); x++)
                {
                    if(x == playerPosition[(int)Coord.X] && y == playerPosition[(int)Coord.Y])
                        Console.Write("* ");
                    else
                        Console.Write(map[y,x] + " ");


                }
                Console.Write("\n");
            }
        }
        public void Go()
        {
            var tile = Choose(PossibleMovements());
            playerPosition = tile;

            walked.Add(tile);

        }

        public bool CanGo(int[] tile)
        {
            if (InsideBoundaries(tile) && Walkable(tile))
                return true;
            else
            {
                Console.WriteLine("erro");
                return false;
            }
        }

        public bool InsideBoundaries(int[] tile)
        {
           
            return tile[(int) Coord.X] <= map.GetUpperBound((int) Coord.X) &&
                   tile[(int) Coord.Y] <= map.GetUpperBound((int) Coord.Y) &&
                   tile[(int) Coord.X] >= 0 &&
                   tile[(int) Coord.Y] >= 0;
        }

        public bool Walkable(int[] tile)
        {
            return map[tile[(int) Coord.Y], tile[(int) Coord.X]] == 0;
        }

        public double Distance(int[] tile)
        {
            return Math.Sqrt(
                Math.Pow(tile[(int) Coord.Y] - objetivePosition[(int) Coord.Y], 2)
                +
                Math.Pow(tile[(int) Coord.X] - objetivePosition[(int) Coord.X], 2)
            );
        }

        public int[] Choose(List<int[]> tiles)
        {

            //return tiles.OrderBy(x => (Distance(x))).FirstOrDefault();
            return tiles.OrderBy(x => walked.FindAll(entity=>(entity[0] == x[0] && entity[1]==x[1])).Count()).ThenBy(x => (Distance(x))).FirstOrDefault();

        }

        public List<int[]> PossibleMovements()
        {
            var movements = new List<int[]>();

            for (int y = -1; y <= 1; y++)
                for (int x = -1; x <= 1; x++)
                {
                    int[] tile =
                    {
                        y + playerPosition[(int) Coord.Y],
                        x + playerPosition[(int) Coord.X]
                    };
    

                    if (CanGo(tile) && (x!=0 || y!=0))
                        movements.Add(tile);
                }
          
            return movements;
        }
    }
}