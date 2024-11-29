using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleLight : MonoBehaviour
{
    [SerializeField] private float minScale, maxScale;
    [SerializeField] private bool areLightsOn;
    [SerializeField] private float t;

    private void Start()
    {
        areLightsOn = true;
        StartCoroutine(FlameScale());
    }
    IEnumerator FlameScale()
    {
        while (areLightsOn)
        {
            float scale = Random.Range(minScale, maxScale);
            transform.localScale = new Vector3(scale, scale, scale);
            yield return new WaitForSeconds(t);

        }
    }
}
