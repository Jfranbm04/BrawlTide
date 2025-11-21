using UnityEngine;

public class ThrowableBall : MonoBehaviour
{
    // Propiedades configurables
    public float speed = 15f;
    public int damageAmount = 1;
    public float lifetime = 2f;

    // Referencia al Rigidbody2D (se asigna automáticamente)
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // La bola se lanza en la dirección derecha local (esto se ajusta al instanciarla)
        rb.linearVelocity = transform.right * speed;

        // Auto-destrucción
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            Health enemyHealth = hitInfo.GetComponent<Health>();
            if (enemyHealth != null)
            {
                // Llama a la función de tu script Health.cs
                enemyHealth.TakeDamage(1);
            }
            Destroy(gameObject);
        }
        else if (!hitInfo.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}