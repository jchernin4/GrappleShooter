using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScirpt : MonoBehaviour {
    public GameObject bulletPrefab;
    public float bulletSpeed = 50f;
    public GameObject bulletExit;


    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            GameObject spawnedBullet =
                Instantiate(bulletPrefab, bulletExit.transform.position, bulletExit.transform.rotation);
            spawnedBullet.GetComponent<Rigidbody>()
                .AddForce(spawnedBullet.transform.forward * bulletSpeed, ForceMode.Impulse);
        }
    }
}