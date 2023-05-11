using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using Autohand;
using UniRx;
using UniRx.Triggers;

public class ChamberAnimation : MonoBehaviour
{
    [SerializeField] float tweenTime;
    [SerializeField] float unloadChamberPosition;
    [SerializeField] Ease shutterEase;

    [SerializeField] Transform ejectTransform;
    [SerializeField] GameObject bulletShellPrefab;
    bool inAnimation = false;

    [SerializeField] GameObject GunCollidersObj;

    [SerializeField] float loadChamberShutterPos;
    [SerializeField] float unloadChamberShutterPos;
    [SerializeField] Weapon weapon;
    bool canManualReload = true;
    Grabbable grabbable;
    [SerializeField] BulletsEjectorContainer bulletsEjectorCont;
    ProjectileShellEjector activeBulletToEject;
    int loopCount = 0;
    void Start()
    {
        grabbable = GetComponent<Grabbable>();
        weapon = GunCollidersObj.GetComponent<Weapon>();
        DisableCollisionSet.IgnoreSetOfColliders(gameObject, GunCollidersObj, true);
        
        loadChamberShutterPos = transform.localPosition.z;

        IObservable<Unit> update = this.UpdateAsObservable();

        update
            .Where(_ => grabbable.IsHeld())
            .Subscribe(_ => ManualChamberReload());

    }

    public void ShutterAnimationTrigger()
    {
        if (!inAnimation)
        {
            inAnimation = true;
            transform.DOLocalMoveZ(unloadChamberPosition, tweenTime).SetEase(shutterEase).SetLoops(2, LoopType.Yoyo)
                .OnStepComplete(CountLoopsToEjectShell)
                .OnComplete(() =>
                {
                    inAnimation = false;
                });
        }
    }
    void ManualChamberReload()
    {
        if (transform.localPosition.z <= unloadChamberShutterPos && canManualReload)
        {
            canManualReload = false;
            weapon.LoadingNextBulletOnChamber();
            bulletsEjectorCont.EjectBulletFromChamber(ejectTransform.forward);
        }
        if (transform.localPosition.z >= loadChamberShutterPos && !canManualReload)
        {
            canManualReload = true;
            bulletsEjectorCont.PrepareAndGetBullet();
        }
        
    }
   
    void CountLoopsToEjectShell()
    {
        if (loopCount == 0)
        {
            loopCount++;
            EjectBulletShell();
        }else
        {
            loopCount = 0;
        }
    }
    void EjectBulletShell()
    {
        GameObject shell = Instantiate(bulletShellPrefab, ejectTransform.position, bulletShellPrefab.transform.rotation);
        shell.GetComponent<ProjectileShellEjector>().AddEjectForce(ejectTransform.forward);
    }

}
