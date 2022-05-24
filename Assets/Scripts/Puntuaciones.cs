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
        if (File.Exists(Application.dataPath + "/puntuaciones.txt"))
            GetComponent<TextMeshProUGUI>().text = File.ReadAllText(Application.dataPath + "/puntuaciones.txt");
        else File.WriteAllText(Application.dataPath + "/puntuaciones.txt", "");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
