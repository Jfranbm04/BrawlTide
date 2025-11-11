using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Ajusta la velocidad en el Inspector. 5f es un buen valor inicial.
    public float moveSpeed = 5f;

    // Referencias a los componentes (las inicializamos en Start())
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Obtenemos los componentes adjuntos al inicio para usarlos
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 1. CAPTURAR ENTRADA
        // Obtiene un valor entre -1 (izquierda), 1 (derecha) o 0 (quieto)
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // 2. APLICAR MOVIMIENTO (Física)
        // Mueve al personaje horizontalmente, manteniendo su velocidad vertical (para el salto)
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // 3. CONTROLAR ANIMATOR
        // Le dice al Animator si el personaje se está moviendo o no.
        // Mathf.Abs() devuelve el valor absoluto (si es -1 o 1, el Animator ve 1)
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // 4. VOLTEAR EL SPRITE (FLIP)
        if (horizontalInput > 0.01f) // Si se mueve a la derecha
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < -0.01f) // Si se mueve a la izquierda
        {
            spriteRenderer.flipX = true;
        }
    }
}