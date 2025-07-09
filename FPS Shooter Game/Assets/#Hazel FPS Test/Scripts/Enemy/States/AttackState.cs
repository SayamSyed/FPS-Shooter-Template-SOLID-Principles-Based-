using UnityEngine;

public class AttackState : IEnemyState
{
    private EnemyAI enemy;
    private float lastAttackTime;
    private float attackCooldown = 1f;

    private AIWeaponController weaponController;

    public void EnterState(EnemyAI enemy)
    {
        this.enemy = enemy;
        enemy.agent.isStopped = true;
        lastAttackTime = Time.time;

        // Get reference to WeaponController
        weaponController = enemy.GetComponentInChildren<AIWeaponController>();
    }

    public void UpdateState()
    {
        if (enemy.player == null)
        {
            enemy.SwitchState(new PatrolState());
            return;
        }

        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);

        if (distance > enemy.detectionConfig.attackRange)
        {
            enemy.SwitchState(new ChaseState());
            return;
        }

        // Face player
        Vector3 direction = (enemy.player.position - enemy.transform.position).normalized;
        direction.y = 0;
        enemy.transform.forward = direction;

        // Fire weapon
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;

            if (weaponController != null)
            {
                weaponController.TryFire(); // AI-friendly fire
            }
        }
    }

    public void ExitState()
    {
        if (enemy.agent != null)
            enemy.agent.isStopped = false;
    }
}