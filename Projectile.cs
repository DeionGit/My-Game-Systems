using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float projectileVelocity;


    [SerializeField] GameObject impactFXPrefab;
    private void Awake()
    {
        ReferenceDependencies();
    }
    void Start()
    {

        IObservable<Collision> collisionEnterObservable = this.OnCollisionEnterAsObservable();

        collisionEnterObservable
            .Subscribe(Impact);

        AddBulletVelocity();
    }

    void AddBulletVelocity()
    {
        rb.AddForce(transform.forward * projectileVelocity, ForceMode.Impulse);
    }
    void ReferenceDependencies()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Impact(Collision collision)
    {
        Instantiate(impactFXPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    
}
