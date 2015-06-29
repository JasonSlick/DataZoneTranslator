/* 
 * 
 * DataZoneForm.cs
 * Author Stephanie Schutz for Xtricate.
 *  Updated by Jason Slick
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace DZT
{
    public partial class DataZoneForm : Form
    {
        /// <summary>
        /// Auto generated for initializing components.
        /// </summary>
        public DataZoneForm()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// Initialize my widgets.
        /// </summary>
        private void Init()
        {
            typeComboBox.Items.Add("Debit");
            typeComboBox.Items.Add("Credit");
            typeComboBox.Items.Add("Mixed");
            //typeComboBox.Items.Add("Balance");
            //typeComboBox.Items.Add("Summary");
            typeComboBox.SelectedIndex = 0;

            // Set which data format option is initialy displayed
            dataFormatComboBox.Items.Add(new Item("check# date amt","check_date_amount_F"));
            dataFormatComboBox.Items.Add(new Item("check# date amt bal","check_date_amount_balance_F"));
            dataFormatComboBox.Items.Add(new Item("check# date note amt","check_date_note_amount_F"));
            dataFormatComboBox.Items.Add(new Item("check# amt date","check_amount_date_F"));
            dataFormatComboBox.Items.Add(new Item("date check# amt","date_check_amount_F"));
            dataFormatComboBox.Items.Add(new Item("date check# amt bal","date_check_amount_balance_F"));   
            dataFormatComboBox.Items.Add(new Item("date note amt note","date_note_amount_note_F"));
            dataFormatComboBox.Items.Add(new Item("note date amt note NewLine", "note_date_amount_note_T"));
            //dataFormatComboBox.Items.Add("date payee note amt note");
            //dataFormatComboBox.Items.Add("date payee note amt bal note");  
            dataFormatComboBox.SelectedIndex = 0;

            // @@@ DEBUG
            startDateTimePicker.Value = new DateTime(2012, 1, 1);
            endDateTimePicker.Value = new DateTime(2012, 12, 31);

            updateLogDisplay();
        }

        private class Item
        {
            public string Name;
            public string Value;
            public Item(string name, string value)
            {
                Name = name;
                Value = value;
            }
            public override string ToString()
            {
                // Generates the text shown in the combo box
                return Name;
            }

        }

        private void updateLogDisplay()
        {
            //string pathA = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //string pathB = Path.GetDirectoryName(pathA);
            //string pathC = Path.Combine(pathB, "log.txt");
            if (File.Exists(LogManager.GetPath()))
            {
                logRichTextBox.LoadFile(LogManager.GetFileName(), RichTextBoxStreamType.PlainText);
                logRichTextBox.SelectionStart = logRichTextBox.Text.Length;
                logRichTextBox.ScrollToCaret();
            }
        }


        /// <summary>
        /// When input text is changed the output text is cleared.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inRichTextBox_TextChanged(object sender, EventArgs e)
        {
            outRichTextBox.Clear();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.LoadFormatFile();
        }

        private void LoadFormatFile()
        {
            string formatFile = @"dataformats.txt";
            if (File.Exists(formatFile))
            {
                int counter = 0;
                string line;
                string delim = ":";
                string errorString = "Error Line(s): ";
                bool errorSeen = false;
                int errorCount = 0;

                // Read the file and display it line by line.
                System.IO.StreamReader file = new System.IO.StreamReader(formatFile);
                while ((line = file.ReadLine()) != null)
                {
                    counter++;
                    if (line.Contains(delim))
                    {
                        string[] formatLine;
                        formatLine = line.Split(delim.ToCharArray(),2);

                        dataFormatComboBox.Items.Add(new Item(formatLine[0], formatLine[1]));
                    }
                    else
                    {
                        errorSeen = true;
                        if(errorCount > 0)
                        {
                            errorString = errorString + ", ";
                        }
                        errorCount++;
                        errorString = errorString + counter.ToString();
                    }

                    
                }

                file.Close();

                if (errorSeen)
                {
                    MessageBox.Show(errorString);
                }

            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                tabControl1.SelectedTab = tabPage2;
                toolStripButton1.Text = "Back";
            }
            else
            {
                tabControl1.SelectedTab = tabPage1;
                toolStripButton1.Text = "Show Log";
            }
        }

        /// <summary>
        /// Kicks off data input and output.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goButton_Click_1(object sender, EventArgs e)
        {
            // Get the inputs from the GUI
            string inputData = inRichTextBox.Text;
            if (inputData.Length == 0)
            {
                MessageBox.Show("There must be text in the input text box.", "ERROR");
                return;
            }

            DateTime startDate = startDateTimePicker.Value;
            DateTime endDate = endDateTimePicker.Value;
            if (startDate.Year != endDate.Year)
            {
                MessageBox.Show("Dates need to be in same year.", "ERROR");
                return;
            }
            if (DateTime.Compare(startDate, endDate) > 0)
            {
                MessageBox.Show("Start date must be before end date.", "ERROR");
                return;
            }

            //int dataFormat = dataFormatComboBox.SelectedIndex;
            int dataType = typeComboBox.SelectedIndex;

            Item itm = (Item) dataFormatComboBox.SelectedItem;

            // Parse data
            bool checkLog = false;
            DataZoneReader r = new DataZoneReader(inputData, itm.Value, dataType, startDate, endDate, ref checkLog);
            if (checkLog)
            {
                updateLogDisplay();
                toolStripButton1.Enabled = true;
                toolStripButton1.Visible = true;
                //MessageBox.Show("Check log file.");
            }

            // Output data
            DataZoneWriter w = new DataZoneWriter(r.GetTransactionList(), startDate, endDate);
            outRichTextBox.Text = w.GetTransactionString();
        }

        private void startDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            endDateTimePicker.Value = new DateTime(startDateTimePicker.Value.Year,12,31);
        }
    }
}
