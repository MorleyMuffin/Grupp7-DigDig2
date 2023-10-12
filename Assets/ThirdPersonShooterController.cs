using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;

    [SerializeField] private float normalCameraSensitivity;
    [SerializeField] private float aimCameraSensitivity;

    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;

    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetInputs;

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetInputs = GetComponent<StarterAssetsInputs>();
    }
    void Update()
    {
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

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f); 
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive (false);
            thirdPersonController.SetCameraSensitivity (normalCameraSensitivity);
            thirdPersonController.SetRotationOnMove(true);
        }

        if  (starterAssetInputs.shoot)
        {
            if(hitTransform != null) 
            {
                if(hitTransform.GetComponent<BulletTarget>() != null)
                {
                    Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(vfxHitRed, transform.position, Quaternion.identity);
                }
            }
            starterAssetInputs.shoot = false;
        }

    }
}
