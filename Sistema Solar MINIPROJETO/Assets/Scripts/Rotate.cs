using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Transform target; // O centro de rota��o
    public int speed; // Velocidade de rota��o

    void Update()
    {
        if (target != null)
        {
            // Rota��o ao redor do centro definido
            transform.RotateAround(target.position, target.up, speed * Time.deltaTime);
        }
    }
}
