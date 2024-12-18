using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // A nave que a câmera seguirá
    public Vector3 offset;   // Distância entre a câmera e a nave
    public float smoothSpeed = 0.125f; // Velocidade de suavização
    public float rotationSmoothSpeed = 5f; // Suavização para a rotação

    private Vector3 velocity = Vector3.zero; // Usado para suavizar a movimentação

    void LateUpdate()
    {
        if (target != null)
        {
            // Calcula a posição desejada com o offset
            Vector3 desiredPosition = target.position + offset;
            // Suaviza a transição de posição
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

            // Faz a rotação da câmera se suavizar para sempre olhar para o alvo
            Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSmoothSpeed * Time.deltaTime);
        }
    }
}
