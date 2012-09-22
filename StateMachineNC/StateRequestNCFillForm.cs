using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachineNC
{
    public class StateRequestNCFillForm : StateRequestNC
    {
        public StateRequestNCFillForm(RequestNC r)
            : base(r)
        {
            //entrée : la demande a été crée et assignée, l'assistante avertie
            currentState = inStateRequestNC.FillForm;
            //sortie :  l'assistante a rempli le formulaire
            //          un fichier XML est déposé sur ARC pour créer le ticket à l'IT
            //          un EMail est envoyé aux demandes nécessitant un EMail
            //          le statut des demandes faites passe au vert
            //          envoie un mail au parrain pour l'avertir de contacter l'assistante
            //          envoie un résumé des demandes au DH
            //          l'état passe à Arrival

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
            throw new TransitionEtatImpossibleException("Impossible, cette procédure est en cours");
        }

        public override StateRequestNC Arrival()
        {
            return new StateRequestNCArrival(requestNC);
        }

        public override StateRequestNC Close()
        {
            throw new TransitionEtatImpossibleException("Vous pouvez annuler la demande mais pas la cloturer sans la completer");
        }
    }
}
