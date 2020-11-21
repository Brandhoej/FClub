using System;
using System.Collections.Generic;
using System.Linq;

namespace FClub.DAL
{
	public class CollectionRepository<T> : IRepository<T>
	{
		public CollectionRepository(ICollection<T> collection)
		{
			Collection = collection ?? throw new ArgumentNullException(nameof(collection), "Seed collection cannot be null");	
		}

		public ICollection<T> Collection { get; }

		public T Insert(T value)
		{
			if (Collection.Contains(value))
			{
				throw new ArgumentException(nameof(value), "The entity value is already in the collection");
			}

			Collection.Add(value);
			return value;
		}

		public T Delete(T value)
		{
			return Collection.Remove(value) ? value : default;
		}

		public T Delete(object id)
		{
			return Delete(GetById(id));
		}

		public T Find(Predicate<T> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null");
			}

			try
			{
				return Collection.First(curr => predicate(curr));
			}
			catch
			{
				return default;
			}
		}

		public ICollection<T> FindAll(Predicate<T> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null");
			}

			return new HashSet<T>(Collection.Where(curr => predicate(curr)));
		}

		public ICollection<T> GetAll()
		{
			return Collection;
		}

		public T GetById(object id)
		{
			return Find(curr => curr.GetHashCode() == (int)id);
		}
	}
}
