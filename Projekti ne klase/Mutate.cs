using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekti_ne_klase
{
    class Mutate
    {
        Random r = new Random();
        public void ApplySwap(ref int[,] individ, List<Flight> FlightsData)
        {
            int FlightAIndex = getFlightAIndex(individ.GetLength(1));
            int FlightBIndex = getFlightBIndex(individ.GetLength(1),FlightAIndex);
            int temp = individ[0, FlightAIndex];
            individ[0, FlightAIndex] = individ[0, FlightBIndex];
            individ[0, FlightBIndex]=temp;
            int startIndex = (FlightAIndex < FlightBIndex) ? (FlightAIndex) : (FlightBIndex);
            SetChildTime(ref individ, startIndex,FlightsData);
        }

        private void SetChildTime(ref int[,] Individ, int StartIndex, List<Flight> FlightsData)
        {
            int ChildID, ChildLastTime = -1, ChildCurrentScheduledTime;
            if (StartIndex > 0)
            {
                ChildLastTime = Individ[1, StartIndex - 1];
            }
            for (int index = StartIndex; index < Individ.GetLength(1); index++)
            {
                ChildID = Individ[0, index];

                ChildCurrentScheduledTime = FlightsData[ChildID].intScheduledTime;

                if (index == 0)
                {
                    Individ[1, index] = ChildCurrentScheduledTime;
                }
                else
                {
                    int PreviousFlightID = Individ[0, index - 1];
                    int CurrentFlightID = Individ[0, index];
                    char PreviousFlightCat = FlightsData[PreviousFlightID].Cat;
                    char CurrentFlightCat = FlightsData[CurrentFlightID].Cat;
                    char PreviousFlightAD = FlightsData[PreviousFlightID].AD;
                    char CurrentFlightAD = FlightsData[CurrentFlightID].AD;
                    int SeperationTime = BasicFunctions.GetSeperationDistance(PreviousFlightCat, CurrentFlightCat,PreviousFlightAD,CurrentFlightAD);
                    if (ChildCurrentScheduledTime >= (ChildLastTime + SeperationTime))
                    {
                        Individ[1, index] = ChildCurrentScheduledTime;
                    }
                    else
                    {
                        Individ[1, index] = ChildLastTime + SeperationTime;
                    }
                }

                ChildLastTime = Individ[1, index];
            }
        }
        int FindPreviousFlightTime(int[,] Individ, int StartIndex)
        {
            int result = -1;
            int MinimalDistance = int.MaxValue;
            int CurrentFlightID = Individ[0, StartIndex];
            int CurrentFlightTime = Individ[1, CurrentFlightID];
            int TempFlightID=-1;
            int TempFlightTime=-1;
            int DistanceFromCurrenFlight = -1;
            
            for (int i = 0; i < Individ.GetLength(1); i++)
            {
                if (i != StartIndex)
                {
                    TempFlightID=Individ[0, i];
                    TempFlightTime=Individ[1, TempFlightID];
                    DistanceFromCurrenFlight = CurrentFlightTime - TempFlightTime;
                    if (DistanceFromCurrenFlight > 0)
                    {
                        if (DistanceFromCurrenFlight < MinimalDistance)
                        {
                            MinimalDistance = DistanceFromCurrenFlight;
                            result = TempFlightTime;
                        }
                    }
                }
            }
            return result;
        }

        private int getFlightAIndex(int NumberOfFlights)
        {
            return r.Next(0, NumberOfFlights); //kontrollo a e merr vleren maksimale
        }

        private int getFlightBIndex(int NumberOfFlights, int FlightAIndex)
        {
            int FlightBIndex = FlightAIndex;
            int RandomDifference = r.Next(1, Parameters.MutateMaxDistance);
            // Trajtimi i situates ne fillim te vargut
            if (FlightBIndex - RandomDifference < 0)
                FlightBIndex += RandomDifference;
            // Trajtimi i situates ne fund te vargut
            else if (FlightBIndex + RandomDifference > NumberOfFlights - 1)
                FlightBIndex -= RandomDifference;
            else
            { // Trajtimi i situates ne pjesen tjeter te vargut
                bool IsPositive = (r.Next(0, 2) == 1) ? (true) : (false);
                FlightBIndex += (IsPositive) ? (RandomDifference) : (-RandomDifference);
            }
            return FlightBIndex;
        }



       
    }
}
