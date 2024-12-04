using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float speed = 5f;
    public bool useArrowKeys = false; // Determina o esquema de controle

    void Update()
    {
        float horizontal = 0;
        float vertical = 0;

        if (useArrowKeys)
        {
            // Controles para a segunda nave
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        else
        {
            // Controles para a primeira nave (WASD)
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}
