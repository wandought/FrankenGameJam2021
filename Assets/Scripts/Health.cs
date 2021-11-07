using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float maxHealth = 25;
    public float MaxHealth { get { return maxHealth; } }

    private float currentHealth;
    public float CurrentHealth { get { return currentHealth; } }

    private GameObject canvas;
    private void Awake()
    {
        if (gameObject.tag == "Player")
        {
            canvas = GameObject.FindWithTag("Canvas");
            canvas.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage)
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
        healthBar.SetHealth(currentHealth);
    }

    private void OnEnable()
    {
        ResetHealth();
    }

    private void GainHealth(float amount)
    {
        if (amount < 0)
            amount = Mathf.Abs(amount);

        currentHealth = (currentHealth + amount) % (maxHealth + 1);
        healthBar.SetHealth(currentHealth);
    }

    void Die()
    {
        if (gameObject.tag != "Player")
        {
            int bounty = gameObject.GetComponent<Enemy>().Bounty;
            Administrator.Instance.CreditsAccount.Earn(bounty);
        }
        else
        {
            Debug.Log("Activating Canvas");
            canvas.SetActive(true);
        }

        //remove after defeat UI is implemented
        gameObject.SetActive(false);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<PlayerController>() != null)
            TakeDamage(3);
        else
            TakeDamage(other.transform.parent.parent.GetComponent<BasicTower>().DamagePerBullet);
    }

}
