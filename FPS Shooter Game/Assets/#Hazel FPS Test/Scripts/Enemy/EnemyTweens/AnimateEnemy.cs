using System;
using UnityEngine;
using DG.Tweening;

public class AnimateEnemy : MonoBehaviour
{
    public DOTweenAnimation damageTween, deathTween;
    public string damageTweenID = "TakingDamage";
    public string deathTweenID = "Death";
    private HealthComponent healthComponent;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
    }

    private void OnEnable()
    {
        healthComponent.OnDamaged += PlayDamageTween;
        healthComponent.OnDeath += PlayDeathTween;
    }
    private void OnDisable()
    {
        healthComponent.OnDamaged -= PlayDamageTween;
        healthComponent.OnDeath -= PlayDeathTween;
    }

    public void PlayDamageTween()
    {
        damageTween.DORestartById(damageTweenID);
    }

    public void PlayDeathTween()
    {
        deathTween.DOPlayById(deathTweenID);
        // DOTween.Play(deathTweenID);
    }
}