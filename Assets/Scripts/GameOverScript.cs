/*
    Script para la escena de GameOver

    Autor: Juan de la Cruz Caravaca Guerrero (@Anthypatos/@Quadraxisv2)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class GameOverScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int iPuntuacion = PlayerPrefs.GetInt("Puntuacion"); // Obtener última puntuación
        GetComponent<TextMeshProUGUI>().text += iPuntuacion.ToString(); // Mostrarla por pantalla

        // Guardar la puntuación en almacenamiento persistente
        File.AppendAllText(Application.dataPath + "/puntuaciones.txt", iPuntuacion.ToString() + "\n");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
