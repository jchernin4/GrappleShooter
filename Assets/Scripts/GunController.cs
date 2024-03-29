using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class GunController : NetworkBehaviour {
    public Transform playerControl;
    public Transform shooterSphere;
    public GameObject bulletPrefab;
    public float bulletSpeed = 40f;
    public float bulletLife = 10f;
    public GameObject bulletExit;

    void Update() {
        if (!isLocalPlayer) {
            return;
        }
        
        shooterSphere.transform.position = playerControl.position;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        
        if (plane.Raycast(ray, out float distance)) {
            Vector3 target = ray.GetPoint(distance);

            Vector3 direction = target - shooterSphere.transform.position;
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            shooterSphere.transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
        
        if (Input.GetMouseButtonDown(0)) {
            CmdShootBullet(GetComponent<PlayerControl>());
        }

        /* Vector3 mousePos = Input.mousePosition;
         mousePos.z = Camera.main.nearClipPlane;
         Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
         worldPosition.y = 0.5f;

        transform.LookAt (mousePos);
       */
    }

    [Command]
    void CmdShootBullet(PlayerControl parent) {
        GameObject spawnedBullet = Instantiate(bulletPrefab, bulletExit.transform.position, bulletExit.transform.rotation);
        spawnedBullet.GetComponent<Rigidbody>().AddForce(spawnedBullet.transform.forward * bulletSpeed, ForceMode.Impulse);
        spawnedBullet.GetComponent<BulletScript>().parent = parent;
        NetworkServer.Spawn(spawnedBullet);
    }
}