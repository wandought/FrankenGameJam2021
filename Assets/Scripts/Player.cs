using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

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

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    void Die()
    {
        // TODO this is probably stupid
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
