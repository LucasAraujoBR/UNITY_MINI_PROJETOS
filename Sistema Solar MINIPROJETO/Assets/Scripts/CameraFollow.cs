using UnityEngine;

public class FollowSpaceship : MonoBehaviour
{
    public Transform spaceship; // Referência à nave espacial
    public Vector3 offset = new Vector3(0, 5, -10); // Offset da câmera em relação à nave
    public float smoothSpeed = 0.125f; // Velocidade de suavização

    void LateUpdate()
    {
        if (spaceship == null) return;

        // Calcula a posição desejada
        Vector3 desiredPosition = spaceship.position + offset;

        // Suaviza a transição para a posição desejada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Atualiza a posição da câmera
        transform.position = smoothedPosition;

        // Faz a câmera olhar para a nave
        transform.LookAt(spaceship);
    }
}
