using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachineNC
{
    public class StateRequestNCCreate : StateRequestNC
    {
        public StateRequestNCCreate(RequestNC r)
            : base(r)
        {
            currentState = inStateRequestNC.Create;
            //entrée : les RH remplissent le premier formulaire
            //sortie :  la demande est crée et assignée à l'assistante 1
            //          un EMail d'avertissement est envoyé à l'assitante 1 et 2
            //          l'etat passe à FillForm 
            
        }

        public override StateRequestNC Withdraw()
        {
            return new StateRequestNCWithdrawn(requestNC);
        }

        public override StateRequestNC Create()
        {
            throw new TransitionEtatImpossibleException("Impossible de recréer cette demande");
        }

        public override StateRequestNC FillForm()
        {
            return new StateRequestNCFillForm (requestNC);
        }

        public override StateRequestNC Arrival()
        {
            throw new TransitionEtatImpossibleException("Il faut d'abord remplir le formulaire");
        }

        public override StateRequestNC Close()
        {
            throw new TransitionEtatImpossibleException("Vous pouvez annuler la demande mais pas la cloturer sans la completer");
        }
    }
}
