using UnityEngine;
using System.Collections;
public class PowerUp : MonoBehaviour
{
    private bool isPickedUp = false;
    public float rotationSpeed = 200f; // Velocidad de rotación en grados por segundo
    public float scaleFactor = 2f; // Factor de escala deseado
    public float destroyTime = 10f; // Tiempo después del cual se destruirá el objeto
    public int damageAmount = 10; // Cantidad de daño que causa el power-up

    void OnTriggerEnter(Collider other)
    {
        if (!isPickedUp)
        {
            // Verifica si el objeto con el que colisiona es un jugador o un enemigo
            if ((other.CompareTag("Player") || other.CompareTag("Enemy")) && !other.gameObject.Equals(transform.parent))
            {
                // Asigna el power-up al carro
                AssignPowerUp(other.gameObject);
            }
        }
    }

    void AssignPowerUp(GameObject collector)
    {
        isPickedUp = true;

        // Desactiva el RigidBody para evitar que la física afecte al power-up
        GetComponent<Rigidbody>().isKinematic = true;

        // Haz que el power-up siga al carro
        transform.parent = collector.transform;

        // Inicia la rotación y escala del power-up
        StartCoroutine(RotateAndScale(collector.transform));
    }

    IEnumerator RotateAndScale(Transform collectorTransform)
    {
        // Posiciona el power-up en el centro del carro
        transform.position = collectorTransform.position;

        // Cambia la escala inmediatamente al valor deseado
        transform.localScale *= scaleFactor;

        while (isPickedUp) // Continúa rotando mientras el power-up está recogido
        {
            // Aplica la rotación alrededor del eje Y desde el centro del carro
            transform.RotateAround(collectorTransform.position, Vector3.right, rotationSpeed * Time.deltaTime);

            yield return null; // Espera hasta el próximo frame
        }

        yield return new WaitForSeconds(destroyTime);

        // Destruye el power-up después del tiempo determinado
        Destroy(gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log(transform.parent);
        // Verifica si el power-up ha sido recogido y si el objeto colisionado es un jugador o enemigo
        if (isPickedUp && (other.CompareTag("Player") || other.CompareTag("Enemy")) && !other.gameObject.Equals(transform.parent))
        {
            // Aplica daño al objeto colisionado
            DealDamage(other.gameObject);
        }
    }

    void DealDamage(GameObject target)
    {
        // Implementa aquí la lógica para aplicar daño al objeto objetivo
        // Puedes usar métodos, eventos o scripts específicos para gestionar el daño.

        Debug.Log("El power-up ha causado " + damageAmount + " de daño a: " + target.name);
    }
}
