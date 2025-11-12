using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Arrastra aquí el objeto 'Player'

    // Almacenamos la posición Y inicial de la cámara al comenzar.
    private float fixedYPosition;

    void Start()
    {
        // Guardamos la posición Y en la que empieza la cámara
        // (La posición vertical que quieres mantener).
        fixedYPosition = transform.position.y;
    }

    void LateUpdate()
    {
        // LateUpdate es mejor para cámaras porque se ejecuta DESPUÉS del movimiento del personaje.
        if (target != null)
        {
            // 1. Obtenemos la nueva posición, usando la X del personaje.
            Vector3 newPosition = new Vector3(
                target.position.x,     // Coge la posición X del personaje
                fixedYPosition,        // Mantiene la Y que guardamos en Start()
                transform.position.z   // Mantiene la Z (profundidad)
            );

            // 2. Movemos la cámara a esa nueva posición.
            transform.position = newPosition;
        }
    }
}