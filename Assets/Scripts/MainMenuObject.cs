/*
    Script para las opciones del menú principal

    Autor: Juan de la Cruz Caravaca Guerrero (@Anthypatos/@Quadraxisv2)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuObject : MonoBehaviour
{
    // Estas variables son para desplazar los textos de fuera a dentro del canvas
    // y hacer un bonito efecto. Coordenadas objetivo y velocidad.
    public float fXTarget = 0;
    public float fYTarget = 0;
    public float fSpeed = 1000;
    private RectTransform rectTransform;

    public float smoothTime = 0.75f;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // SmoothDamp suaviza los desplazamientos
        rectTransform.position = Vector3.SmoothDamp(rectTransform.position, 
                                                    new Vector3(fXTarget, fYTarget,0), 
                                                    ref velocity, smoothTime, fSpeed);
    }
}
