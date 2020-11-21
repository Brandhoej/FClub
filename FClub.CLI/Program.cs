﻿using FClub.Model;
using FClub.DAL;
using FClub.BLL;
using FClub.UI;
using FClub.Controller;
using System;

namespace FClub.CLI
{
	class Program
	{
		static void Main(string[] args)
		{
			IFClubContext _context = new HashsetFClubContext();
			IUnitOfWork _unitOfWork = new HashsetUnitOfWork(_context);
			IStregsystem _stregsystem = new Stregsystem(_unitOfWork);
			IStregsystemUI _stregsystemUI = new StregsystemCLI(_stregsystem);
			IStregsystemController _stregsystemController = new StregsystemController(_stregsystemUI, _stregsystem);

			_stregsystemUI.Start();
		}
	}
}