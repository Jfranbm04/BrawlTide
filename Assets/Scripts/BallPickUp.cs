using UnityEngine;

public class BallPickup : MonoBehaviour
{
    // Cantidad de bolas que se añadirán al jugador al recogerlo
    public int ballsToAdd = 10;

    // Se llama cuando un Collider 2D entra en contacto con este objeto
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Verificamos que el objeto que colisionó sea el jugador.
        // Asume que tu jugador tiene el tag "Player" (es lo estándar).
        if (other.CompareTag("Player"))
        {
            // 2. Intentamos obtener el script PlayerAttackController del jugador.
            PlayerAttackController playerAttack = other.GetComponent<PlayerAttackController>();

            // Si el PlayerAttackController no está directamente en el Collider que golpeó lo buscamos en el objeto raíz del jugador.
            if (playerAttack == null)
            {
                playerAttack = other.GetComponentInParent<PlayerAttackController>();
            }

            // 3. Si encontramos el script, añadimos las bolas.
            if (playerAttack != null)
            {
                playerAttack.AddBalls(ballsToAdd);

                // 4. Destruimos este objeto de recogida.
                Destroy(gameObject);
            }
        }
    }
}