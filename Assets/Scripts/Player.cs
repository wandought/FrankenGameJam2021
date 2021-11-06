using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))] [RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance { get { return instance; } }

    private Health health;
    public int Health { get { return health.CurrentHealth; }
        set { health.TakeDamage(value - health.MaxHealth);/*health.TakeDamage(health.MaxHealth - value);*/ } }
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
        health = GetComponent<Health>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            health.TakeDamage(20);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
		BasicTower attackingTower = other.transform.parent.parent.GetComponent<BasicTower>();

        int damageToTake = attackingTower.damagePerBullet;
		Debug.Log("Taking " + damageToTake + " damage... ouwie wouwie :(");
		health.TakeDamage(damageToTake);
    }
}
