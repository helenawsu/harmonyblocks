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
  //public TextMeshPro tmp;
  private float cooldownTime = 0.1f; // Cooldown time in seconds
  private float lastCollisionTime = -1; // Time of the last collision

  void Start() {
    // Get the AudioSource component
    //audioSource1 = GetComponent<AudioSource>();
    //audioSource2 = GetComponent<AudioSource>();
  }

  private void Update() {
    //tmp.text = PoseManager.Instance.getPose(true).ToString();
  }

  void OnTriggerEnter(Collider other) {
    // Play the sound when another object enters the trigger
      //audioSource.Play();
  }

  //Optional: Use OnCollisionEnter for physical collisions
   void OnCollisionEnter(Collision collision) {
   
      string otherObjectName = collision.gameObject.name;
      bool isLeft = false;

      if (otherObjectName.Contains("Left")) 
        isLeft = true;

      //tmp.text = PoseManager.Instance.getPose(isLeft).ToString();
      if (Time.time - lastCollisionTime >= cooldownTime) {
        if (PoseManager.Instance.isFist(isLeft)) {
          if (!audioSource1.isPlaying) {
            audioSource1.Play();
          }
        } else if (PoseManager.Instance.isPalm(isLeft)) {
            if (!audioSource2.isPlaying) {
              audioSource2.Play();
          }
      }

      lastCollisionTime = Time.time;
    }
    
      // Now you can use otherObjectName as needed
      Debug.Log("Collided with: " + otherObjectName);
  }
}
