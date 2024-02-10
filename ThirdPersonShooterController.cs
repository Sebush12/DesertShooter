using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ThirdPersonShooterController : MonoBehaviour {

    public GameObject bulletHit;
    //public GameObject gunTip;
    public GameObject playerObject;
    public int gunDamage;
    public float maxGunDistance;
    public Text displayBulletCount;
    public int MAX_BULLETS;

    public static int currentBullets;

    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    private AudioSource audioSource;
    private GameManager gameManager;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        currentBullets = MAX_BULLETS;
        UpdateBulletText();
    }

    private void Update() {

        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        
        Transform hitTransform = null;

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
         {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;
        }

        if (starterAssetsInputs.aim) {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        } else {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 13f));
        }
    
        /*
        if(starterAssetsInputs.shoot && currentBullets >0)
            {
            currentBullets--;
            UpdateBulletText();
            RaycastHit hit;
            if(Physics.Raycast(gunTip.transform.position, transform.forward, out hit, 1000f))
            {
                if(healthCollectables != null)
                {
                    healthCollectables.TakeDamage(gunDamage);
                    Debug.Log("Collectable took damage");
                }
                Instantiate(bulletHit, hit.point, Quaternion.identity);
                if (health != null)
                {
                    health.TakeDamage(gunDamage);
                    Debug.Log("Enemy hit");
                }
                //Instantiate(bulletHit, hit.point, Quaternion.identity);
                audioSource.Play();
            // Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask
            }
            }
            */
        
        if (starterAssetsInputs.shoot && currentBullets > 0) {
            currentBullets--;
            UpdateBulletText();
                

            // Hit Scan Shoot
            if (hitTransform != null) 
            {
                HealthSystemCollectables healthCollectables = hitTransform.GetComponent<HealthSystemCollectables>();
                HealthSystem health = hitTransform.GetComponent<HealthSystem>();
                // Hit something
                //if (hitTransform.GetComponent<BulletTarget>() != null) {
                    // Hit target
                    
                    //health.TakeDamage(gunDamage);
                   // Debug.Log("Enemy hit");
                
                if(healthCollectables != null)
                {
                    healthCollectables.TakeDamage(gunDamage);
                    Debug.Log("Collectable hit");
                }
                Instantiate(vfxHitGreen, mouseWorldPosition, Quaternion.identity);
                
                if (health != null)
                {
                    health.TakeDamage(gunDamage);
                    Debug.Log("Enemy hit");
                }
                
                /*
                 else {
                    // Hit something else
                    Instantiate(vfxHitRed, mouseWorldPosition, Quaternion.identity);

                //Instantiate(bulletHit, hit.point, Quaternion.identity);
                }
                */
                audioSource.Play();
            
            // Projectile Shoot
            //Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            //Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            
            starterAssetsInputs.shoot = false;
        }
            }
    }
    
    void UpdateBulletText()
    {
        displayBulletCount.text = currentBullets.ToString() + "/" + MAX_BULLETS.ToString();
    }
 }
 

    

