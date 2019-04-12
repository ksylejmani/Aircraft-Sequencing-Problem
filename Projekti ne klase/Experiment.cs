using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Excel=Microsoft.Office.Interop.Excel;

namespace Projekti_ne_klase
{
    class Experiment
    {
        int SetBIndex1 = 0,  SetBIndex2=14;
        int SetbBIndex2RestartValue = 14;
        String mInstanceSet, mMethodName;
        int NumberOfExecutions;
        int NumberOfInstances;
        double[,] Evaluation, Time, CumulativeEvaluation, CumulativeTime;
        String[] InstanceNames;
        public Experiment(String strInstanceSet, String strMethodName, String strNumberOfExecutions, String strNumberOfInstances)
        {
            mInstanceSet = strInstanceSet;
            mMethodName = strMethodName;
            NumberOfExecutions = int.Parse(strNumberOfExecutions);
            NumberOfInstances = int.Parse(strNumberOfInstances);
            Evaluation = new double[NumberOfInstances, NumberOfExecutions];
            Time = new double[NumberOfInstances, NumberOfExecutions];
            CumulativeEvaluation = new double[NumberOfInstances, 3];
            CumulativeTime = new double[NumberOfInstances, 3];
            InstanceNames = new String[NumberOfInstances];
         }

        public void ExecuteExperiment()
        {
            

            int Min = int.MaxValue;
            int Max = int.MinValue;

            for (int InstanceIndex = 0; InstanceIndex < NumberOfInstances; InstanceIndex++)
            {
                //if (InstanceIndex != 10)
                //    continue;
                String InstanceName = "FPT" + this.rregulateInstanceIndex(InstanceIndex + 1,mInstanceSet);
                InstanceNames[InstanceIndex] = InstanceName;
                String FullPath = "Instances\\" + mInstanceSet + "\\" + InstanceName + ".txt";
                List<Flight> data = this.getInstanceData(FullPath);
                int currentInstancePeriodLength = data[data.Count - 1].intScheduledTime - data[0].intScheduledTime;
                if (currentInstancePeriodLength < Min)
                    Min = currentInstancePeriodLength;
                if (currentInstancePeriodLength > Max)
                    Max = currentInstancePeriodLength;

                for (int ExecutionIndex = 0; ExecutionIndex < NumberOfExecutions; ExecutionIndex++)
                {
                    BasicFunctions.sw.Restart();               
                    if (mMethodName == "Generational")
                    {
                        AlgorithmGenerational ag = new AlgorithmGenerational();
                        ag.Execute(data.OrderBy(rw => rw.intScheduledTime).ToList());
                        Evaluation[InstanceIndex, ExecutionIndex] = ag.getBestQuality();
                    }
                    else if (mMethodName == "Elitism")
                    {
                        AlgorithmElitism ag = new AlgorithmElitism();
                        ag.Execute(data.OrderBy(rw => rw.intScheduledTime).ToList());
                        Evaluation[InstanceIndex, ExecutionIndex] = ag.getBestQuality();
                    }
                    else if (mMethodName == "Steady state")
                    {
                        AlgorithmSteadyState ag = new AlgorithmSteadyState();
                        ag.Execute(data.OrderBy(rw => rw.intScheduledTime).ToList());
                        Evaluation[InstanceIndex, ExecutionIndex] = ag.getBestQuality();
                    }
                    else if (mMethodName == "Tree style")
                    {
                        AlgorithmTreeStyle ag = new AlgorithmTreeStyle();
                        ag.Execute(data.OrderBy(rw => rw.intScheduledTime).ToList());
                        Evaluation[InstanceIndex, ExecutionIndex] = ag.getBestQuality();
                    }
                    
                    BasicFunctions.sw.Stop();
                    Time[InstanceIndex, ExecutionIndex] = (int)BasicFunctions.calculationTime;
                    //Time[InstanceIndex, ExecutionIndex] = (int)sw.Elapsed.TotalMilliseconds;
                }
            }


            this.CalculateCumulatives(Evaluation, ref CumulativeEvaluation);
            this.CalculateCumulatives(Time, ref CumulativeTime);

            Console.WriteLine("Maximal period of schedule: " + Max);
            Console.WriteLine("Minimal period of schedule: " + Min);

        }
        void CalculateCumulatives(double [,] Input, ref double [,] Output)
        {
            for (int InstanceIndex = 0; InstanceIndex < NumberOfInstances; InstanceIndex++)
            {
                double Sum = Input[InstanceIndex, 0];
                double Min = Input[InstanceIndex, 0];
                double Max = Input[InstanceIndex, 0];
                for (int ExecutionIndex = 1; ExecutionIndex < NumberOfExecutions; ExecutionIndex++)
                {
                    Sum += Input[InstanceIndex, ExecutionIndex];
                    if (Input[InstanceIndex, ExecutionIndex] < Min)
                        Min = Input[InstanceIndex, ExecutionIndex];
                    if (Input[InstanceIndex, ExecutionIndex] > Max)
                        Max = Input[InstanceIndex, ExecutionIndex];
                }
                Output[InstanceIndex, 0] = Min;
                Output[InstanceIndex, 1] = Max;
                Output[InstanceIndex, 2] = ((double)Sum) / Input.GetLength(1);
            }
        }
        
