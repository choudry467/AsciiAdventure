using System;
using System.Collections.Generic;

namespace asciiadventure {
    public class Mob : MovingGameObject {

        public Boolean alive;
        public Mob(int row, int col, Screen screen) : base(row, col, "#", screen) { alive = true;}

        public Boolean move(Screen screen, Player player){
            if (!alive) return false;
            List<Tuple<int, int>> moves = screen.GetLegalMoves(this.Row, this.Col);
            if (moves.Count == 0){
                return false;
            }

            //Linq doesn't have shortcut for this and the only other way was comparable which
            // I felt was overkill
            
            int deltaRow = 0;
            int deltaCol = 0;
            int min = int.MaxValue;
            foreach(var (y, x) in moves){
                int score = Manhattan(player, this.Row + y, this.Col + x);
                if (score < min){
                    min = score;
                    deltaRow = y;
                    deltaCol = x;
                }
            }
            if (screen[this.Row + deltaRow, this.Col + deltaCol] is Player){
                this.Move(deltaRow, deltaCol);
                this.Token = "*";
                return true;
            }else if (screen[this.Row + deltaRow, this.Col + deltaCol] is Mine){
                this.Move(deltaRow, deltaCol);
                this.Delete();
                alive = false;
                return false;
            }
            this.Move(deltaRow, deltaCol);
            return false;


        }
        private int Manhattan(Player player, int y, int x){
            return Math.Abs(player.Col - x) + Math.Abs(player.Row - y);
        }
    }
}