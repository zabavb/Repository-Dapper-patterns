using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperRepository
{
    public class GenericUnitOfWork
    {
        private string _connectionString;

        public GenericUnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (repositories.Keys.Contains(typeof(TEntity)) == true)
                return repositories[typeof(TEntity)] as IGenericRepository<TEntity>;

            IGenericRepository<TEntity> repos = new DapperGenericRepository<TEntity>(typeof(TEntity).Name, CreateConnection());

            repositories.Add(typeof(TEntity), repos);
            return repos;
        }
    }
}