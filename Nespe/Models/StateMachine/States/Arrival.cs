namespace Nespe
{
    public class Arrival : State
    {
        public Arrival(StateMachine stateMachine)
            : base(stateMachine)
        {
            // le numéro de ticket est renseigné dans le tableau de l'assistante
            // le technicien en charge est également signalé dans le tableau de l'assistante
            // l'assistante signale qu'elle a complété les tâches au fur et à mesure qu'elle les exécute
            // le nouvel arrivant valide
            // la demande est clôturée
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