using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    private SkyboxSwitch[] skyboxSwitches;

    void Start()
    {
        skyboxSwitches = FindObjectsOfType<SkyboxSwitch>();
    }

    public void SetCurrentSkybox(Material newSkybox)
    {
        foreach (var skyboxSwitch in skyboxSwitches)
        {
            if (skyboxSwitch.linkedSkybox == newSkybox)
            {
                skyboxSwitch.SetActiveState(true);
                RenderSettings.skybox = newSkybox;
                DynamicGI.UpdateEnvironment();
            }
            else
            {
                skyboxSwitch.SetActiveState(false);
            }
        }
    }
}
