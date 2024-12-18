using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // A nave que a c�mera seguir�
    public Vector3 offset;   // Dist�ncia entre a c�mera e a nave
    public float smoothSpeed = 0.125f; // Velocidade de suaviza��o
    public float rotationSmoothSpeed = 5f; // Suaviza��o para a rota��o

    private Vector3 velocity = Vector3.zero; // Usado para suavizar a movimenta��o

    void LateUpdate()
    {
        if (target != null)
        {
            // Calcula a posi��o desejada com o offset
            Vector3 desiredPosition = target.position + offset;
            // Suaviza a transi��o de posi��o
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

            // Faz a rota��o da c�mera se suavizar para sempre olhar para o alvo
            Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSmoothSpeed * Time.deltaTime);
        }
    }
}
