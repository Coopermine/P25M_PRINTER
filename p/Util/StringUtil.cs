using System;
using Java.Lang;

namespace p
{
	public class StringUtil
	{
		public static int LCD_WIDTH = 16;
		public static string specialSaveChars = "=: \t\r\n\f#!";

		/** A table of hex digits */
		public static char[] hexDigit = new char[] { '0', '1', '2', '3', '4', '5', '6',
			'7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'
		};

		/**
   * Convert a nibble to a hex character
   * 
   * @param nibble
   *          the nibble to convert.
   */
		public static char toHexChar (int nibble)
		{
			return hexDigit [(nibble & 0xF)];
		}

		public static int getSaveConvertLength (string theString)
		{
			return saveConvert (theString, null, 0, true, true, true);
		}

		public static int saveConvert (string theString, byte[] dst, int offset)
		{
			return saveConvert (theString, dst, offset, true, true, false);
		}
		/*
   * Converts unicodes to encoded &#92;uxxxx and writes out any of the
   * characters in specialSaveChars with a preceding slash
   *
   * @param theString
   *            the string needing convert. 
   * @param dst
   *            Save of the result 
   * @param offset
   *            the offset of result 
   * @param escapeSpace
   *            if <code>true</code>, escape Space 
   * @param lengthFlag
   *            Whether add one byte of length in the result. 
   *            <code>true</code> add one byte of length in the result
   * @param getLengthFlag
   *            Calculate the length of result, if <code>true</code>, thestring length that return. 
   * @return  if getLengthFlag = false, return offset of the result.
   *          if getLengthFlag = true, the length of the sequence of characters represented by this
   *          object.
   */
		public static int saveConvert (string theString, byte[] dst, int offset, bool escapeSpace, bool lengthFlag, bool getLengthFlag)
		{
			if (false == getLengthFlag
			    && (null == dst || dst.Length < (offset + (lengthFlag ? 1 : 0))
			    || dst.Length < 1 || offset < 0))
				return -1;
			if (null == theString)
				theString = "";
			int length = theString.Length;

			StringBuffer outBuffer = new StringBuffer (length * 2);

			for (int x = 0; x < length; x++) {
				char aChar = theString [x];
				switch (aChar) {
				case ' ':
					if (x == 0 || escapeSpace)
						outBuffer.Append ('\\');

					outBuffer.Append (' ');
					break;
				case '\\':
					outBuffer.Append ('\\');
					break;
				case '\t':
					outBuffer.Append ('\\');
					outBuffer.Append ('t');
					break;
				case '\n':
					outBuffer.Append ('\\');
					outBuffer.Append ('n');
					break;
				case '\r':
					outBuffer.Append ('\\');
					outBuffer.Append ('r');
					break;
				case '\f':
					outBuffer.Append ('\\');
					outBuffer.Append ('f');
					break;
				default:
					if ((aChar < 0x0020) || (aChar > 0x007e)) {
						outBuffer.Append ('\\');
						outBuffer.Append ('u');
						outBuffer.Append (toHexChar ((aChar >> 12) & 0xF));
						outBuffer.Append (toHexChar ((aChar >> 8) & 0xF));
						outBuffer.Append (toHexChar ((aChar >> 4) & 0xF));
						outBuffer.Append (toHexChar (aChar & 0xF));
					} else {
						if (specialSaveChars.IndexOf (aChar) != -1)
							outBuffer.Append ('\\');
						outBuffer.Append (aChar);
					}
					break;
				}
			}
			length = outBuffer.Length ();
			if (length > 255)
				length = 255;
			if (!getLengthFlag) {
				if (dst.Length >= offset + length + (lengthFlag ? 1 : 0)) {
					if (lengthFlag) {
						dst [offset] = (byte)(length & 0xFF);
						offset++;
					}
					for (int i = 0; i < length; i++) {
						dst [offset] = (byte)outBuffer.CharAt (i);
						offset++;
					}
					length = offset;
				} else {
					length = -1;
				}
			} else {
				if (lengthFlag)
					length = length + 1;
			}

			outBuffer = null;

			return length;
		}


