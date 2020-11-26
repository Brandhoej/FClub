using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using FClub.Model;

namespace FClub.BLL.IO.Logging
{
	internal class TransactionLogger : ILogger<Transaction>
	{
		private readonly string m_path;
		private readonly string m_fileName;
		private readonly string m_separator;

		public TransactionLogger(string path, string fileName, string separator = ",")
		{
			m_path = path;
			m_fileName = fileName;
			m_separator = separator;
		}

		public string FullPath => Path.Combine(m_path, m_fileName);

		public void Log(Transaction transaction)
		{
			if (!Directory.Exists(m_path))
			{
				CreateDirectory();
				WriteColumnNames();
			}

			WriteLine(transaction.User.Id, transaction.User.Username, transaction.Amount, transaction.Date, transaction.GetType().Name);
		}

		private void CreateDirectory()
		{
			Directory.CreateDirectory(m_path);
		}

		private void WriteColumnNames()
		{
			WriteLine("User_Id", "User_Username", "Transaction_Amount", "Transaction_Date", "Transaction_Type");
		}

		private void WriteLine(params object[] columnValues)
		{
			using StreamWriter _writer = File.AppendText(FullPath);
			_writer.WriteLine(BuildLine(columnValues));
		}

		private string BuildLine(params object[] columnValues)
		{
			StringBuilder _builder = new StringBuilder();
			foreach (object _obj in columnValues)
			{
				_builder.Append(_obj.ToString());
				if (_obj != columnValues.Last())
				{
					_builder.Append(m_separator);
				}
			}
			return _builder.ToString();
		}
	}
}
