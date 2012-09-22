using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachineNC
{
    public class StateRequestNCClosed : StateRequestNC
    {
        public StateRequestNCClosed(RequestNC r)
            : base(r)
        {
            // entrée :  la checklist est complétée
            currentState = inStateRequestNC.Close;
            //sortie :  la demande est archivée et n'apparait plus dans les demandes en cours
        }

        public override StateRequestNC Withdraw()
        {
            throw new TransitionEtatImpossibleException("Demande clôturée");
        }

        public override StateRequestNC Create()
        {
            throw new TransitionEtatImpossibleException("Demande clôturée");
        }

        public override StateRequestNC FillForm()
        {
            throw new TransitionEtatImpossibleException("Demande clôturée");
        }

        public override StateRequestNC Arrival()
        {
            throw new TransitionEtatImpossibleException("Demande clôturée");
        }

        public override StateRequestNC Close()
        {
            throw new TransitionEtatImpossibleException("Demande clôturée");
        }
    }
}
