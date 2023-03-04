using Microsoft.EntityFrameworkCore;
using StoreWPF.DAL.Context;
using StoreWPF.DAL.Entities.Base;
using StoreWPF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StoreWPF.DAL {
	class DbRepository<T> : IRepository<T> where T : Entity, new() {
		private readonly StoreDB _db;
		private readonly DbSet<T> _Set;
		public bool AutoSaveChanges { get; set; } = true;

		public DbRepository(StoreDB db) {
			_db = db;
			_Set = db.Set<T>();
		}

		public IQueryable<T> Items => _Set;
		public T Get(long id) { 
			return Items.SingleOrDefault(i => i.Id == id);
		}

		public async Task<T> GetAsync(long id, CancellationToken Cancel = default) {
			return await Items.SingleOrDefaultAsync(i => i.Id == id, Cancel).ConfigureAwait(false);
		}

		public T Add(T item) {
			if (item is null)
				throw new ArgumentNullException(nameof(item));
			_db.Entry(item).State = EntityState.Added;
			if (AutoSaveChanges)
				_db.SaveChanges();
			return item;
		}

		public async Task<T> AddAsync(T item, CancellationToken Cancel = default) {
			if (item is null)
				throw new ArgumentNullException(nameof(item));
			_db.Entry(item).State = EntityState.Added;
			if (AutoSaveChanges)
				await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
			return item;
		}

		public void Update(T item) {
			if (item is null)
				throw new ArgumentNullException(nameof(item));
			_db.Entry(item).State = EntityState.Modified;
			if (AutoSaveChanges)
				_db.SaveChanges();
		}

		public async Task UpdateAsync(T item, CancellationToken Cancel = default) {
			if (item is null)
				throw new ArgumentNullException(nameof(item));
			_db.Entry(item).State = EntityState.Modified;
			if (AutoSaveChanges)
				await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
		}

		public void Remove(long id) {
			_db.Remove(new T { Id = id });
			if (AutoSaveChanges)
				_db.SaveChanges();
		}

		public async Task RemoveAsync(long id, CancellationToken Cancel = default) {
			_db.Remove(new T { Id = id });
			if (AutoSaveChanges)
				await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
		}
	}
}
