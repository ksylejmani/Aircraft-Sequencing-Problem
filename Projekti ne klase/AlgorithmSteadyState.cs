using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekti_ne_klase
{
    class AlgorithmSteadyState : Crossover
    {
        int[,] P;
        int[,] PopQuality;
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
            for (int individIndex = 0; individIndex < P.GetLength(0); individIndex += 2)
            {
                int currentFitness = Constraints.AssessFitness(P, individIndex, FlightsData);
                PopQuality[0, individIndex / 2] = individIndex;
                PopQuality[1, individIndex / 2] = currentFitness;
                if (BestFitness == -1 || currentFitness < BestFitness)
                {
                    Best = BasicFunctions.GetIndivid(P, individIndex);
                    BestFitness = currentFitness;
                    //Console.WriteLine("BestFitness = " + BestFitness);
                    //BasicFunctions.PrintIndivid(Best);
                }
            }

            do
            {
                List<int> RemoveList = this.GetRandomListOfIndividualsToRemove();
                
                int[,] ParentA, ParentB;
                int ParentAIndex, ParentBIndex;
                int[,] ChildA = new int[2,FlightsData.Count];
                int[,] ChildB = new int[2,FlightsData.Count];
                List<int> TournamentSelectionIndexList;
                Parameters.ResetParameterValues(FlightsData.Count);
                for (int RemoveListIndex = 0; RemoveListIndex < RemoveList.Count; RemoveListIndex += 2)
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
                    int ChildAFitness = Constraints.AssessFitness(ChildA, FlightsData);

                    //Console.WriteLine("ChildAFitness = " + ChildAFitness);
                    if (ChildAFitness < BestFitness)
                    {
                        Best = ChildA;
                        BestFitness = ChildAFitness;
                        BasicFunctions.calculationTime = BasicFunctions.sw.ElapsedMilliseconds;
                        //Console.WriteLine("BestFitness = " + BestFitness);
                        //BasicFunctions.PrintIndivid(Best);
                    }
                    int ChildBFitness = Constraints.AssessFitness(ChildB, FlightsData);
                    //Console.WriteLine("ChildBFitness = " + ChildBFitness);
                    if (ChildBFitness < BestFitness)
                    {
                        Best = ChildB;
                        BestFitness = ChildBFitness;
                        BasicFunctions.calculationTime = BasicFunctions.sw.ElapsedMilliseconds;
                        //Console.WriteLine("BestFitness = " + BestFitness);
                        //BasicFunctions.PrintIndivid(Best);
                    }
                    BasicFunctions.IndividInsert(ref P, ChildA, RemoveList[RemoveListIndex]);
                    BasicFunctions.IndividInsert(ref P, ChildB, RemoveList[RemoveListIndex + 1]);
                    PopQuality[0, RemoveList[RemoveListIndex] / 2] = RemoveList[RemoveListIndex];
                    PopQuality[1, RemoveList[RemoveListIndex] / 2] = ChildAFitness;
                    PopQuality[0, RemoveList[RemoveListIndex+1] / 2] = RemoveList[RemoveListIndex+1];
                    PopQuality[1, RemoveList[RemoveListIndex+1] / 2] = ChildBFitness;
                }
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

        List<int> GetRandomListOfIndividualsToRemove()
        {
            List<int> result = new List<int>();
            List<int> CandidateList = new List<int>();
            for (int i = 0; i < Parameters.PopSize*2; i+=2)
            {
                CandidateList.Add(i);
            }
            for (int i = 0; i < Parameters.NumberOfIndividualsToReplace; i++)
            {
                int RandomIndex = BasicFunctions.rand.Next(0, CandidateList.Count - 1);
                result.Add(CandidateList[RandomIndex]);
                CandidateList.RemoveAt(RandomIndex);
            }
            return result;
        }
    }
}
