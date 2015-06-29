using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dio
{

    /// <summary>
    ///  Defines Dio's error codes 
    /// </summary>
    public enum ErrorCodes
    {
        Success = 0,
        ValueGreaterThanMaxChars = 1,
        ValueMustExist = 2,
        ValueMustNotContainSpecialChars = 3,
        ValueMustBeNumber = 4,
        CharacterCountNotEqualToSpecifiedCount = 5,
        ValueMustBeAValidDate = 6,
        ValueMustBeASocialSecurityNumber = 7,
        ValueMustBeMoney = 8,
        ValueMustBeAValidDateTime = 9,
        ValueMustBeAValidTime = 10,
        MultipleImagesNotAllowed = 20,
        ItemIsNotChangeable = 21,
        DatabaseProcessNotRunning = 100,
        DatabaseConnectionFailed = 101,
        DatabaseStoredProcedureFailed = 102,
        MaxWindowsReached = 200,
        OnlyOneWindowOfTypeCanBeOpen = 201,
        DocumentImageDoesNotExist = 300,
        UserActionWarning = 500
    }


    class DataObjects
    {
        /// <summary>
        ///  Contains Transaction fields.
        ///  Public interface for getting TransactionType (Deposit, Debit)
        /// </summary>
        public class Transaction
        {
            public int id = 0;
            public DateField date;
            public TimeField time;
            public MoneyField total;
            public string description = "";
 
            private TransactionType type;
 
            public Transaction()
            {
                date = new DateField();
                date.Text = "Date";
                date.AllowNull = false;
 
                time = new TimeField();
                time.Text = "Time";
                time.AllowNull = true;
 
                total = new MoneyField();
                total.Text = "Total";
                total.AllowNull = false;
 
            }
 
            public TransactionType Type
            {
                get{return this.type;}
                set{this.type = value;}
            }
        }
 
 
        public enum TransactionType
        {
            Deposit,Debit
        }


        /// <summary>
        ///  Uses an ArrayList to store and acess Transactions.
        /// </summary>
        public class TransactionList
        {
            private ArrayList items = new ArrayList();

            //private int compare(Transaction A, Transaction B)
            //{
            //    if (  A. .CompareTo() != 0)
            //    {
            //        return x.Height.CompareTo(y.Height);
            //    }
            //}
 
            public Transaction GetItem(int ItemNumber)
            {
                return (Transaction) this.items[ItemNumber - 1];
            }
 
            public void Add(Transaction Transaction)
            {
                 this.items.Add(Transaction);
            }

            //public void AddSorted(Transaction Transaction)
            //{
            //    int position = items.BinarySearch(Transaction,
            //}
 
            public void Reverse()
            {
                this.items.Reverse();
            }
 
            public int Count
            {
                get{return this.items.Count;}
            }
 
            public void Clear()
            {
                this.items.Clear();
            }
        }



        public class DateField
        {
            private string sText;
            private string sValue;
            private string sName = "";
            private bool bAllowNull = false;
 
            /// <summary>
            /// Get the value of the string
            /// </summary>
            public string Value
            {
                get{return sValue;}
            }
 
            /// <summary>
            /// Get or Set the name of the string to be displayed within a label
            /// </summary>
            public string Text
            {
                get{return sText;}
                set{sText = value;}
            }
 
            /// <summary>
            /// Get or Set whether to allow this string to be empty
            /// </summary>
            public bool AllowNull
            {
                get{return bAllowNull;}
                set{bAllowNull = value;}
            }
 
            /// <summary>
            /// Get or Set the name of the item of the object
            /// </summary>
            public string Name
            {
                get{return sName;}
                set{sName = value;}
            }

            /// <summary>
            /// Function used to update the text value of the date, returns errorcodes depending upon string settings
            /// </summary>
            public ErrorCodes updateValue(string thisString)
            {
                FormFunctions formFunctions = new FormFunctions();
                if ((!bAllowNull) && (formFunctions.IsNull(thisString)))
                {
                    return ErrorCodes.ValueMustExist;
                }
                if (!formFunctions.IsNull(thisString))
                {
                    if (!formFunctions.DateIsValid(thisString))
                    {
                        return ErrorCodes.ValueMustBeAValidDate;
                    }
                }
                this.sValue = thisString;
                return ErrorCodes.Success;
            }
 
        }   


        public class TimeField
        {
            private string sText;
            private string sValue;
            private string sName = "";
            private bool bAllowNull = false;
 
            /// <summary>
            /// Get the value of the string
            /// </summary>
            public string Value
            {
                get{return sValue;}
            }
 
            /// <summary>
            /// Get or Set the name of the string to be displayed within a label
            /// </summary>
            public string Text
            {
                get{return sText;}
                set{sText = value;}
            }
 
            /// <summary>
            /// Get or Set the name of the item of the object
            /// </summary>
            public string Name
            {
                get{return sName;}
                set{sName = value;}
            }
 
            /// <summary>
            /// Get or Set whether to allow this string to be empty
            /// </summary>
            public bool AllowNull
            {
                get{return bAllowNull;}
                set{bAllowNull = value;}
            }

            /// <summary>
            /// Function used to update the text value of the date, returns errorcodes depending upon string settings
            /// </summary>
            public ErrorCodes updateValue(string thisTime)
            {
                FormFunctions formFunctions = new FormFunctions();
                if ((!bAllowNull) && (formFunctions.IsNull(thisTime)))
                {
                    return ErrorCodes.ValueMustExist;
                }
                if (!formFunctions.IsNull(thisTime))
                {
                    if (!formFunctions.TimeIsValid(thisTime))
                    {
                        return ErrorCodes.ValueMustBeAValidTime;
                    }
                }
                this.sValue = thisTime;
                return ErrorCodes.Success;
            }

        }   


        public class DateTimeField
        {
            private string sText;
            private string sValue;
            private string sName = "";
            private bool bAllowNull = false;

            /// <summary>
            /// Get the value of the string
            /// </summary>
            public string Value
            {
                get { return sValue; }
            }


            /// <summary>
            /// Get or Set the name of the string to be displayed within a label
            /// </summary>
            public string Text
            {
                get { return sText; }
                set { sText = value; }
            }


            /// <summary>
            /// Get or Set whether to allow this string to be empty
            /// </summary>
            public bool AllowNull
            {
                get { return bAllowNull; }
                set { bAllowNull = value; }
            }


            /// <summary>
            /// Get or Set the name of the item of the object
            /// </summary>
            public string Name
            {
                get { return sName; }
                set { sName = value; }
            }


            /// <summary>
            /// Function used to update the text value of the date, returns errorcodes depending upon string settings
            /// </summary>
            public ErrorCodes updateValue(string thisString)
            {
                FormFunctions formFunctions = new FormFunctions();
                if ((!bAllowNull) && (formFunctions.IsNull(thisString)))
                {
                    return ErrorCodes.ValueMustExist;
                }

                if (!formFunctions.IsNull(thisString))
                {
                    string delim = @" ";
                    string[] mySplit = thisString.Split(delim.ToCharArray());

                    if (mySplit.Length > 2)
                    {
                        return ErrorCodes.ValueMustBeAValidDateTime;
                    }

                    string thisDate = mySplit[0].ToString();
                    string thisTime = mySplit[1].ToString();

                    if (!formFunctions.DateIsValid(thisDate))
                    {
                        return ErrorCodes.ValueMustBeAValidDateTime;
                    }

                    if (!formFunctions.TimeIsValid(thisTime))
                    {
                        return ErrorCodes.ValueMustBeAValidDateTime;
                    }
                }

                this.sValue = thisString;
                return ErrorCodes.Success;
            }

        }   


        public class MoneyField
        {
            private string sText;
            private string sValue = "0.00";
            private string sName = "";
            private bool bAllowNull = false;

            /// <summary>
            /// Get the value of the string
            /// </summary>
            public string Value
            {
                get { return sValue; }
            }


            /// <summary>
            /// Get or Set the name of the string to be displayed within a label
            /// </summary>
            public string Text
            {
                get { return sText; }
                set { sText = value; }
            }

            /// <summary>
            /// Get or Set the name of the item of the object
            /// </summary>
            public string Name
            {
                get { return sName; }
                set { sName = value; }
            }

            /// <summary>
            /// Get or Set whether to allow this string to be empty
            /// </summary>
            public bool AllowNull
            {
                get { return bAllowNull; }
                set { bAllowNull = value; }
            }

            ///// <summary>
            ///// Function used to update the text value of the money, returns errorcodes depending upon string settings
            ///// </summary>
            //public ErrorCodes updateValue(string thisString)
            //{
            //    FormFunctions formFunctions = new FormFunctions();
            //    if ((!bAllowNull) && (formFunctions.IsNull(thisString)))
            //    {
            //        return ErrorCodes.ValueMustExist;
            //    }

            //    if (!formFunctions.IsNull(thisString))
            //    {
            //        if (!formFunctions.MoneyIsValid(thisString))
            //        {
            //            return ErrorCodes.ValueMustBeMoney;
            //        }
            //    }

            //    this.sValue = thisString;
            //    return ErrorCodes.Success;
            //}

        }   


    }   // End class DataObjects


}   // End namespace Dio
