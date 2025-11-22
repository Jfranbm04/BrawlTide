using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Necesario para Coroutines (Parpadeo/Invulnerabilidad)
using UnityEngine.SceneManagement; // Necesario para la función de Game Over

public class Health : MonoBehaviour
{
    // VARIABLES PÚBLICAS
    public int maxHealth = 2;
    public GameObject healthBarUIPrefab;
    public float healthBarOffsetY = 0.5f;

    // >> NUEVAS VARIABLES PARA INVULNERABILIDAD <<
    [Header("Invulnerabilidad (Solo Player)")]
    public float invulnerabilityTime = 2f; // Duración de invulnerabilidad en segundos
    public float blinkInterval = 0.1f; // Frecuencia de parpadeo

    // VARIABLES PRIVADAS
    private int currentHealth;
    private GameObject healthBarInstance;
    private Slider healthSlider;
    private bool isInvulnerable = false; // Bandera de invulnerabilidad
    private SpriteRenderer spriteRenderer; // Para manejar el parpadeo

    void Start()
    {
        currentHealth = maxHealth;

        // Obtener el SpriteRenderer para el parpadeo, solo si es el Player
        if (gameObject.CompareTag("Player"))
        {
            // El componente SpriteRenderer debe estar en el mismo GameObject que Health.cs
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // 1. Instanciar la Barra de Vida (UI)
        if (healthBarUIPrefab != null)
        {
            healthBarInstance = Instantiate(healthBarUIPrefab, transform.position, Quaternion.identity);

            healthSlider = healthBarInstance.GetComponentInChildren<Slider>();
            if (healthSlider != null)
            {
                healthSlider.maxValue = maxHealth;
                healthSlider.value = currentHealth;

                // Ocultar la barra al inicio (Vida completa)
                healthBarInstance.SetActive(false);
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        // 1. Si es el jugador E invulnerable, ignorar el daño y salir.
        if (isInvulnerable && gameObject.CompareTag("Player"))
        {
            return;
        }

        // 2. Mostrar la barra de vida
        if (healthBarInstance != null)
        {
            healthBarInstance.SetActive(true);
        }

        currentHealth -= damageAmount;

        // 3. Actualizar la barra de vida
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        // 4. Comprobar si está muerto
        if (currentHealth <= 0)
        {
            Die();
        }
        // 5. Si es el jugador y sigue vivo, iniciar invulnerabilidad
        else if (gameObject.CompareTag("Player"))
        {
            StartCoroutine(InvulnerabilityCoroutine());
        }
    }

    void Die()
    {
        // Limpiar la barra de vida
        if (healthBarInstance != null)
        {
            Destroy(healthBarInstance);
        }

        // Lógica de muerte:
        if (gameObject.CompareTag("Player"))
        {
            // *** LÓGICA DE GAME OVER ***
            Debug.Log("¡Juego Terminado! El jugador ha muerto.");

            // Destruir el jugador. Para reiniciar, puedes cargar la escena actual:
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name); 

            Destroy(gameObject);
        }
        else // Enemigo (o cualquier otra cosa que no sea el Player)
        {
            // ¡Destruir el enemigo! (Lógica solicitada previamente)
            Destroy(gameObject);
        }
    }

    // Coroutine para manejar el tiempo de invulnerabilidad y el efecto visual
    IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;

        // Si no tenemos SpriteRenderer, no podemos parpadear
        if (spriteRenderer == null)
        {
            Debug.LogError("El jugador necesita un SpriteRenderer para el efecto de parpadeo.");
            // Esperamos el tiempo de invulnerabilidad de todas formas
            yield return new WaitForSeconds(invulnerabilityTime);
            isInvulnerable = false;
            yield break; // Salir de la coroutine
        }

        float endTime = Time.time + invulnerabilityTime;

        // Parpadeo
        while (Time.time < endTime)
        {
            // Alternar visibilidad (parpadeo)
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }

        // Asegurarse de que el sprite esté visible al finalizar
        spriteRenderer.enabled = true;
        isInvulnerable = false;
    }

    void LateUpdate()
    {
        if (healthBarInstance != null)
        {
            // Usamos la variable pública para el desplazamiento Y
            Vector3 offset = new Vector3(0, healthBarOffsetY, 0);
            healthBarInstance.transform.position = transform.position + offset;
        }
    }
}