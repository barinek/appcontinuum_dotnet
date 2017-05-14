using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace DatabaseSupport
{
    public class DatabaseTemplate : IDatabaseTemplate
    {
        private readonly IDataSourceConfig _dataSourceConfig;

        public DatabaseTemplate(IDataSourceConfig dataSourceConfig)
        {
            _dataSourceConfig = dataSourceConfig;
        }

        public List<T> FindBy<T>(string sql, Func<DbDataReader, T> mapper, string name, long id)
        {
            return Query(sql, mapper,
                new List<DbParameter>
                {
                    new MySqlParameter
                    {
                        ParameterName = name,
                        DbType = DbType.Int32,
                        Value = id,
                    }
                });
        }

        public List<T> Query<T>(string sql, Func<DbDataReader, T> mapper, List<DbParameter> parameters)
        {
            var list = new List<T>();

            using (var connection = _dataSourceConfig.GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = sql;

                foreach (var param in parameters)
                {
                    command.Parameters.Add(param);
                }

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(mapper(reader));
                    }
                }
            }
            return list;
        }


        public T Create<T>(string sql, Func<long, T> mapper, params KeyValuePair<string, object>[] list)
        {
            using (var connection = _dataSourceConfig.GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand() as MySqlCommand;

                command.CommandText = sql;

                foreach (var pair in list)
                {
                    switch (pair.Value.GetType().ToString())
                    {
                        case "System.String":
                            command.Parameters.Add(new MySqlParameter
                            {
                                ParameterName = pair.Key,
                                DbType = DbType.String,
                                Value = pair.Value,
                            });
                            break;
                        case "System.Int32":
                            command.Parameters.Add(new MySqlParameter
                            {
                                ParameterName = pair.Key,
                                DbType = DbType.Int32,
                                Value = pair.Value,
                            });
                            break;
                        case "System.Int64":
                            command.Parameters.Add(new MySqlParameter
                            {
                                ParameterName = pair.Key,
                                DbType = DbType.Int32,
                                Value = pair.Value,
                            });
                            break;
                        case "System.DateTime":
                            command.Parameters.Add(new MySqlParameter
                            {
                                ParameterName = pair.Key,
                                DbType = DbType.DateTime,
                                Value = pair.Value,
                            });
                            break;
                    }
                }
                command.ExecuteNonQuery();

                return mapper(command.LastInsertedId);
            }
        }
    }
}