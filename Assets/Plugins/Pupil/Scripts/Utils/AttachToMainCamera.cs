using UnityEngine;

public class AttachToMainCamera : MonoBehaviour
{
    public Transform VRCamera;
    void Start()
    {
        this.transform.SetParent(VRCamera, false);
    }
}
