using UnityEngine;
using UnityEngine.UI; // Necesario para la barra de vida (Slider)

public class BossController : MonoBehaviour
{
    // ====================================================================
    // 1. PROPIEDADES DE SALUD (Tomadas de Health.cs)
    // ====================================================================
    [Header("Salud y UI")]
    public int maxHealth = 10; // Vida aumentada para el Jefe
    public GameObject healthBarUIPrefab;
    public float healthBarOffsetY = 0.5f;

    private int currentHealth;
    private GameObject healthBarInstance;
    private Slider healthSlider;


    // ====================================================================
    // 2. PROPIEDADES DE MOVIMIENTO (Tomadas de EnemyController.cs)
    // ====================================================================
    [Header("Movimiento y Patrulla")]
    public float moveSpeed = 4f;
    public Transform leftLimit;
    public Transform rightLimit;

    private Rigidbody2D rb;
    private bool movingRight = true;
    private Animator animator;

    private float leftLimitX;
    private float rightLimitX;


    // ====================================================================
    // 3. PROPIEDADES DE ATAQUE (Tomadas de BossContactDamage.cs)
    // ====================================================================
    [Header("Ataque y Knockback")]
    [Tooltip("Da�o que el Jefe inflige al jugador al tocarlo.")]
    public int damageToPlayer = 2;
    [Tooltip("Fuerza con la que el jugador ser� lanzado (knockback).")]
    public float knockbackForce = 15f;


    // ====================================================================
    // M�TODOS DE START Y UPDATE
    // ====================================================================

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // 1. Inicializaci�n de L�mites de Patrulla
        if (leftLimit != null)
        {
            leftLimitX = leftLimit.position.x;
        }
        if (rightLimit != null)
        {
            rightLimitX = rightLimit.position.x;
        }

        // 2. Inicializaci�n de la Salud y la Barra de Vida
        currentHealth = maxHealth;
        if (healthBarUIPrefab != null)
        {
            healthBarInstance = Instantiate(healthBarUIPrefab, transform.position, Quaternion.identity);
            healthSlider = healthBarInstance.GetComponentInChildren<Slider>();
            if (healthSlider != null)
            {
                healthSlider.maxValue = maxHealth;
                healthSlider.value = currentHealth;
                healthBarInstance.SetActive(false); // Oculta la barra al inicio
            }
        }
    }

    void Update()
    {
        // 1. L�gica de Patrulla y Movimiento (de EnemyController.cs)
        float direction = movingRight ? 1f : -1f;

        // Utilizamos linearVelocity para mantener la consistencia con tu c�digo
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        if (animator != null)
        {
            animator.SetBool("isRunning", Mathf.Abs(rb.linearVelocity.x) > 0.01f);
        }

        // 2. L�gica de Giro al llegar a los l�mites
        if (movingRight)
        {
            if (transform.position.x >= rightLimitX)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            if (transform.position.x <= leftLimitX)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    void LateUpdate()
    {
        // Mantiene la barra de vida sobre el jefe (de Health.cs)
        if (healthBarInstance != null)
        {
            Vector3 offset = new Vector3(0, healthBarOffsetY, 0);
            healthBarInstance.transform.position = transform.position + offset;
        }
    }


    // ====================================================================
    // M�TODOS DE ATAQUE Y DA�O RECIBIDO
    // ====================================================================

    // Funci�n llamada por los ataques del jugador para infligir da�o al Jefe
    public void TakeDamage(int damageAmount)
    {
        // 1. Mostrar la barra de vida
        if (healthBarInstance != null)
        {
            healthBarInstance.SetActive(true);
        }

        currentHealth -= damageAmount;

        // 2. Actualizar la barra de vida
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        // 3. Comprobar si est� muerto
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // L�gica de ataque y knockback al jugador (de BossContactDamage.cs)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Solo actuamos si colisionamos con el Jugador
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            Rigidbody2D playerRB = other.GetComponent<Rigidbody2D>();

            // 1. Aplicar Da�o
            if (playerHealth != null)
            {
                // Llama al Health.cs del jugador con el valor de da�o configurado (2)
                playerHealth.TakeDamage(damageToPlayer);
            }

            // 2. Aplicar Lanzamiento (Knockback)
            if (playerRB != null)
            {
                // a. Calcular la direcci�n desde el Jefe (this) al Jugador (other)
                Vector2 direction = other.transform.position - transform.position;
                direction.Normalize();

                // b. Componente vertical de lanzamiento
                direction = (direction + Vector2.up * 0.5f).normalized;

                // c. Aplicar la fuerza de impulso
                playerRB.linearVelocity = Vector2.zero; // Resetea la velocidad para un impulso limpio
                playerRB.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }


    // ====================================================================
    // M�TODOS AUXILIARES
    // ====================================================================

    void Die()
    {
        // Limpiar la barra de vida
        if (healthBarInstance != null)
        {
            Destroy(healthBarInstance);
        }

        // Destruir el jefe (aqu� puedes a�adir efectos de muerte o spawnear un premio)
        Destroy(gameObject);
    }

    void Flip()
    {
        // Voltea el sprite para cambiar la direcci�n de mirada (de EnemyController.cs)
        Quaternion targetRotation;

        if (movingRight)
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            targetRotation = Quaternion.Euler(0, 180, 0);
        }

        transform.rotation = targetRotation;
    }
}