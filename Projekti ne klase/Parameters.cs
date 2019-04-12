using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekti_ne_klase
{
    public static class Parameters
    {
       static Random r = new Random();

        public  const int MinimumShiftTime = 2;

        public static int PopSize = 2000;
        public static int maxGenerations = 200;

        public static int TournamentSize = (int)(0.01*PopSize);

        static float CrossoverWideness = 0.04f;
        static float[] CrossoverWidenessList = {0.03f, 0.04f, 0.05f}; 
        public static int CrossoverWidenessThreshold = 1;

        public static int CrossoverWindowWidth = 5;
        public static int CrossoverMaxDifference = 5;
        

        static float MutateWideness = 0.13f;
        static float[] MutateDistanceList = {0.12f,0.14f,0.16f}; 

        public static int MutateMaxDistance = 15;
        public static int MutateDistanceThreshold = 5;

        public static int NumberOfEliteIndividuals = (int)(0.05 * PopSize);
        public static int NumberOfIndividualsToReplace = (int)(0.6*PopSize);

        public static double ProbabilityOfPerformingDirectReproduction = 0.5;
        
        public static void SetParameterValues(
            String strPopSize,
            String strMaxGeneration,
            String strTournamentSize,
            String strCrossoverWideness,
            String strNumberOfEliteIndividuals,
            String strNumberOfIndividualsToReplace,
            String strProbabilityOfPerformingDirectReproduction
            )
        {
            PopSize = int.Parse(strPopSize);
            maxGenerations = int.Parse(strMaxGeneration);
            TournamentSize = (int)(0.01*PopSize);
            CrossoverWideness = (float)(0.01*int.Parse(strCrossoverWideness));
            NumberOfEliteIndividuals = (int)(0.05 * PopSize);
            //NumberOfEliteIndividuals = (int)(int.Parse(strNumberOfEliteIndividuals) / 100.0 * PopSize);
            NumberOfIndividualsToReplace = (int)(0.6 * PopSize);
            //NumberOfIndividualsToReplace = (int)(int.Parse(strNumberOfIndividualsToReplace) / 100.0 * PopSize);
            ProbabilityOfPerformingDirectReproduction = (double)(int.Parse(strProbabilityOfPerformingDirectReproduction) / 100.0);
        }
        
        public static void ResetParameterValues(int NoOfFlights)
        {
            int randomCrosoverWidenessIndex = r.Next(0, CrossoverWidenessList.Length);
            CrossoverWindowWidth = BasicFunctions.rand.Next(1, CrossoverWidenessThreshold + (int)(NoOfFlights * CrossoverWidenessList[randomCrosoverWidenessIndex]));
            CrossoverMaxDifference = BasicFunctions.rand.Next(1, CrossoverWidenessThreshold + (int)(NoOfFlights * CrossoverWidenessList[randomCrosoverWidenessIndex]));

            int randomMutateDistanceIndex = r.Next(0, MutateDistanceList.Length);
            MutateMaxDistance = BasicFunctions.rand.Next(1, MutateDistanceThreshold + (int)(NoOfFlights * MutateDistanceList[randomMutateDistanceIndex]));

        }
    }
}
