using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    class NPC: Sprite
    {
        private string message; //poruka koja se ispiše
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        private int timeMin; //vrijeme u minutama koje se potroši prilikom razgovora s NPC
        public int TimeMin
        {
            get { return timeMin; }
            set { timeMin = value; }
        }

        protected int charismaValue; //vrijednost za koju se charisma uvećava
        public int CharismaValue
        {
            get { return charismaValue; }
            set { charismaValue = value; }
        }

        protected int skillValue; //vrijednost za koju se skill uvećava
        public int SkillValue
        {
            get { return skillValue; }
            set { skillValue = value; }
        }

        public NPC(string s, int x, int y, int charisma, int skill, int time, string mes="Hello!")
            :base (s,x,y)
        {
            this.CharismaValue = charisma;
            this.SkillValue = skill;
            this.TimeMin = time;
            this.Message = mes;
        }
    }

    class EnergyException : Exception
    {
        private static string message = "You don't have enough energy!";
        public EnergyException()
            :base(message)
        {
        }
    }

    class StressException: Exception
    {
        private static string message = "You are too stressed!";
        public StressException()
            :base(message)
        {
        }
    }
}
