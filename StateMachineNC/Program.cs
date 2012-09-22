using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using Nespe.Data;
using Nespe;
using System.DirectoryServices;

namespace StateMachineNC
{

    class Program
    {
        static void Main(string[] args)
        {
            Spike7(args);

        }
        static void Spike0(string[] args)
        {
            WorkflowInvoker.Invoke(new Workflow1());
            
        }
        static void Spike1(string[] args)
        {
            //WorkflowInvoker.Invoke(new Workflow1());
            var db = new NespeDbContext();
            db.Database.CreateIfNotExists();
            var tc = db.RequestSet;
            var dr = tc.Create();
            dr.NameNC = "MArtin";
            dr.SurnameNC = "Airas";
            db.SaveChanges();
        }
        static void Spike2(string[] args)
        {
            var model = new Nespe.Models.TransfertNewRequestModel { 
                SurnameNC="Martin",
                NameNC = "Airas",
                LocalNC="Vaud1",
                SuperiorNC="Pinolo"
            };
            Request req = model;
            Console.WriteLine(model);
            Console.WriteLine(req);
            var model1 = new Nespe.Models.DepartureNewRequestModel
            {
                SurnameNC = "Martin",
                NameNC = "Airas",
            };
            req = model1;
            Console.WriteLine(model);
            Console.WriteLine(req);


        }
        static void Spike3(string[] args)
        {
            using (var db = new System.Data.SqlServerCe.SqlCeConnection(@"Data Source=C:\Users\RDAirasMa\Desktop\Nespe\Nespe\Nespe\App_Data\ncom.sdf"))
            {

                db.Open();
                using (var cmd = db.CreateCommand()) {
                    cmd.CommandText = "select 1 ";
                    var v=cmd.ExecuteScalar();
                    Console.WriteLine(v);
                    //using (var dr = cmd.ExecuteReader()) { 
                        
                    //}
                }
                
            }
        }
        static void Spike4(string[] args)
        {
            
            using (var cnn = new System.Data.SqlServerCe.SqlCeConnection(@"Data Source=C:\Users\RDAirasMa\Desktop\Nespe\Nespe\Nespe\App_Data\ncom.sdf"))
            {

                //cnn.Open();
                var db = new NespeDbContext(cnn, true);
                db.Database.CreateIfNotExists();
                var tc = db.RequestSet;
                var dr = tc.Create();
                dr.NameNC = "MArtin";
                dr.SurnameNC = "Airas";
                db.SaveChanges();

                

            }
        }
        static void Spike5(string[] args)
        {

            using (var db = new NespeEntityContainer())
            {
                var tc = db.Requests;
                var dr = tc.CreateObject();
                dr.NameNC = "MArtin";
                dr.SurnameNC = "Airas";
                tc.AddObject(dr);
                db.SaveChanges();

                var q = (from t in tc select t);
                foreach (var o in q) {
                    Console.WriteLine(o);
                }
                Console.ReadLine();
            }
        }
        static void Spike6(string[] args)
        {



            var s = "Airas";
            var result = FindUser(s = "Airas");
            Console.WriteLine(" resulta pour "+s);
            display(result);
            result = FindUser(s="Martin");
            Console.WriteLine(" resulta pour "+s);
            display(result);
            result = FindUser(s = "Penalva");
            Console.WriteLine(" resulta pour " + s);
            display(result);
            result = FindUser(s = "RDAirasMa");
            Console.WriteLine(" resulta pour " + s);
            display(result);
        }
        static void Spike7(string[] args)
        {
            //WorkflowInvoker.Invoke(new Workflow1());
            using (var cnn = new System.Data.SqlServerCe.SqlCeConnection(@"Data Source=C:\Users\RDAirasMa\Desktop\Nespe\Nespe\Nespe\App_Data\ncom.sdf"))
            {

                //cnn.Open();
                var db = new NespeDbContext(cnn, true);
                //var db = new NespeDbContext();
                db.Database.CreateIfNotExists();
                var tc = db.RequestInfoSet;
                var dr = tc.Create();
                dr.Name = "Demande IT";
                dr.Description = "Demade par le formulaire";
                db.SaveChanges();


            }
            
        }
        static void display(SearchResultCollection result, bool showProperties=false)
        {

            Console.WriteLine(result.Count);

            foreach (SearchResult item in result)
            {
                Console.WriteLine(item.Path);
                if (showProperties == false)
                    continue;
                foreach (string pn in item.Properties.PropertyNames)
                {
                    var pvc = item.Properties[pn];
                    Console.WriteLine("\t{0}", pn);
                    foreach (var pv in pvc)
                    {
                        Console.WriteLine("\t\t{0}", pv);
                    }
                }
            }
        }
        private static DirectorySearcher searcher = null;
        public static SearchResultCollection FindUser(String findIt) {
            
            if (searcher == null)
            {
                searcher = new DirectorySearcher();
                searcher.SearchScope = SearchScope.Subtree;
                searcher.SizeLimit = 20;
                //searcher.SearchScope = SearchScope.OneLevel;
            }
            /*
             * http://msdn.microsoft.com/en-us/library/windows/desktop/aa746475(v=vs.85).aspx
            Search filter                                                                   Description 
            "(objectClass=*)                                                                All objects. 
            "(&(objectCategory=person)(objectClass=user)(!cn=andy))"                        All user objects but "andy". 
            "(sn=sm*)"                                                                      All objects with a surname that starts with "sm". 
            "(&(objectCategory=person)(objectClass=contact)(|(sn=Smith)(sn=Johnson)))"      All contacts with a surname equal to "Smith" or "Johnson". 

            */
            //searcher.Filter = string.Format("(&(sAMAccountName={0})(UserAccountControl=???))", findIt);
            
            searcher.Filter = string.Format(string.Concat("(&(objectClass=user)(|",
                "(sAMAccountName={0}*)",
                "(cn={0}*)",
                "(mailnickname={0}*)",
                "(userprincipalname={0})*",
                "(givenname={0}*)",
                "(samaccountname={0}*)",
                "(description=*{0}*)",
                "(mail=*{0}*)",
                "(displayname=*{0}*)",
                "(sn={0}*)",
                "))"), findIt);
            Console.WriteLine(searcher.Filter);
            //LDAP://CN=RDAirasMa,OU=CH-OrbeRD-Site,OU=Users and Groups,OU=RD EUR,OU=EUR,OU=Organizations,DC=nestle,DC=com
            /*
             LDAP://CN=RDAirasMa,OU=CH-OrbeRD-Site,OU=Users and Groups,OU=RD EUR,OU=EUR,OU=Or
ganizations,DC=nestle,DC=com
        directreports
                CN=adm2airas,OU=Admins,OU=Users and Groups,OU=GC CTR,OU=CTR,OU=O
rganizations,DC=nestle,DC=com
        nestleuserauthkey
                OXOBJDJI
        submissioncontlength
                7168
        msrtcsip-userenabled
                True
        logoncount
                7542
        msexchuseraccountcontrol
                0
        objectguid
                System.Byte[]
        cn
                RDAirasMa
        accountexpires
                2650466880000000000
        badpasswordtime
                129911571619515156
        nestleshareserver
                CHORRW0000
        msexchumdtmfmap
                reversedPhone:43172444214+
                emailAddress:62784624727
                lastNameFirstName:24727627846
                firstNameLastName:62784624727
        mailnickname
                RDAirasMa
        lastlogon
                129913309404346607
        msexchpoliciesincluded
                {AC863F4D-B65C-4CA4-A0E7-D3B7552C97BD},{26491CFC-9E50-4857-861B-
0CB8DF22B5D7}
        publicdelegates
                CN=mairas,OU=HQ-Vevey-Corporate,OU=Users and Groups,OU=HQ,OU=CTR
,OU=Organizations,DC=nestle,DC=com
        company
                Nestlé PTC Orbe
        postalcode
                1350
        adspath
                LDAP://CN=RDAirasMa,OU=CH-OrbeRD-Site,OU=Users and Groups,OU=RD
EUR,OU=EUR,OU=Organizations,DC=nestle,DC=com
        userprincipalname
                Martin.Airas@rdor.nestle.com
        instancetype
                4
        st
                Vaud
        loginshell
                /bin/false
        msrtcsip-internetaccessenabled
                True
        physicaldeliveryofficename
                A124
        c
                CH
        givenname
                Martin
        objectsid
                System.Byte[]
        samaccountname
                RDAirasMa
        lastlogoff
                0
        msexchhidefromaddresslists
                False
        msrtcsip-federationenabled
                True
        distinguishedname
                CN=RDAirasMa,OU=CH-OrbeRD-Site,OU=Users and Groups,OU=RD EUR,OU=
EUR,OU=Organizations,DC=nestle,DC=com
        memberof
                CN=EUR: HPAMPR_USERS,OU=Users and Groups,OU=GC EUR,OU=EUR,OU=Org
anizations,DC=nestle,DC=com
                CN=RDORR: NDR_Engineering_Biblio_M,OU=Distribution-Groups,OU=Use
rs and Groups,OU=RD EUR,OU=EUR,OU=Organizations,DC=nestle,DC=com
                CN=RDORR: NDR_MGS_IT_M,OU=Distribution-Groups,OU=Users and Group
s,OU=RD EUR,OU=EUR,OU=Organizations,DC=nestle,DC=com
                CN=RDORR: NDR_ADM_MGS_IT,OU=CH-OrbeRD-Site,OU=Users and Groups,O
U=RD EUR,OU=EUR,OU=Organizations,DC=nestle,DC=com
                CN=HQ: SIN1 CLGO Viewer,OU=Users and Groups,OU=HQ,OU=CTR,OU=Orga
nizations,DC=nestle,DC=com
                CN=RDEUR: CH_DevProfileUsers,OU=Users and Groups,OU=RD EUR,OU=EU
R,OU=Organizations,DC=nestle,DC=com
                CN=RDORR: Management Services,OU=Distribution-Groups,OU=Users an
d Groups,OU=RD EUR,OU=EUR,OU=Organizations,DC=nestle,DC=com
                CN=RDORR: NeroBurningRom7960,OU=CH-OrbeRD-Site,OU=Users and Grou
ps,OU=RD EUR,OU=EUR,OU=Organizations,DC=nestle,DC=com
                CN=RDORR: N_ENG_Biblio,OU=CH-OrbeRD-Site,OU=Users and Groups,OU=
RD EUR,OU=EUR,OU=Organizations,DC=nestle,DC=com
                CN=RDORR: Access11,OU=CH-OrbeRD-Site,OU=Users and Groups,OU=RD E
UR,OU=EUR,OU=Organizations,DC=nestle,DC=com
                CN=RDORR: CAO,OU=CH-OrbeRD-Site,OU=Users and Groups,OU=RD EUR,OU
=EUR,OU=Organizations,DC=nestle,DC=com
                CN=RDORR: ORR_DEF_PTCLib20EN,OU=CH-OrbeRD-Site,OU=Users and Grou
ps,OU=RD EUR,OU=EUR,OU=Organizations,DC=nestle,DC=com
                CN=RDORR: VPN Access,OU=CH-OrbeRD-Site,OU=Users and Groups,OU=RD
 EUR,OU=EUR,OU=Organizations,DC=nestle,DC=com
                CN=RDORR: All Users,OU=CH-OrbeRD-Site,OU=Users and Groups,OU=RD
EUR,OU=EUR,OU=Organizations,DC=nestle,DC=com
                CN=NBS: Employee Services Notifications - PTC Orbe,OU=Distributi
on-Groups,OU=Users and Groups,OU=RD EUR,OU=EUR,OU=Organizations,DC=nestle,DC=com

                CN=RDORR: Informatique,OU=CH-OrbeRD-Site,OU=Users and Groups,OU=
RD EUR,OU=EUR,OU=Organizations,DC=nestle,DC=com
                CN=RDORR: ECSCAD,OU=CH-OrbeRD-Site,OU=Users and Groups,OU=RD EUR
,OU=EUR,OU=Organizations,DC=nestle,DC=com
                CN=RDORR: MFS Default,OU=CH-OrbeRD-Site,OU=Users and Groups,OU=R
D EUR,OU=EUR,OU=Organizations,DC=nestle,DC=com
                CN=RDORR: Backup Operators,OU=Distribution-Groups,OU=Users and G
roups,OU=RD EUR,OU=EUR,OU=Organizations,DC=nestle,DC=com
                CN=RDORR: ServiceCenter515,OU=CH-OrbeRD-Site,OU=Users and Groups
,OU=RD EUR,OU=EUR,OU=Organizations,DC=nestle,DC=com
                CN=RDORR: Local IT,OU=CH-OrbeRD-Site,OU=Users and Groups,OU=RD E
UR,OU=EUR,OU=Organizations,DC=nestle,DC=com
                CN=RDORR: N_MGS_IT,OU=CH-OrbeRD-Site,OU=Users and Groups,OU=RD E
UR,OU=EUR,OU=Organizations,DC=nestle,DC=com
                CN=RDORR: SiteUsers,OU=CH-OrbeRD-Site,OU=Users and Groups,OU=RD
EUR,OU=EUR,OU=Organizations,DC=nestle,DC=com
        msrtcsip-optionflags
                256
        objectcategory
                CN=Person,CN=Schema,CN=Configuration,DC=nestle,DC=com
        msrtcsip-primaryhomeserver
                CN=LC Services,CN=Microsoft,CN=CTROCS2007R2Pool,CN=Pools,CN=RTC
Service,CN=Microsoft,CN=System,DC=nestle,DC=com
        msexchmailboxguid
                System.Byte[]
        msrtcsip-primaryuseraddress
                sip:RDAirasMa@nestle.com
        legacyexchangedn
                /O=NESTLE/OU=EU02/cn=Recipients/cn=RDAirasMa
        dscorepropagationdata
                03.05.2012 22:08:35
                27.11.2011 22:53:05
                25.09.2011 13:49:11
                01.07.2011 18:01:22
                14.07.1601 22:36:49
        description
                Martin Airas
        whencreated
                01.05.2009 09:14:34
        lastlogontimestamp
                129911301362800548
        objectclass
                top
                person
                organizationalPerson
                user
        proxyaddresses
                sip:RDAirasMa@nestle.com
                X400:c=US;a=INFONET;p=NESTLE;o=EU02;s=Airas;g=Martin;i= ;
                smtp:RDAirasMa@nestle.com
                SMTP:Martin.Airas@rdor.nestle.com
        msexchalobjectversion
                512
        samaccounttype
                805306368
        homemdb
                CN=Mailbox Store44 (HQVEVE0026),CN=Fourth Storage Group,CN=Infor
mationStore,CN=HQVEVE0026,CN=Servers,CN=GC EUR,CN=Administrative Groups,CN=NESTL
E,CN=Microsoft Exchange,CN=Services,CN=Configuration,DC=nestle,DC=com
        msexchhomeservername
                /O=NESTLE/OU=EU02/cn=Configuration/cn=Servers/cn=HQVEVE0026
        telephonenumber
                +41 24 442 71 34
        initials

        countrycode
                756
        useraccountcontrol
                512
        pwdlastset
                129893980436667542
        co
                SWITZERLAND
        homedirectory
                \\CHORRW0001\Users\RDAirasMa
        name
                RDAirasMa
        homedrive
                H:
        nestlelanguageiso
                EN
        whenchanged
                03.09.2012 07:15:36
        userparameters
                CtxCfgPresent                                   ☺CtxCfgPresent??
??☺CtxCfgFlags1???? :☺CtxWFProfilePath?????????????????????????????↑:☺CtxWFHomeD
ir?????????????????????????????"♠☺CtxWFHomeDirDrive???☺CtxMaxDisconnectionTime??
??☺CtxMaxIdleTime????☺CtxMaxConnectionTime????
        l
                Orbe
        badpwdcount
                0
        department
                MGS
        displayname
                Airas,Martin,ORBE,MGS
        homemta
                CN=Microsoft MTA,CN=HQVEVE0026,CN=Servers,CN=GC EUR,CN=Administr
ative Groups,CN=NESTLE,CN=Microsoft Exchange,CN=Services,CN=Configuration,DC=nes
tle,DC=com
        msradiusservicetype
                4
        textencodedoraddress
                c=US;a=INFONET;p=NESTLE;o=EU02;s=Airas;g=Martin;i= ;
        usnchanged
                571096864
        msexchmailboxsecuritydescriptor
                System.Byte[]
        mail
                Martin.Airas@rdor.nestle.com
        title
                Technicien Informatique
        mdbusedefaults
                True
        publicdelegatesbl
                CN=mairas,OU=HQ-Vevey-Corporate,OU=Users and Groups,OU=HQ,OU=CTR
,OU=Organizations,DC=nestle,DC=com
        primarygroupid
                513
        usncreated
                154665797
        codepage
                0
        wwwhomepage
                http://mysite-EUR.nestle.com/Person.aspx?accountname=nestle\RDAi
rasMa
        showinaddressbook
                CN=Default Global Address List,CN=All Global Address Lists,CN=Ad
dress Lists Container,CN=NESTLE,CN=Microsoft Exchange,CN=Services,CN=Configurati
on,DC=nestle,DC=com
                CN=All Users,CN=All Address Lists,CN=Address Lists Container,CN=
NESTLE,CN=Microsoft Exchange,CN=Services,CN=Configuration,DC=nestle,DC=com
        sn
                Airas

             */
            //searcher.Filter = "(&(sAMAccountName=RDAirasMa))";
            return searcher.FindAll();
        }
    }
}
