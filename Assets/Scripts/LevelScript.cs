using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelScript : MonoBehaviour
{
    public string[] sSenales = new string[2];
    public float fTiempoEspera = 10;
    public List<GameObject> imagesList;
    public RectTransform canvas;
    public TextMeshProUGUI textoPuntuacion;

    private KeywordRecognizer keywordRecognizer;
    private int iSublevel = 2;
    private int iNivelActual = 0;
    private int iRandomImage = 0;
    private float fTiempoNivel = 0;
    private float fFactorEscala = 1;
    private bool bAcierto = false;
    private bool bError = false;
    private int iVidas = 3;
    private int iPuntuacion = 0;
    private GameObject gameObjectSpawned;
    private GameObject[] gameObjectsVidas;

    public delegate void OnNewLevel(int iNivel);
    public static event OnNewLevel OnLevelUp;
    

    // Start is called before the first frame update
    void Start()
    {
        keywordRecognizer = new KeywordRecognizer(sSenales);
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;

        fTiempoNivel = Time.time + fTiempoEspera;
        NextSubLevel();
    }

    // Update is called once per frame
    void Update()
    {
        iPuntuacion += iNivelActual;
        textoPuntuacion.text = iPuntuacion.ToString();

        if (bAcierto || bError || Time.time > fTiempoNivel)
        {
            GameObject.Destroy(gameObjectSpawned);
            if (bError ||Time.time > fTiempoNivel) 
            {
                iVidas -= 1;
                if (iVidas == 0) 
                {
                    PlayerPrefs.SetInt("Puntuacion", iPuntuacion);
                    SceneManager.LoadScene("GameOver");
                }
                else 
                {
                    gameObjectsVidas = GameObject.FindGameObjectsWithTag("Vida");
                    GameObject.Destroy(gameObjectsVidas[gameObjectsVidas.Length - 1]);
                }
            }
            keywordRecognizer.Stop();

            NextSubLevel();
        }
    }

    private void NextSubLevel()
    {
        bAcierto = bError = false;
        
        if (++iSublevel >= 3)
        {
            iSublevel = 0;
            if (++iNivelActual < 10)
            {
                fTiempoEspera--;
                fFactorEscala -= 0.1f;
            }
            if (OnLevelUp != null) OnLevelUp(iNivelActual);
        }
        
        iRandomImage = UnityEngine.Random.Range(0, imagesList.Count);
        GameObject imageToSpawn = imagesList[iRandomImage];

        Vector3 spawnPosition = GetBottomLeftCorner(canvas) - 
            new Vector3(-UnityEngine.Random.Range(0, canvas.rect.width), 
            -UnityEngine.Random.Range(100, canvas.rect.height - 100), 0);

        gameObjectSpawned = Instantiate(imageToSpawn, spawnPosition, Quaternion.identity, canvas);
        gameObjectSpawned.transform.localScale = new Vector3(fFactorEscala, fFactorEscala, 1);

        keywordRecognizer.Start();
        fTiempoNivel = Time.time + fTiempoEspera;
    }

    private Vector3 GetBottomLeftCorner(RectTransform rt)
    {
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        return v[0];
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);

        switch(speech.text)
        {
            case "salir":   SceneManager.LoadScene("MainMenu"); break;
            default:
                if (speech.text == imagesList[iRandomImage].name) bAcierto = true;
                else bError = true;
                break;
        }
    }
}
