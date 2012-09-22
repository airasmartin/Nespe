

using Nespe.Models;
namespace Nespe
{
    public class StateMachine
    {
        private IState           _state;
        private readonly Request _request;

        public StateMachine(Request request)
        {
            _request = request;

            BuildStates();

            State = Created;
        }

        public Request Request { get { return _request; } }

        public IState State
        {
            get { return _state; }

            set
            {
                IState currentState = _state;
                IState     newState = value;

                if (currentState != null)
                    currentState.StateExit();

                _state = newState;

                if (newState != null)
                    newState.StateEntry();
            }
        }

        public IState   Created { get; private set; }
        public IState  FillForm { get; private set; }
        public IState   Arrival { get; private set; }
        public IState    Closed { get; private set; }
        public IState Withdrawn { get; private set; }

        private void BuildStates()
        {
            Created   = new  CreateState(this);
            FillForm  = new FillForm(this);
            Arrival   = new Arrival(this);
            Closed    = new Closed(this);
            Withdrawn = new Withdrawn(this);

        }
    }
}