using UnityEngine;

public class CrosshairBinder : MonoBehaviour
{
    [SerializeField] private WeaponSwitcher weaponSwitcher;
    [SerializeField] private CrosshairUIManager crosshairUI;

    private void OnEnable()
    {
        weaponSwitcher.OnWeaponSwitched += HandleWeaponSwitched;
    }
    private void OnDestroy()
    {
        weaponSwitcher.OnWeaponSwitched -= HandleWeaponSwitched;
    }
    private void HandleWeaponSwitched(WeaponConfigSO config)
    {
        crosshairUI.UpdateCrosshair(config.crosshairSprite, config.crosshairScale);
    }
}