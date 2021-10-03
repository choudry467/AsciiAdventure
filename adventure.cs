using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

/*
 Cool ass stuff people could implement:
 > jumping
 > attacking
 > randomly moving monsters
 > smarter moving monsters
*/
namespace asciiadventure {
    public class Game {
        private static Boolean Eq(char c1, char c2){
            return c1.ToString().Equals(c2.ToString(), StringComparison.OrdinalIgnoreCase);
        }

        private static string Menu() {
            return "WASD to move\nIJKL to attack/interact\nEnter command: ";
        }

        private static void PrintScreen(Screen screen, string message, string menu) {
            Console.Clear();
            Console.WriteLine(screen);
            Console.WriteLine($"\n{message}");
            Console.WriteLine($"\n{menu}");
        }
        public void Run() {
            Console.ForegroundColor = ConsoleColor.Green;

            Screen screen = new Screen(10, 15);
            // add a couple of walls
            for (int i=0; i < 3; i++){
                new Wall(1, 2 + i, screen);
            }
            for (int i=0; i < 4; i++){
                new Wall(5 + i, 4, screen);
            }
            
            // add a player
            Player player = new Player(0, 0, screen, "Zelda");
            
            // add a treasure
            Treasure treasure = new Treasure(6, 2, screen);

            //Add portals and connect them
            Portal portal1 = new Portal(2, 2, screen);
            Portal portal2 = new Portal(8, 4, screen);
            portal1.Connect(portal2);

            //Add Armory
            Armory armory = new Armory(0, 10, screen);

            // add some mobs
            List<Mob> mobs = new List<Mob>();
            mobs.Add(new Mob(9, 14, screen));
            
            // initially print the game board
            PrintScreen(screen, "Welcome!", Menu());
            
            Boolean gameOver = false;
            
            while (!gameOver) {
                char input = Console.ReadKey(true).KeyChar;

                String message = "";

                if (Eq(input, 'q')) {
                    break;
                } else if (Eq(input, 'w')) {
                    message += player.Move(-1, 0) + "\n";
                } else if (Eq(input, 's')) {
                    message += player.Move(1, 0) + "\n";
                } else if (Eq(input, 'a')) {
                    message += player.Move(0, -1) + "\n";
                } else if (Eq(input, 'd')) {
                    message += player.Move(0, 1) + "\n";
                } else if (Eq(input, 'i')) {
                    message += player.Action(-1, 0) + "\n";
                } else if (Eq(input, 'k')) {
                    message += player.Action(1, 0) + "\n";
                } else if (Eq(input, 'j')) {
                    message += player.Action(0, -1) + "\n";
                } else if (Eq(input, 'l')) {
                    message += player.Action(0, 1) + "\n";
                } else if (Eq(input, 'v')) {
                    player.Mine();
                } else {
                    message = $"Unknown command: {input}";
                }

                // OK, now move the mobs
                foreach (Mob mob in mobs){

                    gameOver = mob.move(screen, player);
                    if (gameOver) message += "A MOB GOT YOU! GAME OVER\n";
                }
                if (player.Token == "*") gameOver = true;

                PrintScreen(screen, message, Menu());
            }
        }

        public static void Main(string[] args){
            Game game = new Game();
            game.Run();
        }
    }
}