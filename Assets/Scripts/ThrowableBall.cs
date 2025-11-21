using UnityEngine;

public class ThrowableBall : MonoBehaviour
{
    // Propiedades configurables
    public float horizontalSpeed = 10f; // Velocidad horizontal inicial
    public float initialVerticalForce = 3f; // Un pequeño empuje inicial hacia arriba (opcional)
    public int damageAmount = 1;
    public float lifetime = 5f;

    private Rigidbody2D rb;
    private float launchDirectionX = 1f;

    public void SetDirection(float direction)
    {
        launchDirectionX = direction;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("ERROR: El Prefab de la bola necesita un componente Rigidbody2D.");
            Destroy(gameObject);
            return;
        }

        // Aplicamos la velocidad usando la dirección X que nos dio el jugador
        rb.linearVelocity = new Vector2(launchDirectionX * horizontalSpeed, initialVerticalForce);

        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            // Busca Health en el objeto golpeado o en su padre (para evitar problemas con jerarquías)
            Health enemyHealth = hitInfo.GetComponent<Health>();
            if (enemyHealth == null)
            {
                enemyHealth = hitInfo.GetComponentInParent<Health>();
            }

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }
            // Destruye la bola al golpear al enemigo (o a cualquier parte de él)
            Destroy(gameObject);
        }
        else if (!hitInfo.CompareTag("Player"))
        {
            // Destruye la bola al golpear cualquier otra cosa (suelo, paredes)
            Destroy(gameObject);
        }
    }
}