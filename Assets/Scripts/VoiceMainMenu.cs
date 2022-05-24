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

    void Iniciar() { SceneManager.LoadScene("Jugar"); }
    void Puntuaciones() { SceneManager.LoadScene("Puntuaciones"); }
    void Ayuda() { SceneManager.LoadScene("Ayuda"); }
    void Creditos() { GameObject.Find("Canvas").transform.GetChild(0).GetChild(0).gameObject.SetActive(true); }
    void Salir() 
    { 
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    private void AccionReconocida(PhraseRecognizedEventArgs speech) { actions[speech.text].Invoke(); }
}
