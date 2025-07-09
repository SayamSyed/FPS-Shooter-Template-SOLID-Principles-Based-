using System;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public event Action<WeaponConfigSO> OnWeaponSwitched;

    [SerializeField] private WeaponBase[] weapons;
    [SerializeField] private int startIndex = 0;

    private int currentIndex = 0;
    public WeaponBase CurrentWeapon => weapons[currentIndex];

    private void Start()
    {
        EquipWeapon(startIndex);
    }

    public void EquipNext()
    {
        currentIndex = (currentIndex + 1) % weapons.Length;
        EquipWeapon(currentIndex);
    }

    public void EquipPrevious()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = weapons.Length - 1;
        EquipWeapon(currentIndex);
    }

    private void EquipWeapon(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == index);
        }

        currentIndex = index;

        // Fire event for UI or any other systems
        OnWeaponSwitched?.Invoke(weapons[index].config); 
    }
}