		/*
   * Converts encoded &#92;uxxxx to unicode chars and changes special saved
   * chars to their original forms
   *
   * @param s
   *            the  byte arrary needing convert. 
   * @param offset
   *            the offset of byte arrary 
   * @param lengthFlag
   *            Whether add one byte of length in the result. 
   *            <code>true</code> add one byte of length in the result
   * @return  the convert result of the byte arrary. 
   */
		public static string loadConvert (byte[] s, int offset, bool lengthFlag)
		{
			if (null == s || (offset + (lengthFlag ? 1 : 0)) > s.Length)
				throw new IllegalArgumentException ("invalid byte arrary");

			char aChar;
			int len = (s.Length - offset);

			if (lengthFlag) {
				len = s [offset] & 0xFF;
				offset++;
			}

			StringBuffer outBuffer = new StringBuffer (len);

			for (int x = offset; x < (offset + len);) {
				aChar = (char)s [x++];
				if (aChar == '\\') {
					aChar = (char)s [x++];
					if (aChar == 'u') {
						// Read the xxxx
						int value = 0;
						for (int i = 0; i < 4; i++) {
							aChar = (char)s [x++];
							switch (aChar) {
							case '0':
							case '1':
							case '2':
							case '3':
							case '4':
							case '5':
							case '6':
							case '7':
							case '8':
							case '9':
								value = (value << 4) + aChar - '0';
								break;
							case 'a':
							case 'b':
							case 'c':
							case 'd':
							case 'e':
							case 'f':
								value = (value << 4) + 10 + aChar - 'a';
								break;
							case 'A':
							case 'B':
							case 'C':
							case 'D':
							case 'E':
							case 'F':
								value = (value << 4) + 10 + aChar - 'A';
								break;
							default:
								throw new IllegalArgumentException ("Malformed \\uxxxx encoding.");
							}
						}
						outBuffer.Append ((char)value);
					} else {
						if (aChar == 't')
							aChar = '\t';
						else if (aChar == 'r')
							aChar = '\r';
						else if (aChar == 'n')
							aChar = '\n';
						else if (aChar == 'f')
							aChar = '\f';
						outBuffer.Append (aChar);
					}
				} else
					outBuffer.Append (aChar);
			}
			return outBuffer.ToString ();
		}

		public static string returnstring (string str)
		{
			if (null == str)
				return "";
			else
				return str;
		}

		public static string returnstring (int intValue)
		{
			if (intValue < 0)
				return "";
			else
				return "" + intValue;
		}

		public static string returnstring (short shortValue)
		{
			return returnstring ((int)shortValue);
		}

		public static string returnstring (byte byteValue)
		{
			return returnstring ((int)byteValue);
		}

		/**
  * Method trim space
  *
  * @param The string to be format.
  *
  */
		public static string trimSpace (string oldString)
		{
			if (null == oldString)
				return null;
			if (0 == oldString.Length)
				return "";

			StringBuffer sbuf = new StringBuffer ();
			int oldLen = oldString.Length;
			for (int i = 0; i < oldLen; i++) {
				if (' ' != oldString [i])
					sbuf.Append (oldString [i]);
			}     
			string returnString = sbuf.ToString ();
			sbuf = null;
			return returnString;
		}

		/**
  * Method convert byte[] to String
  *
  * @param The string to be format.
  *
  */
		public static string tostring (byte[] buffer)
		{
			if (null == buffer)
				return null;
			else
				return System.Text.Encoding.Default.GetString (buffer);
		}

		/**
   * Format buffer into the designated width and height, for example: 
   * bufferstring = "123456789012345678901234567890"
   * width = 16 , height = 0
   * String[] = {
   * {"1234567890123456"},
   * {"78901234567890"},
   * }
   *
   * @param The string to be format.
   *
   */
		public static string[] buffer2Message (string bufferString, int width, int height)
		{
			int buffLen;
			int i = 0;
			int h, w;
			if (null == bufferString)
				buffLen = 0;
			else
				buffLen = bufferString.Length;

			if (height < 1 && width > 0) {
				if (0 == (buffLen % width))
					h = buffLen / width;
				else
					h = (buffLen / width) + 1;
				w = width;
			} else {
				if (height > 0 && width < 1) {
					if (0 == (buffLen % height))
						w = buffLen / height;
					else
						w = (buffLen / height) + 1;
					h = height;
				} else {
					if (height > 0 && width > 0) {
						h = height;
						w = width;
					} else {
						return null;
					}
				}
			}

			string[] buff = new string[h];

			for (i = 0; i < h; i++) {
				if ((w * (i + 1)) < buffLen)
					buff [i] = bufferString.Substring (w * i, w * (i + 1));
				else if ((w * (i + 1)) >= buffLen && (w * i) < buffLen)
					buff [i] = bufferString.Substring (w * i, buffLen);
				else
					buff [i] = "";
			}

			return buff;
		}

