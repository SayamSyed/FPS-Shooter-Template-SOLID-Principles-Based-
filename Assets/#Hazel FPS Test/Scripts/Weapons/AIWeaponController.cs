using StarterAssets;
using UnityEngine;

public class AIWeaponController : MonoBehaviour
{
    [SerializeField] private WeaponSwitcher weaponSwitcher;
    
    public WeaponBase EquippedWeapon => weaponSwitcher.CurrentWeapon;

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