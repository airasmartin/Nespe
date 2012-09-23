using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Common;
using Nespe.Models;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

//[assembly: EdmRelationshipAttribute("Nespe.Models", "FK_person_department__Department_Id", "tbl_department", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Nespe.Models.Department), "tbl_person_department", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Nespe.Models.PersonDepartment), true)]
//[assembly: EdmRelationshipAttribute("Nespe.Models", "FK_person_department__Person_Id", "tbl_person", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Nespe.Models.Person), "tbl_person_department", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Nespe.Models.PersonDepartment), true)]
[assembly: EdmRelationshipAttribute("Nespe.Models", "FK_person_department_2_Person_Id", "tbl_person_department", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Nespe.Models.PersonDepartment), "tbl_person", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Nespe.Models.Person), true)]
[assembly: EdmRelationshipAttribute("Nespe.Models", "FK_person_department_2_Department_Id", "tbl_person_department", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Nespe.Models.PersonDepartment), "tbl_department", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Nespe.Models.Department), true)]

#endregion
namespace Nespe.Context
{
    [DbModelBuilderVersion(DbModelBuilderVersion.V5_0_Net4)]
    public partial class NespeObjectContext : ObjectContext
    {
        #region Constructors
        /// <summary>
        /// Initializes a new NespeObjectContext object using the connection string found in the 'NespeObjectContext' section of the application configuration file.
        /// </summary>
        public NespeObjectContext() : base("name=NespeObjectContext", "NespeObjectContext")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new NespeObjectContext object.
        /// </summary>
        public NespeObjectContext(string connectionString) : base(connectionString, "NespeObjectContext")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new NespeObjectContext object.
        /// </summary>
        public NespeObjectContext(EntityConnection connection) : base(connection, "NespeObjectContext")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    

        #endregion

    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion

        public ObjectSet<Request> RequestSet { get; set; }
        public ObjectSet<RequestTypeInfo> RequestInfoSet { get; set; }
        public ObjectSet<Person> PersonSet { get; set; }
        public ObjectSet<Department> DepartmentSet { get; set; }
        public ObjectSet<PersonDepartment> PersonDepartmentSet { get; set; }
    }
}
