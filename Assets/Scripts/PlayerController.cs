using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // VARIABLES DE MOVIMIENTO Y LÍMITE
    public float moveSpeed = 5f;
    public float jumpForce = 10f; 

    public float minX = -10f; 
    public float maxX = 10f;  

    // VARIABLES DE DOBLE SALTO (Nuevas)
    public int maxJumps = 2; // Número máximo de saltos permitidos (2 para doble salto)
    private int remainingJumps; // Saltos restantes en el aire

    // VARIABLES DE SALTO Y COMPONENTES
    public Transform groundCheck; 
    public LayerMask groundLayer; 
    
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 1. COMPROBACIÓN DE SUELO
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // 1.1. LÓGICA DE REINICIO DE SALTO (NUEVO)
        if (isGrounded)
        {
            // Si el personaje está tocando el suelo, reinicia los saltos disponibles.
            remainingJumps = maxJumps;
        }

        // 2. LÓGICA DE SALTO (MODIFICADO)
        // El salto se permite si se pulsa "Jump" Y (hay saltos restantes > 0)
        if (Input.GetButtonDown("Jump") && remainingJumps > 0)
        {
            // Aplica la fuerza de salto
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            
            // Consume un salto
            remainingJumps--;
        }

        // 3. MOVIMIENTO Y CONTROL DE ANIMATOR
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
        
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // 4. LÓGICA DE ATAQUE
        // (0 = click izquierdo, 1 = click derecho, 2 = click central)
        if (Input.GetMouseButtonDown(0))
        {
            // Activa el Trigger "Attack" en el Animator
            animator.SetTrigger("Attack");
        }

        // 5. VOLTEAR EL SPRITE (FLIP)
        if (horizontalInput > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < -0.01f)
        {
            spriteRenderer.flipX = true;
        }

        // 6. APLICAR LÍMITES DE MAPA (CLAMPING)
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);
        transform.position = currentPosition;
    }
}