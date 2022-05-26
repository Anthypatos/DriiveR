/*
    Script para evitar que se reinicie la música.

    Autor: Juan de la Cruz Caravaca Guerrero (@Anthypatos/@Quadraxisv2)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Update() 
    {
        // En estas 2 escenas no queremos música
        if (SceneManager.GetActiveScene().name == "Jugar" ||
            SceneManager.GetActiveScene().name == "Creditos")
        {
            Destroy(gameObject);
        }
    }
}
