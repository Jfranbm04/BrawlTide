using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para la función de Salir/Menú

public class PauseManager : MonoBehaviour
{
    // Asigna el PausePanel (el objeto visual) en el Inspector
    public GameObject pauseMenuUI;

    // Bandera para saber si el juego está pausado
    public static bool GameIsPaused = false;

    void Update()
    {
        // Detecta la pulsación de la tecla ESCAPE
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // ===================================
    // >> MÉTODOS PÚBLICOS (Para botones)
    // ===================================

    // Función que reanuda el juego
    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Oculta el menú
        Time.timeScale = 1f;          // Reanuda el tiempo normal
        GameIsPaused = false;         // Actualiza la bandera de estado
    }

    // Función que pausa el juego
    public void Pause()
    {
        pauseMenuUI.SetActive(true);  // Muestra el menú
        Time.timeScale = 0f;          // Detiene el tiempo (frame rate queda intacto)
        GameIsPaused = true;          // Actualiza la bandera de estado
    }

    // Función para salir del nivel (puedes adaptarla para cargar el menú principal)
    public void LoadMenu()
    {
        // Asegúrate de que el tiempo se reanude antes de cargar la escena
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}