using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance { get { return instance; } }

    [SerializeField] private int maxHealth = 100;
    public int MaxHealth { get { return maxHealth; } }

    [SerializeField] private int currentHealth;
    public int CurrentHealth { get { return currentHealth; } }

    public HealthBar healthBar;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }

        if (currentHealth < 1)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    //Maybe for health pickups
    private void GainHealth(int amount)
    {
        currentHealth = (currentHealth + amount) % (maxHealth + 1);
    }

    void Die()
    {
        // TODO Display defeat UI

        //remove after defeat UI is implemented
        Destroy(this.gameObject);
        Destroy(this.healthBar.gameObject);
    }

    private void OnParticleCollision(GameObject other)
    {
		GameObject attackingTowerObject = other.transform.parent.gameObject.transform.parent.gameObject;
		BasicTower attackingTower = attackingTowerObject.GetComponent<BasicTower>();

		int damageToTake = attackingTower.damagePerBullet;
		Debug.Log("Taking " + damageToTake + " damage... ouwie wouwie :(");
		TakeDamage(damageToTake);
    }
}
