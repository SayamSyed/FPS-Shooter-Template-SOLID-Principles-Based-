public interface IWeapon
{
    void Fire();
    void Reload();
    bool CanFire { get; }
}