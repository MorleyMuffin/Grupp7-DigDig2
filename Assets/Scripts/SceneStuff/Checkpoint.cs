using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Checkpoint system when player touches this object,
/// it will save the player's position and rotation and when the player dies,
/// it will respawn at the last checkpoint touched.
/// </summary>
public class Checkpoint : MonoBehaviour
{
    public static Vector3 LastCheckpointPosition { get; private set; }
    public static Quaternion LastCheckpointRotation { get; private set; }
    public static bool HasCheckpoint { get; private set; }

    private void Awake()
    {
        Debug.Log(HasCheckpoint);
        Debug.Log(LastCheckpointPosition);
    }

    private void Update()
    {
        Debug.Log(LastCheckpointPosition);
    }


    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        LastCheckpointPosition = gameObject.transform.position;
        LastCheckpointRotation = other.transform.rotation;
        HasCheckpoint = true;
        Debug.Log("Checkpoint saved!");

        //TODO: Play checkpoint sound and particle effect!
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}