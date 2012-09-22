using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachineNC
{
    public class TransitionEtatImpossibleException : Exception
    {
        public TransitionEtatImpossibleException(string message)
            : base(message)
        {}
    }

    public abstract class StateRequestNC
    {
        protected RequestNC requestNC;
        protected inStateRequestNC currentState;

        public StateRequestNC(RequestNC r)
        {
            requestNC = r;
        }

        public inStateRequestNC GetCurrentState()
        {
            return currentState;
        }

        //Méthodes abstraites appelées par la demande Withdraw, Open, FillForm, BeforeArrival, FirstDay, Closed
        public abstract StateRequestNC Withdraw();
        public abstract StateRequestNC Create();
        public abstract StateRequestNC FillForm();
        public abstract StateRequestNC Arrival();
        public abstract StateRequestNC Close();
    }
}
