using FClub.BLL;
using FClub.Controller;
using FClub.DAL;
using FClub.UI;
using FClub.UI.Scene;
using System;
using System.IO;

namespace FClub.CLI
{
	class Program
	{
		static void Main(string[] args)
		{
			IFClubContext _context = new HashsetFClubContext(
				Path.Combine(Environment.CurrentDirectory, "data", "products.csv"),
				Path.Combine(Environment.CurrentDirectory, "data", "users.csv"));
			IUnitOfWork _unitOfWork = new HashsetUnitOfWork(_context);

			IStregsystem _stregsystem = new Stregsystem(_unitOfWork);
			IStregsystemUI _stregsystemUI = new StregsystemCLI();
			IStregsystemController _stregsystemController = new StregsystemController(_stregsystem, _stregsystemUI);
			_stregsystemController.Execute("/products");
			_stregsystemUI.Start();
		}
	}
}
