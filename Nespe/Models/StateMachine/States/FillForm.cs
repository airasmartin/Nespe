using Nespe.Models;
namespace Nespe
{
    public class FillForm : State
    {
        public FillForm(StateMachine stateMachine)
            : base(stateMachine) 
        {
            //l'assistante qui a reçu le mail est envoyée sur sa page personnelle par un lien
            //Elle y trouve le lien pour remplir le formulaire du nouvel arrivant
            //Elle remplit le formulaire
            //Les demandes sont envoyées aux différentes adresses
            //un fichier XML est envoyé dans ARC pour créer le ticket
            
        }

        public void SendEmail(Request request)
        {
            //if(necessaryPhone ==true){
            //    try {
            //        WebMail.SmtpServer = "smtp.eur.nestle.com";
            //        WebMail.SmtpPort = 25;
            //        WebMail.EnableSsl = false;
            //        WebMail.From = "martin.airas@rdor.nestle.com";

            //        WebMail.Send("martin.airas@rdor.nestle.com", "[NESPE] Demande New comer",
            //                     " Bonjour, "+
            //                     "Le "+ StartDate + "nous accueillerons "+ LastName + " " +FirstName +
            //                     " au local " + localNC+ "."+
            //                     "Pour cette date, nous aurons besoin de"+
            //                     (fixPhone == true) ? " - un téléphone fixe " : "" + 
            //                     (cordless == true) ? " - un Cordless" : "" + 
            //                     (mobile == true) ? " - un téléphone mobile": "" +
            //                     (smartphone == true) ? " - un Smartphone " : "" +
            //                     (headphoneForFix == true) ? " - un casque pour téléphone fixe" : "" +
            //                     (freeHandsForFix == true) ? " - un casque pour fixe" : "" +
            //                     (freeHandsForCordless == true) ? " - un kit mains libres pour Cordless" +
            //                     commentPhone);
            
            //    } catch (Exception) {
            //        @:<b>Sorry - we couldn't send the EMail to confirm your Request.</b> 
            //    }


            //}
        }
        public override void StateEntry()
        {



        }

        public override void Next()
        {
            _stateMachine.State = _stateMachine.Arrival;

        }

    }
}