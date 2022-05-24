using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public float fZOffset = 70;
    public float fSpeed = 50;
    private Vector3 vector3OriginalPosition;

    // Start is called before the first frame update
    void Start()
    {
        vector3OriginalPosition = transform.position;

        LevelScript.OnLevelUp += ChangeSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z >= vector3OriginalPosition.z + fZOffset) 
            transform.position = vector3OriginalPosition;

        transform.position = Vector3.MoveTowards(transform.position, 
                                                    transform.position + new Vector3(0, 0, fZOffset), 
                                                    fSpeed * Time.deltaTime);
    }

    private void ChangeSpeed(int iNivelActual)
    {
        switch (iNivelActual)
        {
            case 15: 
            case 30:
                fSpeed *= 2; 
                break;
            default: break;
        }
    }
}
