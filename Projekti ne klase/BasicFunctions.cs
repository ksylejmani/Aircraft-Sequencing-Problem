using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Projekti_ne_klase
{
    class BasicFunctions
    {
        public static Random rand = new Random(DateTime.Now.Millisecond);
        public static Stopwatch sw = new Stopwatch();
        public static long calculationTime;
        public static void IndividInsert(ref int[,] Population, int[,] Individ, int IndividIndex)
        {
            for (int i = 0; i < Individ.GetLength(1); i++)
            {
                Population[IndividIndex, i] = Individ[0, i];
                Population[IndividIndex + 1, i] = Individ[1, i];

            }
        }
        public static int[,] GetIndivid(int[,] P, int IndividIndex)
        {
            int[,] result = new int[2, P.GetLength(1)];
            for (int i = 0; i < P.GetLength(1); i++)
            {
                result[0, i] = P[IndividIndex, i];
                result[1, i] = P[IndividIndex + 1, i];
            }
            return result;
        }

        public static void SetCalculatedFlightTime(int[,] Individ, List<Flight> FlightsData)
        {
            for (int i = 0; i < Individ.GetLength(1); i++)
            {
                int FlightID = Individ[0, i];
                int FlightTime = Individ[1, i];
                FlightsData[FlightID].SetTimeToDateTime(FlightTime);
            }
        }
        public static void SetCalculatedPosition(int[,] Individ, List<Flight> FlightsData)
        {
            for (int i = 0; i < Individ.GetLength(1); i++)
            {
                int FlightID = Individ[0, i];
                //int FlightTime = Individ[1, i];
                FlightsData[FlightID].CalculatedPosition = i;
            }
        }

        public static void PrintIndivid(int[,] P, int IndividIndex)
        {
            for (int i = 0; i < P.GetLength(1); i++)
            {
                Console.Write("[" + P[IndividIndex, i] + ", " + P[IndividIndex + 1, i] + "]");
            }
            Console.WriteLine();
        }
        public static void PrintIndivid(int[,] P)
        {
            for (int i = 0; i < P.GetLength(1); i++)
            {
                Console.Write("[" + P[0, i] + ", " + P[1, i] + "]");
            }
            Console.WriteLine();
        }
        public static void PrintFlightTime(List<Flight> FlightsData)
        {
            Console.Write("Sched. time: ");
            foreach (Flight flight in FlightsData)
            {
                Console.Write("<" + flight.FlightNumber + " - " + flight.Weight + " - " + flight.ScheduledTime.Hour + ":" + flight.ScheduledTime.Minute + "> ");
            }
            Console.Write("\nCalcu. time: ");
            foreach (Flight flight in FlightsData)
            {
                Console.Write("<" + flight.FlightNumber + " - " + flight.Weight + " - " + flight.CalculatedTime.Hour + ":" + flight.CalculatedTime.Minute + "> ");
            }
        }
        public static List<int> GetPopulationIndexList()
        {
            List<int> result = new List<int>();
            for (int i = 0; i < Parameters.PopSize*2; i+=2)
            {
                result.Add(i);
            }
            return result;
        }
        public static List<int> GetTournamentSelectionIndexList(List<int> PopulationIndexList)
        {
            List<int> result = new List<int>();
            List<int> TempList = new List<int>(PopulationIndexList);
            int RandomIndex = -1;
            for (int i = 0; i < Parameters.TournamentSize; i ++)
            {
                RandomIndex = rand.Next(0, TempList.Count - 1);
                result.Add(TempList[RandomIndex]);
                TempList.RemoveAt(RandomIndex);
            }
            return result;
        }

        public static int GetParentIndex(List<int> TournamentSelectionIndexList, int [,] PopQuality)
        {
            int MinIndex = TournamentSelectionIndexList[0];
            int MinQuality=PopQuality[1,MinIndex/2];
            for (int i = 1; i < TournamentSelectionIndexList.Count; i++)
            {
                int CurrentIndex=TournamentSelectionIndexList[i];
                if (PopQuality[1, CurrentIndex / 2] < MinQuality)
                {
                    MinQuality = PopQuality[1, CurrentIndex / 2];
                    MinIndex = CurrentIndex;
                }
            }
            return MinIndex;
        }

        public static int GetSeperationDistance(char FirstFlightCat, char SecondFlightCat, char FirstFlightAD, char SecondFlightAD)
        {
            int result = -1;
            if (FirstFlightAD == 'A' && SecondFlightAD == 'A')
            {
                if (FirstFlightCat == 'M' && SecondFlightCat == 'L')
                    result = 3;
                else if (FirstFlightCat == 'H' && SecondFlightCat == 'L')
                    result = 4;
                else
                    result = 2;
            }
            else
                result = 2;
            return result;
        }
        public static void CalculatePeriodOfSchedule(List<Flight> FlightsData)
        {

        }
    }
}
