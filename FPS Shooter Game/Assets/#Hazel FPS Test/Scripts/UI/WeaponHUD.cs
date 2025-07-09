using UnityEngine;
using TMPro;

public class WeaponHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gunNameText;
    [SerializeField] private TextMeshProUGUI ammoText;

    [SerializeField] private WeaponSwitcher weaponSwitcher;

    private WeaponBase currentWeapon;

    private void Start()
    {
        UpdateWeaponUI();
    }

    private void Update()
    {
        if (weaponSwitcher.CurrentWeapon != currentWeapon)
        {
            currentWeapon = weaponSwitcher.CurrentWeapon;
            UpdateWeaponUI();
        }

        UpdateAmmoUI();
    }

    void UpdateWeaponUI()
    {
        if (currentWeapon == null || currentWeapon.config == null) return;

        gunNameText.text = currentWeapon.config.weaponName;
    }

    void UpdateAmmoUI()
    {
        if (currentWeapon == null) return;

        ammoText.text = $"{currentWeapon.AmmoLeft}/{currentWeapon.config.ammoCapacity}";
    }
}