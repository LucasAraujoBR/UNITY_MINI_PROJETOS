using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // A nave que a c�mera seguir�
    public Vector3 offset = new Vector3(0, 5, -10); // Offset inicial da c�mera em rela��o � nave
    public float smoothSpeed = 0.125f; // Velocidade de suaviza��o para posi��o
    public float rotationSmoothSpeed = 5f; // Velocidade de suaviza��o para rota��o

    private Vector3 velocity = Vector3.zero; // Usado para suavizar a movimenta��o

    void LateUpdate()
    {
        if (target != null)
        {
            // Calcula a posi��o desejada com base na orienta��o da nave e no offset
            Vector3 desiredPosition = target.TransformPoint(offset);

            // Suaviza a transi��o da posi��o
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

            // Calcula e suaviza a rota��o para olhar para a nave
            Vector3 direction = target.position - transform.position;
            if (direction.sqrMagnitude > 0.001f) // Garante que h� uma dire��o v�lida
            {
                Quaternion desiredRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSmoothSpeed * Time.deltaTime);
            }
        }
    }
}
