using UnityEngine;

public class BossContactDamage : MonoBehaviour
{
    // Variables ajustables en el Inspector
    [Header("Daño y Fuerza")]
    public int damageAmount = 2;        // Daño que inflige el jefe (2 vidas)
    public float knockbackForce = 15f;  // Fuerza con la que sera lanzado el jugador

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("¡Colisión detectada! Objeto tocado: " + other.gameObject.name);
        // Solo actuamos si colisionamos con el Jugador
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            Rigidbody2D playerRB = other.GetComponent<Rigidbody2D>();

            // 1. Aplicar Da�o (Usando el valor hardcodeado de 2)
            if (playerHealth != null)
            {
                // Llama a la funci�n TakeDamage del jugador con el valor de 2
                playerHealth.TakeDamage(damageAmount);
            }

            // 2. Aplicar Lanzamiento (Knockback)
            if (playerRB != null)
            {
                // a. Calcular la direcci�n desde el Jefe (this.transform) al Jugador
                // Vector2.up se a�ade para un ligero componente vertical de lanzamiento
                Vector2 direction = other.transform.position - transform.position;
                direction.Normalize(); // Asegura que la direcci�n sea unitaria (longitud 1)

                // b. Opcional: A�adir un peque�o componente vertical para que parezca un lanzamiento
                direction = (direction + Vector2.up * 0.5f).normalized;

                // c. Aplicar la fuerza de impulso
                playerRB.linearVelocity = Vector2.zero; // Resetea la velocidad del jugador
                playerRB.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

                // Opcional: Si el jugador tiene un script de movimiento (ej: PlayerMovement), 
                // deber�as desactivarlo temporalmente aqu� y volver a activarlo despu�s de un peque�o retraso.
            }
        }
    }
}