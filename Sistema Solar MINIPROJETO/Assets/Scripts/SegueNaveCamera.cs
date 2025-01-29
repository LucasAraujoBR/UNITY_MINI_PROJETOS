using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // A nave que a câmera seguirá
    public Vector3 offset = new Vector3(0, 5, -10); // Offset inicial da câmera em relação à nave
    public float smoothSpeed = 0.125f; // Velocidade de suavização para posição
    public float rotationSmoothSpeed = 5f; // Velocidade de suavização para rotação

    private Vector3 velocity = Vector3.zero; // Usado para suavizar a movimentação

    void LateUpdate()
    {
        if (target != null)
        {
            // Calcula a posição desejada com base na orientação da nave e no offset
            Vector3 desiredPosition = target.TransformPoint(offset);

            // Suaviza a transição da posição
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

            // Calcula e suaviza a rotação para olhar para a nave
            Vector3 direction = target.position - transform.position;
            if (direction.sqrMagnitude > 0.001f) // Garante que há uma direção válida
            {
                Quaternion desiredRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSmoothSpeed * Time.deltaTime);
            }
        }
    }
}
