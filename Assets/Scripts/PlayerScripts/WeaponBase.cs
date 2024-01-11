using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponBase : ScriptableObject//MonoBehaviour
{
    [Header("Sensitivity")]
    [SerializeField] float normalWeaponSensitivity = 350f;
    [SerializeField] float zoomedWeaponSensitivity = 50f;

    [Header("Weapon Shooting")]
    [SerializeField] float weaponDamage = 1f;

    [SerializeField] float weaponWaitTime = 0.2f;
    [SerializeField] float weaponReloadTime = 1.4f;

    [SerializeField] float weaponAmmoAmount = 25f;

    [SerializeField] ParticleSystem weaponShootingVFX;
    [SerializeField] ParticleSystem onHitParticles;
    [SerializeField] ParticleSystem onMissParticles;
}
