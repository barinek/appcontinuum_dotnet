using System.Collections.Generic;
using System.Linq;
using DatabaseSupport;

namespace Users
{
    public class UserDataGateway : IUserDataGateway
    {
        private readonly IDatabaseTemplate _template;

        public UserDataGateway(IDatabaseTemplate template)
        {
            _template = template;
        }

        public UserRecord Create(string name)
        {
            var sql = @"insert into users (name) values (@name)";

            return _template.Create(sql, id => new UserRecord(id, name),
                new KeyValuePair<string, object>("@name", name));
        }

        public UserRecord FindObjectBy(long id)
        {
            var sql = @"select id, name from users where id = @id limit 1";

            var userRecords = _template.FindBy(sql, reader => new UserRecord(
                reader.GetInt64(0),
                reader.GetString(1)
            ), "@id", id);

            return userRecords.Count > 0 ? userRecords.First() : null;
        }
    }
}