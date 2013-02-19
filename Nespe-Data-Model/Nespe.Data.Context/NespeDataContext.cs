using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.Entity;
using System.Data.OleDb;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity.ModelConfiguration.Conventions;

namespace Nespe.Data.Context
{
    [DbModelBuilderVersion(DbModelBuilderVersion.V5_0)]
    public class NespeDataContext : DbContext
    {
    }
}
