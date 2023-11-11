using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PlayableObject : MonoBehaviour
{
  // Start is called before the first frame update
  private AudioSource audioSource;

  void Start() {
    // Get the AudioSource component
    audioSource = GetComponent<AudioSource>();
  }

  void OnTriggerEnter(Collider other) {
    // Play the sound when another object enters the trigger
      audioSource.Play();
  }

  //Optional: Use OnCollisionEnter for physical collisions
   void OnCollisionEnter(Collision collision) {
    if (!audioSource.isPlaying) {
      audioSource.Play();
    }
  }
}
