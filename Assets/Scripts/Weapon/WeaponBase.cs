using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponBase : ScriptableObject//MonoBehaviour
{
    [Header("Sensitivity")]
    public float normalWeaponSensitivity = 350f;
    public float zoomedWeaponSensitivity = 50f;

    [Header("Weapon Shooting")]
    public float weaponDamage = 1f;

    public float weaponWaitTime = 0.2f;
    public float weaponReloadTime = 1.4f;

    public int weaponAmmoAmount = 25;

    [SerializeField] ParticleSystem weaponShootingVFX;
    [SerializeField] ParticleSystem onHitParticles;
    [SerializeField] ParticleSystem onMissParticles;
}
