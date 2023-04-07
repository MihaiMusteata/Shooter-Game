using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using StarterAssets;
using System.Diagnostics;

public class ThirdPersonShooterController : MonoBehaviour
{
     [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
     [SerializeField] private float normalSensivity;
     [SerializeField] private float aimSensivity;
     [SerializeField] private LayerMask aimColliderLayMask = new LayerMask();
     [SerializeField] private Transform debugTransform;
     [SerializeField] private Transform pfBulletProjectile;
     [SerializeField] private Transform spawnBulletPosition;



     private ThirdPersonController thirdPersonController;
     private StarterAssetsInputs starterAssetsInputs;


     private void Awake()
     {
          thirdPersonController = GetComponent<ThirdPersonController>();
          starterAssetsInputs = GetComponent<StarterAssetsInputs>();
     }

     void Update()
     {
          Vector3 mouseWorldPosition = Vector3.zero;
          Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
          Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
          if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayMask))
          {
               debugTransform.position = raycastHit.point;
               mouseWorldPosition = raycastHit.point;
          }
          if (starterAssetsInputs.aim)
          {
               aimVirtualCamera.gameObject.SetActive(true);
               thirdPersonController.SetSensivity(aimSensivity);
               thirdPersonController.SetRotateOnMove(false);
               Vector3 worldAimTarget = mouseWorldPosition;
               worldAimTarget.y = transform.position.y;
               Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

               transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
          }
          else
          {
               aimVirtualCamera.gameObject.SetActive(false);
               thirdPersonController.SetSensivity(normalSensivity);
               thirdPersonController.SetRotateOnMove(true);
          }

          if (starterAssetsInputs.fire)
          {
               Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
               Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
               starterAssetsInputs.fire = false;
          }

     }
}
