using UnityEngine;

public class enemy_ia : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad a la que el enemigo seguirá al jugador
    private Transform jugador; // Referencia al jugador

    void Start()
    {
        // Encuentra al jugador en la escena
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Calcula la dirección hacia el jugador
        Vector3 direccion = (jugador.position - transform.position).normalized;

        // Hacer que el enemigo mire hacia el jugador
        Quaternion lookRotation = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * velocidad);

        // Mover al enemigo hacia adelante
        transform.position += transform.forward * velocidad * Time.deltaTime;
    }
}

