using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public GameObject player;
    public HealthBar healthBar;

    //private ThirdPersonController thirdPersonController;
   // private StarterAssetsInputs starterAssetsInputs;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    private GameManager gameManager;
   

    
    // Start is called before the first frame update
    void Start()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(starterAssetsInputs.jump)
        {
            TakeDamage(1);
        }
        
    }

    void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        healthBar.SetHealth(currentHealth);
    }
}
