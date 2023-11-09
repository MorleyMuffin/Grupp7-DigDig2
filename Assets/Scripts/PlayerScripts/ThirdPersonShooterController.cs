using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;
using System;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;

    [Header("Sensitivity + else")]
    [SerializeField] private float normalCameraSensitivity;
    [SerializeField] private float aimCameraSensitivity;

    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;

    [Header("Shooting")]
    [SerializeField] private float weaponDamage = 3f;

    [SerializeField] float shootingWaitTime = 1f;
    [SerializeField] float currentShootingWaitTime;
    [SerializeField] bool canShoot = true;

    [SerializeField] bool isShooting = false;
    [SerializeField] float shootingTimer = 0.0f;

    [SerializeField] bool canFire = true; // erm isnt this just the same thing as canShoot?

    [SerializeField] float reloadTime = 1.5f;
    private bool canReload = true;
    [SerializeField] TextMeshProUGUI reloadingDisplay; // Temporary until sound and animation is added to indicate that reloading is happening.

    [SerializeField] int maxBullets = 10;
    [SerializeField] int currentBulletAmount;
    [SerializeField] TextMeshProUGUI bulletDisplay; // Change to show x amount of bullets instead of text    

    [SerializeField] Slider canShootBar;


    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetInputs;

    private EnemyHealth enemyHealth;

    private void Awake()
    {
        reloadingDisplay.enabled = false;

        currentBulletAmount = maxBullets;

        canShootBar.maxValue = shootingWaitTime;
        currentShootingWaitTime = canShootBar.maxValue;


        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetInputs = GetComponent<StarterAssetsInputs>();
    }
    void Update()
    {
        

        #region Aiming
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

        if ( starterAssetInputs.aim == true) 
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetCameraSensitivity(aimCameraSensitivity);
            thirdPersonController.SetRotationOnMove(false);

            thirdPersonController.currentSprintSpeed = thirdPersonController.aimingSprintSpeed;
            thirdPersonController.currentMoveSpeed = thirdPersonController.aimingMoveSpeed;


            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f); 
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive (false);
            thirdPersonController.SetCameraSensitivity (normalCameraSensitivity);
            thirdPersonController.SetRotationOnMove(false);

            thirdPersonController.currentSprintSpeed = thirdPersonController.sprintSpeed;
            thirdPersonController.currentMoveSpeed = thirdPersonController.moveSpeed;

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

        }
        #endregion 

        #region Shooting

        if (currentBulletAmount <= 0)
        {
            canShoot = false;
        }
        if (starterAssetInputs.reload == true && isShooting == false && canReload == true && currentBulletAmount < maxBullets)// temperary
        {
            

            StartCoroutine(TimeToReload());
        }

        if  (starterAssetInputs.shoot && canFire)
        {
            if (canShoot == true)
            {
                isShooting = true;
                shootingTimer = 0.0f;

                if (hitTransform != null)
                {
                    if (hitTransform.GetComponent<EnemyHealth>() != null)
                    {
                        Instantiate(vfxHitRed, raycastHit.point, Quaternion.identity); 
                        enemyHealth = hitTransform.GetComponent<EnemyHealth>(); 
                        enemyHealth.DamageToEnemy(weaponDamage);
                    }
                    else
                    {
                        Instantiate(vfxHitGreen, raycastHit.point, Quaternion.identity);
                    }
                }

                currentBulletAmount--;

                starterAssetInputs.shoot = false;
                StartCoroutine(TimeUntilCanShoot());

               // canShoot = false;
               canFire = false;
            }
        }
        bulletDisplay.text = ($"bullets {currentBulletAmount}");

        if (isShooting)
        {
            // Increment shooting timer
            shootingTimer += Time.deltaTime;

            // Calculate the interpolation factor (0 to 1)
            float t = Mathf.Clamp01(shootingTimer / shootingWaitTime);

            // Interpolate the value using SmoothStep (you can also use Lerp)
            currentShootingWaitTime = Mathf.SmoothStep(0.0f, shootingWaitTime, t);

            starterAssetInputs.shoot = false;
            // Check if shooting time has reached the max
            if (shootingTimer >= shootingWaitTime)
            {
                isShooting = false;  // Reset shooting flag

                //canShoot = true;
                canFire = true;
            }
        }


        canShootBar.value = currentShootingWaitTime;
        
        #endregion

    }

    private IEnumerator TimeUntilCanShoot()  
    {
        canShoot = false;
        Debug.Log("Cant shoott");
        yield return new WaitForSeconds(shootingWaitTime);
        canShoot = true;
        Debug.Log("Can shoooot");
    }

    private IEnumerator TimeToReload()
    {
        canReload = false;
        starterAssetInputs.reload = false;

        reloadingDisplay.text = ("Reloading");
        reloadingDisplay.enabled = true;

        yield return new WaitForSeconds(reloadTime);

        reloadingDisplay.enabled = false;
        currentBulletAmount = maxBullets;
        canShoot = true;
        starterAssetInputs.shoot = false;

        canReload = true;
    }
    
}
