using System;
using System.Linq;
using System.Reflection;

namespace FClub.Controller.Command
{
	public interface IStregsystemCommandResult
	{

	}

	internal interface IStregsystemCommand
	{
		IStregsystemCommandResult Run(object thisRef, string input);
		IStregsystemCommandResult Run(object thisRef, object[] parameters);

		bool Match(string name, string input);
		object[] GetParamsFromInput(string input);
	}

	internal class StregsystemCommand : IStregsystemCommand
	{
		private readonly string m_name;

		public StregsystemCommand(string name, MethodInfo endpoint)
		{
			m_name = name;
			MethodInfo = endpoint;
		}

		private MethodInfo MethodInfo;
		private ParameterInfo[] ParameterInfos => MethodInfo.GetParameters();

		public IStregsystemCommandResult Run(object thisRef, string input)
		{
			try
			{
				return Run(thisRef, GetParamsFromInput(input));
			}
			catch
			{
				throw new Exception("Input did not match command");
			}
		}

		public IStregsystemCommandResult Run(object thisRef, object[] parameters)
		{
			return (IStregsystemCommandResult)MethodInfo.Invoke(thisRef, parameters);
		}

		public object[] GetParamsFromInput(string input)
		{
			try
			{
				string[] _split = SplitString(input);
				int _defaultCount = ParameterInfos.Count(info => info.HasDefaultValue);
				if (_split.Length + _defaultCount < ParameterInfos.Length)
				{
					throw new ArgumentException("Split length is too small", nameof(input));
				}

				object[] _parameters = new object[ParameterInfos.Length];

				for (int i = 0; i < ParameterInfos.Length; i++)
				{
					/* parse splits until no more.
					 * Defaults are always at the end (language rule) */
					if (i < _split.Length && 
						ParseParameter(_split[i], ParameterInfos[i].ParameterType, out object result))
					{
						_parameters[i] = result;
					}
					else if (i >= _split.Length &&
						ParameterInfos[i].HasDefaultValue)
					{
						_parameters[i] = ParameterInfos[i].DefaultValue;
					}
					else
					{
						return default;
					}
				}

				return _parameters;
			}
			catch
			{
				return default;
			}
		}

		public bool Match(string name, string input)
		{
			try
			{
				return name == m_name && GetParamsFromInput(input) != default;
			}
			catch
			{
				return false;
			}
		}

		private bool ParseParameter(string input, Type type, out object result)
		{
			return (result = ParseParameter(input, type)) != null;
		}

		private object ParseParameter(string input, Type type)
		{
			return Convert.ChangeType(input, type);
		}

		private string[] SplitString(string input)
		{
			const string separator = " ";
			return input.Split(separator, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}
