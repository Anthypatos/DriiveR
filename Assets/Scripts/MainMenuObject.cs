using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuObject : MonoBehaviour
{
    public float fXTarget = 0;
    public float fYTarget = 0;
    public float fSpeed = 1000;
    private RectTransform rectTransform;

    public float smoothTime = 0.75f;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.position = Vector3.SmoothDamp(rectTransform.position, 
                                                    new Vector3(fXTarget, fYTarget,0), 
                                                    ref velocity, smoothTime, fSpeed);
    }
}
