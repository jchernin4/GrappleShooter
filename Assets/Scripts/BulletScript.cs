using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class BulletScript : NetworkBehaviour {
    public ParticleSystem impactSystem;
    public PlayerControl parent;

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == 6) {
            return;
        }

        // TODO: This is ass
        // TODO: Actually this whole file is ass
        if (collision.gameObject.name.Equals("Plane") || collision.gameObject.name.Equals("Bullet(Clone)")) {
            return;
        }
        
        if (collision.gameObject.CompareTag("Player")) {
            CmdDealDamage(collision.gameObject.GetComponent<PlayerControl>(), 25);
        }

        CmdDestroyBullet();
    }

    [Command(requiresAuthority = false)]
    void CmdDealDamage(PlayerControl player, int damage) {
        player.health -= damage;
        if (player.health <= 0) {
            parent.score++;
            player.health = 100;
        }
    }

    [Command(requiresAuthority = false)]
    void CmdDestroyBullet() {
        //RpcSpawnParticles();
        NetworkServer.Destroy(gameObject);
    }

    [ClientRpc]
    void RpcSpawnParticles() {
        ParticleSystem spawnedImpact = Instantiate(impactSystem, transform.position, Quaternion.Euler(90, 0, 0));
        Destroy(spawnedImpact, 5f);
    }
}