using UnityEngine;

public class ControladorCarro : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento del carro

    void Update()
    {
        // Obtener las entradas de teclado o joystick
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calcular el vector de dirección basado en las entradas
        Vector3 direccion = new Vector3(horizontal, 0f, vertical).normalized;

        // Mover el carro en la dirección calculada
        MoverCarro(direccion);
    }

    void MoverCarro(Vector3 direccion)
    {
        // Obtener el componente Rigidbody adjunto al carro
        Rigidbody rb = GetComponent<Rigidbody>();

        // Calcular el vector de movimiento
        Vector3 movimiento = direccion * velocidad * Time.deltaTime;

        //Se transforma el collider del carro para rotarlo según si va a la derecha o izquierda
        if (direccion.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (direccion.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }

        // Aplicar el movimiento al Rigidbody
        rb.MovePosition(rb.position + movimiento);
    }
}
