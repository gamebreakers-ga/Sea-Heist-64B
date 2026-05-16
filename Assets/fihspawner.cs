using System.Collections;
using UnityEngine;

public class fishspawner : MonoBehaviour
{
    public GameObject fishPrefab;

    public float spawnInterval = 3f;
    public float spawnRange = 15f;

    void Start()
    {
        StartCoroutine(SpawnFish());
        spawnInterval = spawnInterval + Random.Range(-1f, 1f);
    }

    IEnumerator SpawnFish()
    {
        while (true)
        {
            Vector3 spawnPos = transform.position + new Vector3(
                Random.Range(-spawnRange, spawnRange),
                Random.Range(-spawnRange, spawnRange),
                Random.Range(-spawnRange, spawnRange)
            );

            Instantiate(fishPrefab, spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}