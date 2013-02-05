using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using Nespe.Models;
using System.Data;
using System.Data.Linq.Mapping;

namespace Nespe.Context
{
    public class NespeDataContext:DataContext
    {
        // Summary:
        //     Initializes a new instance of the System.Data.Linq.NespeDataContext class by referencing
        //     the connection used by the .NET Framework.
        //
        // Parameters:
        //   connection:
        //     The connection used by the .NET Framework.
        public NespeDataContext(IDbConnection connection):base(connection) { }
        //
        // Summary:
        //     Initializes a new instance of the System.Data.Linq.NespeDataContext class by referencing
        //     a file source.
        //
        // Parameters:
        //   fileOrServerOrConnection:
        //     This argument can be any one of the following:The name of a file where a
        //     SQL Server Express database resides.The name of a server where a database
        //     is present. In this case the provider uses the default database for a user.A
        //     complete connection string. LINQ to SQL just passes the string to the provider
        //     without modification.
        public NespeDataContext(string fileOrServerOrConnection) : base(fileOrServerOrConnection) { }
        //
        // Summary:
        //     Initializes a new instance of the System.Data.Linq.NespeDataContext class by referencing
        //     a connection and a mapping source.
        //
        // Parameters:
        //   connection:
        //     The connection used by the .NET Framework.
        //
        //   mapping:
        //     The System.Data.Linq.Mapping.MappingSource.
        public NespeDataContext(IDbConnection connection, MappingSource mapping) : base(connection, mapping) { }
        //
        // Summary:
        //     Initializes a new instance of the System.Data.Linq.NespeDataContext class by referencing
        //     a file source and a mapping source.
        //
        // Parameters:
        //   fileOrServerOrConnection:
        //     This argument can be any one of the following:The name of a file where a
        //     SQL Server Express database resides.The name of a server where a database
        //     is present. In this case the provider uses the default database for a user.A
        //     complete connection string. LINQ to SQL just passes the string to the provider
        //     without modification.
        //
        //   mapping:
        //     The System.Data.Linq.Mapping.MappingSource.
        public NespeDataContext(string fileOrServerOrConnection, MappingSource mapping) : base(fileOrServerOrConnection, mapping) { }

        public Table<Person> PersonTable { get; set; }
        public Table<Department> DepartmentTable { get; set; }
        public Table<PersonDepartment> PersonDepartmentTable { get; set; }
    }
}