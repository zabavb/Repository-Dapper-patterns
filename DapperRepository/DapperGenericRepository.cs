using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DapperRepository
{
    public class DapperGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private string _tableName;
        private IDbConnection _dbConnection;

        public DapperGenericRepository(string tableName, IDbConnection dbConnection)
        {
            _tableName = tableName;
            _dbConnection = dbConnection;
        }

        private IEnumerable<PropertyInfo> GetProperties()
        {
            return typeof(TEntity).GetProperties();
        }

        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> properties)
        {
            return (from prop in properties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name).ToList();

        }

        public void Add(TEntity entity)
        {
            var insertQuery = GenerateInsertQuery();
            _dbConnection.Query(insertQuery, entity);
        }

        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT {_tableName}");
            insertQuery.Append("(");

            var properties = GenerateListOfProperties(GetProperties());
            properties.ForEach(prop =>
            {
                if (!prop.Equals("ID"))
                    insertQuery.Append($"[{prop}],");
            });
            insertQuery.Remove(insertQuery.Length - 1, 1).Append(") VALUES(");

            properties.ForEach(prop =>
            {
                if (!prop.Equals("ID"))
                    insertQuery.Append($"@{prop},");
            });
            insertQuery.Remove(insertQuery.Length - 1, 1).Append(")");

            return insertQuery.ToString();
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().AsQueryable().Where(predicate).ToList();
        }

        public TEntity FindById(object id)
        {
            return _dbConnection.Query<TEntity>($"SELECT * FROM {_tableName} WHERE ID = @Id", new { Id = id } ).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbConnection.Query<TEntity>($"SELECT * FROM {_tableName}");
        }

        public void Update(TEntity entity)
        {
            var updateQuery = GenerateUpdateQuery();
            _dbConnection.Query(updateQuery, entity);
        }

        private string GenerateUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE {_tableName} SET");
            var properies = GenerateListOfProperties(GetProperties());

            properies.ForEach(prop =>
            {
                if (!prop.Equals("ID"))
                    updateQuery.Append($"[{prop}] = @{prop},");
            });

            updateQuery.Remove(updateQuery.Length - 1, 1);
            updateQuery.Append(" WHERE ID = @ID");

            return updateQuery.ToString();
        }

        public void Remove(object id)
        {
            _dbConnection.Query($"DELETE FROM {_tableName} WHERE ID = @Id", new { Id = id } );
        }
    }
}