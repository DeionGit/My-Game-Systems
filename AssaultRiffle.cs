using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRiffle : Weapon
{
    [SerializeField] Transform firePoint;
    [SerializeField] ParticleSystem fxFlash;
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] float timeToNextShoot;
    [SerializeField] TypeDamage typeDamage;

    [Header("Chamber Animation")]
    [SerializeField] ChamberAnimation chamberAnim;
    [Header("Testing")]
    public bool testing = false;
    public KeyCode shoot;
    public KeyCode reload;

    void Start()
    {
        InitilizeWeapon(timeToNextShoot, typeDamage);
    }

   
    void Update()
    {
        if ((Input.GetKeyDown(shoot) || Input.GetKey(shoot)) && testing) ShootProjectile();

        if (Input.GetKeyDown(reload)&& testing) LoadingNextBulletOnChamber();
    }

     public void ShootProjectile()
    {
        if (chamberBullet > 0 && canShootWeapon)
        {
            chamberBullet--;
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            fxFlash.Play();
            chamberAnim.ShutterAnimationTrigger();
            Invoke("LoadingNextBulletOnChamber", timeToNextShoot);
        }
    }

}
