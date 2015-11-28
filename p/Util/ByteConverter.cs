﻿using System;
using Java.Lang;
using Java;

namespace p
{
	public class ByteConvert
	{
	 static System.String lineSeperate = "\r\n";

		 public static int DEFAULT_BUFFER_LENGTH = 1024 * 3;

		 public static int DEFAULT_TABLE_LENGTH = 256;

		/**** convert table for bcd to hex **************/
		private static string[] convertTable;

		/**
	 *  Innialize convertTable
	 */

		/**
	 * convert two bytes to int, in fact return int for represent unsigned short.
	 * 
	 * @param convertByte-byte stream
	 * @return int number
	 */
		public static int byte2int2(byte[] convertByte)
		{
			return byte2int2(convertByte, 0, true);
		}

		/**
	 * convert two bytes to int, in fact return int for represent unsigned short.
	 * 
	 * @param convertByte - byte stream
	 * @param isBigIndian - if true, bigIndian, if false, littleIndian
	 * @return int number
	 */
		public static int byte2int2(byte[] convertByte, bool isBigIndian)
		{
			return byte2int2(convertByte, 0, isBigIndian);
		}

		/**
	 * convert two byte to int, in fact return int for represent unsigned short.
	 * 
	 * @param convertByte - byte stream
	 * @param offset - offset of byte stream to be converted
	 * @return int number
	 */
		public static int byte2int2(byte[] convertByte, int offset)
		{
			return byte2int2(convertByte, offset, true);
		}

		/**
	 * convert two bytes to int, in fact return int for represent unsigned short.
	 * 
	 * @param convertByte - byte stream
	 * @param offset - offset of byte stream to be converted
	 * @param isBigIndian - if true, bigIndian, if false, littleIndian
	 * @return int number
	 */
		public static int byte2int2(byte[] convertByte, int offset,
			bool isBigIndian)
		{
			int value = 0;
			int byte0, byte1;

			byte0 = convertByte[0 + offset] < 0 ? convertByte[0 + offset] + 256
				: convertByte[0 + offset];
			byte1 = convertByte[1 + offset] < 0 ? convertByte[1 + offset] + 256
				: convertByte[1 + offset];

			if (!isBigIndian)
				value = byte1 * 256 + byte0;
			else
				value = byte0 * 256 + byte1;

			return value;
		}

		/**
	 * convert four bytes to int.
	 * 
	 * @param convertByte - byte stream
	 * @return int number
	 */
		public static int byte2int4(byte[] convertByte)
		{
			return byte2int4(convertByte, 0, true);
		}

		/**
	 * convert four bytes to int.
	 * 
	 * @param convertByte - byte stream
	 * @param isBigIndian - if true, bigIndian, if false, littleIndian
	 * @return int number
	 */
		public static int byte2int4(byte[] convertByte, bool isBigIndian)
		{
			return byte2int4(convertByte, 0, isBigIndian);
		}

		/**
	 * convert four bytes to int.
	 * 
	 * @param convertByte - byte stream
	 * @param offset - offset of byte stream to be converted
	 * @return int number
	 */
		public static int byte2int4(byte[] convertByte, int offset)
		{
			return byte2int4(convertByte, offset, true);
		}

		/**
	 * convert four bytes to int.
	 * 
	 * @param convertByte - byte stream
	 * @param offset - offset of byte stream to be converted
	 * @param isBigIndian - if true, bigIndian, if false, littleIndian
	 * @return int number
	 */
		public static int byte2int4(byte[] convertByte, int offset,
			bool bigIndian)
		{
			int value = 0;
			int byte0, byte1, byte2, byte3;

			byte0 = convertByte[0 + offset] < 0 ? convertByte[0 + offset] + 256
				: convertByte[0 + offset];
			byte1 = convertByte[1 + offset] < 0 ? convertByte[1 + offset] + 256
				: convertByte[1 + offset];
			byte2 = convertByte[2 + offset] < 0 ? convertByte[2 + offset] + 256
				: convertByte[2 + offset];
			byte3 = convertByte[3 + offset] < 0 ? convertByte[3 + offset] + 256
				: convertByte[3 + offset];
			if (!bigIndian)
				value = (byte3 << 24) + (byte2 << 16) + (byte1 << 8) + byte0;
			else
				value = (byte0 << 24) + (byte1 << 16) + (byte2 << 8) + byte3;
			return value;
		}

		/**
	 * convert short to two bytes.
	 * 
	 * @param value - int value represent unsigned short
	 * @return two bytes array
	 */
		public static byte[] int2byte2(int value)
		{
			return int2byte2(value, true);
		}

