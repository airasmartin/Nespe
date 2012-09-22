using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachineNC
{
    public class StateRequestNCArrival : StateRequestNC
    {
        public StateRequestNCArrival(RequestNC r)
            : base(r)
        {
            //entrée    : le formulaire a été rempli par l'assitante
            currentState = inStateRequestNC.Arrival;
            //sortie:   le numéro du ticket généré, son statut ainsi que le nom du technicien en charge est renseigné dans la checklist
            //          le userID du NewComer est renseigné dans le champ concernant les droits à rajouter
            //          L'assistante / le Head department / le parrain et le NewComer remplissent la checklist
            //          La demande passe en Close
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
            throw new TransitionEtatImpossibleException("Impossible, le formulaire a déjà été complété");
        }

        public override StateRequestNC Arrival()
        {
            throw new TransitionEtatImpossibleException("Opération en cours");
        }

        public override StateRequestNC Close()
        {
            throw new TransitionEtatImpossibleException("Vous pouvez annuler la demande mais pas la cloturer sans la completer");
        }
    }
}
