using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    abstract class GeneralItem : Sprite
    {
        public GeneralItem(string s, int x, int y, int knowledge, int charisma, int fitness, int stress, int energy)
            :base(s,x,y)
        {
            this.knowledgeChange = knowledge;
            this.charismaChange = charisma;
            this.fitnessChange = fitness;
            this.stressChange = stress;
            this.energyChange = energy;
        }

        protected int knowledgeChange;
        public int KnowledgeChange
        {
            get { return knowledgeChange; }
            set { KnowledgeChange = value; }
        }

        protected int charismaChange;
        public int CharismaChange
        {
            get { return charismaChange; }
            set { charismaChange = value; }
        }

        protected int fitnessChange;
        public int FitnessChange
        {
            get { return fitnessChange; }
            set { fitnessChange = value; }
        }

        protected int stressChange;
        public int StressChange
        {
            get { return stressChange; }
            set { stressChange = value; }
        }

        protected int energyChange;
        public int EnergyChange
        {
            get { return energyChange; }
            set { energyChange = value; }
        }

    }

    class Item : GeneralItem
    {
        private int timeMin;
        public int TimeMin
        {
            get { return timeMin; }
            set { timeMin = value; }
        }

        public Item(string s, int x, int y, int knowledge, int charisma, int fitness, int stress, int energy, int time)
            :base(s,x,y,knowledge, charisma, fitness, stress, energy)
        {
            this.timeMin = time;
        }

        public Item(string s, int x, int y) //overload konstruktor
            :base(s,x,y,0,0,0,0,0)
        {
            this.timeMin = 0;
        }
    }

    class Book:GeneralItem
    {
        private int moneyValue;
        public int MoneyValue
        {
            get { return moneyValue; }
            set { moneyValue = value; }
        }

        public Book(string s, int x, int y, int knowledge, int charisma, int fitness, int stress, int energy, int mon )
            :base(s,x,y,knowledge, charisma, fitness, stress, energy)
        {
            this.moneyValue = mon;
        }
    }

    

}
