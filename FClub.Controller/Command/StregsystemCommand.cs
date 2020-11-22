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
		bool Run(object[] parameters);
		bool Match(string input);
		object[] GetParamsFromInput(string input);
	}

	internal class StregsystemCommand : IStregsystemCommand
	{
		public StregsystemCommand(StregsystemCommandStatement stregsystemCommand)
			: this(string.Empty, stregsystemCommand)
		{ }

		public StregsystemCommand(string format, StregsystemCommandStatement stregsystemCommand)
		{
			Format = format;
			StregsystemCommandStatement = stregsystemCommand ?? throw new ArgumentNullException(nameof(stregsystemCommand));
		}

		public string Format { get; }
		public StregsystemCommandStatement StregsystemCommandStatement { get; }

		public bool Run(string input)
		{
			object[] parameters = GetParamsFromInput(input);
			return StregsystemCommandStatement(parameters);
		}

		public bool Run(object[] parameters)
		{
			return StregsystemCommandStatement(parameters);
		}

		public object[] GetParamsFromInput(string input)
		{ 
			string[] _parameters = SplitInputString(input);
			int _offset = 0;
			string _cmdName = _parameters[0];
			object[] _parsedParameters;
			if (_cmdName == Format)
			{
				_parsedParameters = new object[_parameters.Length - 1];
				_offset = 1;
			}
			else
			{
				_parsedParameters = new object[_parameters.Length];
			}


			for (int i = 0; i < _parsedParameters.Length; i++)
			{
				/* C# Literals:
				 * Integer Literals -> Not supported
				 * Floating-point Literals -> double
				 * Character Literals -> char
				 * String Literals -> string
				 * Boolean Literals -> bool */
				string curr = _parameters[_offset + i];

				if (double.TryParse(curr, out double doubleResult))
				{
					_parsedParameters[i] = doubleResult;
				}
				else if (char.TryParse(curr, out char charResult))
				{
					_parsedParameters[i] = charResult;
				}
				else if (curr.ToLower() == "true" || curr.ToLower() == "false")
				{
					/* We dont need try parse because we know for sure 
					 * it will pass because of the conditional before */
					_parsedParameters[i] = bool.Parse(curr);
				}
				else
				{
					_parsedParameters[i] = curr;
				}
			}

			return _parsedParameters;
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
