using System;
using UnityEngine;
using TMPro;

public class CreditsAccount : MonoBehaviour
{
    private int creditBalance = 550;
    public TextMeshProUGUI currentBalance = null;

    public void Start()
    {
        currentBalance = GetComponent<TMPro.TextMeshProUGUI>();
        currentBalance.text = String.Format("Balance: ₡{0}", creditBalance.ToString());
    }

    public void SetBalance(int amount)
    {
        creditBalance = amount;
        currentBalance.text = String.Format("Balance: ₡{0}", creditBalance.ToString());
    }

    public void Pay(int amount)
    {
        creditBalance -= amount;
        currentBalance.text = String.Format("Balance: ₡{0}", creditBalance.ToString());
    }

    public bool HasSufficientFunds(int cost)
    {
        return cost <= creditBalance;
    }

}
