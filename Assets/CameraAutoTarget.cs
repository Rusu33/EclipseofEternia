using UnityEngine;
using Invector.vCamera;

public class CameraAutoTarget : MonoBehaviour
{
    void Start()
    {
        var camera = GetComponent<vThirdPersonCamera>();
        var player = FindObjectOfType<Invector.vCharacterController.vThirdPersonController>();

        if (camera != null && player != null)
        {
            camera.SetMainTarget(player.transform); // ✅ metoda oficială sigură
        }
    }
}
