using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    class Player : Sprite
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private bool gender; // false for male, true for female
        public bool Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        
        //Skills
        private int knowledge;
        public int Knowledge
        {
            get { return knowledge; }
            set
            {
                if (value < 0)
                    knowledge = 0;
                else if (value > 100)
                    knowledge = 100;
                else
                    knowledge = value;
            }
        }

        private int charisma;
        public int Charisma
        {
            get { return charisma; }
            set
            {
                if (value < 0)
                    charisma = 0;
                else if (value > 100)
                    charisma = 100;
                else
                    charisma = value;
            }
        }

        private int fitness;
        public int Fitness
        {
            get { return fitness; }
            set
            {
                if (value < 0)
                    fitness = 0;
                else if (value > 100)
                    fitness = 100;
                else
                    fitness = value;

            }
        }

        private int stress;
        public int Stress
        {
            get { return stress; }
            set
            {
                if (value < 0)
                    stress = 0;
                else if (value > 80)
                    throw new StressException();
                else
                    stress = value;
            }
        }

        private int energy;
        public int Energy
        {
            get { return energy; }
            set
            {
                if (value < 10)
                    throw new EnergyException();
                else if (value > 100)
                    energy = 100;
                else
                    energy = value;

            }
        }

        private int money;
        public int Money
        {
            get { return money; }
            set
            {
                if (value < 0)
                    throw (new ArgumentException());
                else money = value;
            }
        }

        private string place;
        public string Place
        {
            get { return place; }
            set { place = value; }
        }

        public string CheckPlace()
        {
            if (this.X >= 0 && this.X <= 150 && this.Y <= 150)
                return "home";
            else if (this.X >= 200 && this.X <= 350 && this.Y <= 150)
                return "university";
            else if (this.X >= 400 && this.X <= 550 && this.Y <= 150)
                return "lounge";
            else if (this.X >= 0 && this.X <= 150 && this.Y >= 270)
                return "shop";
            else if (this.X >= 200 && this.X <= 350 && this.Y >= 270)
                return "company";
            else if (this.X >= 400 && this.X <= 550 && this.Y >= 270)
                return "gym";
            else
                return "";
        }
                
        public override int X
        {
            get { return base.X; }
            set {
                if (value < GameOptions.LeftEdge-GameOptions.SpriteWidth/2)
                    base.X = GameOptions.RightEdge-GameOptions.SpriteWidth-50;
                else if (value > GameOptions.RightEdge-GameOptions.SpriteWidth)
                    base.X = -GameOptions.SpriteWidth / 2;
                else
                    base.X = value;
            }
        } //override-ano svojstvo za X koordinatu
       
        public Player(string s, int x, int y)
            :base (s,x,y)
        {
        } //konstruktor

        //metode za kretanje, osim što se lik pomiče, mijenjaju mu se i kostimi svakim korakom
        public void Right(int k, ref int step)
        {
            //step++;
            this.X += k;
            if (step % 3 == 0) { this.CurrentCostume = this.Costumes[3]; }
            if (step % 3 == 1) { this.CurrentCostume = this.Costumes[4]; }
            if (step % 3 == 2) { this.CurrentCostume = this.Costumes[5]; }
        }
        public void Left(int k, ref int step)
        {
            //step++;
            this.X -= k;
            if (step % 3 == 0) { this.CurrentCostume = this.Costumes[9]; }
            if (step % 3 == 1) { this.CurrentCostume = this.Costumes[10]; }
            if (step % 3 == 2) { this.CurrentCostume = this.Costumes[11]; }
        }
        public void Up(int k, ref int step)
        {
            //step++;
            this.Y -= k;
            if (step % 3 == 0) { this.CurrentCostume = this.Costumes[6]; }
            if (step % 3 == 1) { this.CurrentCostume = this.Costumes[7]; }
            if (step % 3 == 2) { this.CurrentCostume = this.Costumes[8]; }
        }
        public void Down(int k, ref int step)
        {
            //step++;
            this.Y += k;
            if (step % 3 == 0) { this.CurrentCostume = this.Costumes[0]; }
            if (step % 3 == 1) { this.CurrentCostume = this.Costumes[1]; }
            if (step % 3 == 2) { this.CurrentCostume = this.Costumes[2]; }
        }
    }

    
}
