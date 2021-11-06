using UnityEngine;
using UnityEngine.Events;

public class CreditsAccount : MonoBehaviour
{
    [System.Serializable] public class BalanceUpdateEvent : UnityEvent<float> { }
    public BalanceUpdateEvent OnBalanceUpdate;
    [SerializeField] private int currentBalance;
    public int CurrentBalance { get { return currentBalance; } }

    public void Pay(int amount)
    {
        currentBalance -= amount;
        OnBalanceUpdate.Invoke(currentBalance);
    }

    public void Earn(int amount)
    {
        currentBalance += amount;
        OnBalanceUpdate.Invoke(currentBalance);
    }

    public bool HasSufficientBalance(int amount)
    {
        return currentBalance > amount;
    }
}
