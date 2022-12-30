using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int goldReward = 20;
    [SerializeField] private int goldPenalty = 40;

    private Bank bank;

    private void Awake()
    {
        bank = FindObjectOfType<Bank>();
        if (bank == null)
            throw new Exception("There is no bank found on scene!");
    }

    public void RewardGold()
    {
        bank.Deposite(goldReward);
    }

    public void StealGold()
    {
        bank.Withdraw(goldPenalty);
    }
}
