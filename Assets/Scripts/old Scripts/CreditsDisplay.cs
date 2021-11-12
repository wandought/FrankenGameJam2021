using System;
using UnityEngine;
using TMPro;

public class CreditsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentBalanceText;

    public void HandleOnBalanceUpdateEvent(int newBalance)
    {
        currentBalanceText.text = String.Format("Balance: ₡{0}", newBalance);
    }

}
