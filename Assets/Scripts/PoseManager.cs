using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoseManager : MonoBehaviour
{
  public static PoseManager Instance { get; private set; }
  int leftPose = -1;
  int rightPose = -1;

  void Awake() {
    if (Instance == null) {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    } else {
      Destroy(gameObject);
    }
  }

  public bool isFist(bool isLeft) {
    if (isLeft) { return leftPose == 1; } else { return rightPose == 1; }
  }

  public bool isPalm(bool isLeft) {
    if (isLeft) { return leftPose == -1; } else { return rightPose == -1; }

  }

  public void setFist(bool isLeft) {
    if (isLeft) { leftPose = 1; } else { rightPose = 1; }
  }

  public void setPalm(bool isLeft) {
    if (isLeft) { leftPose = 0; } else { rightPose = 0; }
  }

  public void resetPose(bool isLeft) {
    if (isLeft) { leftPose = -1; } else { rightPose = -1; }
  }

  public int getPose(bool isLeft) {

    if (isLeft) { return leftPose; } else { return rightPose; }
  }
}
