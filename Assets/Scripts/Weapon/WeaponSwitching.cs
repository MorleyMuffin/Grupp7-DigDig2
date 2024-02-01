using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;

    public WeaponBase weapon0;
    int weapon0CurrentAmmoAmount;
    
    public WeaponBase weapon1;
    int weapon1CurrentAmmoAmount;

    WeaponBase currentWeapon;
    int currentWeaponCurrentAmmoAmount;

    ThirdPersonShooterController parentShootingScript;

    private void Start()
    {
        parentShootingScript = GetComponentInParent<ThirdPersonShooterController>();

        // Just until we make a system where you can change weapons.
        weapon0CurrentAmmoAmount = weapon0.weaponAmmoAmount;
        weapon1CurrentAmmoAmount = weapon1.weaponAmmoAmount;
        currentWeaponCurrentAmmoAmount = weapon1CurrentAmmoAmount;
        parentShootingScript.maxBullets = weapon0.weaponAmmoAmount;
        parentShootingScript.currentBulletAmount = weapon0CurrentAmmoAmount;

   
        ChangeToNewShootingValues();


    }

    private void Update()
    {

        int previousSelectedWeapon = selectedWeapon;

        if(parentShootingScript.canShoot == true) // cant change weapon if bullet amount is 0
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (selectedWeapon >= transform.childCount - 1)
                {
                    if (weapon0 != null)
                    {
                        selectedWeapon = 0;
                        currentWeapon = weapon0;
                        weapon1CurrentAmmoAmount = parentShootingScript.currentBulletAmount;
                    }

                    ChangeToNewShootingValues();
                    currentWeaponCurrentAmmoAmount = weapon1CurrentAmmoAmount;
                }
                else
                {


                    if (weapon1 != null)
                    {
                        selectedWeapon++;
                        currentWeapon = weapon1;
                        weapon0CurrentAmmoAmount = parentShootingScript.currentBulletAmount;
                    }

                    ChangeToNewShootingValues();
                    currentWeaponCurrentAmmoAmount = weapon0CurrentAmmoAmount;
                }
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (selectedWeapon <= 0)
                {
                    if (weapon1 != null)
                    {
                        selectedWeapon = transform.childCount - 1;
                        currentWeapon = weapon1;
                        weapon0CurrentAmmoAmount = parentShootingScript.currentBulletAmount;
                    }

                    ChangeToNewShootingValues();
                    currentWeaponCurrentAmmoAmount = weapon0CurrentAmmoAmount;
                }
                else
                {

                    if (weapon0 != null)
                    {
                        selectedWeapon--;
                        currentWeapon = weapon0;
                        weapon1CurrentAmmoAmount = parentShootingScript.currentBulletAmount;
                    }

                    ChangeToNewShootingValues();
                    currentWeaponCurrentAmmoAmount = weapon1CurrentAmmoAmount;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (weapon0 != null)
                {
                    selectedWeapon = 0;
                    currentWeapon = weapon0;
                    weapon1CurrentAmmoAmount = parentShootingScript.currentBulletAmount;
                }

                ChangeToNewShootingValues();
                currentWeaponCurrentAmmoAmount = weapon1CurrentAmmoAmount;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (weapon1 != null)
                {
                    selectedWeapon = 1;
                    currentWeapon = weapon1;
                    weapon0CurrentAmmoAmount = parentShootingScript.currentBulletAmount;

                }

                ChangeToNewShootingValues();
                currentWeaponCurrentAmmoAmount = weapon0CurrentAmmoAmount;
            }

            if (previousSelectedWeapon != selectedWeapon)
            {
                SelectWeapon();
            }

        }

    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if( i == selectedWeapon )
            {
                weapon.gameObject.SetActive( true );
            }
            else
            {
                weapon.gameObject.SetActive ( false );
            }
            i++;
        }
    }

    void ChangeToNewShootingValues()
    {

            parentShootingScript.normalCameraSensitivity = currentWeapon.normalWeaponSensitivity;
            parentShootingScript.aimCameraSensitivity = currentWeapon.zoomedWeaponSensitivity;
            parentShootingScript.weaponDamage = currentWeapon.weaponDamage;
            parentShootingScript.shootingWaitTime = currentWeapon.weaponWaitTime;
            parentShootingScript.reloadTime = currentWeapon.weaponReloadTime;
            parentShootingScript.maxBullets = currentWeapon.weaponAmmoAmount;

            parentShootingScript.currentBulletAmount = currentWeaponCurrentAmmoAmount;
    }
}
