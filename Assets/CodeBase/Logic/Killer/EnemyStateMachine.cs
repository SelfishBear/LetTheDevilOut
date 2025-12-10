namespace CodeBase.Logic.Killer
{
    public class EnemyStateMachine
    {
        private IEnemyState _currentState;

        public void ChangeState<T>(T newState) where T : IEnemyState
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}