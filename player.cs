using System;

namespace asciiadventure {
    public class Player : MovingGameObject {

        Boolean armed;
        public Player(int row, int col, Screen screen, string name) : base(row, col, "@", screen) {
            Name = name;
            this.armed = false;
        }
        public string Name {
            get;
            protected set;
        }
        public override Boolean IsPassable(){
            return true;
        }
        public void Mine(){
            if (armed){
                Screen[Row, Col] = new Mine(Row, Col, Screen);
                armed = false;
            }
            
        }
        public String Action(int deltaRow, int deltaCol){
            int newRow = Row + deltaRow;
            int newCol = Col + deltaCol;
            if (!Screen.IsInBounds(newRow, newCol)){
                return "nope";
            }
            GameObject other = Screen[newRow, newCol];
            if (other == null){
                return "negative";
            }
            // TODO: Interact with the object
            if (other is Treasure){
                other.Delete();
                return "Yay, we got the treasure!";
            }
            if (other is Armory){
                return arm();
            }
            return "ouch";
        }
        private string arm(){
            this.armed = true;
            return "Time to retaliate";
        }
    }
}