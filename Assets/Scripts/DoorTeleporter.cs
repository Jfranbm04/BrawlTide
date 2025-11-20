using UnityEngine;
using UnityEngine.SceneManagement;
using MaskTransitions;

public class DoorTeleporter : MonoBehaviour
{
    [Header("Configuración")]
    public string targetSceneName = "MainMenu"; // El nombre de la escena a cargar
    public KeyCode activationKey = KeyCode.Mouse0; // Clic izquierdo

    [Header("Transición")]
    // Referencia directa al script del asset
    public TransitionManager transitionManager;

    private bool playerIsNear = false;

    // Se llama cuando el jugador entra al área de la puerta (Trigger)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
            Debug.Log("Jugador cerca de la puerta.");
        }
    }

    // Se llama cuando el jugador sale del área de la puerta
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
            Debug.Log("Jugador lejos de la puerta.");
        }
    }

    void Update()
    {
        // 1. Comprueba si el jugador está cerca Y si se presiona la tecla de activación
        if (playerIsNear && Input.GetKeyDown(activationKey))
        {
            if (transitionManager != null)
            {
                TransitionManager.Instance.LoadLevel(targetSceneName);
            }
            else
            {
                // Fallback: Si el TransitionManager no está asignado, carga la escena directamente.
                SceneManager.LoadScene(targetSceneName);
            }
        }
    }

    // NOTA: Hemos eliminado la función LoadScene() de aquí porque el TransitionManager 
    // se encarga de llamar a SceneManager.LoadSceneAsync() por ti.
}