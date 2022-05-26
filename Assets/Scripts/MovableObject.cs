/*
    Script para desplazar los props (modelos) en el juego
    Se recomienda revisar la documentación de este juego

    Autor: Juan de la Cruz Caravaca Guerrero (@Anthypatos/@Quadraxisv2)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public float fZOffset = 70; // Distancia que queremos desplazar
    public float fSpeed = 50;
    private Vector3 vector3OriginalPosition;    // Posición original a la que volver (ver documentación)

    // Start is called before the first frame update
    void Start()
    {
        vector3OriginalPosition = transform.position;   // Almacenamos la posición original

        LevelScript.OnLevelUp += ChangeSpeed;   // Nos suscribimos al evento de subida de nivel
    }

    // Update is called once per frame
    void Update()
    {
        // Si hemos llegado al punto de retorno, teletransportamos
        if (transform.position.z >= vector3OriginalPosition.z + fZOffset) 
            transform.position = vector3OriginalPosition;

        // Movimiento hacia el objetivo
        transform.position = Vector3.MoveTowards(transform.position, 
                                                    transform.position + new Vector3(0, 0, fZOffset), 
                                                    fSpeed * Time.deltaTime);
    }

    /* Cambia la velocidad de desplazamiento en ciertos niveles */
    private void ChangeSpeed(int iNivelActual)
    {
        switch (iNivelActual)
        {
            case 4: 
            case 7:
                fSpeed += 50; 
                break;
            default: break;
        }
    }
}
