using UnityEngine;

public class ControladorCarro : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento del carro
    public float velocidadRotacion = 100f; // Velocidad de rotación del carro
    public GameObject ancla;

    void Update()
    {
        // Obtener las entradas de teclado o joystick
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calcular el vector de dirección basado en las entradas
        Vector3 direccion = new Vector3(horizontal, 0f, vertical).normalized;

        // Mover y rotar el carro en la dirección calculada
        MoverCarro(direccion);
    }

    void MoverCarro(Vector3 direccion)
    {
        // Obtener el componente Rigidbody adjunto al carro
        Rigidbody rb = GetComponent<Rigidbody>();

        // Calcular el vector de movimiento en coordenadas globales
        Vector3 movimiento = transform.TransformDirection(direccion) * velocidad * Time.deltaTime;

        // Aplicar el movimiento al Rigidbody
        rb.MovePosition(rb.position + movimiento);

        // Calcular la rotación en función de la entrada horizontal
        if (direccion.x > 0)
        {
            // Rotar gradualmente hacia la derecha
            transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime);
        }
        else if (direccion.x < 0)
        {
            // Rotar gradualmente hacia la izquierda
            transform.Rotate(Vector3.up, -velocidadRotacion * Time.deltaTime);
        }
    }

}
