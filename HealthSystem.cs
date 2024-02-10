using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthSystem : MonoBehaviour
{

    public int maxHealth;
    private int currentHealth;
    public GameObject player;
    private bool isAlive;
    public Text healthText;

    //public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
        SetHealthText();
        
    }

    public void TakeDamage(int damageToTake)
    {
        if(isAlive)
        {
            currentHealth -= damageToTake;
            if(currentHealth <=0)
            {
            isAlive = false;
            currentHealth = 0;
            Death();
            }
            SetHealthText();
           
        }
        //healthBar.SetHealth(currentHealth);
    }

    public void Death() 
    {
        Debug.Log("Enemy Died");
        Destroy(gameObject);
        player.GetComponent<GameManager>().enemyScore ++;
        

    }

    public void SetHealthText()  
    {
        healthText.text = "Health: " + currentHealth.ToString();

    }
}
