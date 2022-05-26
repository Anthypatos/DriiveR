/*
    Script para el control de voz del menú de puntuaciones
    (Posteriormente usado en otras escenas, debería cambiarle el nombre)

    Autor: Juan de la Cruz Caravaca Guerrero (@Anthypatos/@Quadraxisv2)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;
using System;
using UnityEngine.SceneManagement;

public class VoicePuntuaciones : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    // Start is called before the first frame update
    void Start()
    {
        // Acción de volver al menú principal
        actions.Add("atrás", Atras);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += AccionReconocida;
        keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Atras() { SceneManager.LoadScene("MainMenu"); }

    private void AccionReconocida(PhraseRecognizedEventArgs speech) { actions[speech.text].Invoke(); }
}
