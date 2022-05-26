/*
    Script para el postprocesamiento y los cambios visuales

    Autor: Juan de la Cruz Caravaca Guerrero (@Anthypatos/@Quadraxisv2)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class PostProcessScript : MonoBehaviour
{
    public GameObject gameObjectTCross; // Objeto vehículo (sería mejor hacerlo en su propio script...)
    private Material[] materiales;  // Materiales del coche

    // Volumen y efectos a cambiar
    private PostProcessVolume postProcessVolume;
    private ColorGrading colorGrading;
    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;

    private int iDayState = 0;  // Cada estado del día dura 2 niveles

    // Start is called before the first frame update
    void Start()
    {
        // Obtener los valores de los efectos actuales y materiales del coche
        postProcessVolume = GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out colorGrading);
        postProcessVolume.profile.TryGetSettings(out chromaticAberration);
        postProcessVolume.profile.TryGetSettings(out lensDistortion);

        materiales = gameObjectTCross.GetComponent<Renderer>().materials;

        LevelScript.OnLevelUp += CheckLevel;    // Suscribirnos al evento de subida de nivel
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Cambios visuales según nivel */
    private void CheckLevel(int iNivelActual)
    {
        iDayState++;
        Debug.Log(iDayState);
        if (iDayState >= 6) // Pasar de noche a día
        {
            // Apaga luces del coche
            foreach (Material material in materiales)
                if (material.name == "Rojo (Instance)") material.SetColor("_EmissionColor", 
                                                        new Color(0.1946179f, 0.03189604f, 0.04231142f));
            colorGrading.temperature.value = 0;
            iDayState = -1;
        }
        else if (iDayState == 4) // Pasar de tarde a noche
        {
            colorGrading.temperature.value = -50;

            // Enciende luces del coche
            foreach (Material material in materiales)
                if (material.name == "Rojo (Instance)") material.SetColor("_EmissionColor", 
                                                        new Color(1, 0, 0));
        }
        else if (iDayState == 2) colorGrading.temperature.value = 50;    // Pasar de día a tarde

        // Agrega nuevos efectos en ciertos niveles
        switch (iNivelActual)
        {
            case 4:
                chromaticAberration.active = true;
                chromaticAberration.intensity.value = 1;
                break;
            case 7:
                lensDistortion.active = true;
                lensDistortion.intensity.value = -50;
                break;
        }
    }
}
