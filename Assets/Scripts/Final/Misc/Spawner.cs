using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject leader;
    public GameObject prefabToSpawn;  // Objeto que se spawnea
    int numRows = 5;            // Número de filas en el triángulo
    public float spacing = 1.0f;       // Espaciado entre los objetos spawneados
    public int amountToSpawn;

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        Vector3 referencePosition = leader.transform.position;

        int counter = 0;

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col <= row; col++)
            {
                if (counter == amountToSpawn)
                    break;

                float xOffset = col * spacing - row * spacing * 0.5f;
                float zOffset = spacing + row * spacing;
                Vector3 spawnPosition = referencePosition + leader.transform.forward * zOffset +
                                        leader.transform.right * xOffset;

                Instantiate(prefabToSpawn, spawnPosition, leader.transform.rotation);
                counter++;
            }
        }
    }
}
