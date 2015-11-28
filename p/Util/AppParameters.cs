using System;
using Java;
using Java.Lang;
namespace p
{
	public class AppParameters
	{
		public static string VERSION = "";

		public static string hardwareVersion = "G3";
		public static string softwareVersion = "Android";
		public static string printerSN = "13701886760";

		/** Specify application in debug mode or not.*/
		public static bool isDebugMode = false;

		/** Specify whether application executing first time or not */
		public static bool isFirstTime = true;

		/** Keep track of status blutooth detected device.*/ 
		public static bool hasBluetoothDetected = false;//false;

		/**
	 * Basic Constructor
	 */
		public AppParameters()
		{

		}


	}
}

