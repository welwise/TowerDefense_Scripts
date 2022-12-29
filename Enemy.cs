using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int goldReward = 20;
    [SerializeField] private int goldPenalty = 40;

    private Bank bank;

    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        if(bank == null) {return;}
        bank.Deposite(goldReward);
    }

    public void StealGold()
    {
        if (bank == null) { return; }
        bank.Withdraw(goldPenalty);
    }
}
