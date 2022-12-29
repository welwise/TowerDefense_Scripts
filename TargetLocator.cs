using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    [SerializeField] [Range(1f, 100f)] private float weaponRange = 10f;
    [SerializeField] private ParticleSystem projectilesParticles;
    private Transform target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        TargetAim();  
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistanse = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if(targetDistance < maxDistanse)
            {
                maxDistanse = targetDistance;
                closestTarget = enemy.transform;
            }
        }
        target = closestTarget;
    }

    void TargetAim()
    {
        if (target != null)
        {
            float targetDistance = Vector3.Distance(transform.position, target.position);
            weapon.transform.LookAt(target);
            if (targetDistance < weaponRange)
            {
                Attack(true);
            }
            else
            {
                Attack(false);
            }

        }
    }

    void Attack(bool isActive)
    {
        var emissionModule = projectilesParticles.emission;
        emissionModule.enabled = isActive;
    }
}
