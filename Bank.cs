using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] private int startBalance;
    [SerializeField] private TextMeshProUGUI goldText;
    private int currentBalance;

    public int CurrentBalance {get { return currentBalance;}}
    void Awake()
    {
        currentBalance = startBalance;
        DisplayGold();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        DisplayGold();
        if (currentBalance < 0)
        {
            ReloadScene();
        }
    }

    public void Deposite(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        DisplayGold();
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
