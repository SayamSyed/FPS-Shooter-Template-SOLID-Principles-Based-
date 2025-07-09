using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "FPS/WeaponConfig")]
public class WeaponConfigSO : ScriptableObject
{
    [Header("Weapon Details")]
    public string weaponName;
    public float fireRate;
    public int damage;
    public int ammoCapacity;
    public float range;
    public bool isAutomatic;
    public GameObject muzzleFlashPrefab;
   
    [Header("Crosshair UI")]
    public Sprite crosshairSprite;
    public Vector2 crosshairScale = Vector2.one;

}