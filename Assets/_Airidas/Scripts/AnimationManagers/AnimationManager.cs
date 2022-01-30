using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void StartAnimation(string animName)
    {
        anim.SetBool(animName, true);
        AudioManager.instance.PlaySound("Machine", this.gameObject);
    }
}
