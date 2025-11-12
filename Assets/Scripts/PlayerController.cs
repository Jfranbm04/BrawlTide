using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    // VARIABLES DE SALTO (Nuevas)
    public float jumpForce = 10f; // Fuerza de salto (ajusta en Inspector)
    public Transform groundCheck; // Objeto de detección de suelo (Arrastra aquí el objeto GroundCheck)
    public LayerMask groundLayer; // La capa "Ground" que creaste
    private bool isGrounded; // Indica si está tocando el suelo

    // Componentes del jugador
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
        // isGrounded es verdadero si el círculo de detección toca la capa "Ground"
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // 2. LÓGICA DE SALTO
        if (Input.GetButtonDown("Jump") && isGrounded) // "Jump" es la barra espaciadora por defecto
        {
            // Aplica fuerza vertical
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // 3. CONTROLAR ANIMATOR
        // a) Correr/Idle (ya existente)
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // b) Salto (Nueva)
        // Usa la comprobación de suelo para decirle al Animator si está saltando
        animator.SetBool("isJumping", !isGrounded);

        // 4. VOLTEAR EL SPRITE (ya existente)
        if (horizontalInput > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < -0.01f)
        {
            spriteRenderer.flipX = true;
        }
    }
}