using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject enemy; // Лучше вместо GameObject класс Enemy, чтобы обозначить контракт. Вместо врага можно кнопку передать
    [SerializeField] [Range(0.1f, 35f)] private float spawnTime = 1f;
    [SerializeField] [Range(0,30)] private int poolSize = 5;

    private GameObject[] pool; 

    private void Awake()
    {
        PopulatePool();
    }
    
    private void Start()
    {
        StartCoroutine(SpawnEnemys());
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize]; 

        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(enemy, transform);
            pool[i].SetActive(false);
        }
    }

    private void EnablePoolMember()
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

    private IEnumerator SpawnEnemys()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            EnablePoolMember();
        }
    }
    
}
