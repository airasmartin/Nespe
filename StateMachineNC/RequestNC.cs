using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachineNC
{
    public enum inStateRequestNC {Withdraw, Create, FillForm, Arrival, Close };
    public class RequestNC
    {
        private StateRequestNC state;

        public RequestNC (inStateRequestNC isr)
        {
            switch (isr) //permet de démarrer la machine dans n'importe quel état
            {
                case inStateRequestNC.Withdraw :
                    state = new StateRequestNCWithdrawn(this);
                    break;
                case inStateRequestNC.Create:
                    state = new StateRequestNCCreate(this);
                    break;
                case inStateRequestNC.FillForm:
                    state = new StateRequestNCFillForm(this);
                    break;
                case inStateRequestNC.Arrival:
                    state = new StateRequestNCArrival(this);
                    break;

                case inStateRequestNC.Close:
                    state = new StateRequestNCCreate(this);
                    break;  
            }   
        }

        public void Withdraw()
        {
            state = state.Withdraw();
        }
        
        public void Create()
        {
            state = state.Create();
        }

        public void FillForm()
        {
            state = state.FillForm();
        }
        public void Arrival()
        {
            state = state.Arrival();
        }

        public void Close()
        {
            state = state.Close();
        }

        public inStateRequestNC GetCurrentState()
        {
            return state.GetCurrentState();
        }
      
    }
}
