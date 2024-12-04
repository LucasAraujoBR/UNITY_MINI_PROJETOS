using UnityEngine;

public class CameraModeSwitcher : MonoBehaviour
{
    public Camera shipCamera1; // Primeira câmera (nave 1)
    public Camera shipCamera2; // Segunda câmera (nave 2)

    public float defaultDistance = 5f; // Distância padrão
    public float defaultFOV = 60f; // Campo de visão padrão

    public float distantModeDistance = 10f; // Distância para o modo distante
    public float distantModeFOV = 30f; // Campo de visão para o modo distante

    void Update()
    {
        // Controles para a câmera da primeira nave (teclas 1 e 2)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetCameraMode(shipCamera1, defaultDistance, defaultFOV);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetCameraMode(shipCamera1, distantModeDistance, distantModeFOV);
        }

        // Controles para a câmera da segunda nave (teclas 9 e 0)
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SetCameraMode(shipCamera2, defaultDistance, defaultFOV);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SetCameraMode(shipCamera2, distantModeDistance, distantModeFOV);
        }
    }

    void SetCameraMode(Camera cam, float distance, float fieldOfView)
    {
        if (cam != null)
        {
            cam.transform.position = cam.transform.parent.position + new Vector3(0, distance, -distance);
            cam.fieldOfView = fieldOfView;
        }
    }
}
