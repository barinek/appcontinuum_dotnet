using System.Data.Common;

namespace DatabaseSupport
{
    public interface IDataSourceConfig
    {
        DbConnection GetConnection();
    }
}