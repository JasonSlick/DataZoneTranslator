using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;

namespace Dio
{
	/// <summary>
	/// FormFunctions are reusable functions for checking form fields meeting specific criteria
	/// </summary>
	public class FormFunctions
	{
		public bool CharactersLessThanMaxCount(string thisString, int maxCount)
		{
			int thisCount = thisString.Length;
			if(thisCount > maxCount)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool CharacterCountEqualToSpecifiedCount(string thisString, int specifiedCount)
		{
			int thisCount = thisString.Length;
			if(thisCount != specifiedCount)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public bool IsNull(string thisString)
		{
			if(thisString == null)
			{
				return true;
			}
			int thisCount = thisString.Length;
			if(thisCount <= 0)
			{
				return true;
			}
			else
			{
				return false;
			}

		}

		public bool StringHasSpecialCharacters(string thisString)
		{
			bool specialExist = false;
			specialExist = Regex.IsMatch(thisString,"[^a-z0-9\\s]",RegexOptions.IgnoreCase);

			return specialExist;
		}

		public bool StringOnlyHasNumbers(string thisString)
		{
			bool specialExist = false;
			specialExist = Regex.IsMatch(thisString,"^[0-9]+$",RegexOptions.IgnoreCase);

			return specialExist;

		}

		public bool DateIsValid(string thisDate)
		{
			string[] myDate;
			string myMonth;
			int month;
			string myDay;
			int day;
			string myYear;
			int year;
			string delim = "/";

			myDate = thisDate.Split(delim.ToCharArray());
			if(myDate.Length != 3)
			{
				return false;
			}

			//check whether we have a valid month
			myMonth = myDate[0];
			if(!StringOnlyHasNumbers(myMonth))
			{
				return false;
			}
			if((myMonth.Length > 2)||(myMonth.Length == 0))
			{
				return false;
			}

			month = Convert.ToInt32(myMonth);
			if((month > 12)||(month <= 0))
			{
				return false;
			}

			//check whether we have a valid year
			myYear = myDate[2];
			if(!StringOnlyHasNumbers(myYear))
			{
				return false;
			}
			year = Convert.ToInt32(myYear);
			if(year <= 1777)
			{
				return false;
			}
			else if(year > 9999)
			{
				return false;
			}

			//check whether we have a valid day
			myDay = myDate[1];
			if(!StringOnlyHasNumbers(myDay))
			{
				return false;
			}
			if((myDay.Length > 2)||(myDay.Length == 0))
			{
				return false;
			}

			int[] monthDays = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};

	
			if((year % 4 == 0)  &&  (year % 100 != 0) || (year % 400 == 0))
			{
				monthDays[1] = 29;
			}
			int numberDays = monthDays[month-1];
			
			day = Convert.ToInt32(myDate[1]);
			if((day <= 0)||(day > numberDays))
			{
				return false;
			}
			return true;
		}


		public bool SocialSecurityNumberIsValid(string thisString)
		{
			string thisNumber = "";
			//Check whether it has dots (.)
			bool specialExist = false;
			specialExist = Regex.IsMatch(thisString,".",RegexOptions.IgnoreCase);
			if(specialExist)
			{
				string delim = ".";
				string[] parts = thisString.Split(delim.ToCharArray());
				for(int i = 0;i<parts.Length;i++)
				{
					thisNumber += parts[i];
				}
			}
			else
			{
				thisNumber = thisString;
			}

			if(thisNumber.Length != 9)
			{
				return false;
			}

			return true;
		}

		public string StringMinusDots(string thisString)
		{
			string thisNumber = "";
			//Check whether it has dots (.)
			bool specialExist = false;
			specialExist = Regex.IsMatch(thisString,".",RegexOptions.IgnoreCase);
			if(specialExist)
			{
				string delim = ".";
				string[] parts = thisString.Split(delim.ToCharArray());
				for(int i = 0;i<parts.Length;i++)
				{
					thisNumber += parts[i];
				}
			}
			else
			{
				thisNumber = thisString;
			}

			return thisNumber;
		}

		public bool ZipCodeIsValid(string thisString)
		{
			string thisNumber = "";
			//Check whether it has dash (-)
			bool specialExist = false;
			specialExist = Regex.IsMatch(thisString,"-",RegexOptions.IgnoreCase);
			if(specialExist)
			{
				string delim = "-";
				string[] parts = thisString.Split(delim.ToCharArray());
				for(int i = 0;i<parts.Length;i++)
				{
					thisNumber += parts[i];
				}
			}
			else
			{
				thisNumber = thisString;
			}

			specialExist = this.StringOnlyHasNumbers(thisNumber);
			return true;
		}

		public bool MoneyIsValid(string thisString)
		{
			string dollarNumber = "";
			string centNumber = "";
			//if negative strip off minus
			thisString = thisString.Replace("-","");
			//Check whether it has dots (.)
			bool specialExist = false;
			specialExist = Regex.IsMatch(thisString,".",RegexOptions.IgnoreCase);
			if(!specialExist)
			{
				return false;
			}
			
			string delim = ".";
			string[] parts = thisString.Split(delim.ToCharArray());
			if(parts.Length != 2)
			{
				return false;
			}

			dollarNumber = parts[0].ToString();
			if(!StringOnlyHasNumbers(dollarNumber))
			{
				return false;
			}

			centNumber = parts[1].ToString();
			if((!StringOnlyHasNumbers(centNumber))||(!CharacterCountEqualToSpecifiedCount(centNumber, 2)))
			{
				return false;
			}

			return true;
		}

		public string ReturnAsMoney(string thisString)
		{
			bool specialExist = false;
			specialExist = Regex.IsMatch(thisString,"[.]");
			//Note update MoneyIsValid and anything else looking for decimal
			if(!specialExist)
			{
				return thisString +".00";
			}
			else
			{
				string delim = ".";
				string[] parts = thisString.Split(delim.ToCharArray());
				if(parts[1].Length == 1)
				{
					parts[1] = parts[1] + "0";
				}
				else if(parts[1].Length > 2)
				{
					string centsNumber = parts[1].Substring(0,2);
					string nextNumber = parts[1].Substring(2,1);
					if(Convert.ToInt32(nextNumber) >= 5)
					{
						//Round Up
						int cents = Convert.ToInt32(centsNumber) + 1;
						parts[1] = cents.ToString();
					}
					else
					{
						parts[1] = centsNumber;
					}
				}
				thisString = parts[0] + "." + parts[1];

			}

			return thisString;

		}

		public bool TimeIsValid(string thisTime)
		{
			string[] myTime;
			string delim = ":";
			string hour;
			string minute;
			string second;

			myTime = thisTime.Split(delim.ToCharArray());
			//check whether we have a valid time
			if(myTime.Length != 3)
			{
				return false;
			}
			hour = myTime[0];
			minute = myTime[1];
			second = myTime[2];

            if (!StringOnlyHasNumbers(hour))
            {
                return false;
            }
			if((hour.Length > 2)||(hour.Length == 0))
			{
				return false;
			}
			else
			{
				int iHour = Convert.ToInt32(hour);
				if((iHour < 0)||(iHour > 23))
				{
					return false;
				}
			}

            if (!StringOnlyHasNumbers(minute))
            {
                return false;
            }
			if((minute.Length > 2)||(minute.Length == 0))
			{
				return false;
			}
			else
			{
				int iMinute = Convert.ToInt32(minute);
				if((iMinute < 0)||(iMinute > 59))
				{
					return false;
				}
			}

            if (!StringOnlyHasNumbers(second))
            {
                return false;
            }
			if((second.Length > 2)||(second.Length == 0))
			{
				return false;
			}
			else
			{
				int iSecond = Convert.ToInt32(second);
				if((iSecond < 0)||(iSecond > 59))
				{
					return false;
				}
			}
			return true;
		}

		public bool FileExists(string Filename)
		{
			if(!File.Exists(Filename))
			{
				return false;
			}
			return true;
		}



		public string ReturnAsMoneyWithCommas(string inputValue)
		{
            if (inputValue == "")
            {
                return "0.00";
            }


			string delim = ".";
			bool isNeg = false;
            string outMoney = "";

            string[] tempMoney = inputValue.Split(delim.ToCharArray());
            string centsMoney = tempMoney[1].ToString();
            string dollarsMoney = tempMoney[0].ToString();

                
            if (String.Compare(dollarsMoney.Substring(0, 1), "-") == 0)
            {
                isNeg = true;
                dollarsMoney = dollarsMoney.Substring(1, dollarsMoney.Length - 1);
            }
            if (dollarsMoney.Length <= 3)
            {
                outMoney = dollarsMoney + "." + centsMoney;
            }
            else
            {
                for (int i = dollarsMoney.Length; i > 0; i = i - 3)
                {
                    if ((i - 3) > 0)
                    {
                        outMoney = "," + dollarsMoney.Substring(i - 3, 3) + outMoney;
                    }
                }
                int topNum = dollarsMoney.Length % 3;
                if (topNum == 0)
                {
                    topNum = 3;
                }
                outMoney = dollarsMoney.Substring(0, topNum) + outMoney;
                outMoney = outMoney + "." + centsMoney;
            }

            if (isNeg)
            {
                outMoney = "-" + outMoney;
            }

			
			return outMoney;
		}

        public string ReturnDescriptionWithConvertedMoneyUsingDollarSignAsMoneyLocator(string inputValue)
        {
            string outputString = "";

            //Start Configure Money in Description
            //Look for money value in description and add commas
            string tempDesc = inputValue;
            string delim = @" ";
            string[] mySplit = tempDesc.Split(delim.ToCharArray());
            for (int i = 0; i < mySplit.Length; i++)
            {
                //Add space back to description
                if (i > 0)
                {
                    outputString += " ";
                }

                string tempString = mySplit[i].ToString();
                if (tempString.StartsWith("$"))
                {
                    tempString = tempString.Remove(0, 1);
                    outputString += "$" + this.ReturnAsMoneyWithCommas(tempString);
                }
                else
                {
                    outputString += tempString;
                }

            }
            //End configure money in description

            return outputString;
        }


		public string ReturnSumAsMoney(string inputA, string inputB)
		{
			if(!MoneyIsValid(inputA))
			{
				return "";
			}

			if(!MoneyIsValid(inputB))
			{
				return "";
			}

			decimal oldAmount = Convert.ToDecimal(inputA);
			decimal newAmount = Convert.ToDecimal(inputB);
			newAmount += oldAmount;
			string amountString = ReturnAsMoney(newAmount.ToString());

			return amountString;
		}


		/// <summary>
		/// Returns inputA - inputB
		/// </summary>
		/// <param name="inputA">string of money x.xx</param>
		/// <param name="inputB">string of money x.xx</param>
		/// <returns></returns>
		public string ReturnDifferenceAsMoney(string inputA, string inputB)
		{
			if(!MoneyIsValid(inputA))
			{
				return "";
			}

			if(!MoneyIsValid(inputB))
			{
				return "";
			}

			decimal oldAmount = Convert.ToDecimal(inputA);
			decimal newAmount = Convert.ToDecimal(inputB);
			newAmount = oldAmount - newAmount;
			string amountString = ReturnAsMoney(newAmount.ToString());

			return amountString;
		}

		public string ReturnGreaterEqualAsMoney(string inputA, string inputB)
		{
			if(!MoneyIsValid(inputA))
			{
				return "";
			}

			if(!MoneyIsValid(inputB))
			{
				return "";
			}

			decimal oldAmount = Convert.ToDecimal(inputA);
			decimal newAmount = Convert.ToDecimal(inputB);
			if(oldAmount > newAmount)
			{
				newAmount = oldAmount;
			}
			string amountString = ReturnAsMoney(newAmount.ToString());

			return amountString;
		}

		public bool MoneyIsGreater(string inputA, string inputB)
		{
			if(!MoneyIsValid(inputA))
			{
				return false;
			}

			if(!MoneyIsValid(inputB))
			{
				return true;
			}

			decimal inputAAmount = Convert.ToDecimal(inputA);
			decimal inputBAmount = Convert.ToDecimal(inputB);
			if(inputAAmount >= inputBAmount)
			{
				return true;
			}
			return false;
		}

        public bool MoneyIsEqual(string inputA, string inputB)
        {
            if (!MoneyIsValid(inputA))
            {
                return false;
            }

            if (!MoneyIsValid(inputB))
            {
                return false;
            }

            decimal inputAAmount = Convert.ToDecimal(inputA);
            decimal inputBAmount = Convert.ToDecimal(inputB);
            if (inputAAmount == inputBAmount)
            {
                return true;
            }
            return false;
        }

		/// <summary>
		///  Compares whether firstDateTime is greater than secondDateTime returns true or false
		/// </summary>
		/// <param name="firstDateTime"></param>
		/// <param name="secondDateTime"></param>
		/// <returns>true or false</returns>
		public bool DateTimeIsGreater(string firstDateTime, string secondDateTime)
		{
			string[] mySplit1;
			string[] myDate1;
			string[] myTime1;
			string[] mySplit2;
			string[] myDate2;
			string[] myTime2;

			if((String.Compare(firstDateTime,"")==0)||(String.Compare(secondDateTime,"")==0))
			{
				return false;
			}
			string delim = " ";
			mySplit1 = firstDateTime.Split(delim.ToCharArray());
			mySplit2 = secondDateTime.Split(delim.ToCharArray());

			string date1 = mySplit1[0].ToString();
			string date2 = mySplit2[0].ToString();

			if((!this.DateIsValid(date1))||(!this.DateIsValid(date2)))
			{
				return false;
			}

            delim = "/";
			myDate1 = mySplit1[0].Split(delim.ToCharArray());
			myDate2 = mySplit2[0].Split(delim.ToCharArray());
			string myMonth1 = myDate1[0].ToString();
			string myDay1 = myDate1[1].ToString();
			string myYear1 = myDate1[2].ToString();
			string myMonth2 = myDate2[0].ToString();
			string myDay2 = myDate2[1].ToString();
			string myYear2 = myDate2[2].ToString();

			if(Convert.ToInt32(myYear1) > Convert.ToInt32(myYear2))
			{
				return true;
			}
			if(Convert.ToInt32(myYear1) < Convert.ToInt32(myYear2))
			{
				return false;
			}

			if(Convert.ToInt32(myMonth1) > Convert.ToInt32(myMonth2))
			{
				return true;
			}
			if(Convert.ToInt32(myMonth1) < Convert.ToInt32(myMonth2))
			{
				return false;
			}
			if(Convert.ToInt32(myDay1) > Convert.ToInt32(myDay2))
			{
				return true;
			}
			if(Convert.ToInt32(myDay1) < Convert.ToInt32(myDay2))
			{
				return false;
			}

			string time1 = "00:00:00";
			string time2 = "00:00:00";

			if(mySplit1.Length > 1)
			{
				time1 = mySplit1[1].ToString();
			}

			if(mySplit2.Length > 1)
			{
				time2 = mySplit2[1].ToString();
			}

			if((!this.TimeIsValid(time1))||(!this.TimeIsValid(time2)))
			{
				return false;
			}

			delim = ":";
			myTime1 = time1.Split(delim.ToCharArray());
			myTime2 = time2.Split(delim.ToCharArray());
			string myHour1 = myTime1[0].ToString();
			string myMinute1 = myTime1[1].ToString();
			string mySecond1 = myTime1[2].ToString();
			string myHour2 = myTime2[0].ToString();
			string myMinute2 = myTime2[1].ToString();
			string mySecond2 = myTime2[2].ToString();

			if(Convert.ToInt32(myHour1) > Convert.ToInt32(myHour2))
			{
				return true;
			}
			if(Convert.ToInt32(myHour1) < Convert.ToInt32(myHour2))
			{
				return false;
			}

			if(Convert.ToInt32(myMinute1) > Convert.ToInt32(myMinute2))
			{
				return true;
			}
			if(Convert.ToInt32(myMinute1) < Convert.ToInt32(myMinute2))
			{
				return false;
			}
			if(Convert.ToInt32(mySecond1) > Convert.ToInt32(mySecond2))
			{
				return true;
			}

			return true;
		}

        public string ReturnDateTimeMinusYear(string thisDateTime)
        {
            string[] mySplit;
            string[] myDate;
            string myTime;

            if (String.Compare(thisDateTime, "") == 0)
            {
                return "";
            }
            string delim = " ";
            mySplit = thisDateTime.Split(delim.ToCharArray());

            string date1 = mySplit[0].ToString();
            myTime = mySplit[1].ToString();

            if (!this.DateIsValid(date1))
            {
                return "";
            }

            delim = "/";
            myDate = date1.Split(delim.ToCharArray());
            string myMonth = myDate[0].ToString();
            string myDay = myDate[1].ToString();
            string myYear = myDate[2].ToString();

            int thisYear = Convert.ToInt32(myYear);

            if (thisYear == 1777)
            {
                return "";
            }

            int newYear = thisYear - 1;
            string newDate = myMonth + "/" + myDay + "/" + newYear;
            string newDateTime = "";
            if (DateIsValid(newDate))
            {
                newDateTime = newDate + " " + myTime;
                return newDateTime;
            }
            else
            {
                int thisMonth = Convert.ToInt32(myMonth);
                int thisDay = Convert.ToInt32(myDay);
                if ((thisMonth == 2) && (thisDay == 29))
                {
                    newDate = "2/28/" + newYear;
                }

                newDateTime = newDate + " " + myTime;
                return newDateTime;
            }

        }

		public string ReturnYearFromDateTime(string thisDateTime)
		{
			string[] mySplit;
			string[] myDate;
			string myTime;

			if(String.Compare(thisDateTime,"")==0)
			{
				return "";
			}
			string delim = " ";
			mySplit = thisDateTime.Split(delim.ToCharArray());

			string date1 = mySplit[0].ToString();
			myTime = mySplit[1].ToString();

			if(!this.DateIsValid(date1))
			{
				return "";
			}

			delim = "/";
			myDate = date1.Split(delim.ToCharArray());
			string myMonth = myDate[0].ToString();
			string myDay = myDate[1].ToString();
			string myYear = myDate[2].ToString();

            return myYear;
		}


		/// <summary>
		///  Returns the number of days in a given month for a given year
		/// </summary>
		/// <param name="month">integer between 1 and 12</param>
		/// <param name="year">integer between 1777 and 9999</param>
		/// <returns></returns>
        public int ReturnDaysInMonth(int month, int year)
        {
            if ((month < 1) || (month > 12))
            {
                return 0;
            }

            if ((year < 1777) || (year > 9999))
            {
                return 0;
            }

            int[] monthDays = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            int numberDays = 0;

            if ((year % 4 == 0) && (year % 100 != 0) || (year % 400 == 0))
            {
                numberDays = 29;
            }
            else
            {
                numberDays = monthDays[month - 1];
            }

            return numberDays;
        }



		public bool SearchValueInString(string searchValue, string searchString)
		{
			bool answer = false;

			if(String.Compare(searchString,"") == 1)
			{
				if(Regex.IsMatch(searchString,searchValue,RegexOptions.IgnoreCase))
				{
					answer = true;
				}
			}
			return answer;
		}
    }
}
