using StarterAssets;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponSwitcher weaponSwitcher;
    [SerializeField] private StarterAssetsInputs inputs;
    
    public WeaponBase EquippedWeapon => weaponSwitcher.CurrentWeapon;

    private void Update()
    {
        if (inputs == null) return;

        if (inputs.shoot)
            TryFire();

        if (inputs.reload)
            TryReload();
    }

    public void TryFire()
    {
        if (EquippedWeapon != null)
            EquippedWeapon.Fire();
    }

    public void TryReload()
    {
        if (EquippedWeapon != null)
            EquippedWeapon.Reload();
    }
}