using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int towerCost = 50;

    public bool CreateTower(Vector3 position)
    {
        var bank = TryFindBank();
        var isEnoughBalance = bank.CurrentBalance >= towerCost;
        if (!isEnoughBalance)
            return;

        Instantiate(this, position, Quaternion.identity);
        bank.Withdraw(towerCost);
        return isEnoughBalance;
    }

    private Bank TryFindBank()
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null)
            throw new Exception("Failed to find Band!");

        return bank;
    }
}
