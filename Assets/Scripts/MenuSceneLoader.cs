using UnityEngine;
using UnityEngine.SceneManagement;
using MaskTransitions; // Para acceder a TransitionManager.Instance

public class MenuSceneLoader : MonoBehaviour
{
    // NO necesitamos una variable pública aquí, ya que el botón nos pasará la escena.

    // Función pública para ser llamada por el botón UI
    // Recibe el nombre de la escena como un argumento string.
    public void LoadSceneByButton(string sceneName)
    {
        // 1. Validar que la escena no esté vacía
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("El nombre de la escena no puede estar vacío. Revisa la configuración del botón.");
            return;
        }
        TransitionManager.Instance.LoadLevel(sceneName);
        //// 2. Comprobamos si el TransitionManager está listo
        //if (TransitionManager.Instance != null)
        //{
        //    // Le pedimos al Jefe de Transición que inicie la animación y cargue la escena.
        //    TransitionManager.Instance.LoadLevel(sceneName);
        //}
        //else
        //{
        //    // Fallback: Carga directa si el Manager no se encuentra.
        //    Debug.LogError("TransitionManager.Instance no encontrado. Cargando la escena directamente.");
        //    SceneManager.LoadScene(sceneName);
        //}
    }
}