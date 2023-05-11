using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShellEjector : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] float minEjectForce;
    [SerializeField] float maxEjectForce;

    bool ejected = false;
    private void OnEnable()
    {
        ejected = true;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void AddEjectForce(Vector3 ejectDirection)
    {
        if (rb.isKinematic) rb.isKinematic = false;

        float ejectForce = Random.Range(minEjectForce, maxEjectForce);
        rb.AddForceAtPosition(ejectDirection * ejectForce,transform.position, ForceMode.Force);
        ejected = true;
        Destroy(gameObject, 3f);
    }
    

}
