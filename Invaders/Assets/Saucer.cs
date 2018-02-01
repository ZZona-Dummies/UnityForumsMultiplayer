using UnityEngine;

public class Saucer : MonoBehaviour
{
    private float moveSpeed = 0.12f;

    private void FixedUpdate()
    {
        if (transform.position.x > 14)
        {
            Destroy(gameObject);
            return;
        }

        transform.Translate(moveSpeed, 0, 0);
    }
}