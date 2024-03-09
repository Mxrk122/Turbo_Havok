using UnityEngine;
using System.Collections;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject[] weaponPrefabs; // Lista de modelos de armas
    public int maxWeapons = 20; // Cantidad máxima de armas permitidas en el juego
     public float spawnRadius = 10f; // Radio en el que se generarán las armas
    public float spawnInterval = 5f; // Intervalo de tiempo entre spawns
    public float maxRayDistance = 100f;//Debug
    void Start()
    {
        StartCoroutine(SpawnWeaponsCoroutine());
    }

    IEnumerator SpawnWeaponsCoroutine()
    {
        while (true)
        {
            if (transform.childCount < maxWeapons)
            {
                SpawnWeapon();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnWeapon()
    {
        Vector3 randomPosition = transform.position + Random.onUnitSphere * spawnRadius;
        randomPosition.y = 0f; // Asegúrate de que las armas estén en el plano del suelo
        randomPosition.y = 0f; // Asegúrate de que las armas estén en el plano del suelo

        // 3.51 y

        // Generar un número float aleatorio entre -40 y 40
        float x = UnityEngine.Random.Range(-30f, 30f);

        // Generar un número float aleatorio entre -30 y 30
        float z = UnityEngine.Random.Range(-40f, 40f);

        randomPosition = new Vector3(x, 3.51f, z);
        GameObject weaponPrefab = GetRandomWeaponPrefab();
        GameObject weapon = Instantiate(weaponPrefab, randomPosition, Quaternion.identity);
        weapon.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f); // Gira el arma al azar
        weapon.transform.parent = transform; // Asegúrate de que las armas estén bajo el WeaponManager
    }
    GameObject GetRandomWeaponPrefab()
    {
        if (weaponPrefabs.Length == 0)
        {
            Debug.LogError("No hay modelos de armas asignados en el inspector.");
            return null;
        }

        int randomIndex = Random.Range(0, weaponPrefabs.Length);
        return weaponPrefabs[randomIndex];
    }
}
