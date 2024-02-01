using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Gate : MonoBehaviour
{
    [SerializeField] GameObject Player;
    public AnimationClip openGateAnim;
    Animation anim;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            anim.clip = openGateAnim;   
            anim.Play();
        }
    }
}
