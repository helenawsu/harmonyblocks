using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]

public class PlayableObject : MonoBehaviour
{
  // Start is called before the first frame update
  public AudioSource audioSource1;
  public AudioSource audioSource2;
  public TextMeshPro tmp;
  public float cooldownTime = 0.3f; // Cooldown time in seconds
  private float lastCollisionTime = -1f; // Time of the last collision

  void Start() {
    // Get the AudioSource component
    //audioSource1 = GetComponent<AudioSource>();
    //audioSource2 = GetComponent<AudioSource>();
  }

  void OnTriggerEnter(Collider other) {
    // Play the sound when another object enters the trigger
      //audioSource.Play();
  }

  //Optional: Use OnCollisionEnter for physical collisions
   void OnCollisionEnter(Collision collision) {
   
      string otherObjectName = collision.gameObject.name;

      tmp.text = otherObjectName;

    if (Time.time - lastCollisionTime >= cooldownTime) {
      if (otherObjectName.Contains("Joint 2") || otherObjectName.Contains("Joint 1")) {
        if (!audioSource2.isPlaying) { 
          audioSource1.Play();
        }
      } else {
        if (!audioSource1.isPlaying) { 
          audioSource2.Play();
        }
      }
    }
      // Now you can use otherObjectName as needed
      Debug.Log("Collided with: " + otherObjectName);
  }
}
