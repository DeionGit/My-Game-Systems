using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    float fireRateWeapon;
    
    TypeDamage typeDamageWeapon;

    //----Cargador-------
    [SerializeField] WeaponMag weaponMag;
    public int chamberBullet;
    public bool canShootWeapon;

    public enum TypeDamage
    {
        Kinetic,
        Plasma,
        Virus
    }
    void Start()
    {

    }

    void Update()
    {
       
    }

    

    public void InitilizeWeapon(float fireRate, TypeDamage typeDamage)
    {
        fireRateWeapon = fireRate;

        typeDamageWeapon = typeDamage;

    }

    public void GetAndReleaseWeaponMag(WeaponMag mag, bool get)
    {
        if (get)
        {
            weaponMag = mag;

        }else
        {
            weaponMag = null;
        }
    }

    bool CanLoadChamber()
    {
        if (weaponMag != null && weaponMag.GetCurrentAmmount() > 0)
        {
            weaponMag.UseBullets(1);
            chamberBullet++;
            return true;
        }else if( weaponMag.GetCurrentAmmount() == 0)
        {
            weaponMag.EjectMag();
            return false;
        }else
        {
            return false;
        }
    }

    public void LoadingNextBulletOnChamber()
    {
        canShootWeapon = CanLoadChamber();
    }

    public void SetWeaponMag(WeaponMag mag)
    {
        weaponMag = mag;
    }
}
