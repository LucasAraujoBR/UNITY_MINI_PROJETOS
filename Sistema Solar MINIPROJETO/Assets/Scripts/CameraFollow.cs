using UnityEngine;

public class FollowSpaceship : MonoBehaviour
{
    public Transform spaceship; // Refer�ncia � nave espacial
    public Vector3 offset = new Vector3(0, 5, -10); // Offset da c�mera em rela��o � nave
    public float smoothSpeed = 0.125f; // Velocidade de suaviza��o

    void LateUpdate()
    {
        if (spaceship == null) return;

        // Calcula a posi��o desejada
        Vector3 desiredPosition = spaceship.position + offset;

        // Suaviza a transi��o para a posi��o desejada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Atualiza a posi��o da c�mera
        transform.position = smoothedPosition;

        // Faz a c�mera olhar para a nave
        transform.LookAt(spaceship);
    }
}