		/**
	 * convert short to two bytes.
	 * 
	 * @param value - int value represent unsigned short
	 * @param fillByte - byte stream to set
	 * @param isBigIndian - if true, bigIndian, if false, littleIndian
	 */
		public static byte[] int2byte2(int value, bool isBigIndian)
		{
			byte[] byteValue = new byte[2];
			if (!isBigIndian)
			{
				byteValue[0] = (byte) (value & 0xff);
				byteValue[1] = (byte) ((value & 0xff00) >> 8);
			} else
			{
				byteValue[1] = (byte) (value & 0xff);
				byteValue[0] = (byte) ((value & 0xff00) >> 8);
			}
			return byteValue;
		}

		/**
	 * convert short to two bytes.
	 * 
	 * @param value - int value represent unsigned short
	 * @param fillByte - byte stream to set
	 */
		public static void int2byte2(int value, byte[] fillByte)
		{
			int2byte2(value, fillByte, 0, true);
		}

		/**
	 * convert short to two bytes.
	 * 
	 * @param value - int value represent unsigned short
	 * @param fillByte - byte stream to set
	 * @param isBigIndian - if true, bigIndian, if false, littleIndian
	 */
		public static void int2byte2(int value, byte[] fillByte, bool isBigIndian)
		{
			int2byte2(value, fillByte, 0, isBigIndian);
		}

		/**
	 * convert short to two byte.
	 * 
	 * @param value - int value represent unsigned short
	 * @param fillByte - byte stream to set
	 * @param offset - at the offset of byte stream to set
	 */
		public static void int2byte2(int value, byte[] fillByte, int offset)
		{
			int2byte2(value, fillByte, offset, true);
		}

		/**
	 * convert short to two byte.
	 * 
	 * @param value - int value represent unsigned short
	 * @param fillByte -byte stream to set
	 * @param offset - at the offset of byte stream to set
	 * @param isBigIndian - if true, bigIndian, if false, littleIndian
	 */
		public static void int2byte2(int value, byte[] fillByte, int offset,
			bool isBigIndian)
		{
			if (!isBigIndian)
			{
				fillByte[0 + offset] = (byte) (value & 0xff);
				fillByte[1 + offset] = (byte) ((value & 0xff00) >> 8);
			} else
			{
				fillByte[1 + offset] = (byte) (value & 0xff);
				fillByte[0 + offset] = (byte) ((value & 0xff00) >> 8);
			}
		}

		/**
	 * convert int to four byte.
	 * 
	 * @param value - int value represent unsigned int
	 */
		public static byte[] int2byte4(int value)
		{
			return int2byte4(value, true);
		}

		/**
	 * convert int to four byte.
	 * 
	 * @param value - int value represent unsigned int
	 * @param isBigIndian - if true, bigIndian, if false, littleIndian
	 */
		public static byte[] int2byte4(int value, bool isBigIndian)
		{
			byte[] byteValue = new byte[4];
			if (!isBigIndian)
			{
				byteValue[0] = (byte) (value & 0xff);
				byteValue[1] = (byte) ((value & 0xff00) >> 8);
				byteValue[2] = (byte) ((value & 0xff0000) >> 16);
				byteValue[3] = (byte) ((value & 0xff000000) >> 24);
			} else
			{
				byteValue[3] = (byte) (value & 0xff);
				byteValue[2] = (byte) ((value & 0xff00) >> 8);
				byteValue[1] = (byte) ((value & 0xff0000) >> 16);
				byteValue[0] = (byte) ((value & 0xff000000) >> 24);
			}
			return byteValue;
		}

		/**
	 * convertint to four byte.
	 * 
	 * @param value - int value represent unsigned short
	 * @param fillByte - byte stream to set
	 */
		public static void int2byte4(int value, sbyte[] fillByte)
		{
			int2byte4(value, fillByte, 0, true);
		}

		/**
	 * convertint to four byte.
	 * 
	 * @param value - int value represent unsigned short
	 * @param fillByte - byte stream to set
	 * @param isBigIndian - if true, bigIndian, if false, littleIndian
	 */
		public static void int2byte4(int value, sbyte[] fillByte, bool isBigIndian)
		{
			int2byte4(value, fillByte, 0, isBigIndian);
		}

		/**
	 * convert int to four byte.
	 * 
	 * @param value - int value represent unsigned short
	 * @param fillByte - byte stream to set
	 * @param offset - at the offset of byte stream to set
	 */
		public static void int2byte4(int value, sbyte[] fillByte, int offset)
		{
			int2byte4(value, fillByte, offset, true);
		}

