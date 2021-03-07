using UnityEngine;

public interface IWeapon
{
    void Init();
    void Fire(float shipRotationRads, Rigidbody2D rb2d, Vector3 shipPosition, GameObject target, int idx);
}