using System;
using Java.Lang;
using System.Collections;


namespace p
{
	public class Tracer
	{

		public static char[] hexDigit = { '0', '1', '2', '3', '4', '5', '6',
			'7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

		/**
   * Convert a nibble to a hex character
   *
   * @param nibble
   *          the nibble to convert.
   */
		public static char toHexChar(int nibble)
		{
			return hexDigit[(nibble & 0xF)];
		}

		public static string dump(byte[] abyte0)
		{
			byte[] cont = abyte0;
			int totalcont;

			if(abyte0 == null || cont.Length == 0)
				return "";
			else
				totalcont = cont.Length;
			return dump(cont, 0, totalcont , true);
		}
		public static string dump(byte[] abyte0, int beginIndex, int endIndex, bool spaceFlag)
		{
			
			return dump(abyte0, beginIndex, endIndex, spaceFlag, true, true, 0);
		}
		public static string dump(byte[] abyte0, int beginIndex, int endIndex, bool spaceFlag, bool asciiFlag, bool lineNumberFlag, int linenumber)
		{
			byte[] cont = abyte0;
			if(abyte0 == null || cont.Length  == 0)
				return "";

			string outMsg = "";
			int totalLine = (endIndex - beginIndex) / 16;
			int lineNumber, q;
			int offset = beginIndex;
			byte byte0;
			StringBuffer stringbuffer = new StringBuffer(6 + (spaceFlag?48:32));
			StringBuffer asciibuffer = new StringBuffer();
			string printString;
			int stringcount;
			int asccicount;

			if (linenumber < 0)
				linenumber = 0;
			else
				linenumber = linenumber % 10000;

			for(int i = 0; i <= totalLine; i++, linenumber++)
			{
				if (offset < endIndex) {
					stringcount = stringbuffer.Length(); 
					asccicount = asciibuffer.Length(); 
					stringbuffer.Delete(0, stringcount);
					asciibuffer.Delete(0, asccicount);
					if (lineNumberFlag) {
						stringbuffer.Append("0000: ");
						lineNumber = linenumber;
						for(byte0 = 3; byte0 >=0; byte0--){
							q = (lineNumber * 52429) >> (16+3);
							stringbuffer.SetCharAt(byte0, toHexChar(lineNumber - ((q << 3) + (q << 1)))); // toHexChar(lineNumber-(q*10))
							lineNumber = q;
							if (0 == lineNumber) break;
						}
					}
					for(int j = 0; j < 16; j++, offset++)
					{
						if (offset < endIndex) {
							byte0 = abyte0[offset];
							stringbuffer.Append(toHexChar(byte0 >> 4));
							stringbuffer.Append(toHexChar(byte0));
							if (spaceFlag)
								stringbuffer.Append(' ');
							if (asciiFlag) {
								if (byte0 >= 0x20 && byte0 <= 0x7E)
									asciibuffer.Append((char)byte0);
								else
									asciibuffer.Append('.');
							}
						} else {
							stringbuffer.Append(' ');
							stringbuffer.Append(' ');
							if (spaceFlag)
								stringbuffer.Append(' ');
						}
					}
					if (asciiFlag)
						printString = stringbuffer.ToString() + "; " + asciibuffer.ToString();
					else
						printString = stringbuffer.ToString();
					//        printLine(printString);
					outMsg =  outMsg + printString + "\n";
				} else {
					break;
				}
			}
			printString = null;
			stringbuffer = asciibuffer = null;
			return outMsg;
		}



	}
}

