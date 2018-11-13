using RP.Data.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace RP.Data.StoredProcedures
{
    public class Context
    {
        public string _connectionString;
        private SqlConnection _connection;
        private SqlTransaction _transaction;
        private List<Tuple<object, string>> _entityChanges;

        public Context()
        {
            _connectionString = "Server=(localdb)\\mssqllocaldb;Database=RPDatabase;Trusted_Connection=True;";
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();

            _entityChanges = new List<Tuple<object, string>>();
        }

        public void Add<T>(string storedProcedure, T entity)
        {
            try
            {
                using (var command = new SqlCommand(storedProcedure, 
                    _connection, 
                    _transaction) { CommandType = CommandType.StoredProcedure })
                {
                    foreach(var property in entity.GetType()
                        .GetProperties())
                    {
                        string propertyName = property.Name;

                        if (propertyName == "Id") continue;

                        if (!typeof(ICollection<>).IsAssignableFrom(property.PropertyType))
                        {
                            var parameter = propertyName;
                            var value = property.GetValue(entity);
                            command.Parameters.Add(new SqlParameter($"@{ parameter }", value));
                        }
                    }

                    command.ExecuteNonQuery();
                }
            }

            catch
            {
                _transaction.Rollback();
            }
        }

        public void AddRange<T>(string storedProcedure, IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                Add(storedProcedure, entity);
        }

        public T Get<T>(string storedProcedure, int id)
        {
            try
            {
                var entity = Activator.CreateInstance<T>();

                using (var command = new SqlCommand(storedProcedure, 
                    _connection, 
                    _transaction) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add(new SqlParameter("@Id", id));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            foreach (var property in entity.GetType()
                                .GetProperties())
                            {
                                property.SetValue(entity, 
                                    Convert.ChangeType(reader[property.Name], property.PropertyType));
                            }
                        }
                    }
                }

                return entity;
            }

            catch
            {
                _transaction.Rollback();
                return default(T);
            }
        }

        public IEnumerable<T> GetAll<T>(string storedProcedure)
        {
            try
            {
                List <T> list = new List<T>();

                using (var command = new SqlCommand(storedProcedure,
                    _connection,
                    _transaction)
                { CommandType = CommandType.StoredProcedure })
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var entity = Activator.CreateInstance<T>();

                            foreach (var property in entity.GetType()
                                .GetProperties())
                            {
                                property.SetValue(entity,
                                    Convert.ChangeType(reader[property.Name], property.PropertyType));
                            }

                            list.Add(entity);
                        }
                    }
                }

                return list;
            }

            catch
            {
                _transaction.Rollback();
                return null;
            }
        }

        public void Remove<T>(string storedProcedure, T entity)
        {
            try
            {
                var id = (entity.GetType()
                    .GetProperty("Id")
                    .GetValue(entity)) as int?;

                using (var command = new SqlCommand(storedProcedure,
                    _connection,
                    _transaction)
                { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add(new SqlParameter("@Id", id));
                    command.ExecuteNonQuery();
                }
            }

            catch
            {
                _transaction.Rollback();
            }
        }

        public void RemoveRange<T>(string storedProcedure, IEnumerable<T> entities)
        {
            foreach (var entity in entities)
                Remove(storedProcedure, entity);
        }

        public void SaveChanges()
        {
            if (_transaction != null)
            {
                try
                {
                    foreach(var entityChange in _entityChanges)
                        Update(entityChange.Item2, entityChange.Item1);

                     _transaction.Commit();
                }

                catch
                {
                    _transaction.Rollback();
                }

                Dispose();
                _transaction = null;
            }
            else
                throw new NullReferenceException("No transaction exists.");

            return;
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();
        }

        private void Update(string storedProcedure, object entity)
        {
            using (var command = new SqlCommand(storedProcedure,
                _connection,
                _transaction) { CommandType = CommandType.StoredProcedure })
            {
                foreach (var property in entity.GetType()
                    .GetProperties())
                {
                    if (!typeof(ICollection<>).IsAssignableFrom(property.PropertyType))
                    {
                        var parameter = property.Name;
                        var value = property.GetValue(entity);

                        command.Parameters.Add(new SqlParameter($"@{ parameter }", value));
                    }
                }

                command.ExecuteNonQuery();
            }
        }

        private void HandlePropertyChanges(object sender, PropertyChangedEventArgs e)
        {
            if (!Enum.TryParse(sender.GetType().Name, out Model entity))
                return;

            switch (entity)
            {
                case Model.Employee: _entityChanges.Add(Tuple.Create(sender, "sp_UpdateEmployee")); break;
                default: break;
            }
        }

        private enum EntityState
        {
            Unchanged = 1,
            Added = 2,
            Modified = 3,
            Deleted = 4
        }

        private enum Model
        {
            Employee = 1
        }
    }
}
