


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
            using (var db = new NespeEntityContainer())
            {
                //db.Requests.AddObject(request);
                //db.SaveChanges();
            }
        }
        public void SendEmail(Request request)
        {
            //try {
            //    WebMail.SmtpServer = "smtp.eur.nestle.com";
            //    WebMail.SmtpPort = 25;
            //    WebMail.EnableSsl = false;
            //    WebMail.From = "martin.airas@rdor.nestle.com";

            //    WebMail.Send("martin.airas@rdor.nestle.com", "[NESPE] Demande New comer",
            //                    "Hello, "+
            //                    "Le "+ StartDateNC + " tu auras dans ton département l'arrivée de" NameNC + " " +SurnameNC +
            //                    " au local " + localNC+"."
            //                    "Afin que tout soit prêt lors de son arrivée, tu es priée de remplir le formulaire"+
            //                    " que tu trouveras sur (lien page)"+
            //                    "Merci d'avance");
            
            //} catch (Exception ex) {
            //    throw new Exception(@":<b>Sorry - we couldn't send the EMail to confirm your Request.</b>"); 
            //}

            //            try {
            //    WebMail.SmtpServer = "smtp.eur.nestle.com";
            //    WebMail.SmtpPort = 25;
            //    WebMail.EnableSsl = false;
            //    WebMail.From = "martin.airas@rdor.nestle.com";

            //    WebMail.Send("martin.airas@rdor.nestle.com", "[NESPE] Demande New comer",
            //                    "Hello, "+
            //                    "Le "+ StartDateNC + " tu auras dans ton département l'arrivée de" NameNC + " " +SurnameNC +
            //                    " au local " + localNC+"."
            //                    "Afin que tout soit prêt et que son arrivée se fasse avec le meilleur accompagnement,"+
            //                    " tu es prié de contacter"+ Assistante1 + "afin que vous vous répartissiez les tâches que" +
            //                    "tu trouveras sur (lien page)"+
            //                    "Merci d'avance");
            
            //} catch (Exception) {
            //    @:<b>Sorry - we couldn't send the EMail to confirm your Request.</b> 
            //}
        
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