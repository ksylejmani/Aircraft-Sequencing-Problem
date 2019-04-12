using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekti_ne_klase
{
    public partial class Form1 : Form
    {
        List<Flight> data = new List<Flight>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbMethod.SelectedIndex = 0;
            cmbInstanceSet.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;

            AlgorithmGenerational a = new AlgorithmGenerational();

            //Data d = new Data();
            //a.Execute(d.GetFlightData());

            a.Execute(data.OrderBy(rw => rw.intScheduledTime).ToList());
            this.button1.Enabled = true;
            MessageBox.Show("Evaluation: " + a.getBestQuality());
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.button2.Enabled = false;
           

            //Data d = new Data();
            //AlgorithmElitism a = new AlgorithmElitism();
            //a.Execute(d.GetFlightData());

            AlgorithmElitism a = new AlgorithmElitism();
            a.Execute(data.OrderBy(rw => rw.intScheduledTime).ToList());
            this.button2.Enabled = true;
            MessageBox.Show("Evaluation: " + a.getBestQuality());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.button3.Enabled = false;

            //Data d = new Data();
            //AlgorithmSteadyState a = new AlgorithmSteadyState();
            //a.Execute(d.GetFlightData());

            AlgorithmSteadyState a = new AlgorithmSteadyState();
            a.Execute(data.OrderBy(rw => rw.intScheduledTime).ToList());
            this.button3.Enabled = true;
            MessageBox.Show("Evaluation: " + a.getBestQuality());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.button4.Enabled = false;

            //Data d = new Data();
            //AlgorithmSteadyState a = new AlgorithmSteadyState();
            //a.Execute(d.GetFlightData());

            AlgorithmTreeStyle a = new AlgorithmTreeStyle();
            a.Execute(data.OrderBy(rw => rw.intScheduledTime).ToList());
            this.button4.Enabled = true;
            MessageBox.Show("Evaluation: " + a.getBestQuality());
        }


        private void btnLoadData_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"C:\Users\behar.haziri\Desktop\Gjenerata 2014-2015\Problemet\Aircraft Sequencing Problem\Instances";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";


            Data db = new Data();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                foreach (var item in db.GetFromFile(openFileDialog1.FileName))
                {
                    data.Add(item);
                }

                BindGrid();

                

             }

        }

        private void BindGrid()
        {
            var blist = new BindingList<Flight>(data.OrderBy(rw => rw.intScheduledTime).ToList());
            var source = new BindingSource(blist, null);
            dgData.DataSource = source;
            this.lblTotal.Text = "Total Rreshta: " + dgData.RowCount.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            data.Clear();
            BindGrid();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            btnExecuteAlg.Enabled = false;
            Parameters.SetParameterValues(txtPopSize.Text, txtGenerations.Text, txtTorunamentSize.Text, txtCrossoverWideness.Text,
                txtEliteIndividuals.Text, txtIndividualsToReplace.Text, txtDirectReproduction.Text);
            Experiment exp = new Experiment(cmbInstanceSet.SelectedItem.ToString(), cmbMethod.SelectedItem.ToString(), txtExecutions.Text, txtNumberOfInstances.Text);
            exp.ExecuteExperiment();
            exp.ExportResultToExcel();
            btnExecuteAlg.Enabled = true;
            MessageBox.Show("Experiment finished successfully!");
        }

       








    }
}
