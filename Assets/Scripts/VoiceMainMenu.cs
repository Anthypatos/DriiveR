/*
    Script para el control de voz del menú principal

    Autor: Juan de la Cruz Caravaca Guerrero (@Anthypatos/@Quadraxisv2)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;
using System;
using UnityEngine.SceneManagement;

public class VoiceMainMenu : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    // Start is called before the first frame update
    void Start()
    {
        // Añadir acciones
        actions.Add("iniciar", Iniciar);
        actions.Add("puntuaciones", Puntuaciones);
        actions.Add("ayuda", Ayuda);
        actions.Add("créditos", Creditos);
        actions.Add("salir", Salir);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += AccionReconocida;
        keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Las acciones cargan las demás o se salen del programa */
    void Iniciar() { SceneManager.LoadScene("Jugar"); }
    void Puntuaciones() { SceneManager.LoadScene("Puntuaciones"); }
    void Ayuda() { SceneManager.LoadScene("Ayuda"); }
    void Creditos() { SceneManager.LoadScene("Creditos"); }
    void Salir() 
    { 
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    private void AccionReconocida(PhraseRecognizedEventArgs speech) { actions[speech.text].Invoke(); }
}
