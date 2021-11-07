using UnityEngine;
using UnityEngine.Events;

public class CreditsAccount : MonoBehaviour
{
    [System.Serializable] public class BalanceUpdateEvent : UnityEvent<int> { }
    public BalanceUpdateEvent OnBalanceUpdate;
    [SerializeField] private int currentBalance;
    public int CurrentBalance { get { return currentBalance; } }

    private CreditsDisplay display;

    public void Start()
    {
        if (OnBalanceUpdate == null)
        {
            OnBalanceUpdate = new BalanceUpdateEvent();
        }
        display = GameObject.FindWithTag("Player").GetComponent<CreditsDisplay>();
        Debug.Log(display);

        OnBalanceUpdate.AddListener(updateBalanceDisplay);
        OnBalanceUpdate.Invoke(currentBalance);
    }

    private void updateBalanceDisplay(int newBalance)
    {
        display.HandleOnBalanceUpdateEvent(newBalance);
    }

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
        return currentBalance >= amount;
    }
}
