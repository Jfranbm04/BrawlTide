using System;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Range(0f, 1f)] public float efectoParallax;
    private Transform camara;
    private Vector3 ultimaPosicionCamara;

    void Start()
    {
        camara = Camera.main.transform;
        ultimaPosicionCamara = camara.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 movimientoFondo = camara.position - ultimaPosicionCamara;
        transform.position += new Vector3(movimientoFondo.x * efectoParallax,
            movimientoFondo.y * efectoParallax, 0);
        ultimaPosicionCamara = camara.position;
    }
}