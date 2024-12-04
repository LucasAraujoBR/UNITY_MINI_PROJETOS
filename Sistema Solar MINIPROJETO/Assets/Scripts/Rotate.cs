using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Transform target; // O centro de rotação
    public int speed; // Velocidade de rotação

    void Update()
    {
        if (target != null)
        {
            // Rotação ao redor do centro definido
            transform.RotateAround(target.position, target.up, speed * Time.deltaTime);
        }
    }
}
