using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
			[SerializeField] private int maxHealth = 25;
			public int MaxHealth { get { return maxHealth; } }

			[SerializeField] private int currentHealth;
			public int CurrentHealth { get { return currentHealth; } }

			public HealthBar healthBar;

			// Start is called before the first frame update
			void Start()
    {
						currentHealth = maxHealth;
						healthBar.SetMaxHealth(maxHealth);
			}

    // Update is called once per frame
    void Update()
    {
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

		 public void ResetHealth()
			{
						currentHealth = maxHealth;
						healthBar.SetMaxHealth(maxHealth);
			}

			void Die()
			{
						// TODO Display defeat UI

						//remove after defeat UI is implemented
						this.gameObject.SetActive(false);
			}

			private void OnParticleCollision(GameObject other)
			{
						TakeDamage(other.transform.parent.parent.GetComponent<BasicTower>().damagePerBullet);
			}

}
