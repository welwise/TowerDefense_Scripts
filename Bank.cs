using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] [Min(0)] private int startBalance;
    [SerializeField] private TextMeshProUGUI goldText;

    private int currentBalance;

    public int CurrentBalance => currentBalance;

    private void Awake()
    {
        currentBalance = startBalance;
        DisplayGold();
    }

    public void Withdraw(int amount)
    {
        CheckAmount(amount);

        currentBalance -= amount;
        if (currentBalance < 0)
            ReloadScene();

        DisplayGold();
    }

    public void Deposite(int amount)
    {
        CheckAmount(amount);

        currentBalance += amount;
        DisplayGold();
    }
    
    private void CheckAmount(int amount)
    {
        if (amount < 0)
            throw new Exception("Amount can't be lower than zero!");
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void DisplayGold()
    {
        goldText.text = "Gold: " + currentBalance;
    }
}
