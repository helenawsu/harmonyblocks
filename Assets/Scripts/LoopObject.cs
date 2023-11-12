using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class LoopObject : MonoBehaviour
{
  public AudioSource audioSource;
  public Rotate rotateScript;

  // Add an active state variable
  private bool isActive = false;

  private float cooldownTime = 0.1f; // Cooldown time in seconds
  private float lastCollisionTime = -1; // Time of the last collision

  void Start() {
    // Ensure audio loop is set based on active state
    audioSource.loop = isActive;
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
              rotateScript.enabled = true;
            }
          } else {
            audioSource.Stop();
            rotateScript.enabled = false;
          }

          // Ensure audio loop is set based on active state
          audioSource.loop = isActive;
        } 
      }
      lastCollisionTime = Time.time;
  }

}
