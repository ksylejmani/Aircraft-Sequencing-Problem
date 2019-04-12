using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekti_ne_klase
{
    class Data
    {

        public List<Flight> GetFlightData()
        {
            List<Flight> Result = new List<Flight>();

            Result.Add(new Flight("VLE7100", "A320", 'M', 'D', "14:55", 5));
            Result.Add(new Flight("VLE7096", "A320", 'M', 'D', "14:55", 5));
            Result.Add(new Flight("AZA2061", "A320", 'M', 'D', "15:00", 5));
            Result.Add(new Flight("ONG254", "H25C", 'M', 'A', "15:02", 2));
            Result.Add(new Flight("ISS1508", "MD82", 'M', 'D', "15:05", 6));
            Result.Add(new Flight("SMX5297", "A319", 'M', 'D', "15:15", 4));
            Result.Add(new Flight("VLE7110", "MD82", 'M', 'A', "15:15", 6));
            Result.Add(new Flight("CYL5001", "A320", 'M', 'A', "15:18", 5));
            Result.Add(new Flight("AZA410", "E170", 'M', 'D', "15:25", 3));
            Result.Add(new Flight("VLE7088", "A319", 'M', 'D', "15:25", 4));

            //Result.Add(new Flight("AZA2063", "A320", 'M', 'D', "15:30", 5));
            //Result.Add(new Flight("NJE210A", "C56X", 'M', 'A', "15:31", 1));
            //Result.Add(new Flight("CYL5022", "A320", 'M', 'A', "15:34", 5));
            //Result.Add(new Flight("AZA080", "A320	", 'M', 'D', "15:35", 5));
            //Result.Add(new Flight("BAW14GM", "A319", 'M', 'A', "15:35", 4));
            //Result.Add(new Flight("ICFLY", "LJ31", 'M', 'A', "15:35", 2));
            //Result.Add(new Flight("ISS1205", "A319", 'M', 'A', "15:37", 4));
            //Result.Add(new Flight("SMX5985", "A320", 'M', 'A', "15:37", 5));
            //Result.Add(new Flight("BEL7PC", "RJ85", 'M', 'A', "	15:42", 3));
            //Result.Add(new Flight("SNM643", "F2TH", 'M', 'A', "15:47", 1));

            //DateTime dateTime = DateTime.ParseExact(time, "HH:mm:ss", CultureInfo.InvariantCulture);

            return Result;
        }


       

        public List<Flight> GetFromFile(string filePath)
        {
            List<Flight> lst = new List<Flight>();

            var lines = File.ReadAllLines(filePath).ToList();

            foreach (var ln in lines)
            {
                string[] st = ln.Split(((char)9));
                if (st.Count() == 6)
                {
                    lst.Add(new Flight(st[0], st[1], char.Parse(st[2]), char.Parse((st[3])), FormatTime(st[4]), int.Parse(st[5])));

                }
            }

            return lst.OrderBy(rw => rw.intScheduledTime).ToList() ;
            
        }


        string FormatTime(string tm)
        {

            return string.Format("{0}:{1}", tm.Substring(0, 2), tm.Substring(2, 2));
        }
    
    }
}
