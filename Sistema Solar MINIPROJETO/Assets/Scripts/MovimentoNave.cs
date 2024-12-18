using UnityEngine;

public class ControlSpaceship : MonoBehaviour
{
    public float acceleration = 25f;
    public float speed = 100f;
    public float maxSpeed = 100f;
    public float sidewaysSpeed = 25f;
    public float rotationSpeed = 2f;
    public float gravityInfluenceFactor = 1f;

    private Vector3 targetForward = Vector3.zero;

    // Câmera que seguirá a nave
    public Transform cameraTransform;
    public Vector3 cameraOffset = new Vector3(0, 5, -10);

    // Referência ao AudioSource
    private AudioSource engineAudio;

    void Start()
    {
        // Inicializar a direção da nave
        targetForward = transform.forward;

        // Verificar se a câmera foi atribuída
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        // Obter o AudioSource do GameObject
        engineAudio = GetComponent<AudioSource>();
        if (engineAudio == null)
        {
            Debug.LogWarning("AudioSource não encontrado! Certifique-se de que um está adicionado ao GameObject.");
        }
    }

    void Update()
    {
        // Entrada do mouse para rotação
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        // Aplicar rotação limitada
        float pitch = Mathf.Clamp(-deltaY * rotationSpeed, -45f, 45f);
        float yaw = deltaX * rotationSpeed;

        targetForward = Quaternion.AngleAxis(yaw, transform.up) * targetForward;
        targetForward = Quaternion.AngleAxis(pitch, transform.right) * targetForward;

        transform.forward = Vector3.Lerp(transform.forward, targetForward, Time.deltaTime * rotationSpeed);

        // Aceleração e desaceleração
        if (Input.GetKey(KeyCode.W))
        {
            speed += acceleration * Time.deltaTime;

            // Tocar o som do motor ao acelerar
             if (engineAudio != null && !engineAudio.isPlaying)
             {
              engineAudio.Play();
             }
        }
        else
        {
            speed -= acceleration * Time.deltaTime;

            // Parar o som do motor quando não estiver acelerando
            if (engineAudio != null && engineAudio.isPlaying)
            {
            engineAudio.Stop();
            }
        }

        // Limitar velocidade
        speed = Mathf.Clamp(speed, 0, maxSpeed);

        // Movimentar a nave para frente
        transform.position += transform.forward * speed * Time.deltaTime;

        // Movimentos laterais e verticais
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * sidewaysSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * sidewaysSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position -= transform.up * sidewaysSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.position += transform.up * sidewaysSpeed * Time.deltaTime;
        }

        // Influência gravitacional de corpos celestiais
        foreach (var celestialBody in GameObject.FindGameObjectsWithTag("CelestialBody"))
        {
            GameObject body = celestialBody.transform.Find("Body")?.gameObject;
            if (body != null)
            {
                float radius = body.transform.localScale.x / 2;

                float distanceToSurface = Vector3.Distance(transform.position, celestialBody.transform.position) - radius;
                float gInfluence = gravityInfluenceFactor * radius;

                float t = 1 - Mathf.Clamp01(distanceToSurface / gInfluence);

                if (distanceToSurface < gInfluence)
                {
                    Vector3 targetUp = (transform.position - celestialBody.transform.position).normalized;
                    Vector3 currentUp = Vector3.Lerp(transform.up, targetUp, t);
                    transform.LookAt(transform.position + transform.forward, currentUp);
                }
            }
        }

        // Atualizar posição e orientação da câmera
        if (cameraTransform != null)
        {
            cameraTransform.position = transform.position + cameraOffset;
            cameraTransform.LookAt(transform.position);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + targetForward * 5);
    }
}
