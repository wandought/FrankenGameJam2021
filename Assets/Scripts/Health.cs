using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int maxHealth = 25;
    public int MaxHealth { get { return maxHealth; } }

    private int currentHealth;
    public int CurrentHealth { get { return currentHealth; } }


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            damage = Mathf.Abs(damage);

        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();

        healthBar.SetHealth(currentHealth);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void GainHealth(int amount)
    {
        if (amount < 0)
            amount = Mathf.Abs(amount);

        currentHealth = (currentHealth + amount) % (maxHealth + 1);
    }

    void Die()
    {
        // TODO Display defeat UI

        //remove after defeat UI is implemented
        gameObject.SetActive(false);
    }

    private void OnParticleCollision(GameObject other)
    {
        TakeDamage(other.transform.parent.parent.GetComponent<BasicTower>().damagePerBullet);
    }

}
