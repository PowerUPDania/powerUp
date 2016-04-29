using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
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
        int totalSelected;
        private int maxinout;
        List<int> yValues = new List<int>();
        Database database;


        public Form1()
        {
            InitializeComponent();
            pagecontrol.Appearance = TabAppearance.FlatButtons;
            pagecontrol.ItemSize = new Size(0, 1);
            pagecontrol.SizeMode = TabSizeMode.Fixed;
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
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
                    lbl.Click += delegate
                    {
                        pagecontrol.SelectedTab = tabPage5;
                        label4.Text = lbl.Text;
                        database.Loadprojectfile(label4.Text);
                    };
                    //lbl.Click += delegate
                    //{
                    //    pagecontrol.SelectedTab = tabPage5;
                    //    label4.Text = lbl.Text; database.Loadprojectfile(label4.Text);
                    //    label30.Text = database.projects[0].Description;
                    //};
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
            pagecontrol.SelectedTab = tabPage6;
            string projectName = textBox1.Text;
            string projectDescription = textBox2.Text;
            string startDate = dateTimePicker1.Value.ToShortDateString();
            string endDate = dateTimePicker4.Value.ToShortDateString();
            database.CreateProject(projectName, projectDescription, startDate, endDate);
            label4.Text = textBox1.Text;
            label31.Text = label4.Text;
            listBox2.Items.Clear();
            database.projects[0].iterations.Clear();
            database.LoadIterations(database.projects[0].ID1);
            foreach (var iteration in database.projects[0].iterations)
            {
                listBox2.Items.Add(iteration.Type1 + " : " + iteration.Name + " : " + iteration.Startdato + " : " +
                                   iteration.Slutdato);
            }
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

                    lbl.Click += delegate
                    {
                        pagecontrol.SelectedTab = tabPage5;
                        label4.Text = lbl.Text;
                        //Insert project graph update here
                    };
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
            label31.Text = label4.Text;
            listBox2.Items.Clear();
            database.projects[0].iterations.Clear();
            database.LoadIterations(database.projects[0].ID1);
            foreach (var iteration in database.projects[0].iterations)
            {
                listBox2.Items.Add(iteration.Type1 + " : " + iteration.Name + " : " + iteration.Startdato + " : " +
                                   iteration.Slutdato);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            database.DeleteProject(database.projects[0].ID1);
            pagecontrol.SelectedTab = tabPage3;
            textBox1.Clear();
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

                    lbl.Location = new Point(10, i*60);
                }
                else if (i > 5 && i <= 11)
                {
                    int yakse = i - 6;
                    lbl.Location = new Point(350, yakse*60);
                }
                else
                {
                    int yakse = i - 12;
                    lbl.Location = new Point(700, yakse*60);
                }
                if (count - 1 >= i)
                {
                    lbl.Text = database.projects[i].Name;
                    lbl.Click += delegate
                    {
                        pagecontrol.SelectedTab = tabPage5;
                        label4.Text = lbl.Text;
                        database.Loadprojectfile(label4.Text);
                        yValues.Clear();
                        foreach (int index in database.OrderIterations(database.projects[0].ID1))
                        {
                            yValues.AddRange(database.GetGraphPoints(label12.Text, index));
                        }

                        chart1.Series.Clear();
                        chart1.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Red) });
                        chart1.Series[0].Name = label12.Text;

                        chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
                        chart1.ChartAreas[0].AxisY.Maximum = 10;
                        chart1.ChartAreas[0].AxisY.Minimum = 0;

                        int temp = 0;
                        foreach (var item in yValues)
                        {
                            chart1.Series[0].Points.Add(new DataPoint(i, item));
                            temp++;
                        }

                        yValues.Clear();
                        chart1.ChartAreas[1].AxisX.IsMarginVisible = false;
                        chart1.ChartAreas[1].AxisY.Maximum = 10;
                        chart1.ChartAreas[1].AxisY.Minimum = 0;
                        foreach (int index in database.OrderIterations(database.projects[0].ID1))
                        {
                            yValues.AddRange(database.GetGraphPoints(label14.Text, index));
                        }
                        chart1.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.HotPink) });
                        chart1.Series[1].ChartArea = "ChartArea2";
                        chart1.Series[1].Name = label14.Text;
                        temp = 0;
                        foreach (var item in yValues)
                        {
                            chart1.Series[1].Points.Add(new DataPoint(i, item));
                            temp++;
                        }

                        yValues.Clear();
                        chart1.ChartAreas[2].AxisX.IsMarginVisible = false;
                        chart1.ChartAreas[2].AxisY.Maximum = 10;
                        chart1.ChartAreas[2].AxisY.Minimum = 0;
                        foreach (int index in database.OrderIterations(database.projects[0].ID1))
                        {
                            yValues.AddRange(database.GetGraphPoints(label13.Text, index));
                        }
                        chart1.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Orange) });
                        chart1.Series[2].ChartArea = "ChartArea3";
                        chart1.Series[2].Name = label13.Text;
                        temp = 0;
                        foreach (var item in yValues)
                        {
                            chart1.Series[2].Points.Add(new DataPoint(i, item));
                            temp++;
                        }
                    };
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
        }

        private void button11_Click(object sender, EventArgs e)
        {
            chart2.Visible = false;
            chart2.Width = 2100;
            chart2.Height = 1500;
            chart2.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            // sætningen nedenunder skal ændres så brugeren selv bestemmer navn 
            //eller måske så navnet er datoen og timestamp for idag.
            string nameOfFile = "wollah";
           
               
            Directory.CreateDirectory(Directory.GetCurrentDirectory()+"\\images\\");
            String tempFileName = Directory.GetCurrentDirectory() + "\\images\\" + nameOfFile + ".jpg";
            chart2.SaveImage(tempFileName, ChartImageFormat.Png);
        }

        #endregion

        #region createIteration

        private void button14_Click(object sender, EventArgs e)
        {
            pagecontrol.SelectedTab = tabPage5;
            int internIndex = 1;
            foreach (string iteration in listBox2.Items)
            {
                string removedStuff = iteration.Substring(0, iteration.Length - 26);
                if (iteration.Contains("Inception"))
                {
                    removedStuff = removedStuff.Substring(12);
                }
                if (iteration.Contains("Elaboration"))
                {
                    removedStuff = removedStuff.Substring(14);
                }
                if (iteration.Contains("Construction"))
                {
                    removedStuff = removedStuff.Substring(15);
                }
                if (iteration.Contains("Transition"))
                {
                    removedStuff = removedStuff.Substring(13);
                }
                label9.Text = removedStuff;
                database.UpdateIteration(internIndex, removedStuff);
                internIndex++;
            }
            //Insert project graph update here

            yValues.Clear();
            foreach (int index in database.OrderIterations(database.projects[0].ID1))
            {
                yValues.AddRange(database.GetGraphPoints(label12.Text, index));
            }

            chart1.Series.Clear();
            chart1.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Red) });
            chart1.Series[0].Name = label12.Text;

            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
            chart1.ChartAreas[0].AxisY.Maximum = 10;
            chart1.ChartAreas[0].AxisY.Minimum = 0;

            int i = 0;
            foreach (var item in yValues)
            {
                chart1.Series[0].Points.Add(new DataPoint(i, item));
                i++;
            }

            yValues.Clear();
            chart1.ChartAreas[1].AxisX.IsMarginVisible = false;
            chart1.ChartAreas[1].AxisY.Maximum = 10;
            chart1.ChartAreas[1].AxisY.Minimum = 0;
            foreach (int index in database.OrderIterations(database.projects[0].ID1))
            {
                yValues.AddRange(database.GetGraphPoints(label14.Text, index));
            }
            chart1.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.HotPink) });
            chart1.Series[1].ChartArea = "ChartArea2";
            chart1.Series[1].Name = label14.Text;
            i = 0;
            foreach (var item in yValues)
            {
                chart1.Series[1].Points.Add(new DataPoint(i, item));
                i++;
            }

            yValues.Clear();
            chart1.ChartAreas[2].AxisX.IsMarginVisible = false;
            chart1.ChartAreas[2].AxisY.Maximum = 10;
            chart1.ChartAreas[2].AxisY.Minimum = 0;
            foreach (int index in database.OrderIterations(database.projects[0].ID1))
            {
                yValues.AddRange(database.GetGraphPoints(label13.Text, index));
            }
            chart1.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Orange) });
            chart1.Series[2].ChartArea = "ChartArea3";
            chart1.Series[2].Name = label13.Text;
            i = 0;
            foreach (var item in yValues)
            {
                chart1.Series[2].Points.Add(new DataPoint(i, item));
                i++;
            }
        }



        private void button15_Click(object sender, EventArgs e)
        {
            int Index = listBox2.SelectedIndex; //Selected Index
            object Swap = listBox2.SelectedItem; //Selected Item
            if (Index < listBox2.Items.Count && Index != 0)
            {
                //If something is selected...
                listBox2.Items.RemoveAt(Index); //Remove it
                listBox2.Items.Insert(Index - 1, Swap); //Add it back in one spot up
                listBox2.SelectedItem = Swap; //Keep this item selected

            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            int Index = listBox2.SelectedIndex; //Selected Index
            object Swap = listBox2.SelectedItem; //Selected Item
            if (Index < listBox2.Items.Count && Index + 1 != listBox2.Items.Count)
            {
                //If something is selected...
                listBox2.Items.RemoveAt(Index); //Remove it
                listBox2.Items.Insert(Index + 1, Swap); //Add it back in one spot up
                listBox2.SelectedItem = Swap; //Keep this item selected

            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
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
                totalSelected = database.GetDuration(label9.Text);

                yValues.Clear();
                yValues = database.GetGraphPoints(label12.Text, database.GetIterationID(label9.Text));

                chart2.Series.Clear();
                chart2.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Red) });

                chart2.ChartAreas[0].AxisX.IsMarginVisible = false;
                int i = 0;
                chart2.ChartAreas[0].AxisY.Maximum = 10;
                chart2.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart2.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }

                yValues.Clear();
                yValues = database.GetGraphPoints(label14.Text, database.GetIterationID(label9.Text));

                chart3.Series.Clear();
                chart3.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.HotPink) });

                chart3.ChartAreas[0].AxisX.IsMarginVisible = false;
                i = 0;
                chart3.ChartAreas[0].AxisY.Maximum = 10;
                chart3.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart3.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }

                yValues.Clear();
                yValues = database.GetGraphPoints(label13.Text, database.GetIterationID(label9.Text));

                chart4.Series.Clear();
                chart4.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Orange) });

                chart4.ChartAreas[0].AxisX.IsMarginVisible = false;
                i = 0;
                chart4.ChartAreas[0].AxisY.Maximum = 10;
                chart4.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart4.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }

                yValues.Clear();
                yValues = database.GetGraphPoints(label18.Text, database.GetIterationID(label9.Text));
                chart5.Series.Clear();
                chart5.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Yellow) });

                chart5.ChartAreas[0].AxisX.IsMarginVisible = false;
                i = 0;
                chart5.ChartAreas[0].AxisY.Maximum = 10;
                chart5.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart5.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }

                yValues.Clear();
                yValues = database.GetGraphPoints(label21.Text, database.GetIterationID(label9.Text));
                chart6.Series.Clear();
                chart6.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.LightGreen) });

                chart6.ChartAreas[0].AxisX.IsMarginVisible = false;
                i = 0;
                chart6.ChartAreas[0].AxisY.Maximum = 10;
                chart6.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart6.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }

                yValues.Clear();
                yValues = database.GetGraphPoints(label23.Text, database.GetIterationID(label9.Text));
                chart7.Series.Clear();
                chart7.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Green) });

                chart7.ChartAreas[0].AxisX.IsMarginVisible = false;
                i = 0;
                chart7.ChartAreas[0].AxisY.Maximum = 10;
                chart7.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart7.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }

                yValues.Clear();
                yValues = database.GetGraphPoints(label29.Text, database.GetIterationID(label9.Text));
                chart10.Series.Clear();
                chart10.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.DarkSlateGray) });

                chart10.ChartAreas[0].AxisX.IsMarginVisible = false;
                i = 0;
                chart10.ChartAreas[0].AxisY.Maximum = 10;
                chart10.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart10.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }

                yValues.Clear();
                yValues = database.GetGraphPoints(label28.Text, database.GetIterationID(label9.Text));
                chart9.Series.Clear();
                chart9.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.MediumPurple) });

                chart9.ChartAreas[0].AxisX.IsMarginVisible = false;
                i = 0;
                chart9.ChartAreas[0].AxisY.Maximum = 10;
                chart9.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart9.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }

                yValues.Clear();
                yValues = database.GetGraphPoints(label27.Text, database.GetIterationID(label9.Text));
                chart8.Series.Clear();
                chart8.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Purple) });

                chart8.ChartAreas[0].AxisX.IsMarginVisible = false;
                i = 0;
                chart8.ChartAreas[0].AxisY.Maximum = 10;
                chart8.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart8.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }
            }

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
                database.CreateIteration(database.projects[0].ID1, iterationName, listBox1.SelectedItem.ToString(),
                    (int)total, startDate, endDate);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int Index = listBox2.SelectedIndex;
            int tempID = 0;
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
            foreach (Iteration iteration in database.projects[0].iterations)
            {
                if (removedStuff == iteration.Name)
                {
                    tempID = iteration.ID;
                }
            }
            database.DeleteIteration(database.projects[0].ID1, tempID);
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
            listBox2.Items.Clear();
            database.projects[0].iterations.Clear();
            database.LoadIterations(database.projects[0].ID1);
            foreach (var iteration in database.projects[0].iterations)
            {
                listBox2.Items.Add(iteration.Type1 + " : " + iteration.Name + " : " + iteration.Startdato + " : " +
                                   iteration.Slutdato);
            }
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

        }



        private void button21_Click(object sender, EventArgs e)
        {
            yValues.Clear();
            yValues = database.GetGraphPoints(label12.Text, database.GetIterationID(label9.Text));
            if (yValues.Count != null)
            {
                maxinout = yValues.Count;
            }
            else
            {
                maxinout = 0;
            }

            if (textBox4.Text != "" && maxinout <= totalSelected && yValues.Count != totalSelected+1)
            {
                string derp = textBox4.Text;
                database.SavePoint(Convert.ToInt32(derp), database.GetIterationID(label9.Text), label12.Text, maxinout);
                maxinout++;
                yValues.Clear();
                yValues = database.GetGraphPoints(label12.Text, database.GetIterationID(label9.Text));
                chart2.Series.Clear();
                chart2.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Red) });

                chart2.ChartAreas[0].AxisX.IsMarginVisible = false;
                int i = 0;
                chart2.ChartAreas[0].AxisY.Maximum = 10;
                chart2.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart2.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }
            }
            if (yValues.Count == totalSelected+1)
            {
                chart2.Series.Clear();
                chart2.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Red) });
                if (yValues.Count != null)
                {
                    maxinout = yValues.Count - 1;
                }
                else
                {
                    maxinout = 0;
                }
                chart2.ChartAreas[0].AxisX.IsMarginVisible = false;
                int i = 0;
                chart2.ChartAreas[0].AxisY.Maximum = 10;
                chart2.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart2.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }
            }


        }

        private void button24_Click(object sender, EventArgs e)
        {
            yValues.Clear();
            database.ClearGraph(label12.Text, database.GetIterationID(label9.Text));
            chart2.Series.Clear();
            chart2.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Red) });
            if (yValues.Count != null)
            {
                maxinout = yValues.Count - 1;
            }
            else
            {
                maxinout = 0;
            }
            chart2.ChartAreas[0].AxisX.IsMarginVisible = false;
            int i = 0;
            chart2.ChartAreas[0].AxisY.Maximum = 10;
            chart2.ChartAreas[0].AxisY.Minimum = 0;
            foreach (var item in yValues)
            {
                chart2.Series[0].Points.Add(new DataPoint(i, item));
                i++;
            }
        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {
            yValues.Clear();
            yValues = database.GetGraphPoints(label14.Text, database.GetIterationID(label9.Text));
            if (yValues.Count != null)
            {
                maxinout = yValues.Count;
            }
            else
            {
                maxinout = 0;
            }

            if (textBox5.Text != "" && maxinout <= totalSelected && yValues.Count != totalSelected+1)
            {
                string derp = textBox5.Text;
                database.SavePoint(Convert.ToInt32(derp), database.GetIterationID(label9.Text), label14.Text, maxinout);
                maxinout++;
                yValues.Clear();
                yValues = database.GetGraphPoints(label14.Text, database.GetIterationID(label9.Text));
                chart3.Series.Clear();
                chart3.Series.Add(new Series {ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.HotPink)});
                chart3.ChartAreas[0].AxisX.IsMarginVisible = false;
                int i = 0;
                chart3.ChartAreas[0].AxisY.Maximum = 10;
                chart3.ChartAreas[0].AxisY.Minimum = 0;

                foreach (var item in yValues)
                {
                    chart3.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }
            }
            if (yValues.Count == totalSelected + 1)
            {
                chart3.Series.Clear();
                chart3.Series.Add(new Series {ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.HotPink)});
                if (yValues.Count != null)
                {
                    maxinout = yValues.Count - 1;
                }
                else
                {
                    maxinout = 0;
                }
                chart3.ChartAreas[0].AxisX.IsMarginVisible = false;
                int i = 0;
                chart3.ChartAreas[0].AxisY.Maximum = 10;
                chart3.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart3.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }
            }
        }

        private void chart4_Click(object sender, EventArgs e)
        {

        }

        private void button23_Click(object sender, EventArgs e)
        {
            yValues.Clear();
            yValues = database.GetGraphPoints(label13.Text, database.GetIterationID(label9.Text));
            if (yValues.Count != null)
            {
                maxinout = yValues.Count;
            }
            else
            {
                maxinout = 0;
            }

            if (textBox6.Text != "" && maxinout <= totalSelected && yValues.Count != totalSelected+1)
            {
                string derp = textBox6.Text;
                database.SavePoint(Convert.ToInt32(derp), database.GetIterationID(label9.Text), label13.Text, maxinout);
                maxinout++;
                yValues.Clear();
                yValues = database.GetGraphPoints(label13.Text, database.GetIterationID(label9.Text));
                chart4.Series.Clear();
                chart4.Series.Add(new Series {ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Orange)});

                chart4.ChartAreas[0].AxisX.IsMarginVisible = false;
                int i = 0;
                chart4.ChartAreas[0].AxisY.Maximum = 10;
                chart4.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart4.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }
            }
            if (yValues.Count == totalSelected + 1)
            {
                chart4.Series.Clear();
                chart4.Series.Add(new Series {ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Orange)});
                if (yValues.Count != null)
                {
                    maxinout = yValues.Count - 1;
                }
                else
                {
                    maxinout = 0;
                }
                chart4.ChartAreas[0].AxisX.IsMarginVisible = false;
                int i = 0;
                chart4.ChartAreas[0].AxisY.Maximum = 10;
                chart4.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart4.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }
            }
        }

        private void chart5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button27_Click(object sender, EventArgs e)
        {
            yValues.Clear();
            yValues = database.GetGraphPoints(label18.Text, database.GetIterationID(label9.Text));
            if (yValues.Count != null)
            {
                maxinout = yValues.Count;
            }
            else
            {
                maxinout = 0;
            }

            if (textBox7.Text != "" && maxinout <= totalSelected && yValues.Count != totalSelected + 1)
            {
                string derp = textBox7.Text;
                database.SavePoint(Convert.ToInt32(derp), database.GetIterationID(label9.Text), label18.Text, maxinout);
                maxinout++;
                yValues.Clear();
                yValues = database.GetGraphPoints(label18.Text, database.GetIterationID(label9.Text));
                chart5.Series.Clear();
                chart5.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Yellow) });

                chart5.ChartAreas[0].AxisX.IsMarginVisible = false;
                int i = 0;
                chart5.ChartAreas[0].AxisY.Maximum = 10;
                chart5.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart5.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }
            }
            if (yValues.Count == totalSelected + 1)
            {
                chart5.Series.Clear();
                chart5.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Yellow) });
                if (yValues.Count != null)
                {
                    maxinout = yValues.Count - 1;
                }
                else
                {
                    maxinout = 0;
                }
                chart5.ChartAreas[0].AxisX.IsMarginVisible = false;
                int i = 0;
                chart5.ChartAreas[0].AxisY.Maximum = 10;
                chart5.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart5.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            yValues.Clear();
            yValues = database.GetGraphPoints(label21.Text, database.GetIterationID(label9.Text));
            if (yValues.Count != null)
            {
                maxinout = yValues.Count;
            }
            else
            {
                maxinout = 0;
            }

            if (textBox8.Text != "" && maxinout <= totalSelected && yValues.Count != totalSelected + 1)
            {
                string derp = textBox8.Text;
                database.SavePoint(Convert.ToInt32(derp), database.GetIterationID(label9.Text), label21.Text, maxinout);
                maxinout++;
                yValues.Clear();
                yValues = database.GetGraphPoints(label21.Text, database.GetIterationID(label9.Text));
                chart6.Series.Clear();
                chart6.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.LightGreen) });

                chart6.ChartAreas[0].AxisX.IsMarginVisible = false;
                int i = 0;
                chart6.ChartAreas[0].AxisY.Maximum = 10;
                chart6.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart6.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }
            }
            if (yValues.Count == totalSelected + 1)
            {
                chart6.Series.Clear();
                chart6.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.LightGreen) });
                if (yValues.Count != null)
                {
                    maxinout = yValues.Count - 1;
                }
                else
                {
                    maxinout = 0;
                }
                chart6.ChartAreas[0].AxisX.IsMarginVisible = false;
                int i = 0;
                chart6.ChartAreas[0].AxisY.Maximum = 10;
                chart6.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart6.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            yValues.Clear();
            yValues = database.GetGraphPoints(label23.Text, database.GetIterationID(label9.Text));
            if (yValues.Count != null)
            {
                maxinout = yValues.Count;
            }
            else
            {
                maxinout = 0;
            }

            if (textBox9.Text != "" && maxinout <= totalSelected && yValues.Count != totalSelected + 1)
            {
                string derp = textBox9.Text;
                database.SavePoint(Convert.ToInt32(derp), database.GetIterationID(label9.Text), label23.Text, maxinout);
                maxinout++;
                yValues.Clear();
                yValues = database.GetGraphPoints(label23.Text, database.GetIterationID(label9.Text));
                chart7.Series.Clear();
                chart7.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Green) });

                chart7.ChartAreas[0].AxisX.IsMarginVisible = false;
                int i = 0;
                chart7.ChartAreas[0].AxisY.Maximum = 10;
                chart7.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart7.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }
            }
            if (yValues.Count == totalSelected + 1)
            {
                chart7.Series.Clear();
                chart7.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Green) });
                if (yValues.Count != null)
                {
                    maxinout = yValues.Count - 1;
                }
                else
                {
                    maxinout = 0;
                }
                chart7.ChartAreas[0].AxisX.IsMarginVisible = false;
                int i = 0;
                chart7.ChartAreas[0].AxisY.Maximum = 10;
                chart7.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart7.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            yValues.Clear();
            yValues = database.GetGraphPoints(label29.Text, database.GetIterationID(label9.Text));
            if (yValues.Count != null)
            {
                maxinout = yValues.Count;
            }
            else
            {
                maxinout = 0;
            }

            if (textBox12.Text != "" && maxinout <= totalSelected && yValues.Count != totalSelected + 1)
            {
                string derp = textBox12.Text;
                database.SavePoint(Convert.ToInt32(derp), database.GetIterationID(label9.Text), label29.Text, maxinout);
                maxinout++;
                yValues.Clear();
                yValues = database.GetGraphPoints(label29.Text, database.GetIterationID(label9.Text));
                chart10.Series.Clear();
                chart10.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.DarkSlateGray) });

                chart10.ChartAreas[0].AxisX.IsMarginVisible = false;
                int i = 0;
                chart10.ChartAreas[0].AxisY.Maximum = 10;
                chart10.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart10.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }
            }
            if (yValues.Count == totalSelected + 1)
            {
                chart10.Series.Clear();
                chart10.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.DarkSlateGray) });
                if (yValues.Count != null)
                {
                    maxinout = yValues.Count - 1;
                }
                else
                {
                    maxinout = 0;
                }
                chart10.ChartAreas[0].AxisX.IsMarginVisible = false;
                int i = 0;
                chart10.ChartAreas[0].AxisY.Maximum = 10;
                chart10.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart10.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            yValues.Clear();
            yValues = database.GetGraphPoints(label28.Text, database.GetIterationID(label9.Text));
            if (yValues.Count != null)
            {
                maxinout = yValues.Count;
            }
            else
            {
                maxinout = 0;
            }

            if (textBox11.Text != "" && maxinout <= totalSelected && yValues.Count != totalSelected + 1)
            {
                string derp = textBox11.Text;
                database.SavePoint(Convert.ToInt32(derp), database.GetIterationID(label9.Text), label28.Text, maxinout);
                maxinout++;
                yValues.Clear();
                yValues = database.GetGraphPoints(label28.Text, database.GetIterationID(label9.Text));
                chart9.Series.Clear();
                chart9.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.MediumPurple) });

                chart9.ChartAreas[0].AxisX.IsMarginVisible = false;
                int i = 0;
                chart9.ChartAreas[0].AxisY.Maximum = 10;
                chart9.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart9.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }
            }
            if (yValues.Count == totalSelected + 1)
            {
                chart9.Series.Clear();
                chart9.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.MediumPurple) });
                if (yValues.Count != null)
                {
                    maxinout = yValues.Count - 1;
                }
                else
                {
                    maxinout = 0;
                }
                chart9.ChartAreas[0].AxisX.IsMarginVisible = false;
                int i = 0;
                chart9.ChartAreas[0].AxisY.Maximum = 10;
                chart9.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart9.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            yValues.Clear();
            yValues = database.GetGraphPoints(label27.Text, database.GetIterationID(label9.Text));
            if (yValues.Count != null)
            {
                maxinout = yValues.Count;
            }
            else
            {
                maxinout = 0;
            }

            if (textBox10.Text != "" && maxinout <= totalSelected && yValues.Count != totalSelected + 1)
            {
                string derp = textBox10.Text;
                database.SavePoint(Convert.ToInt32(derp), database.GetIterationID(label9.Text), label27.Text, maxinout);
                maxinout++;
                yValues.Clear();
                yValues = database.GetGraphPoints(label27.Text, database.GetIterationID(label9.Text));
                chart8.Series.Clear();
                chart8.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Purple) });

                chart8.ChartAreas[0].AxisX.IsMarginVisible = false;
                int i = 0;
                chart8.ChartAreas[0].AxisY.Maximum = 10;
                chart8.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart8.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }
            }
            if (yValues.Count == totalSelected + 1)
            {
                chart8.Series.Clear();
                chart8.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Purple) });
                if (yValues.Count != null)
                {
                    maxinout = yValues.Count - 1;
                }
                else
                {
                    maxinout = 0;
                }
                chart8.ChartAreas[0].AxisX.IsMarginVisible = false;
                int i = 0;
                chart8.ChartAreas[0].AxisY.Maximum = 10;
                chart8.ChartAreas[0].AxisY.Minimum = 0;
                foreach (var item in yValues)
                {
                    chart8.Series[0].Points.Add(new DataPoint(i, item));
                    i++;
                }
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            yValues.Clear();
            database.ClearGraph(label14.Text, database.GetIterationID(label9.Text));
            chart3.Series.Clear();
            chart3.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Red) });
            if (yValues.Count != null)
            {
                maxinout = yValues.Count - 1;
            }
            else
            {
                maxinout = 0;
            }
            chart3.ChartAreas[0].AxisX.IsMarginVisible = false;
            int i = 0;
            chart3.ChartAreas[0].AxisY.Maximum = 10;
            chart3.ChartAreas[0].AxisY.Minimum = 0;
            foreach (var item in yValues)
            {
                chart3.Series[0].Points.Add(new DataPoint(i, item));
                i++;
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            yValues.Clear();
            database.ClearGraph(label13.Text, database.GetIterationID(label9.Text));
            chart4.Series.Clear();
            chart4.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Red) });
            if (yValues.Count != null)
            {
                maxinout = yValues.Count - 1;
            }
            else
            {
                maxinout = 0;
            }
            chart4.ChartAreas[0].AxisX.IsMarginVisible = false;
            int i = 0;
            chart4.ChartAreas[0].AxisY.Maximum = 10;
            chart4.ChartAreas[0].AxisY.Minimum = 0;
            foreach (var item in yValues)
            {
                chart4.Series[0].Points.Add(new DataPoint(i, item));
                i++;
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            yValues.Clear();
            database.ClearGraph(label18.Text, database.GetIterationID(label9.Text));
            chart5.Series.Clear();
            chart5.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Red) });
            if (yValues.Count != null)
            {
                maxinout = yValues.Count - 1;
            }
            else
            {
                maxinout = 0;
            }
            chart5.ChartAreas[0].AxisX.IsMarginVisible = false;
            int i = 0;
            chart5.ChartAreas[0].AxisY.Maximum = 10;
            chart5.ChartAreas[0].AxisY.Minimum = 0;
            foreach (var item in yValues)
            {
                chart5.Series[0].Points.Add(new DataPoint(i, item));
                i++;
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            yValues.Clear();
            database.ClearGraph(label21.Text, database.GetIterationID(label9.Text));
            chart6.Series.Clear();
            chart6.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Red) });
            if (yValues.Count != null)
            {
                maxinout = yValues.Count - 1;
            }
            else
            {
                maxinout = 0;
            }
            chart6.ChartAreas[0].AxisX.IsMarginVisible = false;
            int i = 0;
            chart6.ChartAreas[0].AxisY.Maximum = 10;
            chart6.ChartAreas[0].AxisY.Minimum = 0;
            foreach (var item in yValues)
            {
                chart6.Series[0].Points.Add(new DataPoint(i, item));
                i++;
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            yValues.Clear();
            database.ClearGraph(label23.Text, database.GetIterationID(label9.Text));
            chart7.Series.Clear();
            chart7.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Red) });
            if (yValues.Count != null)
            {
                maxinout = yValues.Count - 1;
            }
            else
            {
                maxinout = 0;
            }
            chart7.ChartAreas[0].AxisX.IsMarginVisible = false;
            int i = 0;
            chart7.ChartAreas[0].AxisY.Maximum = 10;
            chart7.ChartAreas[0].AxisY.Minimum = 0;
            foreach (var item in yValues)
            {
                chart7.Series[0].Points.Add(new DataPoint(i, item));
                i++;
            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            yValues.Clear();
            database.ClearGraph(label29.Text, database.GetIterationID(label9.Text));
            chart10.Series.Clear();
            chart10.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Red) });
            if (yValues.Count != null)
            {
                maxinout = yValues.Count - 1;
            }
            else
            {
                maxinout = 0;
            }
            chart10.ChartAreas[0].AxisX.IsMarginVisible = false;
            int i = 0;
            chart10.ChartAreas[0].AxisY.Maximum = 10;
            chart10.ChartAreas[0].AxisY.Minimum = 0;
            foreach (var item in yValues)
            {
                chart10.Series[0].Points.Add(new DataPoint(i, item));
                i++;
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            yValues.Clear();
            database.ClearGraph(label28.Text, database.GetIterationID(label9.Text));
            chart9.Series.Clear();
            chart9.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Red) });
            if (yValues.Count != null)
            {
                maxinout = yValues.Count - 1;
            }
            else
            {
                maxinout = 0;
            }
            chart9.ChartAreas[0].AxisX.IsMarginVisible = false;
            int i = 0;
            chart9.ChartAreas[0].AxisY.Maximum = 10;
            chart9.ChartAreas[0].AxisY.Minimum = 0;
            foreach (var item in yValues)
            {
                chart9.Series[0].Points.Add(new DataPoint(i, item));
                i++;
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            yValues.Clear();
            database.ClearGraph(label27.Text, database.GetIterationID(label9.Text));
            chart8.Series.Clear();
            chart8.Series.Add(new Series { ChartType = SeriesChartType.Area, Color = Color.FromArgb(50, Color.Red) });
            if (yValues.Count != null)
            {
                maxinout = yValues.Count - 1;
            }
            else
            {
                maxinout = 0;
            }
            chart8.ChartAreas[0].AxisX.IsMarginVisible = false;
            int i = 0;
            chart8.ChartAreas[0].AxisY.Maximum = 10;
            chart8.ChartAreas[0].AxisY.Minimum = 0;
            foreach (var item in yValues)
            {
                chart8.Series[0].Points.Add(new DataPoint(i, item));
                i++;
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button39_Click(object sender, EventArgs e)
        {
            if (tabControl2.SelectedIndex != 0)
            {
                tabControl2.SelectedIndex--;

            }
        }

        private void button40_Click(object sender, EventArgs e)
        {

            if (tabControl2.SelectedIndex != 2)
            {
                tabControl2.SelectedIndex++;

            }
        }
    }
}
