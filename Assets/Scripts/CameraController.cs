using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform player;

    [SerializeField] private Vector3 camOffset;

    [Tooltip("x=xMin, y=xMax")]
    [SerializeField] private Vector2 limitX;

    [Tooltip("x=yMin, y=yMax")]
    [SerializeField] private Vector2 limitY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

        float x = Mathf.Clamp(player.position.x + camOffset.x, limitX.x, limitX.y);
        float y = Mathf.Clamp(player.position.y + camOffset.y, limitY.x, limitY.y);
        float z = player.position.z + camOffset.z;

        transform.position = new Vector3(x, y, z);


        
    }
}