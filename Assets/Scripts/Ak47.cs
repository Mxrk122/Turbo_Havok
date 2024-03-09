using UnityEngine;
using System.Collections;

public class Ak47 : MonoBehaviour
{
    private bool isPickedUp = false;
    private bool isRotating = true; // Nueva bandera para controlar la rotación
    public float rotationSpeed = 200f;
    public float scaleFactor = 2f;
    public float rotationDuration = 5f; // Duración de la rotación después de ser recogido
    public float destroyTime = 5f;
    public int damageAmount = 10;
    public float maxRayDistance = 10f;// Rango del arma

    void Update()
    {
        // Obtener la posición y dirección del rayo desde el GameObject actual
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward; // Puedes ajustar esto según la dirección deseada

        // Disparar un raycast desde la posición del GameObject en la dirección especificada
        Ray ray = new Ray(rayOrigin, rayDirection);
        RaycastHit hit;

        // Verificar si el rayo impacta con un objeto
        if (Physics.Raycast(ray, out hit, maxRayDistance))
        {
            // Acciones cuando hay colisión
            Debug.Log("Objeto impactado: " + hit.collider.gameObject.tag);

            if(hit.collider.gameObject.tag == "Enemy"){
                Destroy(hit.collider.gameObject);
            }

            // También puedes realizar otras acciones, como cambiar propiedades del objeto impactado, etc.
        }
    }

    void OnDrawGizmos()
    {
        // Visualizar el rayo en la escena
        Ray ray = new Ray(transform.position, transform.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray.origin, ray.direction * maxRayDistance);
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (!isPickedUp)
        {
            Debug.Log(other.gameObject.name);
            Debug.Log(other.gameObject.tag);
            // Verifica si el objeto con el que colisiona es un jugador o un enemigo
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                Debug.Log("entre en un ");
                
                // Asigna el hacha al jugador o enemigo
                AssignAk(other.gameObject);
            }
        }
    }

    void AssignAk(GameObject collector)
    {
        isPickedUp = true;
        Debug.Log(collector.tag);
        // Desactiva el RigidBody para evitar que la física afecte al hacha
        GetComponent<Rigidbody>().isKinematic = true;

        // Haz que el hacha siga al jugador o enemigo}
        ControladorCarro car = collector.GetComponent<ControladorCarro>();
        if (car != null){
            transform.parent = car.ancla.transform;

            transform.forward = collector.transform.forward;


            // Eleva el objeto en el eje Y después de asignarlo como hijo
            //float elevationHeight = 1.42f; // Ajusta la altura de elevación según tus necesidades
            transform.localPosition = new Vector3(0f, 0f, 0f);

            // Inicia la rotación y escala del hacha
            StartCoroutine(RotateAndScale(collector.transform));

            // Inicia un temporizador para detener la rotación después de un tiempo
            StartCoroutine(StopRotation());
        }
    }

    IEnumerator RotateAndScale(Transform collectorTransform)
    {
        
        // Cambia la escala inmediatamente al valor deseado
        transform.localScale *= scaleFactor;

        while (isRotating) // Continúa rotando mientras la bandera isRotating sea verdadera
        {
            yield return null; // Espera hasta el próximo frame
        }
    }

    IEnumerator StopRotation()
    {
        yield return new WaitForSeconds(rotationDuration);

        // Desactiva la bandera de rotación después de un tiempo
        isRotating = false;

        // // Destruye el hacha después del tiempo determinado
        Destroy(gameObject);
    }

    void DealDamage(GameObject target)
    {
        // Implementa aquí la lógica para aplicar daño al objeto objetivo
        // Puedes usar métodos, eventos o scripts específicos para gestionar el daño.

        Debug.Log("El hacha ha causado " + damageAmount + " de daño a: " + target.name);
    }
}
