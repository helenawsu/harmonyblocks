using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationsPerMinute = 10.0f;
    public float bobbingAmplitude = 0.01f; 
    public float bobbingFrequency = 1.0f; 

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        transform.Rotate(0, 6.0f * rotationsPerMinute * Time.deltaTime, 0);
        
        // Bobbing motion
        float bobbingOffset = Mathf.Abs(Mathf.Sin(Time.time * bobbingFrequency) * bobbingAmplitude);
        transform.localPosition = initialPosition + new Vector3(0, bobbingOffset, 0);
    }
}