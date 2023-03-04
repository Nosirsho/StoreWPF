using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StoreWPF.Interfaces {
	public interface IEntity {
		long Id { get; set; }
	}

	public interface IRepository<T> where T : class, IEntity, new() 
	{
		IQueryable<T> Items { get; }
		T Get(long id);
		Task<T> GetAsync(long id, CancellationToken Cancel = default);
		T Add(T item);
		Task<T> AddAsync(T item, CancellationToken Cancel = default);
		void Update(T item);
		Task UpdateAsync(T item, CancellationToken Cancel = default);
		void Remove(long id);
		Task RemoveAsync(long id, CancellationToken Cancel = default);
	}
}
