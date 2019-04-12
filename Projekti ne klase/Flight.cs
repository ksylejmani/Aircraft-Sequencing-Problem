using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekti_ne_klase
{
    public class Flight
    {
        //public String FlightNumber;
        //public String Type;
        //public Char Cat;
        //public Char AD;
        //public DateTime ScheduledTime;
        //public int intScheduledTime;
        //public int Weight;

        public string FlightNumber { get; set; }
        public string Type { get; set; }
        public char Cat { get; set; }
        public char AD { get; set; }
        public DateTime ScheduledTime { get; set; }
        public int intScheduledTime { get; set; }
        public DateTime CalculatedTime { get; set; }
        public int CalculatedPosition { get; set; }
        public int Weight { get; set; }



        public int GetTimeToInt()
        {
            return this.ScheduledTime.Hour * 60 + this.ScheduledTime.Minute;
        }
        public void SetTimeToDateTime(int intCalculatedTime )
        {
            int Hour =intCalculatedTime / 60;
            int Minutes = intCalculatedTime % 60;
            CalculatedTime = Convert.ToDateTime("10.22.2014 " + Hour + ":" + Minutes + ":00");
        }

        public Flight(String FN, String T, Char C, Char AD, String Time, int Weight)
        {
            this.FlightNumber = FN;
            this.Type = T;
            this.Cat = C;
            this.AD = AD;
            this.ScheduledTime = Convert.ToDateTime("10.22.2014 " + Time + ":00");
            this.intScheduledTime = this.GetTimeToInt();
            this.Weight = Weight;
            this.CalculatedTime = DateTime.Now;
        }


    }
}
