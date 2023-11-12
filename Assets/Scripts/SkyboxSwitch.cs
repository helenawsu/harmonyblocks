using UnityEngine;

public class SkyboxSwitch : MonoBehaviour
{
    public float rotationsPerMinute = 10.0f;
    public float bobbingAmplitude = 0.01f;
    public float bobbingFrequency = 1.0f;

    private Vector3 initialPosition;

    public SkyboxManager skyboxManager;
    public Material linkedSkybox;

    private bool isActive = false;

    private float cooldownTime = 1f; // Cooldown time in seconds
    private float lastCollisionTime = -1; // Time of the last collision

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (isActive)
        {
            // Perform rotation and bobbing
            RotateAndBob();
        }
    }

    void RotateAndBob()
    {
        transform.Rotate(0, 6.0f * rotationsPerMinute * Time.deltaTime, 0);

        // Bobbing motion
        float bobbingOffset = Mathf.Abs(Mathf.Sin(Time.time * bobbingFrequency) * bobbingAmplitude);
        transform.localPosition = initialPosition + new Vector3(0, bobbingOffset, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Time.time - lastCollisionTime >= cooldownTime)
        {
            lastCollisionTime = Time.time;

            // Toggle active state and notify SkyboxManager
            isActive = !isActive;
            skyboxManager.SetCurrentSkybox(isActive ? linkedSkybox : null);
        }
    }

    public void SetActiveState(bool state)
    {
        isActive = state;
    }
}
