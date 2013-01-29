


using Nespe.Models;
using System.Web.Helpers;
using System;

namespace Nespe
{
    
    public class CreateState : State
    {
        public CreateState(StateMachine stateMachine)
            : base(stateMachine) 
        {
            //le premier formulaire est rempli par les RH
            //la demande est crée dans le système, enregistrée dans la DB
            //apparait dans la page personnelle de l'assitante concernée
            //un EMail est envoyé à l'assitante et son backup

        }

        public void SaveToDb(Request request)
        {
            //using (var db = new NespeEntityContainer())
            //{
            //    //db.Requests.AddObject(selected);
            //    //db.SaveChanges();
            //}
        }
        public void SendEmail(Request request)
        {
            var LastName = request.Person.LastName;
            var FirstName = request.Person.FirstName;
            var StartDate = request.StartDate;
            var local = request.Local;
            var eMail = request.Person.EMail;
            
            //System.Web.Security.Membership.GetUser().

            var ui = Nespe.Helpers.ActiveDirectoryHelper.GertUserInfo(System.Web.HttpContext.Current.User.Identity.Name);
            var emailFrom = ui.EMail;

        }
        public override void StateEntry()
        {
            


        }

        public override void Next()
        {


            var request = base._stateMachine.Request;
            SaveToDb(request);
            SendEmail(request);
            _stateMachine.State = _stateMachine.FillForm;

        }

    }
}