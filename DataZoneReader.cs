/* 
 * 
 * DataZoneReader.cs
 * Author Stephanie Schutz for Xtricate.
 *  Updated by Jason Slick
 * Parses a string containing one zone from a bank statement into a Transaction List.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace DZT
{
    public class DataZoneReader
    {
        /* Member variables */
        Transaction T = new Transaction();
        List<Transaction> Transactions = new List<Transaction>();
        List<Field> fields = new List<Field>();

        List<string> debitKeyWords = new List<string>() {
            "Debit", "DEBIT", "Draft", "DRAFT", "WITHDRAWAL", "Withdrwl", 
            "Withdrawal", "PAYMNT", "Pmt", "PAYMT", "PAYMENT", "Pay", "PAY", 
            "Paid", "Purchase", "Mortgage", "Fee" };
        List<string> CreditKeyWords = new List<string>() { "DEPOSIT", "Deposit", "REFUND", "Refund", "Credit", "CREDIT" };
        List<string> Months = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", 
            "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec", "January", "February",
            "March", "April", "May", "June", "July", "August", "September", "October",
            "November", "December" };

        static int DefaultYear = 1;
        //int Year = DefaultYear;
        DateTime startDate = new DateTime();
        DateTime endDate = new DateTime();
        ZoneDataFormat DataFormat = ZoneDataFormat.Failure;
        ZoneTransactionType DataType = ZoneTransactionType.Failure;
        bool UseNewLineAsKey = false;


        /// <summary>
        /// Blank constructor.
        /// Used for testing other methods.
        /// </summary>
        public DataZoneReader() { }

        /// <summary>
        /// Constructor.
        /// Parses input into Transaction List.
        /// Main loop through data.
        /// </summary>
        /// <param name="inputString">Input string to parse.</param>
        /// <param name="format">Format of the data.</param>
        ///  <param name="y">Year of data. (Data can only span one numerical year)</param>
        /// <returns>Nothing.</returns>
        public DataZoneReader(String inputString, string format, int type, DateTime start, DateTime end, ref bool checkLog)
        {
            //Year = y;
            startDate = start;
            endDate = end;
            DataType = GetDataType(type);
            GetFormatData(format);
            CheckData();

            List<string> data = GetDataFromDelimitedString(inputString);
            Parse(data, fields);

            LogManager.WroteToLog();

            return;
        }

        ZoneTransactionType GetDataType(int type)
        {
            if(type == (int)ZoneTransactionType.Debit)
                return ZoneTransactionType.Debit;
            if(type == (int)ZoneTransactionType.Credit)
                return ZoneTransactionType.Credit;
            if(type == (int)ZoneTransactionType.Mixed)
                return ZoneTransactionType.Mixed;
            return ZoneTransactionType.Failure;
        }

        void GetFormatData(string format)
        {
            switch (format)
            {
                case "check_date_amount_F":
                    DataFormat = ZoneDataFormat.check_date_amount_F;
                    fields.Add(Field.Check);
                    fields.Add(Field.Date);
                    fields.Add(Field.Amount);
                    break;
                case "check_date_amount_balance_F":
                    DataFormat = ZoneDataFormat.check_date_amount_balance_F;
                    fields.Add(Field.Check);
                    fields.Add(Field.Date);
                    fields.Add(Field.Amount);
                    fields.Add(Field.Balance);
                    break;
                case "check_amount_date_F":
                    DataFormat = ZoneDataFormat.check_amount_date_F;
                    fields.Add(Field.Check);
                    fields.Add(Field.Amount);
                    fields.Add(Field.Date);
                    break;
                case "check_date_note_amount_F":
                    DataFormat = ZoneDataFormat.check_date_note_amount_F;
                    fields.Add(Field.Check);
                    fields.Add(Field.Date);
                    fields.Add(Field.Note);
                    fields.Add(Field.Amount);
                    break;
                case "date_check_amount_F":
                    DataFormat = ZoneDataFormat.date_check_amount_F;
                    fields.Add(Field.Date);
                    fields.Add(Field.Check);
                    fields.Add(Field.Amount);
                    break;
                case "date_check_amount_balance_F":
                    DataFormat = ZoneDataFormat.date_check_amount_balance_F;
                    fields.Add(Field.Date);
                    fields.Add(Field.Check);
                    fields.Add(Field.Amount);
                    fields.Add(Field.Balance);
                    break;
                case "date_note_amount_note_F":
                    DataFormat = ZoneDataFormat.date_note_amount_note_F;
                    UseNewLineAsKey = true;
                    fields.Add(Field.Date);
                    fields.Add(Field.Note);
                    fields.Add(Field.Amount);
                    fields.Add(Field.Note);
                    break;
                case "note_date_amount_note_T":
                    DataFormat = ZoneDataFormat.note_date_amount_note_T;
                    UseNewLineAsKey = true;
                    fields.Add(Field.Note);
                    fields.Add(Field.Date);
                    fields.Add(Field.Amount);
                    fields.Add(Field.Note);


                    break;
                default:
                    string[] formatValues;
                    string splitChar = "_";
                    formatValues = format.Split(splitChar.ToCharArray());

                    int formatCount = formatValues.Length;
                    string lastValue = formatValues[formatCount-1];

                    bool isError = false;

                    if (lastValue == "T" || lastValue == "F")
                    {

                        if (lastValue == "T")
                        {
                            UseNewLineAsKey = true;
                        }

                        for (int i = 0; i < formatCount - 1; i++)
                        {
                            string currentValue = formatValues[i];
                            switch (currentValue)
                            {
                                case "date":
                                    fields.Add(Field.Date);
                                    break;
                                case "amount":
                                    fields.Add(Field.Amount);
                                    break;
                                case "note":
                                    fields.Add(Field.Note);
                                    break;
                                case "check":
                                    fields.Add(Field.Check);
                                    break;
                                case "balance":
                                    fields.Add(Field.Balance);
                                    break;
                                default:
                                    isError = true;
                                    i = formatCount;
                                    break;

                            }

                        }
                       
                    }

                    if (isError)
                    {
                        MessageBox.Show("Error: Formatting Error");
                        DataFormat = ZoneDataFormat.Failure;
                    }
                    else
                    {
                        DataFormat = ZoneDataFormat.custom_format;
                    }
                    break;
            }
        }

        void CheckData()
        {
            if (DataFormat == ZoneDataFormat.Failure)
            {
                MessageBox.Show("Error getting data format.");
                LogManager.WriteString(String.Format("{0:G} Error getting data format.", System.DateTime.Now));
                return;
            }

            if (DataType == ZoneTransactionType.Failure)
            {
                MessageBox.Show("Error getting data type.");
                LogManager.WriteString(String.Format("{0:G} Error getting data type.", System.DateTime.Now));
                return;
            }
            if (fields.Count == 0)
            {
                MessageBox.Show("Error getting data fields.");
                LogManager.WriteString(String.Format("{0:G} Error getting data fields.", System.DateTime.Now));
                return;
            }
        }

        void NewTransaction()
        {
            Transactions.Add(T);
            T = new Transaction();
            if (DataType == ZoneTransactionType.Debit)
                T.type = "Debit";
            else if (DataType == ZoneTransactionType.Credit)
                T.type = "Credit";
        }

        void Parse(List<string> data, List<Field> field)
        {
            int d = 0;
            if (DataType == ZoneTransactionType.Debit)
                T.type = "Debit";
            else if (DataType == ZoneTransactionType.Credit)
                T.type = "Credit";


            while (d < data.Count)
            {
                for (int f = 0; f < field.Count && d < data.Count; f++)
                {
                    Field dataField = field[f];

                    // If this is the key value and the current transaction is finished,
                    // save current transaction and create new one.
                    if (f == 0)
                    {
                        if (dataField == Field.Date)
                        {


                            // Check that the first field is a date
                            if (IsDate(data[d]))
                            {
                                // New transaction?
                                if (T.date.Year != DefaultYear)
                                {
                                    if (UseNewLineAsKey && d != 0)
                                    {
                                        if (IsNewLine(data[d - 1]))
                                            NewTransaction();
                                    }
                                    else
                                        NewTransaction();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Error: First entry in transaction must be a date.");
                                return;
                            }
                        }
                        else if (dataField == Field.Check)
                        {
                            if (IsCheckNumber(data[d]))
                            {
                                if (T.date.Year != DefaultYear)
                                    NewTransaction();
                            }
                            else
                            {
                                MessageBox.Show("Error: First entry of transaction must be a check number.");
                                return;
                            }
                        }
                        else if (dataField == Field.Note)
                        {
                            NewTransaction();

                        }
                        else if (dataField == Field.Amount)
                        {
                            NewTransaction();
                        }
                        else if (dataField == Field.Balance)
                        {
                            NewTransaction();
                        }

                    }

                    switch (dataField)
                    {
                        case Field.Date:
                            DateTime dt = GetDate(data[d++]);
                            if(dt.CompareTo(new DateTime()) == 0)
                                return;

                            if (T.date.Year == DefaultYear)
                                T.date = dt;
                            else
                                T.note += " " + dt.ToString();
                            break;
                        case Field.Check:
                            uint c = GetCheckNumber(data[d++]);
                            if (T.check == 0)
                                T.check = c;
                            else
                                T.note += " " + c.ToString();
                            break;
                        case Field.Amount:
                            double a = GetAmount(data[d++]);
                            if (a < 0)
                                break;
                                
                            if (T.type == "Debit")
                            {
                                if (T.debit == -1)
                                    T.debit = a;
                                else
                                    T.note += " " + a.ToString();
                            }
                            else if (T.type == "Credit")
                            {
                                if (T.credit == -1)
                                    T.credit = a;
                                else
                                    T.note += " " + a.ToString();
                            }
                            else
                            {
                                T.type = "Debit"; 
                                if (T.debit == -1)
                                    T.debit = a;
                                else
                                    T.note += " " + a.ToString();
                            }
                            break;
                        case Field.Balance:
                            double b = GetAmount(data[d++]);
                            if (b == 0)
                                break;

                            if (T.balance == -1)
                                T.balance = b;
                            else
                                T.note += " " + b.ToString();
                            break;
                        //case Field.Payee:
                        //    // How to separate from note?
                        //    break;
                        case Field.Note:

                            // How to know when the note is over.
                            Field nextField;
                            if (f < field.Count - 1)
                                nextField = field[f + 1];
                            else
                                nextField = field[0];

                            // While this data entry isn't equal to the next field 
                            // put it in the note section and look at the next data entry
                            if (nextField == Field.Date)
                            {
                                while (d < data.Count && !IsDate(data[d]))
                                {
                                    if (!IsNewLine(data[d]))
                                    {
                                        if (T.note.Length > 0)
                                            T.note += " " + data[d];
                                        else if (T.note.Length == 0)
                                            T.note = data[d];
                                    }
                                    d++;
                                }
                            }
                            else if (nextField == Field.Check)
                            {
                                while (d < data.Count && !IsCheckNumber(data[d]))
                                {
                                    if (!IsNewLine(data[d]))
                                    {
                                        if (T.note.Length > 0)
                                            T.note += " " + data[d];
                                        else if (T.note.Length == 0)
                                            T.note = data[d];
                                    }
                                    d++;
                                }
                            }
                            else if (nextField == Field.Amount)
                            {
                                while (d < data.Count && !IsAmount(data[d]))
                                {
                                    if(!IsNewLine(data[d]))
                                    {
                                        if (T.note.Length > 0)
                                            T.note += " " + data[d];
                                        else if (T.note.Length == 0)
                                            T.note = data[d];
                                    }
                                    d++;
                                }
                            }
                            else if (nextField == Field.Note)
                            {
                                while (d < data.Count && !IsNewLine(data[d]))
                                {
                                    if (T.note.Length > 0)
                                            T.note += " " + data[d];
                                        else if (T.note.Length == 0)
                                            T.note = data[d];

                                    d++;
                                }
                            }

                            // If type hasn't been determined, try to determine it.
                            if (T.type == "")
                            {
                                if (debitKeyWords.Any(T.note.Contains))
                                { T.type = "Debit"; }
                                else if (CreditKeyWords.Any(T.note.Contains))
                                { T.type = "Credit"; }
                                //else if (T.note.Contains("Beginning Balance"))
                                //{ T.type = "Balance"; }
                            }
                            break;
                        default:
                            break;
                    } // End Switch
                } // End for loop
            } // End while d < data.count

            // Add last transaction
            if (T.date.Year != DefaultYear)
                Transactions.Add(T);
        }


        /// <summary>
        /// Gets list of Transaction data strings from input strings.
        /// </summary>
        /// <param name="inputString">Delimited string with Transaction data.</param>
        /// <param name="dataFormatType">The format of the data.</param>
        /// <returns>List of Transaction data strings.</returns>
        public List<string> GetDataFromDelimitedString(String inputString)
        {
            // Break delimited string into parts
            string str = "\t\r=* ";
            if (!UseNewLineAsKey)
                str += "\n";
            char[] delimiters = str.ToCharArray();
            string[] splitStringArray = inputString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            List<string> data = new List<string>(splitStringArray);

            // Sort out the \ns
            if (UseNewLineAsKey)
            {
                for (int i = data.Count - 1; i >= 0; i--)
                {
                    // String i has \n.  Insert delimiters around \ns, split
                    // string again.  Replace string i with new strings.
                    if (data[i].IndexOf("\n") >= 0)
                    {
                        int j = 0;
                        string s = data[i];
                        while ((j = s.IndexOf('\n', j)) != -1)
                        {
                            s = s.Insert(j, " ");
                            j = j + 2;
                            if (j < s.Length)
                            {
                                s = s.Insert(j, " ");
                                j++;
                            }
                        }

                        char[] d = { ' ' };
                        string[] split_strings = s.Split(d, StringSplitOptions.RemoveEmptyEntries);
                        data.RemoveAt(i);
                        for (int k = split_strings.Length - 1; k >= 0; k--)
                            data.Insert(i, split_strings[k]);
                    }
                }
            }

            // Clean data traversing backwards to keep counter correct when using Remove.
            // If the item is removed skip to the next item.
            for (int i = data.Count - 1; i >= 0; i--)
            {
                // Remove if string has no length
                if (data[i].Length == 0)            
                {
                    data.RemoveAt(i);
                    continue;
                }

                // Remove any extra spaces from ends
                data[i].Trim();
                if (data[i].Length == 0)
                {
                    data.RemoveAt(i);
                    continue;
                }

                // Remove single special chars and spaces.
                if (data[i].Length == 1 && (StringHasSpecialCharacters(data[i]) || data[i] == " "))
                {
                    data.RemoveAt(i);
                    if (data[i].Length == 0)
                    {
                        data.RemoveAt(i);
                        continue;
                    }
                }

                // Dec 27,2011
                if (Months.Any(data[i].Equals))
                {
                    data[i] = data[i] + " " + data[i + 1];
                    data.RemoveAt(i + 1);
                }

                
            } // End of looping through data.

            // @@@ DEBUG
            System.Console.WriteLine("*** Debug: data list:");
            for (int i = 0; i < data.Count; i++)
                System.Console.WriteLine("{0}", data[i]);
            System.Console.WriteLine("***");
            System.Console.WriteLine("\n");

            return data;
        }

        /// <summary>
        /// Gets the Transaction List
        /// </summary>
        /// <returns>Transaction List</returns>
        public List<Transaction> GetTransactionList()
        { return Transactions; }

        public double GetSum()
        {
            double sum = 0;
            foreach (Transaction t in Transactions)
            {
                if (string.Compare(t.type, "Credit", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    sum += t.credit;
                }
                else if (string.Compare(t.type, "Debit", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    sum -= t.debit;
                }
                else
                {
                    LogManager.WriteString("Error: Can not sum transaction of type {0}\n", t.type);
                }
            }

            return sum;
        }

        /// <summary>
        /// Is the string a new line?
        /// </summary>
        /// <param name="thisString">Input string.</param>
        /// <returns>True if thisString is a new line, else false.</returns>
        public bool IsNewLine(string thisString)
        {
            if (thisString == "\n")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Checks to see if the string is a valid date.
        /// If so, thisString may have an updated, DateTime parsable, string.
        /// </summary>
        /// <param name="thisString">Input string.</param>
        /// <returns>True if thisString is a DateTime parseable date, else false.  
        /// If true, updates thisString for parsing.</returns>
        public bool IsDate(string thisString)
        {
            string s = StringAsDate(thisString);
            if (String.IsNullOrEmpty(s))
                return false;

            DateTime dateValue = new DateTime();
            try
            {
                dateValue = DateTime.Parse(s);
                //Console.WriteLine("'{0}' converted to date {1}.", s, dateValue);     // @@@ DEBUG
                //thisString = s;
                return true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Unable to convert '{0}' to a date.", thisString);
                return false;
            }
        }

        /// <summary>
        /// Parses DateTime from string.  
        /// Verified previously by IsDate();
        /// </summary>
        /// <param name="thisString">String containing date.</param>
        /// <returns>DateTime</returns>
        private DateTime GetDate(string thisString)
        {
            DateTime dateValue = new DateTime();
            string s = StringAsDate(thisString);
            if (String.IsNullOrEmpty(s))
            {
                LogManager.WriteString(String.Format("{1:G} Error: GetDate failed on {0}", thisString, System.DateTime.Now));
                return dateValue;
            }

            try
            {
                dateValue = DateTime.Parse(s);
                return DateTime.Parse(s);
            }
            catch (FormatException)
            {
                Console.WriteLine("Unable to convert '{0}' to a date.", s);
                return dateValue;
            }
        }

        /// <summary>
        /// Attempts to format string to be parsed by DateTime.
        /// </summary>
        /// <param name="thisString">String to format</param>
        /// <returns>DateTime parsable string if possible, else empty string "".</returns>
        private string StringAsDate(string thisString)
        {
            string s = thisString;

            // A date should be within these lengths
            if (s.Length < 3 || s.Length > 12)
                return "";

            // Remove spaces on either end
            s.Trim();

            // Some amounts can be read in as dates
            // so check if it's an amount first.
            if (IsAmount(s))
                return "";

            // Remove mark # if it's on either end
            if (s[0] == '#')
                s = s.Remove(0, 1);
            if (s[s.Length - 1] == '#')
                s = s.Remove(s.Length - 1, 1);

            // Return misread 0s from os
            s = s.Replace('o', '0');
            s = s.Replace('O', '0');

            // Invalid date pattern that can be found in descriptions
            string pattern1 = "^\\d{1,2}\\/\\d{2}\\/\\d{2,4}$";
            Regex re0 = new Regex(pattern1);
            MatchCollection theMatch0 = re0.Matches(s);
            if (theMatch0.Count != 0)
                return "";

            // Valid date formats
            Regex re1 = new Regex("^\\d{1,2}\\/\\d{1,2}$");               // 8/20 or 08/20
            MatchCollection theMatch1 = re1.Matches(s);
            Regex re2 = new Regex("^\\d{1,2}\\-\\d{1,2}$");               // 8-20 or 08-20
            MatchCollection theMatch2 = re2.Matches(s);
            Regex re3 = new Regex("^[A-Z]{3}\\d{1,2}$");                // JAN1 or AUG20
            MatchCollection theMatch3 = re3.Matches(s);
            Regex re4 = new Regex("^\\d{1,2}[A-Z]{3}$");                // 1JAN or 20AUG
            MatchCollection theMatch4 = re4.Matches(s);
            Regex re5 = new Regex("^[A-Za-z]{3}\\s\\d{1,2},\\d{4}$");   // Aug 20,2012
            MatchCollection theMatch5 = re5.Matches(s);
            Regex re6 = new Regex("^[A-Z]{3}\\d{1,2}\\s\\d{4}$");       // Jan1 2012 or AUG20 2012
            MatchCollection theMatch6 = re6.Matches(s);
            Regex re7 = new Regex("^\\d{1,2}\\/\\d{1,2}\\s\\d{4}$");               // 8/20 or 08/20
            MatchCollection theMatch7 = re7.Matches(s);
            Regex re8 = new Regex("^\\d{1,2}\\-\\d{1,2}\\s\\d{4}$");               // 8-20 or 08-20
            MatchCollection theMatch8 = re8.Matches(s);
            Regex re9 = new Regex("^[A-Z]{3}\\d{1,2}\\s\\d{4}$");                // JAN1 or AUG20
            MatchCollection theMatch9 = re9.Matches(s);
            Regex re10 = new Regex("^\\d{1,2}[A-Z]{3}\\s\\d{4}$");                // 1JAN or 20AUG
            MatchCollection theMatch10 = re10.Matches(s);
            if (theMatch1.Count != 0 || theMatch2.Count != 0 ||
                theMatch3.Count != 0 || theMatch4.Count != 0)
            {
                int Year = 1;
                if (startDate.Year == endDate.Year)
                    Year = startDate.Year;
                else if (DateTime.Compare(DateTime.Parse(s), startDate) > 0 &&
                    DateTime.Compare(DateTime.Parse(s), LastDayOfYear(startDate)) < 0)
                    Year = startDate.Year;
                else if (DateTime.Compare(DateTime.Parse(s), endDate) < 0 &&
                    DateTime.Compare(DateTime.Parse(s), FirstDayOfYear(endDate)) > 0)
                    Year = endDate.Year;

                    s = s + " " + Year; // Add year to the date
            }
            else if (theMatch5.Count != 0 || theMatch6.Count != 0 ||
                     theMatch6.Count != 0 || theMatch7.Count != 0 ||
                     theMatch8.Count != 0 || theMatch9.Count != 0 ||
                     theMatch10.Count != 0) { } // Valid format with year
            else
                return "";

            return s;
        }

        /// <summary>
        /// Finds the first day of year of the specified day.
        /// </summary>
        static DateTime FirstDayOfYear(DateTime y)
        {
            return new DateTime(y.Year, 1, 1);
        }

        /// <summary>
        /// Finds the last day of the year for the selected day's year.
        /// </summary>
        static DateTime LastDayOfYear(DateTime d)
        {
            // 1
            // Get first of next year
            DateTime n = new DateTime(d.Year + 1, 1, 1);
            // 2
            // Subtract 1 from it
            return n.AddDays(-1);
        }

        /// <summary>
        /// Check to see if the string is a valid check number.
        /// </summary>
        /// <param name="thisString">Input string.</param>
        /// <returns>True if thisString is a valid check number, else false.  
        /// If true, updates thisString for parsing.</returns>
        public bool IsCheckNumber(string thisString)
        {
            string s = StringAsCheckNumber(thisString);
            if (string.IsNullOrEmpty(s))
                return false;

            return true;
        }

        /// <summary>
        /// Gets the uint32 check number from the string
        /// </summary>
        /// <param name="thisString">String verified to have check number with IsCheck().</param>
        /// <returns>UInt32 Check number.</returns>
        private UInt32 GetCheckNumber(string thisString)
        {
            string s = StringAsCheckNumber(thisString);
            if (String.IsNullOrEmpty(s))
            {
                LogManager.WriteString(String.Format("{1:G} Error: GetCheckNumber failed on {0}", thisString, System.DateTime.Now));
                return 0;
            }
            return Convert.ToUInt32(s);
        }

        /// <summary>
        /// Attempts to format string as a check number.
        /// </summary>
        /// <param name="thisString">String to format</param>
        /// <returns>GetCheckNumber parsable string if possible, else empty string "".</returns>
        private string StringAsCheckNumber(string thisString)
        {
            string s = thisString;

            // Remove spaces on either end
            if (s[0] == ' ')
                s = s.Remove(0, 1);
            if (s[s.Length - 1] == ' ')
                s = s.Remove(s.Length - 1, 1);

            //s = StringMinusOrderMarks(s);
            if (StringOnlyHasNumbers(s))
            {
                // Check # are 1-15 digits
                if (s.Length < 1 || s.Length > 15)
                    return "";

                try
                {
                    Convert.ToUInt32(thisString);
                    return s;
                }
                catch
                {
                    Console.WriteLine("Unable to convert '{0}' to check number.", s);
                    return "";
                }
            }
            else
                return "";
        }

        /// <summary>
        /// Check to see if the string is a valid amount.
        /// </summary>
        /// <param name="thisString">Input string.</param>
        /// <returns>True if thisString is a valid amount, else false.  
        /// If true, updates thisString for parsing.</returns>
        public bool IsAmount(string thisString)
        {
            // Is this a valid money amount?
            // Strip spaces, $, and - signs, if any
            string s = StringAsAmount(thisString);
            if (string.IsNullOrEmpty(s))
                return false;

            return true;
        }

        /// <summary>
        /// Gets dollar amount from string.
        /// Previously verified to be an amount by IsAmount().
        /// </summary>
        /// <param name="thisString"Input string.>Input string containing amount.</param>
        /// <returns>A double with the dollar amount.</returns>
        private Double GetAmount(string thisString)
        {
            string s = StringAsAmount(thisString);
            if (string.IsNullOrEmpty(s))
            {
                LogManager.WriteString(String.Format("{1:G} Error: GetAmount failed on {0}", thisString, System.DateTime.Now));
                return -1;
            }

            return Convert.ToDouble(s);
        }

        /// <summary>
        /// Attempts to format string as a dollar amount.
        /// </summary>
        /// <param name="thisString">String to format</param>
        /// <returns>GetAmount parsable string if possible, else empty string "".</returns>
        private string StringAsAmount(string thisString)
        {
            // Is this a valid money amount?
            // Strip spaces, $, and - signs, if any
            string s = thisString;
            if (s[0] == ' ')
                s = s.Remove(0, 1);
            if (s.Length <= 0)
                return s;
            if (s[s.Length - 1] == ' ')
            {
                s = s.Remove(s.Length - 1, 1);
                if (s.Length <= 0)
                    return s;
            }
            if (s[0] == '$')
            {
                s = s.Remove(0, 1);
                if (s.Length <= 0)
                    return s;
            }
            if (s[0] == '-')
            {
                s = s.Remove(0, 1);
                if (s.Length <= 0)
                    return s;
            }
            if (s[s.Length - 1] == '-')
            {
                s = s.Remove(s.Length - 1, 1);
                if (s.Length <= 0)
                    return s;
            }

            // Check that the number has two decimal places
            int decimalSign = s.IndexOf('.');
            if ((decimalSign < 0) || (decimalSign != s.Length - 3))
                return "";

            return s;
        }


        /// <summary>
        /// Checks if string contains only numbers.
        /// </summary>
        /// <param name="thisString">Input string</param>
        /// <returns>True if string contains only numbers, else false.</returns>
        private bool StringOnlyHasNumbers(string thisString)
        {
            bool specialExist = false;
            specialExist = Regex.IsMatch(thisString, "^[0-9]+$");

            return specialExist;
        }

        /// <summary>
        /// Checks if string has special characters.
        /// From FormFunctions.cs
        /// </summary>
        /// <param name="thisString">Input string</param>
        /// <returns>True if string contains special characters, else false.</returns>
        private bool StringHasSpecialCharacters(string thisString)
        {
            bool specialExist = false;
            specialExist = Regex.IsMatch(thisString, "[^a-z0-9\\s]", RegexOptions.IgnoreCase);

            return specialExist;
        }
    }
}
