using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekti_ne_klase
{
    public  class Constraints
    {
        public static int AssessFitness(int[,] P, int individIndex, List<Flight> FlightsData)
        {
            int result = 0;
            for (int i = 0; i < P.GetLength(1); i++)
            {
                int flightID=P[individIndex,i];
                result += FlightsData[flightID].Weight *
                    (P[individIndex + 1, i] -
                    FlightsData[flightID].intScheduledTime);
            }

            return result;
        }
        public static int AssessFitness(int[,] Individ, List<Flight> FlightsData)
        {
            int result = 0;
            for (int i = 0; i < Individ.GetLength(1); i++)
            {
                int flightID = Individ[0,i];
                result += FlightsData[flightID].Weight *
                    (Individ[1, i] -
                    FlightsData[flightID].intScheduledTime);
            }
            return result;
        }
        public static int CheckMinSeparation(int[,] individ, List<Flight> FlightsData)
        {
            for (int i = 0; i < individ.GetLength(1) - 1; i++)
            {
                int NextFlightID = individ[0, i + 1];
                int CurrentFlightID = individ[0, i];
                char NextFlightCat = FlightsData[NextFlightID].Cat;
                char CurrentFlightCat = FlightsData[CurrentFlightID].Cat;
                char NextFlightAD = FlightsData[NextFlightID].AD;
                char CurrentFlightAD = FlightsData[CurrentFlightID].AD;
                int SeperationTime = BasicFunctions.GetSeperationDistance(CurrentFlightCat, NextFlightCat,CurrentFlightAD,NextFlightAD);
                if (Math.Abs(individ[1, i + 1] - individ[1, i]) < SeperationTime)
                {
                    return i;
                }
            }
            return -1;
        }

        public static int CheckNoAssignBefore(int[,] individ, List<Flight> FlightsData)
        {
            int CurrentFlightID;

            for (int i = 0; i < individ.GetLength(1) - 1; i++)
            {
                CurrentFlightID = individ[0, i];

                if (individ[1, i] < FlightsData[CurrentFlightID].intScheduledTime)
                {
                    return i;
                }
            }

            return -1;
        }

        public static List<int> GetFlightList(int NumberOfFlights)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < NumberOfFlights; i++)
            {
                result.Add(i);
            }
            return result;
        }

        public static bool CheckAllFlightAreAssigned(int[,] individ, List<Flight> FlightsData)
        {
            bool result=true;
            int CurrentFlightID;
            List<int> ListOfFlights = GetFlightList(FlightsData.Count);
            for (int i = 0; i < individ.GetLength(1); i++)
            {
                CurrentFlightID = individ[0, i];
                if (!ListOfFlights.Contains(CurrentFlightID))
                {
                    return false;
                }
            }
            return result;
        }

    }
}
