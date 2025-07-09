using UnityEngine;

public class RaycastWeapon : WeaponBase
{
    [SerializeField] private GameObject weaponMesh;
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask hitMask;

    protected override void OnFire()
    {
        if (config.muzzleFlashPrefab)
        {
            var muzzleFlashRot = Quaternion.LookRotation(-muzzlePoint.forward);
            var go = Instantiate(config.muzzleFlashPrefab,  muzzlePoint.position, muzzleFlashRot);
            Destroy(go, 1f);
        }

        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, config.range, hitMask))
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(config.damage);
            }

            Debug.Log("Raycast hit: " + hit.collider.name);
        }

    }
}