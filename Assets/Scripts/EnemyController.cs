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
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    void Flip()
    {
       // Si se mueve a la derecha, NO volteamos el sprite (flipX = false)
        // Si se mueve a la izquierda, SÍ volteamos el sprite (flipX = true)
        spriteRenderer.flipX = !movingRight;
    }
}