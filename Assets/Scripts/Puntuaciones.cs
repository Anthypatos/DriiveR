/*
    Script para el menú de puntuaciones

    Autor: Juan de la Cruz Caravaca Guerrero (@Anthypatos/@Quadraxisv2)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class Puntuaciones : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Obtener puntuaciones y mostrarlas en pantalla
        if (File.Exists(Application.dataPath + "/puntuaciones.txt"))
            GetComponent<TextMeshProUGUI>().text = File.ReadAllText(Application.dataPath + "/puntuaciones.txt");
        else File.WriteAllText(Application.dataPath + "/puntuaciones.txt", "");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
