using UnityEngine;
using UnityEngine.Networking;

public class AlienBullet : MonoBehaviour
{
    private const float moveSpeed = 0.1f;

    private void FixedUpdate()
    {
        transform.Translate(0, -moveSpeed, 0);

        if (transform.position.y < -4.0f)
        {
            NetworkServer.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!NetworkServer.active)
            return;

        PlayerControl hitPlayer = collider.gameObject.GetComponent<PlayerControl>();
        if (hitPlayer != null)
        {
            hitPlayer.HitByBullet();
            NetworkServer.Destroy(gameObject);
            return;
        }

        Shield hitShield = collider.gameObject.GetComponent<Shield>();
        if (hitShield != null)
        {
            NetworkServer.Destroy(hitShield.gameObject);
            NetworkServer.Destroy(gameObject);
        }
    }
}