using UnityEngine;
using UnityEngine.SceneManagement; 
public class MainMenu : MonoBehaviour
{
    public void PlayGame(int levelIndex)
    {
        // El SceneManager busca el indice de la escena que especificamos
        SceneManager.LoadScene(levelIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");

        // Directiva de preprocesador
        #if UNITY_EDITOR
                // Si estamos en el editor de Unity, usamos el comando para detener el juego.
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    // Si estamos en un ejecutable (Build), cerramos la aplicación.
                    Application.Quit();
        #endif
    }
}