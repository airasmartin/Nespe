using Nespe.Models;
using System.Web.Helpers;
using Nespe.Helpers;
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
            //WebMailHelper.SendEMail(this, request);
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