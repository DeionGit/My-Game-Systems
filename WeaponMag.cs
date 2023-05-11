using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponMag : MonoBehaviour
{
    [Header("Weapon Mag Physics")]
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider magCollider;
    [SerializeField] float minEjectForce;
    [SerializeField] float maxEjectForce;
    [SerializeField] EjectDirection ejectDirection;
    bool placed = false;

    [Space(40)]

    [Header("Weapon Mag Place Animation")]
    [SerializeField] float timeToPlace;
    [SerializeField] Ease easeToPlace;

    [Header("Color Mag Emmision")]
    [SerializeField] MeshRenderer rend;
    [SerializeField] Color fullAmmoEmmisionColor;
    [SerializeField] float hueColor;
    [SerializeField] float hueLoseColor;
    [Space(40)]

    [SerializeField] int maxAmmount;
    [SerializeField] int currentAmmount;
    void Start()
    {
        currentAmmount = maxAmmount;
        hueColor = 120f;
        rend = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        magCollider = GetComponent<Collider>();
        GetHueLoseColor();
    }
    void GetHueLoseColor()
    {
        hueLoseColor = 0.33f / maxAmmount;

        float S, V;
        Color.RGBToHSV(fullAmmoEmmisionColor, out hueColor, out S, out V);
    }
    public int GetCurrentAmmount()
    {
        return currentAmmount;
    }

    public void UseBullets(int numberOfBulletsToUse)
    {
        currentAmmount -= numberOfBulletsToUse;
        UpdateColorMagEmmision(); // Aqui actualizamos el EmmisionColor del Mag
        Debug.Log("Acurre");
    }

    public void SetMagToPlacement(Transform magPlacementTransform)
    {
        if (!placed)
        {
            placed = true;
            PlaceMagPhysics();
            MagPlaceAnimation(magPlacementTransform);
            
        }
    }

    public void EjectMag()
    {
        transform.SetParent(null);
        placed = false;
        rb.isKinematic = false;
        float ejectForce = Random.Range(minEjectForce, maxEjectForce);
        rb.AddForceAtPosition(GetEjectDirection() * ejectForce, transform.position, ForceMode.Force);
    }
    public enum EjectDirection
    {
        Right,
        Left,
        Forward,
        Backward,
        Down
    }
    Vector3 GetEjectDirection()
    {
        switch (ejectDirection)
        {
            case EjectDirection.Right:
                return transform.right;
                break;
            case EjectDirection.Left:
                return -transform.right;
                break;
            case EjectDirection.Forward:
                return transform.forward;
                break;
            case EjectDirection.Backward:
                return -transform.forward;
                break;
            case EjectDirection.Down:
                return -transform.up;
                break;
            default:
                return new Vector3(0,0,0);
                break;
        }
    }

    void PlaceMagPhysics()
    {
        magCollider.isTrigger = true;
        rb.isKinematic = true;
    }

    void MagPlaceAnimation(Transform magPlacementTransform)
    {
        //transform.DOMove(magPlacementTransform.position, timeToPlace).SetEase(easeToPlace);
        //transform.DORotateQuaternion(magPlacementTransform.rotation, timeToPlace).SetEase(easeToPlace)
        //    .OnComplete(() =>
        //    {
        //        transform.SetParent(magPlacementTransform, true);
        //    });

        transform.SetParent(magPlacementTransform, true);
        transform.DOLocalMove(new Vector3(0,0,0), timeToPlace).SetEase(easeToPlace);
        transform.DORotateQuaternion(magPlacementTransform.rotation, timeToPlace).SetEase(easeToPlace)
            .OnComplete(() =>
            {
                
            });
    }

    #region ColorMag

    private void UpdateColorMagEmmision()
    {
        hueColor -= hueLoseColor;
        fullAmmoEmmisionColor = Color.HSVToRGB(hueColor, 1, 1, true);
        rend.material.SetColor("_EmissionColor", fullAmmoEmmisionColor);
    }

    #endregion

}
