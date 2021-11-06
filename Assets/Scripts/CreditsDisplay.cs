using System;
using UnityEngine;
using TMPro;

public class CreditsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentBalance;

    public void Start()
    {
        currentBalance = GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void HandleOnBalanceUpdateEvent(int newBalance)
    {
        currentBalance.text = String.Format("Balance: ₡{0}", newBalance);
    }

}
