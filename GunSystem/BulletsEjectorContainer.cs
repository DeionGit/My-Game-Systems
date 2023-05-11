using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsEjectorContainer : MonoBehaviour
{

    public List<ProjectileShellEjector> bulletsToEject;

    [SerializeField] GameObject bulletsToEjectPrefab;
    [SerializeField] GameObject bulletToEject;
    [SerializeField] int numberOfBulletsPool;

    int bulletIndex = 0;
    void Start()
    {
        CreateAndInsertBulletsToEject(bulletsToEjectPrefab);
    }

    void CreateAndInsertBulletsToEject(GameObject bulletPref)
    {
        for (int i = 0; i < numberOfBulletsPool; i++)
        {
            GameObject bullet = Instantiate(bulletPref, transform);
            bullet.transform.localPosition = new Vector3(0, 0, 0);
            bulletsToEject.Add(bullet.GetComponent<ProjectileShellEjector>());
            bullet.SetActive(false);
        }

    }

    public void PrepareAndGetBullet()
    {
        bulletToEject = bulletsToEject[bulletIndex].gameObject;
        bulletToEject.SetActive(true);
        bulletToEject.transform.localPosition = new Vector3(0, 0, 0);

    }
   
    public void EjectBulletFromChamber(Vector3 direction)
    {
        bulletsToEject[bulletIndex].AddEjectForce(direction);
        bulletToEject = null;
        bulletIndex++;
        CheckAndResetIndex(ref bulletIndex);
    }

    void CheckAndResetIndex(ref int index)
    {
        if (index == bulletsToEject.Count)
        {
            index = 0;
        }
    }
}
