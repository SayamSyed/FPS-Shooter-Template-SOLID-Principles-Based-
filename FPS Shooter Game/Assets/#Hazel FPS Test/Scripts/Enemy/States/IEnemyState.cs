public interface IEnemyState
{
    void EnterState(EnemyAI enemy);
    void UpdateState();
    void ExitState();
}