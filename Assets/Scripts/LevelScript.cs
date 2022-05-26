/*
    Script para el control de niveles

    Autor: Juan de la Cruz Caravaca Guerrero (@Anthypatos/@Quadraxisv2)
*/

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
    public string[] sSenales = new string[6];   // Señales de tráfico a reconocer   
    public float fTiempoMaximo = 11;            // Tiempo en el que las señales permanecen en pantalla
    public List<GameObject> imagesList;         // Lista de prefabs de señales
    public RectTransform canvas;
    public TextMeshProUGUI textoPuntuacion;     // Texto de puntuación en el canvas
    public TextMeshProUGUI textoTiempo;         // Texto de tiempo restante en el canvas

    private KeywordRecognizer keywordRecognizer;
    private int iSublevel = 2;                  // Subniveles = número de señales a reconocer por nivel
    private int iNivelActual = 0;
    private int iRandomImage = 0;               // Índice aleatorio de la siguiente imagen a reconocer
    private float fTiempoNivel = 0;             // Tiempo que nos queda para reconocer la señal actual
    private float fFactorEscala = 1;            // Factor de escalado para reducir el tamaño de las imágenes
    private bool bAcierto = false;              // ¿Ha acertado el usuario?
    private bool bError = false;                // ¿Ha fallado el usuario?
    private int iVidas = 3;                     // Vidas iniciales del usuario
    private int iPuntuacion = 0;           
    private GameObject gameObjectSpawned;       // Imagen aleatoria a spawnear
    private GameObject[] gameObjectsVidas;      // Imágenes de las vidas restantes

    // Lanzar evento al subir de nivel
    public delegate void OnNewLevel(int iNivel);
    public static event OnNewLevel OnLevelUp;
    

    // Start is called before the first frame update
    void Start()
    {
        // Inicializar reconocimiento de voz
        keywordRecognizer = new KeywordRecognizer(sSenales);
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

        // Inicializar tiempo restante y empezar primer nivel
        fTiempoNivel = fTiempoMaximo - 1;
        NextSubLevel();
    }

    // Update is called once per frame
    void Update()
    {
        iPuntuacion += iNivelActual;    // Cada frame aumenta la puntuación según el nivel
        textoPuntuacion.text = iPuntuacion.ToString();  // Mostrar la puntuación

        // Condiciones para eliminar una imagen de señal y pasar a la siguiente
        if (bAcierto || bError || fTiempoNivel <= 0)
        {
            GameObject.Destroy(gameObjectSpawned);  // Destruimos la señal actual
            if (bError || fTiempoNivel <= 0)   // Si es que el jugador ha fallado o agotado el tiempo
            {
                iVidas--;
                if (iVidas == 0) // Si ya no quedan vidas saltamos al gameover
                {
                    PlayerPrefs.SetInt("Puntuacion", iPuntuacion);
                    SceneManager.LoadScene("GameOver");
                }
                else // De lo contrario destruimos la imagen de la vida más a la derecha
                {
                    gameObjectsVidas = GameObject.FindGameObjectsWithTag("Vida");
                    GameObject.Destroy(gameObjectsVidas[gameObjectsVidas.Length - 1]);
                }
            }

            fTiempoNivel = fTiempoMaximo;   // Restauramos el tiempo y vamos a por la siguiente imagen
            NextSubLevel();
        }

        // Control y muestra del tiempo
        fTiempoNivel -= Time.deltaTime;
        textoTiempo.text = "TIEMPO: " + fTiempoNivel.ToString("00.00");
    }

    /* Muestra una señal de tráfico en pantalla */
    private void NextSubLevel()
    {
        bAcierto = bError = false;  // Restauramos las banderas de acierto o error
        
        // Cada nivel constará de 3 subniveles (podría usarse una macro o similar)
        if (++iSublevel >= 3)   // ¿Llevamos tres?
        {
            iSublevel = 0;  // Pasamos al siguiente nivel
            // Durante los 10 primeros niveles vamos reduciendo el tiempo disponible 
            // y el tamaño de las imágenes
            if (++iNivelActual < 10)
            {
                fTiempoMaximo--;
                fFactorEscala -= 0.1f;
            }
            if (OnLevelUp != null) OnLevelUp(iNivelActual); // Lanzamos evento al pasar de nivel
        }
        
        // El resto de este código elige una imagen aleatoria y la muestra
        iRandomImage = UnityEngine.Random.Range(0, imagesList.Count);
        GameObject imageToSpawn = imagesList[iRandomImage];

        Vector3 spawnPosition = GetBottomLeftCorner(canvas) - 
            new Vector3(-UnityEngine.Random.Range(0, canvas.rect.width), 
            -UnityEngine.Random.Range(100, canvas.rect.height - 100), 0);

        gameObjectSpawned = Instantiate(imageToSpawn, spawnPosition, Quaternion.identity, canvas);
        gameObjectSpawned.transform.localScale = new Vector3(fFactorEscala, fFactorEscala, 1);
    }

    /* Obtener la esquina inferior izquierda del canvas */
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
            default:    // Las imágenes deben de estar en orden con sus nombres, esto podría mejorarse
                if (speech.text == imagesList[iRandomImage].name) bAcierto = true;
                else bError = true;
                break;
        }
    }
}
