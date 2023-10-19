using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCheckpoint : MonoBehaviour
{
    [SerializeField] Vector3 spawnOffsetPosition = new(2, 2, 0);

    void Start()
    {
        switch (Checkpoint.HasCheckpoint)
        {
            case true:
                transform.position = Checkpoint.LastCheckpointPosition + spawnOffsetPosition;
                transform.rotation = Checkpoint.LastCheckpointRotation;
                break;

            default:
                Debug.Log("No checkpoint found.");
                break;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManagerExtended.ReloadScene();
        }
    }
}
