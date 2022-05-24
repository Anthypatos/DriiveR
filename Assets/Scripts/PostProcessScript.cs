using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessScript : MonoBehaviour
{
    public GameObject gameObjectTCross;
    private PostProcessVolume postProcessVolume;
    private ColorGrading colorGrading;
    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;

    private Material[] materiales;

    // Start is called before the first frame update
    void Start()
    {
        postProcessVolume = GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out colorGrading);
        postProcessVolume.profile.TryGetSettings(out chromaticAberration);
        postProcessVolume.profile.TryGetSettings(out lensDistortion);

        materiales = gameObjectTCross.GetComponent<Renderer>().materials;

        LevelScript.OnLevelUp += CheckLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckLevel(int iNivelActual)
    {
        if (iNivelActual % 15 == 0) 
        {
            foreach (Material material in materiales)
                if (material.name == "Rojo (Instance)") material.SetColor("_EmissionColor", 
                                                        new Color(0.1946179f, 0.03189604f, 0.04231142f));
            colorGrading.temperature.value = 0;
        }
        else if (iNivelActual % 10 == 0)
        {
            colorGrading.temperature.value = -50;

            foreach (Material material in materiales)
                if (material.name == "Rojo (Instance)") material.SetColor("_EmissionColor", 
                                                        new Color(1, 0, 0));
        }
        else if (iNivelActual % 5 == 0) colorGrading.temperature.value = 50;

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
