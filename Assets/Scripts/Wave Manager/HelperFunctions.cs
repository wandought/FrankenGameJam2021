using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperFunctions
{


			public static IEnumerable<string> SplitToLines(this string input)
			{
						if (input == null)
						{
									yield break;
						}

						using (System.IO.StringReader reader = new System.IO.StringReader(input))
						{
									string line;
									while ((line = reader.ReadLine()) != null)
									{
												yield return line;
									}
						}

			}

			/// <summary>
			/// line Number starts at 0
			/// </summary>
			/// <param name="input"></param>
			/// <param name="lineNumber"></param>
			/// <returns></returns>
			public static IEnumerable<string> GetSpecificLine(this string input, int lineNumber)
			{
						if (input == null)
						{
									yield break;
						}

						using (System.IO.StringReader reader = new System.IO.StringReader(input))
						{
									string line;


									while ((reader.ReadLine()) != null && lineNumber > 0)
									{
												lineNumber--;
									}
									line = reader.ReadLine();

									yield return line;
						}


			}

}
