namespace Projekti_ne_klase
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.txtPopSize = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGenerations = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTorunamentSize = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCrossoverWideness = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEliteIndividuals = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtIndividualsToReplace = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDirectReproduction = new System.Windows.Forms.TextBox();
            this.gbParameters = new System.Windows.Forms.GroupBox();
            this.gbExperimetDetails = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNumberOfInstances = new System.Windows.Forms.TextBox();
            this.cmbMethod = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbInstanceSet = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtExecutions = new System.Windows.Forms.TextBox();
            this.btnExecuteAlg = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.gbParameters.SuspendLayout();
            this.gbExperimetDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 83);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "Execute Generational GA";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(20, 11);
            this.btnLoadData.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(86, 23);
            this.btnLoadData.TabIndex = 1;
            this.btnLoadData.Text = "Load Data";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // dgData
            // 
            this.dgData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Location = new System.Drawing.Point(12, 228);
            this.dgData.Name = "dgData";
            this.dgData.Size = new System.Drawing.Size(893, 428);
            this.dgData.TabIndex = 2;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(125, 14);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(43, 15);
            this.lblTotal.TabIndex = 3;
            this.lblTotal.Text = "Total:";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(20, 49);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(86, 23);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear Data";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(19, 118);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(149, 31);
            this.button2.TabIndex = 5;
            this.button2.Text = "Execute Elitism GA";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(19, 153);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(149, 31);
            this.button3.TabIndex = 6;
            this.button3.Text = "Execute Steady State GA";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(18, 188);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(149, 31);
            this.button4.TabIndex = 7;
            this.button4.Text = "Execute Tree Style GA";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtPopSize
            // 
            this.txtPopSize.Location = new System.Drawing.Point(233, 14);
            this.txtPopSize.Name = "txtPopSize";
            this.txtPopSize.Size = new System.Drawing.Size(100, 20);
            this.txtPopSize.TabIndex = 8;
            this.txtPopSize.Text = "2000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(146, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Population size:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(146, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Generations:";
            // 
            // txtGenerations
            // 
            this.txtGenerations.Location = new System.Drawing.Point(233, 40);
            this.txtGenerations.Name = "txtGenerations";
            this.txtGenerations.Size = new System.Drawing.Size(100, 20);
            this.txtGenerations.TabIndex = 10;
            this.txtGenerations.Text = "500";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(146, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Tournament size:";
            // 
            // txtTorunamentSize
            // 
            this.txtTorunamentSize.Enabled = false;
            this.txtTorunamentSize.Location = new System.Drawing.Point(233, 69);
            this.txtTorunamentSize.Name = "txtTorunamentSize";
            this.txtTorunamentSize.Size = new System.Drawing.Size(100, 20);
            this.txtTorunamentSize.TabIndex = 12;
            this.txtTorunamentSize.Text = "10";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(109, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Crossover wideness(%):";
            // 
            // txtCrossoverWideness
            // 
            this.txtCrossoverWideness.Location = new System.Drawing.Point(233, 95);
            this.txtCrossoverWideness.Name = "txtCrossoverWideness";
            this.txtCrossoverWideness.Size = new System.Drawing.Size(100, 20);
            this.txtCrossoverWideness.TabIndex = 14;
            this.txtCrossoverWideness.Text = "4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Percentage of elite individuals (%):";
            // 
            // txtEliteIndividuals
            // 
            this.txtEliteIndividuals.Enabled = false;
            this.txtEliteIndividuals.Location = new System.Drawing.Point(233, 121);
            this.txtEliteIndividuals.Name = "txtEliteIndividuals";
            this.txtEliteIndividuals.Size = new System.Drawing.Size(100, 20);
            this.txtEliteIndividuals.TabIndex = 16;
            this.txtEliteIndividuals.Text = "5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(196, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Percentage of individuals to replace (%):";
            // 
            // txtIndividualsToReplace
            // 
            this.txtIndividualsToReplace.Enabled = false;
            this.txtIndividualsToReplace.Location = new System.Drawing.Point(233, 147);
            this.txtIndividualsToReplace.Name = "txtIndividualsToReplace";
            this.txtIndividualsToReplace.Size = new System.Drawing.Size(100, 20);
            this.txtIndividualsToReplace.TabIndex = 18;
            this.txtIndividualsToReplace.Text = "60";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(179, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Probabilty for direct reproduction (%):";
            // 
            // txtDirectReproduction
            // 
            this.txtDirectReproduction.Location = new System.Drawing.Point(233, 173);
            this.txtDirectReproduction.Name = "txtDirectReproduction";
            this.txtDirectReproduction.Size = new System.Drawing.Size(100, 20);
            this.txtDirectReproduction.TabIndex = 20;
            this.txtDirectReproduction.Text = "50";
            // 
            // gbParameters
            // 
            this.gbParameters.Controls.Add(this.txtIndividualsToReplace);
            this.gbParameters.Controls.Add(this.label7);
            this.gbParameters.Controls.Add(this.label5);
            this.gbParameters.Controls.Add(this.txtDirectReproduction);
            this.gbParameters.Controls.Add(this.txtEliteIndividuals);
            this.gbParameters.Controls.Add(this.label6);
            this.gbParameters.Controls.Add(this.label4);
            this.gbParameters.Controls.Add(this.txtCrossoverWideness);
            this.gbParameters.Controls.Add(this.label3);
            this.gbParameters.Controls.Add(this.txtTorunamentSize);
            this.gbParameters.Controls.Add(this.label2);
            this.gbParameters.Controls.Add(this.txtGenerations);
            this.gbParameters.Controls.Add(this.label1);
            this.gbParameters.Controls.Add(this.txtPopSize);
            this.gbParameters.Location = new System.Drawing.Point(265, 14);
            this.gbParameters.Name = "gbParameters";
            this.gbParameters.Size = new System.Drawing.Size(342, 202);
            this.gbParameters.TabIndex = 22;
            this.gbParameters.TabStop = false;
            this.gbParameters.Text = "Parameters";
            // 
            // gbExperimetDetails
            // 
            this.gbExperimetDetails.Controls.Add(this.label11);
            this.gbExperimetDetails.Controls.Add(this.txtNumberOfInstances);
            this.gbExperimetDetails.Controls.Add(this.cmbMethod);
            this.gbExperimetDetails.Controls.Add(this.label10);
            this.gbExperimetDetails.Controls.Add(this.cmbInstanceSet);
            this.gbExperimetDetails.Controls.Add(this.label9);
            this.gbExperimetDetails.Controls.Add(this.label8);
            this.gbExperimetDetails.Controls.Add(this.txtExecutions);
            this.gbExperimetDetails.Location = new System.Drawing.Point(613, 14);
            this.gbExperimetDetails.Name = "gbExperimetDetails";
            this.gbExperimetDetails.Size = new System.Drawing.Size(292, 141);
            this.gbExperimetDetails.TabIndex = 23;
            this.gbExperimetDetails.TabStop = false;
            this.gbExperimetDetails.Text = "Experiment details";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 104);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(107, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Number of instances:";
            // 
            // txtNumberOfInstances
            // 
            this.txtNumberOfInstances.Location = new System.Drawing.Point(139, 100);
            this.txtNumberOfInstances.Name = "txtNumberOfInstances";
            this.txtNumberOfInstances.Size = new System.Drawing.Size(121, 20);
            this.txtNumberOfInstances.TabIndex = 17;
            this.txtNumberOfInstances.Text = "12";
            // 
            // cmbMethod
            // 
            this.cmbMethod.FormattingEnabled = true;
            this.cmbMethod.Items.AddRange(new object[] {
            "Generational",
            "Elitism",
            "Steady state",
            "Tree style"});
            this.cmbMethod.Location = new System.Drawing.Point(139, 13);
            this.cmbMethod.Name = "cmbMethod";
            this.cmbMethod.Size = new System.Drawing.Size(121, 21);
            this.cmbMethod.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(77, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Method:";
            // 
            // cmbInstanceSet
            // 
            this.cmbInstanceSet.FormattingEnabled = true;
            this.cmbInstanceSet.Items.AddRange(new object[] {
            "Set A",
            "Set B",
            "Set C"});
            this.cmbInstanceSet.Location = new System.Drawing.Point(139, 72);
            this.cmbInstanceSet.Name = "cmbInstanceSet";
            this.cmbInstanceSet.Size = new System.Drawing.Size(121, 21);
            this.cmbInstanceSet.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(65, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Instance set:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Number of executions:";
            // 
            // txtExecutions
            // 
            this.txtExecutions.Location = new System.Drawing.Point(139, 43);
            this.txtExecutions.Name = "txtExecutions";
            this.txtExecutions.Size = new System.Drawing.Size(121, 20);
            this.txtExecutions.TabIndex = 10;
            this.txtExecutions.Text = "10";
            // 
            // btnExecuteAlg
            // 
            this.btnExecuteAlg.Location = new System.Drawing.Point(693, 172);
            this.btnExecuteAlg.Margin = new System.Windows.Forms.Padding(2);
            this.btnExecuteAlg.Name = "btnExecuteAlg";
            this.btnExecuteAlg.Size = new System.Drawing.Size(150, 31);
            this.btnExecuteAlg.TabIndex = 24;
            this.btnExecuteAlg.Text = "Execute algorithm";
            this.btnExecuteAlg.UseVisualStyleBackColor = true;
            this.btnExecuteAlg.Click += new System.EventHandler(this.button5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 668);
            this.Controls.Add(this.btnExecuteAlg);
            this.Controls.Add(this.gbExperimetDetails);
            this.Controls.Add(this.gbParameters);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.dgData);
            this.Controls.Add(this.btnLoadData);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Aircraft Sequencing Problem";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.gbParameters.ResumeLayout(false);
            this.gbParameters.PerformLayout();
            this.gbExperimetDetails.ResumeLayout(false);
            this.gbExperimetDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtPopSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGenerations;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTorunamentSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCrossoverWideness;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEliteIndividuals;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtIndividualsToReplace;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDirectReproduction;
        private System.Windows.Forms.GroupBox gbParameters;
        private System.Windows.Forms.GroupBox gbExperimetDetails;
        private System.Windows.Forms.ComboBox cmbInstanceSet;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtExecutions;
        private System.Windows.Forms.ComboBox cmbMethod;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnExecuteAlg;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNumberOfInstances;
    }
}

