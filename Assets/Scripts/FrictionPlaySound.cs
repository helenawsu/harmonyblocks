/******************************************************************************
 * Copyright (C) Ultraleap, Inc. 2011-2023.                                   *
 *                                                                            *
 * Use subject to the terms of the Apache License 2.0 available at            *
 * http://www.apache.org/licenses/LICENSE-2.0, or another agreement           *
 * between Ultraleap and you, your company or other organization.             *
 ******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Leap.Unity.Interaction.PhysicsHands.Example
{
    /// <summary>
    /// Example script to test whether an object is being grasped or not.
    /// This function helps you ensure you're doing something when the user is or isn't grasping your object.
    /// It is designed for use when you quickly want events for a single rigidbody only.
    /// Please reference PhysicsInterfaces.cs for more information.
    /// </summary>
    public class FrictionPlaySound : MonoBehaviour, IPhysicsHandGrab
    {
        public float velocityGlideFriction = 1f;
        public float angularGlideFriction = 0.7f;
        public AudioSource audioSource;
        //bool isApplyingFriction = false;

        Rigidbody m_Rigidbody;
        bool positionFinished = true;
        bool rotationFinished = true;


    private void Start()
        {
          if (m_Rigidbody == null)
            m_Rigidbody = GetComponent<Rigidbody>();
          if (m_Rigidbody == null)
            Debug.LogWarning("Grab Interactable does not have a required Rigidbody.", this);
        }

        void Update() {
          if (m_Rigidbody.velocity.sqrMagnitude > 0 && positionFinished && rotationFinished) {
            StartApplyFriction();
          }
        }

        public void OnHandGrab(PhysicsHand hand)
        {
          positionFinished = true;
          rotationFinished = true;
        }

        public void OnHandGrabExit(PhysicsHand hand)
        {
          StartApplyFriction();
          audioSource.Play();
        }

        public void StartApplyFriction() {
          positionFinished = false;
          rotationFinished = false;
          StartCoroutine("ApplyVelocityFriction");
          StartCoroutine("ApplyAngularFriction");
        }

        // Apply friction on position transform
        IEnumerator ApplyVelocityFriction() {
          float multiplier;
          float tParam = 0.0f;
          Vector3 detachVelocity = m_Rigidbody.velocity;

          while (tParam < 1) {
            if(positionFinished) {
              yield break;
            }
            tParam += Time.deltaTime * velocityGlideFriction;

            // Lerp and smoothstep to get continously decreasing multiplier
            multiplier = Mathf.Lerp(0.0f, 1.0f, 1 - Mathf.SmoothStep(0.0f, 1.0f, tParam));

            // Apply multiplier to velocity
            m_Rigidbody.velocity = multiplier * detachVelocity;

            yield return null;
          }
           positionFinished = true;
        }

        // Apply friction on rotation transform
        IEnumerator ApplyAngularFriction() {
          float multiplier;
          float tParam = 0.0f;
          Vector3 detachAngularVelocity = m_Rigidbody.angularVelocity;

          while (tParam < 1) {
            if (rotationFinished) {
              yield break;
            }
            tParam += Time.deltaTime * angularGlideFriction;

            // Lerp and smoothstep to get continously decreasing multiplier
            multiplier = Mathf.Lerp(0.0f, 1.0f, 1 - Mathf.SmoothStep(0.0f, 1.0f, tParam));

            // Apply multiplier to velocity
            m_Rigidbody.angularVelocity = multiplier * detachAngularVelocity;

            yield return null;
          }
          rotationFinished = true;
    }

  }
}