		/**
  * Method Format string
  *
  * @param The string to be format.
  *
  */
		public static string[] buffer2Message (string bufferString)
		{
			return buffer2Message (bufferString, LCD_WIDTH, 3);
		}

		/**
  * Method fill string
  *
  * @param The string to be format.
  *
  */
		public static string fillstring (string formatString, int length, char fillChar, bool leftFillFlag)
		{
			if (null == formatString) {
				formatString = "";
			}
			int strLen = formatString.Length;
			if (strLen >= length) {
				if (true == leftFillFlag)  // left fill 
					return formatString.Substring (strLen - length, strLen);
				else
					return formatString.Substring (0, length);
			} else {
				StringBuffer sbuf = new StringBuffer ();
				int fillLen = length - formatString.Length;
				for (int i = 0; i < fillLen; i++) { 
					sbuf.Append (fillChar);
				}

				if (true == leftFillFlag) {  // left fill 
					sbuf.Append (formatString);
				} else {
					sbuf.Insert (0, formatString);
				}
				string returnstring = sbuf.ToString ();
				sbuf = null;
				return returnstring;
			}
		}

		/**
  * Method fill string
  *
  * @param The string to be format.
  *
  */
		public static string fillSpace (string formatString, int length)
		{
			return fillstring (formatString, length, ' ', false);
		}

		/**
  * Method Format string
  *
  * @param The string to be format.
  *
  */
		public static string formatLine (string formatString, bool leftFillFlag)
		{
			return fillstring (formatString, LCD_WIDTH, ' ', leftFillFlag);
		}


		private static char[] space8 = new char[]{ ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };

		/**
  * Method fill space , converted string length to LCD_WIDTH
  *
  * @param The string to be format.
  *
  */
		public static string fillShowSpace (string formatString)
		{
			if (null == formatString)
				return "";

			if (formatString.Length <= LCD_WIDTH) {
				int len = 8 - (formatString.Length / 2);
				StringBuffer sbuf = new StringBuffer ();
				sbuf.Append (space8, 0, len);
				sbuf.Append (formatString);
				sbuf.Append (space8, 0, len);
				sbuf.SetLength (LCD_WIDTH);

				string returnstring = sbuf.ToString ();
				sbuf = null;
				return returnstring;
			} else {
				return formatString.Substring (0, LCD_WIDTH);
			}
		}

		/**
  * Method Format string
  *
  * @param The string to be format.
  *
  */
		public static string fillZero (string formatString, int length)
		{
			return fillstring (formatString, length, '0', true);
		}

		/**
       * @param s source string (with Hex representation)
       * @return byte array
       */
		public static byte[] hexStringToBytes (string s)
		{
			if (null == s)
				return null;

			return hexStringToBytes (s, 0, s.Length);
		}

		/**
   * @param   hexstring   source string (with Hex representation)
   * @param   offset      starting offset
   * @param   count       the length
   * @return  byte array
   */
		public static byte[] hexStringToBytes (string hexString, int offset, int count)
		{
			if (null == hexString || offset < 0 || count < 2 || (offset + count) > hexString.Length)
				return null;

			byte[] buffer = new byte[count >> 1];
			int stringLength = offset + count;
			int byteIndex = 0;
			for (int i = offset; i < stringLength; i++) {
				char ch = hexString [i];
				if (ch == ' ')
					continue;
				byte hex = isHexChar (ch);
				if (hex < 0)
					return null;
				int shift = (byteIndex % 2 == 1) ? 0 : 4;


				buffer [byteIndex >> 1] |= Convert.ToByte ((hex << shift));
				byteIndex++;
			}
			byteIndex = byteIndex >> 1;
			if (byteIndex > 0) {
				if (byteIndex < buffer.Length) {
					byte[] newBuff = new byte[byteIndex];
					Array.Copy (buffer, 0, newBuff, 0, byteIndex);
					buffer = null;
					return newBuff;
				}
			} else {
				buffer = null;
			}
			return buffer;
		}

