using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    // Asigna el objeto MeleeHitbox (hijo) en el Inspector
    public GameObject meleeHitbox;

    // Esta función activa el hitbox para aplicar daño
    // Se llama al inicio del golpe
    public void EnableHitbox()
    {
        if (meleeHitbox != null)
        {
            meleeHitbox.SetActive(true);
            // Debug.Log("Hitbox ACTIVADO");
        }
    }

    // Esta función desactiva el hitbox para evitar daño constante
    // Se llama al final del golpe
    public void DisableHitbox()
    {
        if (meleeHitbox != null)
        {
            meleeHitbox.SetActive(false);
            // Debug.Log("Hitbox DESACTIVADO");
        }
    }
}