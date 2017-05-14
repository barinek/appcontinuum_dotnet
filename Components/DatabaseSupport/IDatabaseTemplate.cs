using System;
using System.Collections.Generic;
using System.Data.Common;

namespace DatabaseSupport
{
    public interface IDatabaseTemplate
    {
        List<T> FindBy<T>(string sql, Func<DbDataReader, T> mapper, string name, long id);

        List<T> Query<T>(string sql, Func<DbDataReader, T> mapper, List<DbParameter> parameters);

        T Create<T>(string sql, Func<long, T> mapper, params KeyValuePair<string, object>[] list);
    }
}