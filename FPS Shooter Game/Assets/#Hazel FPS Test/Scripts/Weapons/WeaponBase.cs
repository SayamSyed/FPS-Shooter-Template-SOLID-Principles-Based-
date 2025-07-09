using UnityEngine;
using System.Collections;

public abstract class WeaponBase : MonoBehaviour, IWeapon
{
    [SerializeField] public WeaponConfigSO config;
    
    protected int currentAmmo;
    protected float lastFireTime;
    
    public int AmmoLeft => currentAmmo;
    
    public bool CanFire => Time.time - lastFireTime >= 1f / config.fireRate && currentAmmo > 0;

    protected virtual void Start()
    {
        currentAmmo = config.ammoCapacity;
    }

    public virtual void Fire()
    {
        if (!CanFire) return;

        lastFireTime = Time.time;
        currentAmmo--;

        OnFire();
    }

    public virtual void Reload()
    {
        print("Reloading");

        currentAmmo = config.ammoCapacity;
    }

    protected abstract void OnFire(); // Raycast or projectile logic
}