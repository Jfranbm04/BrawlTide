using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{

    public GameObject meleeHitbox;
    [SerializeField] AudioSource punchSound;
    public void EnableHitbox()
    {
        if (meleeHitbox != null)
        {
            meleeHitbox.SetActive(true);
            punchSound.Play();

        }
    }

    public void DisableHitbox()
    {
        if (meleeHitbox != null)
        {
            meleeHitbox.SetActive(false);
        }
    }


    [Header("Ataque a Distancia (Bolas)")]
    public GameObject ballPrefab;
    public Transform launchPoint;

    // El valor por defecto es Clic Derecho
    public KeyCode launchKey = KeyCode.Mouse1;


    private int currentBallCount = 0;

    void Update()
    {
        // El lanzador solo puede usarse si la tecla es presionada Y si hay bolas disponibles
        if (Input.GetKeyDown(launchKey) && currentBallCount > 0)
        {
            LaunchBall();
        }
    }

    void LaunchBall()
    {
        // 1. Instanciar la bola en el punto de lanzamiento
        GameObject ballObject = Instantiate(ballPrefab, launchPoint.position, Quaternion.identity);

        // 2. OBTENER LA DIRECCIÓN DEL JUGADOR
        // Asumimos por defecto que es la escala
        float playerDirection = transform.localScale.x > 0 ? 1f : -1f;

        SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
        if (mySprite != null && mySprite.flipX == true)
        {
            playerDirection = -1f;
        }

        // (Opcional) Si tu juego rota el personaje 180 grados en Y, descomenta esto:
        // if (transform.right.x < 0) playerDirection = -1f;

        // 3. OBTENER EL SCRIPT DE LA BOLA INSTANCIADA
        ThrowableBall ballScript = ballObject.GetComponent<ThrowableBall>();

        if (ballScript != null)
        {
            // 4. PASAR LA DIRECCIÓN
            ballScript.SetDirection(playerDirection);
        }

        // 5. Reducir el contador
        currentBallCount--;

        Debug.Log("Bolas restantes: " + currentBallCount);
    }

    // Método PÚBLICO para ser llamado por el objeto de recogida (Pickup)
    public void AddBalls(int amount)
    {
        // NO se usa Mathf.Min, se añade directamente.
        currentBallCount += amount;
    }

    public int GetBallCount()
    {
        return currentBallCount;
    }
}