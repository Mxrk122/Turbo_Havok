using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    private bool isPickedUp = false;
    private bool isRotating = true; // Nueva bandera para controlar la rotación
    public float rotationSpeed = 200f;
    public float scaleFactor = 2f;
    public float rotationDuration = 10f; // Duración de la rotación después de ser recogido
    public float destroyTime = 10f;
    public int damageAmount = 10;

    void OnTriggerEnter(Collider other)
    {
        
        if (!isPickedUp)
        {
            // Verifica si el objeto con el que colisiona es un jugador o un enemigo
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                
                // asignar variable al carro
                CarUI otherScript = other.gameObject.GetComponent<CarUI>();

                if (otherScript != null)
                {
                    // Accede a la variable miVariable del script OtherScript
                    otherScript.havegun = true;
                    bool havegun = otherScript.havegun;
                    Debug.Log("Valor de miVariable: " + havegun);
                    
                    // Asigna el hacha al jugador o enemigo
                    AssignAxe(other.gameObject);

                    Debug.Log("Valor de miVariable: " + otherScript.havegun);
                }
            }
        }
    }

    void AssignAxe(GameObject collector)
    {
        isPickedUp = true;

        // asignar variable al carro
        CarUI otherScript = collector.GetComponent<CarUI>();

        // Desactiva el RigidBody para evitar que la física afecte al hacha
        GetComponent<Rigidbody>().isKinematic = true;

        // Haz que el hacha siga al jugador o enemigo
        transform.parent = collector.transform;
        
        // Inicia la rotación y escala del hacha
        StartCoroutine(RotateAndScale(collector.transform));

        // Inicia un temporizador para detener la rotación después de un tiempo
        StartCoroutine(StopRotation());

        otherScript.havegun = false;
        
    }

    IEnumerator RotateAndScale(Transform collectorTransform)
    {
        // Posiciona el hacha en el centro del jugador o enemigo
        transform.position = collectorTransform.position;

        // Cambia la escala inmediatamente al valor deseado
        transform.localScale *= scaleFactor;

        while (isRotating) // Continúa rotando mientras la bandera isRotating sea verdadera
        {
            // Aplica la rotación alrededor del eje Y desde el centro del jugador o enemigo
            transform.RotateAround(collectorTransform.position, Vector3.right, rotationSpeed * Time.deltaTime);

            yield return null; // Espera hasta el próximo frame
        }


    }

    IEnumerator StopRotation()
    {
        yield return new WaitForSeconds(rotationDuration);

        // Desactiva la bandera de rotación después de un tiempo
        isRotating = false;

        // Destruye el hacha después del tiempo determinado
        Destroy(gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        if (isPickedUp && (other.CompareTag("Player") || other.CompareTag("Enemy")) && other.gameObject != transform.parent.gameObject)
        {
            // Aplica daño al jugador o enemigo
            DealDamage(other.gameObject);
        }
    }

    void DealDamage(GameObject target)
    {
        // Implementa aquí la lógica para aplicar daño al objeto objetivo
        // Puedes usar métodos, eventos o scripts específicos para gestionar el daño.
        Debug.Log("El hacha ha causado " + damageAmount + " de daño a: " + target.name);

        Destroy(target);
    }
}
