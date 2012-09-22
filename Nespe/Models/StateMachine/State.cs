using System;

namespace Nespe
{
    public interface IState
    {
        void StateEntry();
        void  StateExit();

        void   Next();
        void Cancel();
    }

    public abstract class State : IState
    {
        protected readonly StateMachine _stateMachine;
        
        protected State(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public virtual void StateEntry() { }

        public virtual void StateExit() { }

        public virtual void Next()
        {
            throw new InvalidOperationException();
        }

        public virtual void Cancel()
        {
            throw new InvalidOperationException();
        }
    }
}