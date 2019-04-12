using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekti_ne_klase
{
    class AlgorithmTreeStyle : Crossover
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
            BasicFunctions.calculationTime = BasicFunctions.sw.ElapsedMilliseconds;
            PopQuality = new int[2, Parameters.PopSize];
            do
            {
                for (int individIndex = 0; individIndex < P.GetLength(0); individIndex += 2)
                {
                    int currentFitness = Constraints.AssessFitness(P, individIndex,FlightsData);
                    PopQuality[0, individIndex / 2] = individIndex;
                    PopQuality[1, individIndex / 2] = currentFitness;
                    //Console.WriteLine("currentFitness = " + currentFitness);
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
                int[,] Q = new int[Parameters.PopSize * 2, FlightsData.Count];

                int[,] ParentA, ParentB;
                int ParentAIndex, ParentBIndex;
                int[,] ChildA = new int[2,FlightsData.Count];
                int[,] ChildB = new int[2,FlightsData.Count];
                List<int> TournamentSelectionIndexList;
                for (int NewPopIndividIndex = 0; NewPopIndividIndex < Parameters.PopSize * 2; NewPopIndividIndex += 4)
                {
                    Parameters.ResetParameterValues(FlightsData.Count);
                    TournamentSelectionIndexList = BasicFunctions.GetTournamentSelectionIndexList(PopulationIndexList);
                    ParentAIndex = BasicFunctions.GetParentIndex(TournamentSelectionIndexList, PopQuality);
                    TournamentSelectionIndexList = BasicFunctions.GetTournamentSelectionIndexList(PopulationIndexList);
                    ParentBIndex = BasicFunctions.GetParentIndex(TournamentSelectionIndexList, PopQuality);
                    ParentA = BasicFunctions.GetIndivid(P, ParentAIndex);
                    ParentB = BasicFunctions.GetIndivid(P, ParentBIndex);

                    double RandomNumber = this.GetRandomNumber();
                    if (RandomNumber < Parameters.ProbabilityOfPerformingDirectReproduction)
                    {
                        this.GenerateChildren(ref ChildB, ParentA, ParentB, FlightsData);
                        BasicFunctions.IndividInsert(ref Q, ParentA, NewPopIndividIndex);
                    }
                    else
                    {
                        this.GenerateChildren(ref ChildA, ref ChildB, ParentA, ParentB, FlightsData);
                        this.ApplySwap(ref ChildA, FlightsData);
                        BasicFunctions.IndividInsert(ref Q, ChildA, NewPopIndividIndex);
                    }
                    this.ApplySwap(ref ChildB, FlightsData);
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
        double GetRandomNumber()
        {
            return BasicFunctions.rand.Next(11) / 10.0;
        }
    }
}
