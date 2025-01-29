using UnityEngine;
using TMPro; // Importação necessária para TextMeshProUGUI

public class ControlSpaceship : MonoBehaviour
{
    public float acceleration = 25f;
    public float speed = 100f;
    public float maxSpeed = 100f;
    public float sidewaysSpeed = 25f;
    public float rotationSpeed = 2f;

    private Vector3 targetForward = Vector3.zero;

    public Transform cameraTransform;
    public Vector3 cameraOffset = new Vector3(0, 5, -10);

    private AudioSource engineAudio;
    public ParticleSystem propulsionEffect; // Efeito de propulsão

    // Referência ao campo de texto da UI
    public TextMeshProUGUI nomeJogadoresText;

    void Start()
    {
        targetForward = transform.forward;

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        engineAudio = GetComponent<AudioSource>();
        if (engineAudio == null)
        {
            Debug.LogWarning("AudioSource não encontrado! Certifique-se de que um está adicionado ao GameObject.");
        }

        // Recuperar os nomes dos jogadores armazenados
        string nomeJogador1 = PlayerPrefs.GetString("NomeJogador1", "Jogador 1");
        string nomeJogador2 = PlayerPrefs.GetString("NomeJogador2", "Jogador 2");

        // Exibir os nomes na UI
        if (nomeJogadoresText != null)
        {
            nomeJogadoresText.text = (nomeJogador2 == "Jogador 2")
                ? $"Piloto: {nomeJogador1}"
                : $"Pilotos: {nomeJogador1} & {nomeJogador2}";
        }
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        float pitch = Mathf.Clamp(-deltaY * rotationSpeed, -45f, 45f);
        float yaw = deltaX * rotationSpeed;

        targetForward = Quaternion.AngleAxis(yaw, transform.up) * targetForward;
        targetForward = Quaternion.AngleAxis(pitch, transform.right) * targetForward;

        transform.forward = Vector3.Lerp(transform.forward, targetForward, Time.deltaTime * rotationSpeed);

        if (Input.GetKey(KeyCode.W))
        {
            speed += acceleration * Time.deltaTime;

            if (engineAudio != null && !engineAudio.isPlaying)
                engineAudio.Play();

            if (propulsionEffect != null)
            {
                if (!propulsionEffect.isPlaying)
                {
                    propulsionEffect.Play();
                    Debug.Log("Efeito de propulsão ativado!");
                }
            }
        }

        else
        {
            speed -= acceleration * Time.deltaTime;
            if (engineAudio != null && engineAudio.isPlaying) engineAudio.Stop();

            if (propulsionEffect != null && propulsionEffect.isPlaying)
                propulsionEffect.Stop();
        }

        speed = Mathf.Clamp(speed, 0, maxSpeed);
        transform.position += transform.forward * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) transform.position -= transform.right * sidewaysSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) transform.position += transform.right * sidewaysSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Q)) transform.position -= transform.up * sidewaysSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E)) transform.position += transform.up * sidewaysSpeed * Time.deltaTime;

        if (cameraTransform != null)
        {
            cameraTransform.position = transform.position + cameraOffset;
            propulsionEffect.transform.position = transform.position - transform.forward * 2f; // Ajuste a distância
            propulsionEffect.transform.rotation = transform.rotation;
            cameraTransform.LookAt(transform.position);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + targetForward * 5);
    }
}
