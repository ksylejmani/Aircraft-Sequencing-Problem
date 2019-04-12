using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekti_ne_klase
{
    class InitialSolution
    {
        
        Random r = new Random();
        
        public int[,] GetRandomSolution(List<Flight> FlightsData)
        {
            int[,] result = new int[2, FlightsData.Count];
            int resultIndex=0;
            List<int> Temp=new List<int>();
            int LastAssignedTime =0;
            int TempTime=0;
            for (int FlightID = 0; FlightID <=result.GetLength(1); FlightID++)
            {
                if (FlightID<result.GetLength(1) && Temp.Count == 0)
                {
                    Temp.Add(FlightID);
                    TempTime = FlightsData[FlightID].intScheduledTime;
                }
                else if (FlightID < result.GetLength(1) && FlightsData[FlightID].intScheduledTime == TempTime)
                {
                    Temp.Add(FlightID);
                }
                else
                {
                    if(FlightID<result.GetLength(1))
                        FlightID--;
                    while (Temp.Count>0)
                    {                       
                        int RandIndex = r.Next(Temp.Count);
                        result[0, resultIndex] = Temp[RandIndex];
                        int SeperationTime;
                        if (resultIndex == 0)
                            SeperationTime = 0;
                        else
                        {
                            int PreviousFlightID = result[0, resultIndex - 1];
                            int CurrentFlightID = result[0, resultIndex];
                            char PreviousFlightCat = FlightsData[PreviousFlightID].Cat;
                            char CurrentFlightCat = FlightsData[CurrentFlightID].Cat;
                            char PreviousFlightAD = FlightsData[PreviousFlightID].AD;
                            char CurrentFlightAD = FlightsData[CurrentFlightID].AD;
                            SeperationTime = BasicFunctions.GetSeperationDistance(PreviousFlightCat, CurrentFlightCat,PreviousFlightAD,CurrentFlightAD);
                        }
                        if (LastAssignedTime == 0 || FlightsData[Temp[RandIndex]].intScheduledTime - LastAssignedTime>=SeperationTime)
                        {
                            result[1, resultIndex] = FlightsData[Temp[RandIndex]].intScheduledTime;
                        }
                        else
                        {
                            // Per rastet:
                            // LastAssignedTime > FlightsData[Temp[RandIndex]].intScheduledTime
                            // FlightsData[Temp[RandIndex]].intScheduledTime-LastAssignedTime<2
                            result[1, resultIndex] = LastAssignedTime + SeperationTime;
                        }
                        // Pergaditjet per shtimin e fluturimit vijues
                        LastAssignedTime = result[1, resultIndex];
                        Temp.RemoveAt(RandIndex);
                        resultIndex++;
                    }
                }
            }

            return result;
        }
    }
}
