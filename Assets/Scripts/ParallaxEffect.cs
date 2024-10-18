using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{

    [SerializeField] private Transform cam;
    [SerializeField] private float percent;
    private Vector3 previousPos;
        
    void Start()
    {
        previousPos = cam.position;
    }
    private void LateUpdate()
    {
        Vector3 diferencia = cam.position - previousPos;
        transform.Translate(diferencia * percent);
        previousPos = cam.position;
    }

}
