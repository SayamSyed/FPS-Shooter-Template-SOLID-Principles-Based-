using UnityEngine;

public class PatrolState : IEnemyState
{
    private EnemyAI enemy;

    public void EnterState(EnemyAI enemy)
    {
        this.enemy = enemy;
        enemy.agent.isStopped = false;
        MoveToNextPoint();
    }

    public void UpdateState()
    {
        // if (Vector3.Distance(enemy.transform.position, enemy.player.position) <= enemy.detectionRange)
        // {
        //     enemy.SwitchState(new ChaseState());
        //     return;
        // }
        if (enemy.CanSeePlayer())
        {
            enemy.SwitchState(new ChaseState());
        }

        if (!enemy.agent.pathPending && enemy.agent.remainingDistance < 0.5f)
        {
            MoveToNextPoint();
        }
    }

    public void ExitState() {}

    private void MoveToNextPoint()
    {
        if (enemy.patrolPoints.Length == 0) return;
        enemy.agent.destination = enemy.patrolPoints[enemy.patrolIndex].position;
        enemy.patrolIndex = (enemy.patrolIndex + 1) % enemy.patrolPoints.Length;
    }
}