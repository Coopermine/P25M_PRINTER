using System;
using Java;
using Java.Lang;

namespace p
{
	public class Printer {

		public Printer(){}

		public static byte[] printfont (string content,byte fonttype,byte fontalign,byte linespace,byte language){

			if (content != null && content.Length > 0) {

				content = content + "\n";
				byte[] temp = null;
				temp = PocketPos.convertPrintData(content, 0,content.Length, language, fonttype,fontalign,linespace);

				return temp;
			}else{
				return null;
			}
		}

	}
}