        List<Flight> getInstanceData(String InstanceName)
        {
            List<Flight> data = new List<Flight>();
            Data db = new Data();
            foreach (var item in db.GetFromFile(InstanceName))
            {
                data.Add(item);
            }
            return data;
        }
        String rregulateInstanceIndex(int InstanceIndex, String InstanceSet)
        {
            String result="";
            if (InstanceSet == "Set A")
            {
                if (InstanceIndex < 10)
                    result = "0" + InstanceIndex;
                else
                    result = InstanceIndex.ToString();
            }
            else if (InstanceSet == "Set B")
            {              
                result="_"+SetBIndex1+"_"+SetBIndex2;                              
                SetBIndex2++;
                if (SetBIndex2 > SetbBIndex2RestartValue + 9)
                {
                    SetBIndex1++;
                    SetbBIndex2RestartValue++;
                    SetBIndex2 = SetbBIndex2RestartValue;
                }
            }
            else if (InstanceSet == "Set C")
            {
                List<String> InstanceSuffix = new List<string>();
                InstanceSuffix.Add("_0_69");
                InstanceSuffix.Add("_50_119");
                InstanceSuffix.Add("_100_169");
                InstanceSuffix.Add("_0_89");
                InstanceSuffix.Add("_40_129");
                InstanceSuffix.Add("_80_169");
                InstanceSuffix.Add("_0_109");
                InstanceSuffix.Add("_30_139");
                InstanceSuffix.Add("_60_169");
                InstanceSuffix.Add("_0_129");
                InstanceSuffix.Add("_20_149");
                InstanceSuffix.Add("_40_169");
                InstanceSuffix.Add("_0_149");
                InstanceSuffix.Add("_10_159");
                InstanceSuffix.Add("_20_169");
                result = InstanceSuffix[InstanceIndex-1];
            }
            return result;
        }

        public void ExportResultToExcel()
        {
            Excel.Application MyApp = new Excel.Application();
            MyApp.Visible = false;
            String FileName = this.mInstanceSet+"_"+this.mMethodName+"_"+
                DateTime.Now.Hour+"-"+DateTime.Now.Minute+"_"+DateTime.Now.Day+"-"+DateTime.Now.Month+"-"+DateTime.Now.Year+".xlsx";
            string ProjectPath = System.IO.Directory.GetCurrentDirectory();
            String FullPath = ProjectPath + "\\Experimental Results\\" + FileName;
            Excel.Workbook MyBook = MyApp.Workbooks.Add();
            Excel.Worksheet MyWorkSheet = (Excel.Worksheet)MyBook.Worksheets[1];
            
            //Table title
            MyWorkSheet.Cells[1, 1] = "No.";  
            MyWorkSheet.Cells[1, 2] = "Instance name";
            MyWorkSheet.Cells[1, 3] = "Min evaluation";
            MyWorkSheet.Cells[1, 4] = "Min time [S]";
            MyWorkSheet.Cells[1, 5] = "Max evaluation";
            MyWorkSheet.Cells[1, 6] = "Max time [S]";
            MyWorkSheet.Cells[1, 7] = "Average evaluation";
            MyWorkSheet.Cells[1, 8] = "Average time [S]";
            for (int i=2; i<=8; i++)
            {
                MyWorkSheet.Columns[i].ColumnWidth = 14;
            }

            //Table data
            for (int InstanceIndex = 0; InstanceIndex < NumberOfInstances; InstanceIndex++)
            {
                MyWorkSheet.Cells[2 + InstanceIndex, 1] = InstanceIndex + 1;
                MyWorkSheet.Cells[2 + InstanceIndex, 2] = InstanceNames[InstanceIndex];
                MyWorkSheet.Cells[2 + InstanceIndex, 3] = CumulativeEvaluation[InstanceIndex, 0];
                MyWorkSheet.Cells[2 + InstanceIndex, 4] = CumulativeTime[InstanceIndex, 0]/1000;
                MyWorkSheet.Cells[2 + InstanceIndex, 5] = CumulativeEvaluation[InstanceIndex, 1];
                MyWorkSheet.Cells[2 + InstanceIndex, 6] = CumulativeTime[InstanceIndex, 1]/1000;
                MyWorkSheet.Cells[2 + InstanceIndex, 7] = CumulativeEvaluation[InstanceIndex, 2];
                MyWorkSheet.Cells[2 + InstanceIndex, 8] = CumulativeTime[InstanceIndex, 2]/1000;
            }

            //Save and close
            MyBook.SaveAs(FullPath);
            MyBook.Close();
        }
    }
}
