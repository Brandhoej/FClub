using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FClub.DAL
{
	public interface IRepository<T>
	{
		T Insert(T value);
		T Delete(T value);
		T Delete(object id);

		T Find(Predicate<T> predicate);
		ICollection<T> FindAll(Predicate<T> predicate);
		ICollection<T> GetAll();
		T GetById(object id);
	}
}
