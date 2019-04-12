using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekti_ne_klase
{
    class Crossover: Mutate
    {
        Random r = new Random();
        public void GenerateChildren(ref int[,] ChildA, ref int[,] ChildB, int[,] ParentA, int[,] ParentB, List<Flight> FlightsData)
        { 
            int ParentAindex = getParentAIndex(ParentA.GetLength(1));
            int ParentBindex = getParentBIndex(ParentB.GetLength(1),ParentAindex);
            List<int> ParentAremoveList = GetRemoveList(ParentA, ParentAindex);
            List<int> ParentBremoveList = GetRemoveList(ParentB, ParentBindex);
            List<int> ChildAMissingList = GetMissingList(ParentAremoveList, ParentBremoveList);
            List<int> ChildBMissingList = GetMissingList(ParentBremoveList,ParentAremoveList);
            ChildA = GetChild(ParentA, ParentBremoveList, ChildAMissingList, ChildBMissingList, ParentAindex,FlightsData);
            ChildB = GetChild(ParentB, ParentAremoveList, ChildBMissingList, ChildAMissingList, ParentBindex,FlightsData);           
        }
        public void GenerateChildren( ref int[,] ChildB, int[,] ParentA, int[,] ParentB, List<Flight> FlightsData)
        {
            int ParentAindex = getParentAIndex(ParentA.GetLength(1));
            int ParentBindex = getParentBIndex(ParentB.GetLength(1), ParentAindex);
            List<int> ParentAremoveList = GetRemoveList(ParentA, ParentAindex);
            List<int> ParentBremoveList = GetRemoveList(ParentB, ParentBindex);
            List<int> ChildAMissingList = GetMissingList(ParentAremoveList, ParentBremoveList);
            List<int> ChildBMissingList = GetMissingList(ParentBremoveList, ParentAremoveList);
            ChildB = GetChild(ParentB, ParentAremoveList, ChildBMissingList, ChildAMissingList, ParentBindex, FlightsData);
        }
        private int getParentAIndex(int NumberOfFlights)
        { 
            return r.Next(0, NumberOfFlights - Parameters.CrossoverWindowWidth); //kontrollo a e merr vleren maksimale
        }
        private int getParentBIndex(int NumberOfFlights, int ParentAIndex)
        {
            int ParentBIndex = ParentAIndex;
            int RandomDifference = r.Next(0, Parameters.CrossoverMaxDifference);
            // Trajtimi i situates ne fillim te vargut
            if (ParentBIndex - RandomDifference < 0)
                ParentBIndex += RandomDifference;
            // Trajtimi i situates ne fund te vargut
            else if (ParentBIndex + RandomDifference > NumberOfFlights - 1)
                ParentBIndex -= RandomDifference;
            else
            { // Trajtimi i situates ne pjesen tjeter te vargut
                bool IsPositive = (r.Next(0, 1) == 1) ? (true) : (false);
                ParentBIndex += (IsPositive) ? (RandomDifference) : (-RandomDifference);
            }
            return ParentBIndex;
        }
        private List<int> GetRemoveList(int [,]Parent, int RemoveIndex)
        {
            List<int> Result = new List<int>();
            for (int i =RemoveIndex; i < RemoveIndex + Parameters.CrossoverWindowWidth; i++)
            {
                Result.Add(Parent[0, i]);
            }
            return Result;
        }
        private List<int> GetMissingList(List<int> FirstRemoveList, List<int> SecondRemoveList)
        { 
            List<int> Result = new List<int>();
            for (int i = 0; i < FirstRemoveList.Count; i++)
			{
                if (!SecondRemoveList.Contains(FirstRemoveList[i]))
                {
                    Result.Add(FirstRemoveList[i]);
                }
			}
            return Result;
        }

        private int[,] GetChild(int[,] Parent, List<int> PartnerRemoveList, 
                                List<int> PersonalMissingList,
                                List<int> PartnerMissingList,int RemoveIndex,
                                List<Flight> FlightsData)
        {
            int[,] Result = new int[2, Parent.GetLength(1)];
            List<int> localPartnerRemoveList = new List<int>();
            List<int> localPersonalMissingList = new List<int>();
            List<int> localPartnerMissingList = new List<int>();
            localPartnerRemoveList.AddRange(PartnerRemoveList.GetRange(0, PartnerRemoveList.Count));
            localPersonalMissingList.AddRange(PersonalMissingList.GetRange(0, PersonalMissingList.Count));
            int RandomIndex;
            for (int i = 0; i < Result.GetLength(1); i++)
            {
                if (i >= RemoveIndex && i < RemoveIndex + Parameters.CrossoverWindowWidth)
                {
                    RandomIndex = r.Next(0, localPartnerRemoveList.Count - 1);
                    Result[0, i] = localPartnerRemoveList[RandomIndex];
                    localPartnerRemoveList.RemoveAt(RandomIndex);
                }
                else if (localPersonalMissingList.Count > 0 && PartnerMissingList.Contains(Parent[0, i]))
                {
                    RandomIndex = r.Next(0, localPersonalMissingList.Count - 1);
                    Result[0, i] = localPersonalMissingList[RandomIndex];
                    localPersonalMissingList.RemoveAt(RandomIndex);
                }
                else
                {
                    Result[0, i] = Parent[0,i];
                }
            }

            SetChildTime(ref Result,FlightsData);

            return Result;
        }

        private void SetChildTime(ref int[,] Child, List<Flight> FlightsData)
        {
            int ChildID, ChildLastTime = -1, ChildCurrentScheduledTime;

            for (int index = 0; index < Child.GetLength(1); index++)
            {
                ChildID = Child[0, index];

                ChildCurrentScheduledTime = FlightsData[ChildID].intScheduledTime;

                if (index == 0)
                {
                    Child[1, index] = ChildCurrentScheduledTime;
                }
                else
                {
                    int PreviousFlightID = Child[0, index-1];
                    int CurrentFlightID =  Child[0, index];
                    char PreviousFlightCat = FlightsData[PreviousFlightID].Cat;
                    char CurrentFlightCat = FlightsData[CurrentFlightID].Cat;
                    char PreviousFlightAD = FlightsData[PreviousFlightID].AD;
                    char CurrentFlightAD = FlightsData[CurrentFlightID].AD;
                    int SeperationTime = BasicFunctions.GetSeperationDistance(PreviousFlightCat, CurrentFlightCat, PreviousFlightAD, CurrentFlightAD);
                    if (ChildCurrentScheduledTime >= (ChildLastTime + SeperationTime))
                    {
                        Child[1, index] = ChildCurrentScheduledTime;
                    }
                    else
                    {
                        Child[1, index] = ChildLastTime + SeperationTime;
                    }
                }

                ChildLastTime = Child[1, index];
            }
        }
        //private void SetChildTime(ref int[,] Child)
        //{
        //    int ChildID, ChildLastTime = -1, ChildCurrentScheduledTime;

        //    for (int index = 0; index < Child.GetLength(1); index++)
        //    {
        //        ChildID = Child[0, index];

        //        ChildCurrentScheduledTime = Algorithm.FlightsData[ChildID].intScheduledTime;

        //        if (index == 0)
        //        {
        //            Child[1, ChildID] = ChildCurrentScheduledTime;
        //        }
        //        else
        //        {
        //            if (ChildCurrentScheduledTime >= (ChildLastTime + Parameters.MinimumShiftTime))
        //            {
        //                Child[1, ChildID] = ChildCurrentScheduledTime;
        //            }
        //            else
        //            {
        //                Child[1, ChildID] = ChildLastTime + Parameters.MinimumShiftTime;
        //            }
        //        }

        //        ChildLastTime = Child[1, ChildID];
        //    }
        //}
    }
}
