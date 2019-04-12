using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekti_ne_klase
{
    class AlgorithmElitism : Crossover
    {
        int[,] P;
        int[,] PopQuality;
        int[,] PopQualitySorted;
        int BestFitness = -1;
        public int[,] Execute(List<Flight> FlightsData)
        {
            int currentGeneration = 1;
            P = new int[2 * Parameters.PopSize, FlightsData.Count];  //int[,] P = new int[2 * PopSize, FlightsData.Count];
            InitialSolution IS = new InitialSolution();
            List<int> PopulationIndexList = BasicFunctions.GetPopulationIndexList();

            for (int IndividIndex = 0; IndividIndex < 2 * Parameters.PopSize; IndividIndex += 2)
            {
                int [,] Individ = IS.GetRandomSolution(FlightsData);
                BasicFunctions.IndividInsert(ref P,Individ, IndividIndex);    //IndividInsert(P,Individ, IndividIndex);

            }
            int[,] Best = new int[2, FlightsData.Count];

            PopQuality = new int[2, Parameters.PopSize];
            BasicFunctions.calculationTime = BasicFunctions.sw.ElapsedMilliseconds;
            PopQualitySorted = new int[2, Parameters.PopSize];
            do
            {
                for (int individIndex = 0; individIndex < P.GetLength(0); individIndex += 2)
                {
                    int currentFitness = Constraints.AssessFitness(P, individIndex,FlightsData);
                    //Console.WriteLine("currentFitness = " + currentFitness);
                    PopQuality[0, individIndex / 2] = individIndex;
                    PopQuality[1,individIndex / 2] = currentFitness;
                    PopQualitySorted[0, individIndex / 2] = individIndex;
                    PopQualitySorted[1, individIndex / 2] = currentFitness;
                    if (BestFitness==-1 || currentFitness<BestFitness)
                    {
                        Best = BasicFunctions.GetIndivid(P, individIndex);
                        BestFitness = currentFitness;
                        BasicFunctions.calculationTime = BasicFunctions.sw.ElapsedMilliseconds;
                        //Console.WriteLine("BestFitness = " + BestFitness);
                        //Constraints.CheckMinSeparation(Best);
                        //Constraints.CheckNoAssignBefore(Best);
                        //BasicFunctions.PrintIndivid(Best);
                    }
                }
                QuickSort_Recursive(PopQualitySorted, 0, PopQualitySorted.GetLength(1) - 1);
                List<int> EliteIndexList = this.GetEliteIndividuals(PopQualitySorted);

                int[,] Q = new int[Parameters.PopSize * 2, FlightsData.Count];
                int[,] ParentA, ParentB;
                int ParentAIndex, ParentBIndex;
                int[,] ChildA = new int[2, FlightsData.Count];
                int[,] ChildB = new int[2, FlightsData.Count];
                List<int> TournamentSelectionIndexList;
                Parameters.ResetParameterValues(FlightsData.Count);
                for (int NewPopIndividIndex = 0; NewPopIndividIndex < Parameters.PopSize*2; NewPopIndividIndex+=4)
                {

                    TournamentSelectionIndexList = BasicFunctions.GetTournamentSelectionIndexList(PopulationIndexList);
                    ParentAIndex = BasicFunctions.GetParentIndex(TournamentSelectionIndexList, PopQuality);
                    TournamentSelectionIndexList = BasicFunctions.GetTournamentSelectionIndexList(PopulationIndexList);
                    ParentBIndex = BasicFunctions.GetParentIndex(TournamentSelectionIndexList, PopQuality);
                    ParentA = BasicFunctions.GetIndivid(P, ParentAIndex);
                    ParentB = BasicFunctions.GetIndivid(P, ParentBIndex);
                    this.GenerateChildren(ref ChildA, ref ChildB, ParentA, ParentB, FlightsData);
                    this.ApplySwap(ref ChildA, FlightsData);
                    this.ApplySwap(ref ChildB, FlightsData);

                    if (EliteIndexList.Contains(NewPopIndividIndex))
                    {
                        int[,] EliteIndivid = BasicFunctions.GetIndivid(P, NewPopIndividIndex);
                        BasicFunctions.IndividInsert(ref Q, EliteIndivid, NewPopIndividIndex);
                    }
                    else
                        BasicFunctions.IndividInsert(ref Q, ChildA, NewPopIndividIndex);

                    if (EliteIndexList.Contains(NewPopIndividIndex + 2))
                    {
                        int[,] EliteIndivid = BasicFunctions.GetIndivid(P, NewPopIndividIndex + 2);
                        BasicFunctions.IndividInsert(ref Q, EliteIndivid, NewPopIndividIndex + 2);
                    }
                    else
                        BasicFunctions.IndividInsert(ref Q, ChildB, NewPopIndividIndex + 2);               

                }
                P = Q;
                //Console.WriteLine("CurrentGeneration: " + currentGeneration);
                currentGeneration++;
            } while (currentGeneration<=Parameters.maxGenerations);
            //Console.WriteLine("Best fitness: " + BestFitness);
            //BasicFunctions.SetCalculatedFlightTime(Best,FlightsData);
            //PrintIndivid(Best);
            //PrintFlightTime();
            return Best;
        }
        public int getBestQuality()
        {
            return BestFitness;
        }

        //http://www.softwareandfinance.com/CSharp/QuickSort_Recursive.html
        void QuickSort_Recursive(int[,] arr, int left, int right)
        {
            // For Recusrion
            if (left < right)
            {
                int pivot = Partition(arr, left, right);

                if (pivot > 1)
                    QuickSort_Recursive(arr, left, pivot - 1);

                if (pivot + 1 < right)
                    QuickSort_Recursive(arr, pivot + 1, right);
            }
        }

        int Partition(int[,] numbers, int left, int right)
        {
            int pivot = numbers[1, left];
            while (true)
            {
                if (pivot == numbers[1, left] && pivot == numbers[1, right])
                    left++; // move either lower up, or upper low }

                while (numbers[1, left] < pivot)
                    left++;

                while (numbers[1, right] > pivot)
                    right--;

                if (left < right)
                {
                    int temp0 = numbers[0, right];
                    numbers[0, right] = numbers[0, left];
                    numbers[0, left] = temp0;

                    int temp1 = numbers[1, right];
                    numbers[1, right] = numbers[1, left];
                    numbers[1, left] = temp1;
                }
                else
                {
                    return right;
                }
            }
        }

        List<int> GetTieList(int[,] PopQuality)
        {
            List<int> result=new List<int>();
            int TieQualityValue = PopQuality[1, Parameters.NumberOfEliteIndividuals - 1];
            
            //Left side
            for (int i = Parameters.NumberOfEliteIndividuals - 2; i >= 0; i--)
            {
                if (!(PopQuality[1, i] == TieQualityValue))
                {
                    break;
                }
                else
                {
                    result.Insert(0, i);
                }
            }

            //Center
            result.Add(Parameters.NumberOfEliteIndividuals - 1);

           //Right side
            for (int i = Parameters.NumberOfEliteIndividuals; i < PopQuality.GetLength(1); i++)
            {
                if (!(PopQuality[1, i] == TieQualityValue))
                {
                    break;
                }
                else
                {
                    result.Add(i);
                }
            }
            return result;
        }
        List<int> GetEliteList(int[,] PopQuality)
        {
            List<int> result = new List<int>();
            List<int> TieList = this.GetTieList(PopQuality);
            int i;
            for (i = 0; i < TieList[0]; i++)
            {
                result.Add(i);
            }
            while (i<Parameters.NumberOfEliteIndividuals)
            {
                int RandomIndex = BasicFunctions.rand.Next(0, TieList.Count - 1);
                result.Add(TieList[RandomIndex]);
                TieList.RemoveAt(RandomIndex);
                i++;
            }
            return result;
        }
        List<int> GetEliteIndividuals(int [,] PopQuality)
        {
            List<int> result=new List<int>();
            List<int> EliteList = this.GetEliteList(PopQuality);
            foreach (int item in EliteList)
            {
                result.Add(PopQuality[0,item]);
            }
            return result;
        }
       
    }
}