		/**
	 * convert int to four byte.
	 * 
	 * @param value - int value represent unsigned short
	 * @param fillByte - byte stream to set
	 * @param offset - at the offset of byte stream to set
	 * @param isBigIndian - if true, bigIndian, if false, littleIndian
	 */
		public static void int2byte4(int value, sbyte[] fillByte, int offset, bool isBigIndian)
		{
			if (!isBigIndian)
			{
				fillByte[0 + offset] = unchecked((sbyte)(value & 0xff));
				fillByte[1 + offset] = (sbyte)((int)((uint)(value & 0xff00) >> 8));
				fillByte[2 + offset] = (sbyte)((int)((uint)(value & 0xff0000) >> 16));
				fillByte[3 + offset] = (sbyte)((int)((uint)(value & 0xff000000) >> 24));
			}
			else
			{
				fillByte[3 + offset] = unchecked((sbyte)(value & 0xff));
				fillByte[2 + offset] = (sbyte)((int)((uint)(value & 0xff00) >> 8));
				fillByte[1 + offset] = (sbyte)((int)((uint)(value & 0xff0000) >> 16));
				fillByte[0 + offset] = (sbyte)((int)((uint)(value & 0xff000000) >> 24));
			}
		}

		/**
	 * convert byte array to Hex String.
	 * 
	 * @param info - the propmt String
	 * @param buf - the byte array which will be converted
	 * @return Hex String
	 */
		public static string buf2String(string info, byte[] buf)
		{
			return buf2String(info, buf, 0, buf.Length, true);
		}

		/**
	 * convert byte array to Hex String.
	 * 
	 * @param info - the propmt String
	 * @param buf - the byte array which will be converted
	 * @param isOneLine16 - if true 16 bytes one line, false all bytes in one line
	 * @return Hex String
	 */
		public static string buf2String(string info, byte[] buf, bool isOneLine16)
		{
			return buf2String(info, buf, 0, buf.Length, isOneLine16);
		}

		/**
	 * convert byte array to Hex String.
	 * 
	 * @param info - the propmt String
	 * @param buf - the byte array which will be converted
	 * @param offset - the start position of buf
	 * @param length - the converted length
	 * @return Hex String
	 */
		public static string buf2String(string info, byte[] buf, int offset,
			int length)
		{
			return buf2String(info, buf, offset, length, true);
		}

		/**
	 * convert byte array to Hex String.
	 * 
	 * @param info - the propmt String
	 * @param buf - the byte array which will be converted
	 * @param offset - the start position of buf
	 * @param length - the converted length
	 * @param isOneLine16 - if true 16 bytes one line, false all bytes in one line
	 * @return Hex String
	 */
		public static string buf2String(string info, byte[] buf, int offset,
			int length, bool oneLine16)
		{
			int i, index;

			StringBuffer sBuf = new StringBuffer();
			sBuf.Append(info);

			for (i = 0 + offset; i < length + offset; i++)
			{
				if (i % 16 == 0)
				{
					if (oneLine16)
					{
						sBuf.Append(lineSeperate);
					}
				} else if (i % 8 == 0)
				{
					if (oneLine16)
					{
						sBuf.Append("   ");
					}
				}
				index = buf[i] < 0 ? buf[i] + DEFAULT_TABLE_LENGTH : buf[i];
				sBuf.Append(convertTable[index]);
			}
			return sBuf.ToString();
		}

		/**
	 * convert byte array to Hex String.
	 * 
	 * @param buf - the byte array which will be converted
	 * @param offset - the start position of buf
	 * @param length - the converted length
	 * @return Hex String
	 */
		public static string buf2StringWithoutSpace(byte[] buf, int offset,
			int length)
		{
			int i;
			StringBuffer sBuf = new StringBuffer();
			for (i = 0 + offset; i < length + offset; i++)
			{
				sBuf.Append(changeIntoHex(buf[i], false));
			}
			return sBuf.ToString();
		}

		/**
	 * convert byte to Hex.
	 * 
	 * @param b - the byte which will be converted
	 * @param hasSpace - if true has space, if false, no space
	 * @return Hex String
	 */
		private static string changeIntoHex(byte b, bool hasSpace)
		{
			int index = b < 0 ? b + DEFAULT_TABLE_LENGTH : b;
			string hex = "";
			if (index < 16)
			{
				if (hasSpace)
					hex = "0" + Integer.ToHexString(index) + " ";
				else
					hex = "0" + Integer.ToHexString(index);
			} else
				// 16-256
			{
				if (hasSpace)
					hex = Integer.ToHexString(index) + " ";
				else
					hex = Integer.ToHexString(index);
			}
			return hex;
		}
	}
}
