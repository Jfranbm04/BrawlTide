using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public Transform leftLimit;
    public Transform rightLimit;


    private Rigidbody2D rb;
    private bool movingRight = true; // Indica la direcci�n actual
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Inicializa el SpriteRenderer en Start
        //spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. Determinar la direcci�n de movimiento
        float direction = movingRight ? 1f : -1f;

        // 2. Aplicar el movimiento
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        animator.SetBool("isRunning", Mathf.Abs(rb.linearVelocity.x) > 0.01f);

        // 3. Revisar los l�mites para voltear
        if (movingRight)
        {
            // Si supera el l�mite derecho, cambiamos de direcci�n
            if (transform.position.x >= rightLimit.position.x)
            {
                movingRight = false;
                Flip();
            }
        }
        else // Movi�ndose a la izquierda
        {
            // Si supera el l�mite izquierdo, cambiamos de direcci�n
            if (transform.position.x <= leftLimit.position.x)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. Intentamos obtener el script Health del objeto colisionado
        Health playerHealth = collision.gameObject.GetComponent<Health>();

        // 2. Verificamos si encontramos el script Health Y si el objeto es el jugador
        if (playerHealth != null && collision.gameObject.CompareTag("Player"))
        {
            // 3. Quitamos 1 punto de vida al jugador
            playerHealth.TakeDamage(1);

            // Opcional: Si quieres que el enemigo rebote o se detenga un momento,
            // puedes añadir aquí la lógica. Por ahora, solo quita vida.
        }
    }

    void Flip()
    {
        // Objeto que representa la nueva rotación
        Quaternion targetRotation;

        // Si movingRight es true, la rotación es 0 grados (mirando hacia adelante/derecha)
        if (movingRight)
        {
            // Mirando hacia la derecha (0 grados en Y)
            targetRotation = Quaternion.Euler(0, 0, 0);
        }
        // Si movingRight es false, la rotación es 180 grados (mirando a la izquierda)
        else
        {
            // Mirando hacia la izquierda (180 grados en Y)
            targetRotation = Quaternion.Euler(0, 180, 0);
        }

        // Aplica la rotación instantáneamente al objeto padre
        transform.rotation = targetRotation;
    }


}