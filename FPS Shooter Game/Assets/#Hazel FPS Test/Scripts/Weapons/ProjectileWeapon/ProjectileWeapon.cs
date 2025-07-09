using UnityEngine;

public class ProjectileWeapon : WeaponBase
{
    [SerializeField] private GameObject weaponMesh;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private GameObject grenadePrefab;
    [SerializeField] private float launchForce = 20f;

    protected override void OnFire()
    {
        if (grenadePrefab == null || firePoint == null)
        {
            Debug.LogWarning("Missing grenade prefab or fire point");
            return;
        }

        GameObject grenade = Instantiate(grenadePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(firePoint.forward * launchForce, ForceMode.VelocityChange);
        }

        // Optional: play fire animation or flash
    }

}