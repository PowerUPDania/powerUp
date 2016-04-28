﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PowerUP
{
    public partial class Form1 : Form
    {
        DateTime startTime;
        DateTime endTime;
        double total;
        int totalSecleted;
        List<string> yvalue = new List<string>();
        Database database;


        public Form1()
        {
            InitializeComponent();
            pagecontrol.Appearance = TabAppearance.FlatButtons; pagecontrol.ItemSize = new Size(0, 1); pagecontrol.SizeMode = TabSizeMode.Fixed;
            tabControl1.Appearance = TabAppearance.FlatButtons; tabControl1.ItemSize = new Size(0, 1); tabControl1.SizeMode = TabSizeMode.Fixed;
            database = new Database();
            if (database.Connection() != true)
            {
                database.CreateTable();
               
            }
        }
        #region Startside

        private void button2_Click(object sender, EventArgs e)
        {
            pagecontrol.SelectedTab = tabPage2;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            pagecontrol.SelectedTab = tabPage3;
            database.projects.Clear();
            database.LoadProjects();
            panel1.Controls.Clear();
            Button back = new Button();
            back.Location = new Point(3, 394);
            back.Click += delegate { pagecontrol.SelectedTab = tabPage1; };
            back.Text = "back";
            panel1.Controls.Add(back);
            int count = database.projects.Count();
            for (int i = 0; i < 18; i++)
            {
                Label lbl = new Label();
                if (i <= 5)
                {

                    lbl.Location = new Point(10, i * 60);
                }
                else if (i > 5 && i <= 11)
                {
                    int yakse = i - 6;
                    lbl.Location = new Point(350, yakse * 60);
                }
                else
                {
                    int yakse = i - 12;
                    lbl.Location = new Point(700, yakse * 60);
                }
                if (count - 1 >= i)
                {
                    lbl.Text = database.projects[i].Name;
                    foreach (Project project in database.projects)
                    {
                        if (lbl.Text == project.Name)
                        {
                            int id = project.ID1;
                            database.Loadprojectfile(id);
                        }
                    }
                    lbl.Click += delegate { pagecontrol.SelectedTab = tabPage5; label4.Text = lbl.Text; };
                }
                else
                {
                    lbl.Text = "Create New Project";
                    lbl.Click += delegate { pagecontrol.SelectedTab = tabPage4; };
                }
                lbl.BackColor = Color.LightGray;
                lbl.Width = 300;
                lbl.Height = 50;
                panel1.Controls.Add(lbl);

            }





            //for (int i = 0; i < 6; i++)
            //{
            //    Label lbl = new Label();
            //    lbl.Location = new Point(350, i * 60);
            //    if (count - 1 >= i)
            //    {
            //        lbl.Text = database.projects[i+5].Name;
            //        lbl.Click += delegate { pagecontrol.SelectedTab = tabPage5; };
            //    }
            //    else
            //    {
            //        lbl.Text = "Create New Project";
            //        lbl.Click += delegate { pagecontrol.SelectedTab = tabPage4; };
            //    }
            //    lbl.BackColor = Color.LightGray;
            //    lbl.Width = 300;
            //    lbl.Height = 50;

            //    panel1.Controls.Add(lbl);

            //}
            //for (int i = 0; i < 6; i++)
            //{
            //    Label lbl = new Label();
            //    lbl.Location = new Point(700, i * 60);
            //    lbl.Text = "Create New Project";
            //    lbl.BackColor = Color.LightGray;
            //    lbl.Width = 300;
            //    lbl.Height = 50;
            //    panel1.Controls.Add(lbl);

            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        #endregion
        #region instructions
        private void button4_Click(object sender, EventArgs e)
        {
            pagecontrol.SelectedTab = tabPage1;
        }
        #endregion
        #region projectPage
        private void button5_Click(object sender, EventArgs e)
        {
            pagecontrol.SelectedTab = tabPage1;
        }
        #endregion

        #region createProject
        private void button6_Click(object sender, EventArgs e)
        {
            pagecontrol.SelectedTab = tabPage3;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            pagecontrol.SelectedTab = tabPage5;
            string projectName = textBox1.Text;
            string projectDescription = textBox2.Text;
            string startDate = dateTimePicker1.Value.ToShortDateString();
            string endDate = dateTimePicker4.Value.ToShortDateString();
            database.CreateProject(projectName, projectDescription, startDate, endDate);
            label4.Text = textBox1.Text;
        }
        #endregion
        #region projectView
        private void button9_Click(object sender, EventArgs e)
        {
            database.projects.Clear();
            database.LoadProjects();
            panel1.Controls.Clear();
            Button back = new Button();
            back.Location = new Point(3, 394);
            back.Click += delegate { pagecontrol.SelectedTab = tabPage1; };
            back.Text = "back";
            panel1.Controls.Add(back);
            int count = database.projects.Count();
            for (int i = 0; i < 18; i++)
            {
                Label lbl = new Label();
                if (i <= 5)
                {

                    lbl.Location = new Point(10, i * 60);
                }
                else if (i > 5 && i <= 11)
                {
                    int yakse = i - 6;
                    lbl.Location = new Point(350, yakse * 60);
                }
                else
                {
                    int yakse = i - 12;
                    lbl.Location = new Point(700, yakse * 60);
                }
                if (count - 1 >= i)
                {
                    lbl.Text = database.projects[i].Name;
                    //  var derp= database.projects[i].ID1;
                   
                    lbl.Click += delegate { pagecontrol.SelectedTab = tabPage5; label4.Text = lbl.Text; };
                }
                else
                {
                    lbl.Text = "Create New Project";
                    lbl.Click += delegate { pagecontrol.SelectedTab = tabPage4; };
                }
                lbl.BackColor = Color.LightGray;
                lbl.Width = 300;
                lbl.Height = 50;
                panel1.Controls.Add(lbl);

            }



            pagecontrol.SelectedTab = tabPage3;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            pagecontrol.SelectedTab = tabPage6;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // her puha 

             database.DeleteProject(database.projects[0].ID1);

        }
        private void button11_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region createIteration
        private void button14_Click(object sender, EventArgs e)
        {
            pagecontrol.SelectedTab = tabPage5;
        }



        private void button15_Click(object sender, EventArgs e)
        {
            int Index = listBox2.SelectedIndex;     //Selected Index
            object Swap = listBox2.SelectedItem;   //Selected Item
            if (Index < listBox2.Items.Count && Index != 0)
            {        //If something is selected...
                listBox2.Items.RemoveAt(Index);         //Remove it
                listBox2.Items.Insert(Index - 1, Swap);    //Add it back in one spot up
                listBox2.SelectedItem = Swap;          //Keep this item selected

            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            int Index = listBox2.SelectedIndex;     //Selected Index
            object Swap = listBox2.SelectedItem;   //Selected Item
            if (Index < listBox2.Items.Count && Index + 1 != listBox2.Items.Count)
            {        //If something is selected...
                listBox2.Items.RemoveAt(Index);         //Remove it
                listBox2.Items.Insert(Index + 1, Swap);    //Add it back in one spot up
                listBox2.SelectedItem = Swap;          //Keep this item selected

            }
        }
        private void button17_Click(object sender, EventArgs e)
        {

            pagecontrol.SelectedTab = tabPage7;
            string data = listBox2.SelectedItem.ToString();
            string removedStuff = data.Substring(0, data.Length - 26);
            if (data.Contains("Inception"))
            {
                removedStuff = removedStuff.Substring(12);
            }
            if (data.Contains("Elaboration"))
            {
                removedStuff = removedStuff.Substring(14);
            }
            if (data.Contains("Construction"))
            {
                removedStuff = removedStuff.Substring(15);
            }
            if (data.Contains("Transition"))
            {
                removedStuff = removedStuff.Substring(13);
            }
            label9.Text = removedStuff;
            totalSecleted = Convert.ToInt32(listBox2.SelectedIndex.Equals(total));

        }

        private void button12_Click(object sender, EventArgs e)
        {

            if (textBox3.Text != null)
            {
                if (checkBox1.Checked == true)
                {
                    total = 1 + (endTime - startTime).TotalDays;

                }
                else if (checkBox1.Checked == false)
                {
                    total =
            1 + ((endTime - startTime).TotalDays * 5 -
            (startTime.DayOfWeek - endTime.DayOfWeek) * 2) / 7;

                    if ((int)endTime.DayOfWeek == 6) total--;
                    if ((int)startTime.DayOfWeek == 0) total--;

                }

                //string derp = startTime.ToString();
                string startDate = dateTimePicker2.Value.ToShortDateString();
                string endDate = dateTimePicker3.Value.ToShortDateString();
                string iterationName = textBox3.Text;
                listBox2.Items.Add(listBox1.SelectedItem + " : " + iterationName + " : " + startDate + " : " + endDate);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int Index = listBox2.SelectedIndex;
            listBox2.Items.RemoveAt(Index);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            startTime = dateTimePicker2.Value.Date;


        }
        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            endTime = dateTimePicker3.Value.Date;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {


        }
        #endregion
        #region iterationEditer

        private void button18_Click(object sender, EventArgs e)
        {
            pagecontrol.SelectedTab = tabPage6;
        }

        #endregion

        private void button19_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 0)
            {
                tabControl1.SelectedIndex--;

            }

        }

        private void button20_Click(object sender, EventArgs e)
        {

            if (tabControl1.SelectedIndex != 2)
            {
                tabControl1.SelectedIndex++;

            }
        }

        private void tabPage8_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {
            chart2.Series.Clear();
            chart2.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Red) });

            chart2.ChartAreas[0].AxisX.IsMarginVisible = false;
            int i = 0;
            chart2.ChartAreas[0].AxisY.Maximum = 10;
            chart2.ChartAreas[0].AxisY.Minimum = 0;
            foreach (var item in yvalue)
            {
                chart2.Series[0].Points.Add(new DataPoint(i, item));
                i++;
            }
        }

        int maxinout = 0;
        private void button21_Click(object sender, EventArgs e)
        {

            if (textBox4.Text != "" && maxinout <= total)
            {
                string derp = textBox4.Text;
                yvalue.Add(derp);
                maxinout++;
            }

        }

        private void button24_Click(object sender, EventArgs e)
        {
            yvalue.Clear();

        }

        private void chart3_Click(object sender, EventArgs e)
        {
            chart3.Series.Clear();
            chart3.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Red) });

            chart3.ChartAreas[0].AxisX.IsMarginVisible = false;
            int i = 0;
            chart3.ChartAreas[0].AxisY.Maximum = 10;
            chart3.ChartAreas[0].AxisY.Minimum = 0;
            foreach (var item in yvalue)
            {
                chart3.Series[0].Points.Add(new DataPoint(i, item));
                i++;
            }
        }
        private void button22_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != "" && maxinout <= total)
            {
                string derp = textBox5.Text;
                yvalue.Add(derp);
                maxinout++;
            }
        }

        private void chart4_Click(object sender, EventArgs e)
        {
            chart4.Series.Clear();
            chart4.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Red) });

            chart4.ChartAreas[0].AxisX.IsMarginVisible = false;
            int i = 0;
            chart4.ChartAreas[0].AxisY.Maximum = 10;
            chart4.ChartAreas[0].AxisY.Minimum = 0;
            foreach (var item in yvalue)
            {
                chart4.Series[0].Points.Add(new DataPoint(i, item));
                i++;
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != "" && maxinout <= total)
            {
                string derp = textBox6.Text;
                yvalue.Add(derp);
                maxinout++;
            }
        }

        private void chart5_Click(object sender, EventArgs e)
        {
            //var d = "chart5";

            //derp(d);
            chart4.Series.Clear();
            chart4.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Red) });

            chart4.ChartAreas[0].AxisX.IsMarginVisible = false;
            int i = 0;
            chart4.ChartAreas[0].AxisY.Maximum = 10;
            chart4.ChartAreas[0].AxisY.Minimum = 0;
            foreach (var item in yvalue)
            {
                chart4.Series[0].Points.Add(new DataPoint(i, item));
                i++;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
