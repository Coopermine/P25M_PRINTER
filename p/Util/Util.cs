using System;
using Java.Lang;
using System.Collections;

namespace p
{
	public class Util
	{

		/**
	 * Returns the current HH:MM
	 * 
	 * @param time - time string
	 * @return format time string
	 */
		public static string getHHMM(string time)
		{
			string finalTime = "00/00 AM/PM";
			string hh = time.Substring(0, 2);
			string mm = time.Substring(3, 5);
			int newHH = Integer.ParseInt(hh);
			int newMM = Integer.ParseInt(mm);

			newMM = newMM % 60;

			if (newHH == 0)
			{
				finalTime = "12:" + newMM + " PM";
			} else
			{

				if (newHH > 12)
				{
					newHH = newHH % 12;
					finalTime = newHH + ":" + newMM + " PM";
				} else
					finalTime = newHH + ":" + newMM + " AM";
			}

			string HH = finalTime.Substring(0, finalTime.IndexOf(":"));
			string MM = finalTime.Substring(finalTime.IndexOf(":") + 1, finalTime
				.IndexOf(" "));
			string AMPM = finalTime.Substring(finalTime.IndexOf(" "), finalTime
				.Length);

			if (MM.Length == 1)
				MM = "0" + MM;

			finalTime = HH + ":" + MM /*+ " " */+ AMPM;

			return (finalTime);
		}

		/**
	 * This method returns the numeric value in string so that further it can be
	 * concanated with other value strings like day,hours,min,seconds at the
	 * time of getting eraseTime at savingTransaction.
	 * 
	 * @param month -
	 *            String values like "Jan","Feb"....etc.
	 * @return String
	 */
		private static string[] monthNames = new string[12]{"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct","Nov", "Dec" };

		public static string getMonthDigit(string month)
		{
			if("123456789101112".IndexOf(month)!=-1)
			{
				return month;
			}
			string mon = "-1";
			for (int i = 0; i < 12; i++)
			{
			//	if (EqualsIgnoreCase(month, monthNames[i]))
				if(string.Equals(month, monthNames[i], StringComparison.CurrentCultureIgnoreCase))
				{
					mon = "" + (i + 1);
					break;
				}
			}
			return mon;
		}

		public static string getMonthName(int index)
		{
			if(index >= 1 && index <= 12)
			{
				return monthNames[index-1];
			}
			return "";
		}


		public static bool equalsIgnoreCase(string str1, string str2)

		{
			if(str1 == null && str2 == null)
			{
				return true;
			}
			if((str1 == null && str2 != null)
				|| (str1 != null && str2 == null))
			{
				return false;
			}
		


			if(str1.Equals(str2))
			{
				return true;
			}
			else
			{
				return false;
			}
		}



		public static string trimSpace(string oldString)
		{
			if (null == oldString)
			{
				return null;
			}
			if (0 == oldString.Length)
			{
				return "";
			}
			StringBuilder sbuf = new StringBuilder();
			int oldLen = oldString.Length;
			for (int i = 0; i < oldLen; i++)
			{
				if (' ' != oldString[i])
				{
					sbuf.Append(oldString[i]);
				}
			}
			string returnString = sbuf.ToString();
			sbuf = null;
			return returnString;
		}


		/**
	 * Convert hex string to byte array
	 * 
	 * @param s - input String
	 * @param offset - start position
	 * @param len - byte length
	 * @return byte array
	 */
		public static byte[] hex2byte(string s, int offset, int len)
		{
			byte[] d = new byte[len];
			int byteLen = len * 2;
			for (int i = 0; i < byteLen; i++)
			{
				int shift = (i % 2 == 1) ? 0 : 4;

				d[i >> 1] |= Convert.ToByte(Character.Digit(s[offset + i], 16) << shift);
			}
			return d;
		}

		/**
	 * Convert hex string to byte array
	 * 
	 * @param s - input String
	 * @return byte array
	 */
		public static byte[] hexString2bytes(string s)
		{
			if (null == s)
				return null;
			s = trimSpace(s);
			return hex2byte(s, 0, s.Length >> 1);
		}









		/**
	 * Seperate String with str token
	 * 
	 * @param str - the string which will be cut
	 * @return cut string array
	 */
		// @SuppressWarnings("unused")
		private static string[] seperateStr(string str)
		{
			bool doubleSpace = false;
			int wordCount = 0;
			StringBuffer sb = new StringBuffer();
			if (str == null || str.Length == 0)
			{
				return null;
			}
			for (int j = 0; j < str.Length; j++)
			{
				if (str[j] == ' ')
				{
					if (!doubleSpace)
						wordCount++;

					doubleSpace = true;
					continue;
				}
				doubleSpace = false;
			}
			string[] st = new string[wordCount + 1];

			int i = 0;

			doubleSpace = false;
			string ch = "";
			for (int j = 0; j < str.Length; j++)
			{
				if (str[j] == ' ')
				{
					if (!doubleSpace)
					{
						st[i] = sb.ToString();
						sb.Delete(0, sb.Length());
						i++;
					}
					doubleSpace = true;
					continue;
				} else
				{
					sb.Append(str[j]);
				}
				doubleSpace = false;
			}

			st[i] = sb.ToString();
			;
			return st;
		}

