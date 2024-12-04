using UnityEngine;

public class CameraeModeSwitcher : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;

    public float defaultDistance = 5f;
    public float defaultFOV = 60f;
    public float distantDistance = 10f;
    public float distantFOV = 30f;

    void Update()
    {
        // Controles para a primeira nave (teclas 1 e 2)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ConfigureCameraMode(camera1, defaultDistance, defaultFOV);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ConfigureCameraMode(camera1, distantDistance, distantFOV);
        }

        // Controles para a segunda nave (teclas 9 e 0)
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ConfigureCameraMode(camera2, defaultDistance, defaultFOV);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ConfigureCameraMode(camera2, distantDistance, distantFOV);
        }
    }

    void ConfigureCameraMode(Camera cam, float distance, float fieldOfView)
    {
        if (cam != null)
        {
            cam.transform.position = cam.transform.parent.position + new Vector3(0, distance, -distance);
            cam.fieldOfView = fieldOfView;
        }
    }
}
