using UnityEngine;

/*
 * This script allows user to change the environment settings via the Unity Editor Interface. This includes the
 * following environmental variables: Skybox and floor color, illumination intensity and position (...).
 */
public class EnvironmentSettings : MonoBehaviour
{
    public GameObject Floor;
    public GameObject Illumination;
    
    public Material SkyboxColor;
    public Material FloorMaterial;
    private void Start()
    {
        RenderSettings.skybox = SkyboxColor;
        //RenderSettings.Floor
    }
}