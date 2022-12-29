using AppControleFinanceiro.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppControleFinanceiro.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly LiteDatabase _database;
        private readonly string collectionName = "transactions";
        public TransactionRepository(LiteDatabase database)
        {
            _database = database;
        }
        public List<Transaction> GetAll()
        {
            return _database
                .GetCollection<Transaction>(collectionName)
                .Query()
                .OrderByDescending(a=>a.Date)
                .ToList();
        }
        public void Add(Transaction transaction)
        {
            var col = _database.GetCollection<Transaction>(collectionName);
            col.Insert(transaction);
            col.EnsureIndex(a => a.Date);
        }
        public void Update(Transaction transaction)
        {
            var col = _database.GetCollection<Transaction>(collectionName);
            col.Update(transaction);
        }
        public void Delete(Transaction transaction)
        {
            var col = _database.GetCollection<Transaction>(collectionName);
            col.Delete(transaction.Id);
        }
    }
}
