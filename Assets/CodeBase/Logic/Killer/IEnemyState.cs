namespace CodeBase.Logic.Killer
{
    public interface IEnemyState
    {
        public void Enter();
        public void Update();
        public void Exit();
    }
}