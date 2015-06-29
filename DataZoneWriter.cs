/* 
 * 
 * DataZoneWriter.cs
 * Author Stephanie Schutz for Xtricate.
 * Turns a Transaction List into a displayable string.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZT
{
    public class DataZoneWriter
    {
        /* Member variables */
        List<Transaction> transactions = new List<Transaction>();
        DateTime startDate = new DateTime();
        DateTime endDate = new DateTime();


        /// <summary>
        /// Constructor.
        /// Takes data to be filtered and output.
        /// </summary>
        /// <param name="t">Transaction list.</param>
        /// <param name="start">Start date.</param>
        /// <param name="end">End date.</param>
        public DataZoneWriter(List<Transaction> t, DateTime start, DateTime end)
        {
            transactions = t;
            startDate = start;
            endDate = end;

            if (t.Count == 0)
                return;

            FilterByDate();
            SortByDate();
        }

        /// <summary>
        /// Sorts the Transaction list by date from oldest to newest.
        /// </summary>
        private void SortByDate()
        {
            //transactions.Reverse();     // Reversing here preserves input order after sort
            transactions.Sort((a, b) => DateTime.Compare(a.date, b.date));
        }

        /// <summary>
        /// Filters out transactions from the Transaction list that are
        /// outside of the start or end dates.
        /// </summary>
        private void FilterByDate()
        {
            // Create list of items to be removed
            // Traverse backward to not mess up counter while removing
            for(int i = transactions.Count - 1; i >=0 ; i--)
            {
                if(DateTime.Compare(startDate, transactions[i].date) > 0)
                    transactions.RemoveAt(i);
                else if(DateTime.Compare(endDate, transactions[i].date) < 0)
                    transactions.RemoveAt(i);
            }
        }

        /// <summary>
        /// Returns the transactions in one string.
        /// </summary>
        /// <returns>One string of transactions.</returns>
        public string GetTransactionString()
        {
            //System.Console.WriteLine("transactions.Count is = {0}", transactions.Count);    // @@@ DEBUG
            if(transactions.Count == 0) { return ""; }

            string dateFormat = "MM/dd/yyyy";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Clear();
            for(int i = 0; i < transactions.Count; i++)
            {
                // Place ordered, tab delimited, transaction data into output string.
                string s = "";
                s += transactions[i].date.ToString(dateFormat) + "\t";

                if (transactions[i].type != "")
                    s += transactions[i].type + "\t";
                else
                    s += "\t\t";

                if (transactions[i].credit >= 0)
                    s += transactions[i].credit.ToString("0.00") + "\t";
                else
                    s += "\t";

                if (transactions[i].debit >= 0)
                    s += transactions[i].debit.ToString("0.00") + "\t";
                else
                    s += "\t";

                if (transactions[i].balance >= 0)
                    s += transactions[i].balance.ToString("0.00") + "\t";
                else
                    s += "\t";

                //// @@@ TODO? Parse and add Payee
                s += "\t";

                //// @@@ TODO? Add documentImage
                s += "\t";

                if (transactions[i].check > 0)
                    s += transactions[i].check.ToString() + "\t";
                else
                    s += "\t";

                //// @@@ TODO? Add tags
                s += "\t";

                if (transactions[i].note != "")
                    s += transactions[i].note + "\t";
                else
                    s += "\t\t";

                //// @@@ TODO? Add Trails
                //s += "\t\t";

                //// @@@ TODO? Add review notes
                //s += "\t\t";

                sb.AppendLine(s);
            }
            //System.Console.WriteLine("sb.ToString() is = {0}", sb.ToString());    // @@@ Debug
            return sb.ToString();
        }

    }
}
