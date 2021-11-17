using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManger : MonoBehaviour
{
    public Animator anim;
    public PlayerMovement player;
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (x == 0f && y == 0f)
        {
            anim.SetBool("Breath", true);
            anim.SetBool("Run", false);
            anim.SetBool("Walk", false);
        }
        else
        {
            if (player.isWalking == false)
            {
                anim.SetBool("Breath", false);
                anim.SetBool("Run", true);
                anim.SetBool("Walk", false);
            }
            if (player.isWalking == true)
            {
                anim.SetBool("Breath", false);
                anim.SetBool("Run", false);
                anim.SetBool("Walk", true);
            }
        }

    }
}
