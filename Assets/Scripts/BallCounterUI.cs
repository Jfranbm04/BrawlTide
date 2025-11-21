using UnityEngine;
using TMPro; 

public class BallCounterUI : MonoBehaviour
{
    public PlayerAttackController playerAttackController;
    private TextMeshProUGUI ballText;

    void Start()
    {
        // Obtener el componente TextMeshPro del objeto donde se adjunta este script
        ballText = GetComponent<TextMeshProUGUI>();

        if (ballText == null)
        {
            Debug.LogError("El script BallCounterUI necesita un componente TextMeshProUGUI en este GameObject.");
            return;
        }

        if (playerAttackController == null)
        {
            Debug.LogError("¡ERROR! Arrastra el GameObject del Jugador (Player) al campo 'Player Attack Controller' del Inspector.");
            return;
        }

        // Inicializa el texto al inicio del juego
        UpdateBallDisplay();
    }

    void Update()
    {
        // Es mejor llamar a UpdateBallDisplay solo cuando ocurre un cambio real (ver Sección 3), 
        // pero usar Update() es la forma más fácil y rápida para empezar:
        UpdateBallDisplay();
    }

    // Método principal para actualizar la visualización
    public void UpdateBallDisplay()
    {
        int count = playerAttackController.GetBallCount();
        ballText.text = "x " + count.ToString();
    }
}