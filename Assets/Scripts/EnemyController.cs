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

    private float leftLimitX;
    private float rightLimitX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Inicializa el SpriteRenderer en Start
        //spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // Guardamos la posición mundial (X) de los límites. 
        // Esta posición es FIJA y no cambiará aunque los objetos hijo se muevan.
        if (leftLimit != null)
        {
            leftLimitX = leftLimit.position.x;
        }
        if (rightLimit != null)
        {
            rightLimitX = rightLimit.position.x;
        }
    }

    void Update()
    {
        // 1. Determinar la direcci�n de movimiento
        float direction = movingRight ? 1f : -1f;

        // 2. Aplicar el movimiento
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        animator.SetBool("isRunning", Mathf.Abs(rb.linearVelocity.x) > 0.01f);

        // ** COMPRARACIÓN CONTRA LA POSICIÓN MUNDIAL GUARDADA **
        if (movingRight)
        {
            // Usamos la variable FIJA 'rightLimitX'
            if (transform.position.x >= rightLimitX)
            {
                movingRight = false;
                // Debug.Log("Giro a la izquierda en: " + rightLimitX); // Línea para depurar
                Flip();
            }
        }
        else
        {
            // Usamos la variable FIJA 'leftLimitX'
            if (transform.position.x <= leftLimitX)
            {
                movingRight = true;
                // Debug.Log("Giro a la derecha en: " + leftLimitX); // Línea para depurar
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