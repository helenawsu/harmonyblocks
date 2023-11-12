using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class LoopObject : MonoBehaviour
{
  public AudioSource audioSource;
  public float rotationsPerMinute = 10.0f;
  public float bobbingAmplitude = 0.01f;
  public float bobbingFrequency = 1.0f;

  private Vector3 initialPosition;


  // Add an active state variable
  private bool isActive = false;

  private float cooldownTime = 1f; // Cooldown time in seconds
  private float lastCollisionTime = -1; // Time of the last collision

  void Start() {
    // Ensure audio loop is set based on active state
    audioSource.loop = isActive;
    initialPosition = transform.localPosition;
 }

  void Update() {
    if (isActive) {
      transform.Rotate(0, 6.0f * rotationsPerMinute * Time.deltaTime, 0);

      // Bobbing motion
      float bobbingOffset = Mathf.Abs(Mathf.Sin(Time.time * bobbingFrequency) * bobbingAmplitude);
      transform.localPosition = initialPosition + new Vector3(0, bobbingOffset, 0);
    }
  }

  void OnTriggerEnter(Collider other) {
  }

  void OnCollisionEnter(Collision collision) {
      string otherObjectName = collision.gameObject.name;
      bool isLeft = false;

      if (otherObjectName.Contains("Left")) 
        isLeft = true;

      if (Time.time - lastCollisionTime >= cooldownTime) {
        if (PoseManager.Instance.isFist(isLeft) || PoseManager.Instance.isPalm(isLeft)) {
          // Toggle the active state
          isActive = !isActive;

          // Update audio playing based on active state
          if (isActive) {
            if (!audioSource.isPlaying) {
              audioSource.Play();
            }
          } else {
            audioSource.Stop();
          }

          // Ensure audio loop is set based on active state
          audioSource.loop = isActive;
        } 
      }
      lastCollisionTime = Time.time;
  }

}
