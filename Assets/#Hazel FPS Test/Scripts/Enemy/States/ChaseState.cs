using UnityEngine;
public class ChaseState : IEnemyState
{
    private EnemyAI enemy;

    public void EnterState(EnemyAI enemy)
    {
        this.enemy = enemy;
        enemy.agent.isStopped = false;
    }

    public void UpdateState()
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);

        // if (distance > enemy.detectionRange)
        // {
        //     enemy.SwitchState(new PatrolState());
        // }
        if (!enemy.CanSeePlayer())
        {
            enemy.SwitchState(new PatrolState());
            return;
        }
        else if (distance <= enemy.detectionConfig.attackRange)
        {
            enemy.SwitchState(new AttackState());
        }
        else
        {
            Vector3 toPlayer = enemy.player.position - enemy.transform.position;
            if (toPlayer.magnitude > enemy.detectionConfig.stoppingDistance)
            {
                enemy.agent.SetDestination(enemy.player.position);
            }
            else
            {
                enemy.agent.ResetPath(); // stop moving
            }

        }
    }

    public void ExitState() {}
}