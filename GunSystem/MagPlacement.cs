using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MagPlacement : MonoBehaviour
{
    [SerializeField] Weapon weaponToLoad;

    [SerializeField] Vector3 placementOffset;
    [SerializeField] Vector3 rotationOffset;


    bool magPlaced = false;
    void Start()
    {
        IObservable<Collider> onTriggerEnterObservable = this.OnTriggerEnterAsObservable();

        onTriggerEnterObservable
            .Where(GetMag)
            .Subscribe(PlaceMag);

    }

    bool GetMag(Collider collider)
    {
        if (collider.GetComponent<WeaponMag>() && !magPlaced)
        {
            return true;
        }
        else return false;
    }

    void PlaceMag(Collider collider)
    {
        magPlaced = true;
        WeaponMag weaponMag = collider.GetComponent<WeaponMag>();

        weaponMag.SetMagToPlacement(transform);
        GiveMagToWeapon(weaponMag);

    }


    void GiveMagToWeapon(WeaponMag mag)
    {
        weaponToLoad.SetWeaponMag(mag); 
    }
}
