using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] [Range(0.1f, 35f)] private float spawnTime = 1f;
    [SerializeField] [Range(0,30)] private int poolSize = 5;
    private GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
    }
    
    void Start()
    {
        StartCoroutine(SpawnEnemys());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize]; 

        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(enemy, transform);
            pool[i].SetActive(false);
        }
    }

    void EnablePoolMember()
    {
        
        for (int i = 0; i < poolSize; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemys()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            EnablePoolMember();
        }
    }
    
}
