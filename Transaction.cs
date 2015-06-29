/* 
 * 
 * Transaction.cs
 * Author Stephanie Schutz for Xtricate.
 * Data types for bank transactions.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZT
{
    public enum Field
    {
        Check,
        Date,
        Amount,
        Balance,
        Type,
        Note,
        Failure
    };

    public enum ZoneTransactionType
    {
        Debit,
        Credit,
        Mixed,
        Unknown,
        Summary,
        //Balance,
        Failure
    };

    public enum ZoneDataFormat
    {
        check_date_amount_F,    
        check_date_amount_balance_F,
        check_date_note_amount_F,
        check_amount_date_F,
        date_check_amount_F,
        date_check_amount_balance_F,
        date_note_amount_note_F,
        date_note_amount_balance_note_T,
        note_date_amount_note_T,
        custom_format,
        //date_payee_note_amount_note,
        //date_payee_note_amount_balance_note,
        Failure
    };

    public class Transaction
    {
        public UInt32 check = 0;
        public DateTime date = new DateTime();
        public double debit = -1;
        public double credit = -1;
        public double balance = -1;
        public string type = "";
        public string note = "";
        public string note2 = "";
        public double runningTotal = -1;
    }
}
