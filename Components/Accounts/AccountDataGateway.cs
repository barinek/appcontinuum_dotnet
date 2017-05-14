using System.Collections.Generic;
using System.Linq;
using DatabaseSupport;

namespace Accounts
{
    public class AccountDataGateway : IAccountDataGateway
    {
        private readonly IDatabaseTemplate _template;

        public AccountDataGateway(IDatabaseTemplate template)
        {
            _template = template;
        }

        public AccountRecord Create(long ownerId, string name)
        {
            var sql = @"
insert into accounts (owner_id, name) values (@ownerId, @name)";

            return _template.Create(sql, id => new AccountRecord(id, ownerId, name),
                new KeyValuePair<string, object>("@ownerId", ownerId),
                new KeyValuePair<string, object>("@name", name));
        }

        public AccountRecord FindBy(long ownerId)
        {
            var sql = @"
select id, owner_id, name from accounts where owner_id = @ownerId order by name desc limit 1";

            var userRecords = _template.FindBy(sql, reader => new AccountRecord(
                reader.GetInt64(0),
                reader.GetInt64(1),
                reader.GetString(2)
            ), "@ownerId", ownerId);

            return userRecords.Count > 0 ? userRecords.First() : null;
        }
    }
}