		private static void AppendHex (StringBuffer stringbuffer, byte byte0)
		{
			stringbuffer.Append (toHexChar (byte0 >> 4));
			stringbuffer.Append (toHexChar (byte0));
		}

		public static string toHexstring (byte[] abyte0, int beginIndex, int endIndex, bool spaceFlag)
		{
			if (null == abyte0)
				return null;
			if (0 == abyte0.Length)
				return "";
			StringBuffer sbuf = new StringBuffer ();
			AppendHex (sbuf, abyte0 [beginIndex]);
			for (int i = (beginIndex + 1); i < endIndex; i++) {
				if (spaceFlag)
					sbuf.Append (' ');
				AppendHex (sbuf, abyte0 [i]);
			}
			string returnstring = sbuf.ToString ();
			sbuf = null;
			return returnstring;
		}

		public static string toHexstring (byte[] abyte0, int beginIndex, int endIndex)
		{
			if (null == abyte0)
				return null;
			return toHexstring (abyte0, beginIndex, endIndex, true);
		}

		public static string toHexstring (byte[] abyte0, bool spaceFlag)
		{
			if (null == abyte0)
				return null;
			return toHexstring (abyte0, 0, abyte0.Length, spaceFlag);
		}

		/**
  * Method convert byte[] to HexString
  *
  * @param The string to be format.
  *
  */
		public static string toHexstring (byte[] abyte0)
		{
			if (null == abyte0)
				return null;
			return toHexstring (abyte0, 0, abyte0.Length, true);
		}

		public static string toHexstring (char achar0)
		{
			return toHexstring ((byte)achar0);
		}

		public static string toHexstring (byte abyte0)
		{
			StringBuffer sbuf = new StringBuffer ();
			AppendHex (sbuf, abyte0);

			string returnstring = sbuf.ToString ();
			sbuf = null;
			return returnstring;
		}

		/**
   * Return true if the string is HexChars(1234567890abcdefABCDEF).
   *
   */  
		public static byte isHexChar (char ch)
		{   
			if ('a' <= ch && ch <= 'f')
				return (byte)(ch - 'a' + 10);
			if ('A' <= ch && ch <= 'F')
				return (byte)(ch - 'A' + 10);
			if ('0' <= ch && ch <= '9')
				return (byte)(ch - '0');

			return Convert.ToByte (-1);
		}

		/**
  * Method Check string 
  *
  * @param The string to be format.
  * 
  * @param checkSpaceFlag=false: skip the space.
  *
  */  
		public static bool isHexChar (string hexString, bool checkSpaceFlag)
		{
			if (null == hexString || 0 == hexString.Length)
				return false;

			int hexLen = hexString.Length;
			int hexCharCount = 0;
			char ch;
			for (int i = 0; i < hexLen; i++) {
				ch = hexString [i];
				if (ch == ' ') {
					if (checkSpaceFlag)
						return false;
				} else {
					if (isHexChar (ch) < 0)
						return false;
					else
						hexCharCount++;
				}
			}

			if (hexCharCount % 2 != 0)
				return false;

			return true;
		}

		/**
   * Method Check string 
   *
   * @param The string to be format.
   *
   */  
		public bool isHexChar (string hexString)
		{
			return isHexChar (hexString, true);
		}

		/*
    Return true if thc statiring is alphanum.
    <code>{letter digit }</code>
   */
		public bool isLetterNumeric (string s)
		{
			int i = 0;
			int len = s.Length;
			while (i < len && (Character.IsLowerCase (s [i]) ||
			       Character.IsUpperCase (s [i])) ||
			       Character.IsDigit (s [i])) {
				i++;
			}
			return (i >= len);
		}

		public static string byteTostring (byte[] str)
		{
			StringBuilder sb = new StringBuilder ();

			for (int i = 0; i < str.Length; i++) {
				sb.Append (StringUtil.toHexstring (str [i]));
				sb.Append (" ");
			}

			return sb.ToString ();
		}
	}
}

