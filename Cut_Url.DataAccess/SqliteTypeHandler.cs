using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Cut_Url.DataAccess
{
    abstract class SqliteTypeHandler<T> : SqlMapper.TypeHandler<T>
    {
        // Parameters are converted by Microsoft.Data.Sqlite
        public override void SetValue(IDbDataParameter parameter, T value)
            => parameter.Value = value;
    }
    class GuidHandler : SqliteTypeHandler<Guid>
    {
        public override Guid Parse(object value)
            => Guid.Parse((string)value);
    }
}
