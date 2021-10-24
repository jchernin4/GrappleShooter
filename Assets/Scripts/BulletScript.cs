using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    public ParticleSystem impactSystem;

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == 6) {
            return;
        }

        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<PlayerControl>().TakeDamage(25);
        }

        /*Vector3 pos = transform.forward.normalized;
        Vector3 rot = -collision.contacts[0].normal;*/
        ParticleSystem spawnedImpact = Instantiate(impactSystem, transform.position, Quaternion.Euler(90, 0, 0));
        Destroy(spawnedImpact, 1f);
        Destroy(gameObject, 0f);
    }
}