using UnityEngine;

public class HitboxDamage : MonoBehaviour
{
    public int damage = 1;

    // Se llama cuando el trigger colisiona con otro collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Intentamos obtener el script Health del objeto colisionado
        Health targetHealth = other.GetComponent<Health>();

        // Si el objeto colisionado tiene el script Health
        if (targetHealth != null)
        {
            // Solo golpeamos si el objeto NO somos nosotros mismos (el jugador)
            if (other.gameObject != transform.parent.gameObject)
            {
                // Aplicamos el daño
                targetHealth.TakeDamage(damage);

                // Opcional: Desactivamos el hitbox para evitar múltiples golpes en el mismo frame
                // gameObject.SetActive(false); 
            }
        }
    }
}