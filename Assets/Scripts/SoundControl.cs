/*
    Script para evitar que se reinicie la música.

    Autor: Juan de la Cruz Caravaca Guerrero (@Anthypatos/@Quadraxisv2)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    public static SoundControl soundControlInstance;

    private void Awake() 
    {
        if (SoundControl.soundControlInstance == null)
        {
            SoundControl.soundControlInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}
