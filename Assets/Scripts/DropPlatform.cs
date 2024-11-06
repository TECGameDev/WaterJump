using UnityEngine;

public class DropPlatform : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D victim)
    {
        Debug.Log("Triggered!");

        if (victim.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player touched the platform!");
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
