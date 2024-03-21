using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TrackingSystem : MonoBehaviour
{
    [SerializeField] Transform lookAt;

    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed = 5;
    [SerializeField] float range = 10;
    [SerializeField] float fireRate = 1f;

    private float lastShootTime = 0;

    public Transform spawnPoint;

    [SerializeField] Animator animator;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        animator.SetBool("asleep", true);
    }

    void FixedUpdate()
    {
        Debug.DrawLine(lookAt.position, transform.position, Color.gray);

        Vector3 difference = lookAt.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(difference, Vector3.up);
        transform.rotation = rotation;

        Collider[] hits = Physics.OverlapSphere(transform.position, range);
        foreach (Collider hit in hits )
        {
            if (hit.tag == "Player")
            {
                    ShootInInterval();

                animator.SetBool("asleep", false);
                animator.SetBool("attacking", true);
            }
            else
            {
                //animator.SetBool("asleep", true);
                animator.SetBool("attacking", false);
            }
        }
    }

    private void ShootInInterval()
    {
        if (Time.time > lastShootTime + fireRate)
        {
            lastShootTime = Time.time;
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject cB = Instantiate(bullet, spawnPoint.position, bullet.transform.rotation);
        Rigidbody rb = cB.GetComponent<Rigidbody>();

        rb.AddForce((spawnPoint.right * -1) * bulletSpeed, ForceMode.Impulse);
    }
}