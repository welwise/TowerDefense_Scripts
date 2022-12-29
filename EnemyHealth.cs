using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHitPoints;
    [Tooltip("Add amount to max hitPoint, when enemy dies")]

    [SerializeField] private int difficultyRamp = 1;

    private int currentHitPoints = 0;
    private Enemy enemy;

    void OnEnable()
    {
        currentHitPoints = 0;
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void CheckHealth()
    {
        if(currentHitPoints >= maxHitPoints)
        {
            gameObject.SetActive(false);
            maxHitPoints += difficultyRamp;
            enemy.RewardGold();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.tag == "Bows")
        {
            currentHitPoints++;
        }
        else if(other.gameObject.tag == "Cores")
        {
            currentHitPoints += 2;
        }
        CheckHealth();
    }
}
