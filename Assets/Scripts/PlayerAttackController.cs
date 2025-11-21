using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{

    public GameObject meleeHitbox;

    public void EnableHitbox()
    {
        if (meleeHitbox != null)
        {
            meleeHitbox.SetActive(true);
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
    public KeyCode launchKey = KeyCode.Mouse1;
    public int maxBallCount = 10;

    private int currentBallCount = 10; // Contador actual de bolas.

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
        GameObject ball = Instantiate(ballPrefab, launchPoint.position, Quaternion.identity);

        // 2. Ajustar la dirección de la bola basándose en la dirección del jugador (scale.x)
        float playerDirection = transform.localScale.x > 0 ? 1f : -1f;

        // Creamos un vector de dirección
        Vector3 launchDirection = new Vector3(playerDirection, 0, 0);

        // Rotamos la bola para que su 'derecha' coincida con la dirección del jugador
        ball.transform.right = launchDirection;

        // 3. Reducir el contador
        currentBallCount--;

        Debug.Log("Bolas restantes: " + currentBallCount);
    }

    // Método PÚBLICO para ser llamado por el objeto de recogida (Pickup)
    public void AddBalls(int amount)
    {
        currentBallCount = Mathf.Min(currentBallCount + amount, maxBallCount);
        Debug.Log($"¡Bolas añadidas! Total: {currentBallCount}");
    }

    public int GetBallCount()
    {
        return currentBallCount;
    }
}