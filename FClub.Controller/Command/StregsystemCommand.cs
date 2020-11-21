using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FClub.Controller.Command
{
	public delegate bool StregsystemCommandStatement(params object[] parameters);

	internal interface IStregsystemCommand
	{
		string Format { get; }
		StregsystemCommandStatement StregsystemCommandStatement { get; }

		bool Run(string input);
		bool Match(string input);
	}

	internal class StregsystemCommand : IStregsystemCommand
	{
		public StregsystemCommand(string format, StregsystemCommandStatement stregsystemCommand)
		{
			Format = format;
			StregsystemCommandStatement = stregsystemCommand;
		}

		public string Format { get; }
		public StregsystemCommandStatement StregsystemCommandStatement { get; }

		public bool Run(string input)
		{
			object[] parameters = GetParamsFromInput(input);
			return StregsystemCommandStatement(parameters);
		}

		private object[] GetParamsFromInput(string input)
		{
			string[] parameters = SplitInputString(input);
			object[] parsedParameters = new object[parameters.Length - 1];

			for (int i = 1; i < parameters.Length; i++)
			{
				/* C# Literals:
				 * Integer Literals -> Not supported
				 * Floating-point Literals -> double
				 * Character Literals -> char
				 * String Literals -> string
				 * Boolean Literals -> bool */
				string curr = parameters[i];

				if (double.TryParse(curr, out double doubleResult))
				{
					parsedParameters[i - 1] = doubleResult;
				}
				else if (char.TryParse(curr, out char charResult))
				{
					parsedParameters[i - 1] = charResult;
				}
				else if (curr.ToLower() == "true" || curr.ToLower() == "false")
				{
					/* We dont need try parse because we know for sure 
					 * it will pass because of the conditional before */
					parsedParameters[i - 1] = bool.Parse(curr);
				}
				else
				{
					parsedParameters[i - 1] = curr;
				}
			}

			return parsedParameters;
		}

		public bool Match(string input)
		{
			string expected = SplitInputString(Format)[0];
			string actual = SplitInputString(input)[0];
			return expected == actual;
		}

		private string[] SplitInputString(string input)
		{
			return input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		}
	}
}
