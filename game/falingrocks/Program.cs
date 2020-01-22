using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace falingrocks
{
    class Program
    {
        static void Main()
        {
            // all  window length is 30 , width is 60 
            Random r = new Random();
            int[,] rockspozition = new int[30, 40];
            Console.BufferHeight = Console.WindowHeight = 30;
            Console.BufferWidth = Console.WindowWidth = 60;
            int playw = 40;
            //dwarf initial position in the middel of play 
            int dwarfx = playw/2;
            // put dwarf at the window higth -1 to not be under the window bordars
            int dwarfy = Console.WindowHeight-1;
            // dwarf design
            string dwarf = "<0>";
            // initialize timer 
            var scoretimer = Stopwatch.StartNew();
            bool isgameover = false;
            while (!isgameover)
            {
                Console.Clear();
                // handel keyboard
                if (Console.KeyAvailable==true) 
                {

                    ConsoleKeyInfo press = Console.ReadKey(true);
                    // to let me press key more than one time
                    while (Console.KeyAvailable==true)
                    {
                         Console.ReadKey(true);
                    }
                         if (press.Key == ConsoleKey.LeftArrow) 
                         {
             //dwarf has 3char to be at the end of window it should be at -2 from widow                             
                            if(dwarfx-1>=0)
                             dwarfx -= 2;
                         }
                         else if (press.Key == ConsoleKey.RightArrow)
                         {
                             if (dwarfx +3 <playw)
                                 dwarfx += 2;
                         }
                        
                    
                }
                // creat Rocks
                int numofrocks = r.Next(1,3);
                //initialize rocks
                
                for (int i = 0; i < numofrocks; i++)
                {
                    int rocklength = r.Next(1,3);
                    // play xposition is 40 
                    int rockpositionx = r.Next(0,playw-1);
                    // generate each rock
                    for (int y = 0; y < rocklength; y++)
                    {
//rockpositionx+y for the rock of length 3 first one is in the positionx , second is same last+1
                        rockspozition[0, (rockpositionx + y)] = 1;
                    }
                    
                }
                //move rockes dowen
                //yposition for window is 30 
                for (int t = 29; t >= 0; t--)
                { 
                    //xposition for play window is playw=40
                    for (int i = 0; i < playw; i++)
                    {
                        if (rockspozition[t, i] == 1)
                        {
                            rockspozition[t, i] = 0;
                            //move down y++
                            rockspozition[t + 1, i] = 1;
                        }
                    }
                    // if two rocks are above of each other or the rock at the end of window
                    bool isconflict;
                    int counter=0;
                    for (int col = 0; col < playw; col++)
                    {
                        //dwarfx"("==col or dwarfx+1"0" ==col or dwarfx+2")"==col
                        if (rockspozition[29, col] == 1 && (dwarfx == col || dwarfx + 1 == col || dwarfx + 2 == col))
                        {
                            isgameover = true;

                        }
                    }
                   
                       
                }

                //print dwarf
                Console.SetCursorPosition(dwarfx,dwarfy);
                Console.Write(dwarf);

                //print rocks
                //y
                for (int y = 0; y < 30; y++)
                {
                    for (int x = 0; x < playw; x++)
                    {
                        if (rockspozition[y, x] == 1)
                        { //if we put (y,x) it will print right and  left
                            Console.SetCursorPosition(x,y);
                            Console.Write(" $ ");
                        }
                    }
                }
                //clear last raw 
                for (int col = 0; col < playw; col++)
                {
                    rockspozition[29, col] = 0;
                }
                //draw vitrical line in the right of widow
                for (int row = 0; row < 30; row++)
                {
                    Console.SetCursorPosition(playw,row);
                    Console.Write("|");
                }
                //draw score
                Console.SetCursorPosition(playw+1,3);
                Console.Write("Your Score is :"+scoretimer.ElapsedMilliseconds/20);
                //add sleep time between every new drawing of drawf
                Thread.Sleep(250);
                //end it 
                if (isgameover == true)
                {
                    Console.Clear();
                    Console.WriteLine("\n\n\n\n\n\n\n\n\t\t Game over \n\n\t\t Your Score is : {0}\n\n\n\n\n\n\n\t\t",scoretimer.ElapsedMilliseconds/20);
                    Console.ReadLine();
                }

            }

           

           
}}}
