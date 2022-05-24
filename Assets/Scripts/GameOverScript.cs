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
        int iPuntuacion = PlayerPrefs.GetInt("Puntuacion");
        GetComponent<TextMeshProUGUI>().text += iPuntuacion.ToString();
        File.AppendAllText(Application.dataPath + "/puntuaciones.txt", iPuntuacion.ToString() + "\n");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
