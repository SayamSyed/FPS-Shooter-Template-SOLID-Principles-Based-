using UnityEngine;

public class DieState : IEnemyState
{
    private EnemyAI enemy;

    public void EnterState(EnemyAI enemy)
    {
        this.enemy = enemy;

        enemy.agent.isStopped = true;
        enemy.enabled = false; // Optional: prevent updates

        if (enemy.deathEffect != null)
        {
            GameObject.Instantiate(enemy.deathEffect, enemy.transform.position, Quaternion.identity);
        }

        // Optional: trigger Animator death animation here
        // enemy.animator.SetTrigger("Die");

        Object.Destroy(enemy.gameObject, enemy.destroyDelay);
    }

    public void UpdateState() { }

    public void ExitState() { }
}