		/**
	 * Fit the original string to the page
	 * 
	 * @param matter - input data
	 * @param lineSize - display width
	 * @param isBeginWithSpace - true if begin with space- 
	 * @return display string
	 */
		public static string fitToThePage(string matter,int lineSize,bool isBeginWithSpace)  
		{
			if(matter.Equals(""))
				return ""; 

			string space=" ";
			string bSpace="";
			if(isBeginWithSpace)
				bSpace =" ";

			bool doubleSpace = false;

			int j=0;       
			int word=1;    

			// This loop will find that how much words are present in the string
			for(j=0;j<matter.Length;j++)
			{
				if(matter[j]==' ')
				{
					if(!doubleSpace)
						word++;

					doubleSpace = true;
					continue;
				}
				doubleSpace = false;
			}
			string[] st = new string[word];
			string ch ="";
			int i=0;  

			doubleSpace = false;
			//This loop will store words in the String array st[]
			for(j=0;j<matter.Length;j++)
			{
				if(matter[j]==' ')
				{
					if(!doubleSpace)
					{
						st[i] = ch;
						ch="";   
						i++;     
					}
					doubleSpace = true;
					continue;
				}
				else
				{
					ch = ch + matter[j];
				}
				doubleSpace = false;
			}
			st[i]=ch;

			ch = "";
			string newString="";
		//	@SuppressWarnings("unused")
			int len = st.Length;

			for(i=0 ; i<word ; i++)
			{
				ch = ch + " "+ st[i];

				if(!isBeginWithSpace)
					ch = ch.Trim();
				if( ch.Length > lineSize )
				{
					newString = newString +"\n" +bSpace + st[i];
					ch = "";
					ch = bSpace + st[i];
				}else
				{
					newString = newString + space + st[i];
					if(!isBeginWithSpace)
						newString = newString.Trim();
				}
			}
			return newString;
		}




		/** 
	 * This method splits date in the String array and return it .
	 * @param date - date in the string format
	 * @return String[]
	 */
		public static string[] filterDate(string date)
		{
			return seperateStr(date);
		}

		/**
	 * This method align two string left & right respactively in given char
	 * size.
	 * 
	 * @param param1
	 *            string-1
	 * @param param2
	 *            string-2
	 * @param cpl
	 *            number of characters
	 * @return formated Strings
	 */
		public static string nameLeftValueRightJustify(string param1, string param2,
			int cpl) {
			if(param1 == null)
				param1 = "";
			if(param2 == null)
				param2 = "";
			int len = param1.Length;
			return param1.Trim()+rightJustify(param2, (cpl - len));
		}

		/**
	 * Right align to the string in given number of chars
	 * 
	 * @param item
	 *            String to be aligned
	 * @param digits
	 *            number of characters
	 * @return formated strings
	 */
		public static string rightJustify(string item, int digits) {
			StringBuffer buf = null;
			if(digits < 0)
			{
				buf = new StringBuffer();
			}
			else
			{
				buf = new StringBuffer(digits);
			}
			for (int i = 0; i < digits - item.Length; i++) {
				buf.Append(" ");
			}
			buf.Append(item);
			return buf.ToString();
		}
		/**
	 * this method gives only last 4 digit of card number preceding with *.
	 * 
	 * @param ccNum
	 *            card number
	 * @return string
	 */
		public static string getAcountAsterixData(string ccNum) 
		{
			if(ccNum.Length < 4)
				return ccNum;
			int len = ccNum.Length;
			string temp = "";
			for (int i = 0; i < (len - 4); i++) {
				temp = temp + "*";
			}
			return (temp + ccNum.Substring((len - 4), (len)));
		}


		/**
	 * Center allign the string in specified digits
	 * 
	 * @param item
	 *            String to be allign
	 * @param digits
	 *            number of characters
	 * @return formated string
	 */
		public static string center(string item, int digits) {
			StringBuffer buf = null;
			if(digits < 0)
			{
				buf = new StringBuffer();
			}
			else
			{
				buf = new StringBuffer(digits);
			}
			int len = item.Length;
			for (int i = 0; i < (digits - len) / 2; i++) {
				buf.Append(" ");
			}
			buf.Append(item);
			for (int i = 0; i < (digits - len) / 2; i++) {
				buf.Append(" ");
			}
			return buf.ToString();
		}

		// @SuppressWarnings({ "rawtypes", "unchecked" })
		public static ArrayList processWrappedText(string text, int width)
		{
			ArrayList v = new ArrayList();
			if (text == null)
			{
				return v;
			}
			int cursor = 0;


			// needed to be modified  (xsx) 
			while (cursor < text.Length)
			{
				int remainderLength = text.Length - cursor;
				int increment = 1;
				while ((increment < remainderLength) && (increment <= width))
				{
					increment++;
				}

				string subString = text.Substring(cursor, increment);
				if ((subString[increment - 1] == ' ') || (cursor + increment == text.Length) || (text[cursor + increment] == ' '))
				{
					// no need to find last space
				}
				else
				{
					// need to backtrack to last space
					int lastSpaceIndex = subString.LastIndexOf(' ');
					if (lastSpaceIndex > 0)
					{
						increment = lastSpaceIndex;
					}
				}

				v.Add(text.Substring(cursor, increment).Trim());

				cursor += increment;
			}

			return v;
		}
		public static int checkDeviceType(string deviceName)
		{
			if(deviceName == null || deviceName.Length == 0)
			{
					return DataConstants.DEVICE_NONE;
			}
			if(deviceName.IndexOf("P25") != -1)
			{
				

				return DataConstants.DEVICE_P25M;
			}
			else if (deviceName.IndexOf("H50") != -1)
			{
				return DataConstants.DEVICE_H50;
			}
			else //other devices maybe PC, we will think them as DEVICE_P25M for debugging
			{
				return DataConstants.DEVICE_P25M;
			}
		}

	}
}

