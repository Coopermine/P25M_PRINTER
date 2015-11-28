using System;
using Java.Lang;
using System.IO;
using Java;

namespace p
{
	


	public class Function {
		/**
	 * ������������
	 * @param databyte
	 * @return
	 */
		public static byte Enteryparity(byte[] databyte) {
			byte byteOne = databyte[0];
			byte intTwo = databyte[1];
			byte intResult = (byte) (byteOne ^ intTwo);

			for (int i = 2; i < databyte.Length; i++) {
				intResult = (byte) (intResult ^ databyte[i]);
			}
			return intResult;
		}

		public static sbyte[] readInputStreamToByte(System.IO.Stream @in)
		{
			sbyte[] bytes = new sbyte[0];
			ByteArrayOutputStream @out = new ByteArrayOutputStream();

			sbyte[] cache = new sbyte[1024 * 4];
			int read = -1;
			while ((read = @in.Read(cache, 0, cache.Length)) != -1)
			{
				@out.write(cache, 0, read);
			}
			bytes = @out.toByteArray();
			@out.close();
			return bytes;
		}
		public static string hexString(byte[] b) {
			StringBuffer d = new StringBuffer(b.Length * 2);
			for (int i = 0; i < b.Length; i++) {
				char hi = Character.ForDigit((b[i] >> 4) & 0x0F, 16);
				char lo = Character.ForDigit(b[i] & 0x0F, 16);
				d.Append(Character.ToUpperCase(hi));
				d.Append(Character.ToUpperCase(lo));
			}
			return d.ToString();
		}

		public static byte[] hex2byte (byte[] b, int offset, int len) {
			byte[] d = new byte[len];
			for (int i=0; i<len*2; i++) {
				int shift = i%2 == 1 ? 0 : 4;
				d[i>>1] |= Convert.ToByte(Character.Digit((char) b[offset+i], 16) << shift);
			}
			return d;
		}

		public static byte[] hex2byte (string s) {
			if (s.Length % 2 == 0) {
				return hex2byte (s.getBytes(), 0, s.Length >> 1);
			} else {
				return hex2byte("0"+s);
			}
		}

		public static byte[] toHex2Len(int i) {
			byte[] buffer = new byte[2];
			byte[] b = hex2byte(Integer.ToHexString(i));
			if (b.Length < 2) {
				buffer[0] = 0x00;
				buffer[1] = b[0];
			} else {
				System.Array.Copy(b, 0, buffer, 0, 2);
			}
			return buffer;
		}

		public static string CheckCode(string code) {
			string content = "";
			if (code.IndexOf(PostDefine.CODE_0) > -1) {
				content = "��������";
			} else if (code.IndexOf(PostDefine.CODE_Y4) > -1) {
				content = "������������";
			} else if (code.IndexOf(PostDefine.CODE_A3) > -1) {
				content = "����������";
			} else if (code.IndexOf(PostDefine.CODE_A4) > -1) {
				content = "������,����������";
			} else if (code.IndexOf(PostDefine.CODE_A5) > -1) {
				content = "������������";
			} else if (code.IndexOf(PostDefine.CODE_A6) > -1) {
				content = "��������";
			} else if (code.IndexOf(PostDefine.CODE_XX) > -1) {
				content = "��������";
			}
			return content;
		}

		public static PostMessage DealACKNACK(byte[] inBuffer) {
			PostMessage postMsg = new PostMessage();
			try {
				if (inBuffer != null && inBuffer.Length > 2) {
					int start = 0;
					for (int i = start; i < 3; i++) {
						if (inBuffer[i] == PostDefine.START[0]) {
							start++;
						} else {
							break;
						}
					}
					int end = 0;
					for (int i = inBuffer.Length - 1; i >= inBuffer.Length - 3; i--) {
						if (inBuffer[i] == PostDefine.END[0]) {
							end++;
						} else {
							break;
						}
					}
					int index = start;
					if (inBuffer[index] == PostDefine.ACK) {
						postMsg.setReturnType(ReturnType.ACK);
						postMsg.setErrorInfo(CheckCode(inBuffer.ToString()
							.Substring(index + 1, index + 3)));
					} else if (inBuffer[index] == PostDefine.NAK) {
						postMsg.setReturnType(ReturnType.NAK);
						postMsg.setErrorInfo(CheckCode(inBuffer.ToString()
							.Substring(index + 1, index + 3)));
					} else {
						index += 11;
						if (inBuffer.Length > index) {
							if (inBuffer[index] == PostDefine.SUCCEED) // 0x60
							{
								postMsg.setReturnType(ReturnType.PATH);
								int len = inBuffer.Length - (11 + 2 + start + end);

								// ����������
								byte[] header = new byte[] { inBuffer[start + 1],
									inBuffer[start + 2] };
								int msgLen = Integer.ParseInt(Function
									.hexString(header), 16);
								if (msgLen == len + 8) {
									byte[] bytesContent = new byte[len];
									int j = 0;
									end = start + 11 + len;
									for (int i = start + 11; i < end; i++) {
										bytesContent[j] = inBuffer[i];
										j++;
									}

									postMsg.setContent(bytesContent);

								} else {
									postMsg.setReturnType(ReturnType.Integrity);
								}
							}
						} else {
							postMsg.setReturnType(ReturnType.NONE);
						}
					}
				}
			} catch (System.Exception ex) {
			//	ex.printStackTrace();
			//	System.out.println("����������������  " + "\r\n");
			}
			return postMsg;
		}
	}
}

