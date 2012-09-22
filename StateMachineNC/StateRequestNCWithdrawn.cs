using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachineNC
{
    public class StateRequestNCWithdrawn : StateRequestNC
    {
        public StateRequestNCWithdrawn(RequestNC r)
            : base(r)
        {
            //entrée :  depuis n'importe quel état sauf lui même et cloturé
            currentState = inStateRequestNC.Withdraw;
            //sortie : la demande est supprimée de la DB
        }

        public override StateRequestNC Withdraw()
        {
            throw new TransitionEtatImpossibleException("la demande a été annulée, impossible d'y acceder");
        }

        public override StateRequestNC Create()
        {
            throw new TransitionEtatImpossibleException("la demande a été annulée, impossible d'y acceder");
        }

        public override StateRequestNC FillForm()
        {
            throw new TransitionEtatImpossibleException("la demande a été annulée, impossible d'y acceder");
        }

        public override StateRequestNC Arrival()
        {
            throw new TransitionEtatImpossibleException("la demande a été annulée, impossible d'y acceder");
        }

        public override StateRequestNC Close()
        {
            throw new TransitionEtatImpossibleException("la demande a été annulée, impossible d'y acceder");
        }
    }
}
