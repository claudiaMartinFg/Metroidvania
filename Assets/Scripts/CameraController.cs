using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform player;

    [SerializeField] private Vector3 camOffset;

    [SerializeField] private Vector2 limitX;
    [SerializeField] private Vector2 limitY;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.position + camOffset;
        
    }
}
