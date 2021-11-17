using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RaCastShot : MonoBehaviour
{
    public Camera Playercamera;
    public float FireRate = 10f;
    private float timeBetweenNextShot;
    public float Damage = 20f;
    public float range = 100f;
    public GameObject hitEffect;

    public mouseLook mouse;
    // public Animator moveEffect;
    [Header("Recoil")]
    public float vRecoil = 1f;
    public float hRecoil = 1f;
    public Animator AimAnim;
    public bool aim;

    //Ammo Part
    [Header("Ammo Mangement")]
    public int ammocount = 25;
    public int availableammo = 100;
    public int maxAmmo = 25;
    public Animator anim;
    public Text currentammotext;

    void Update()
    {
        // if (Input.GetButton("Fire2"))
        // {
        //     AimAnim.SetBool("Aim", true);
        // }
        // else
        // {
        //     AimAnim.SetBool("Aim", false);
        // }
        if (Input.GetButtonDown("Fire2"))
        {
            aim = !aim;
        }
        if (aim == true)
        {
            AimAnim.SetBool("Aim", true);
        }
        else
        {
            AimAnim.SetBool("Aim", false);
        }

        currentammotext.text = ammocount.ToString();
        if (Input.GetKeyDown(KeyCode.R) && ammocount < maxAmmo)
        {
            mouse.AddRecoil(0, 0);
            anim.SetBool("Reload", true);
            anim.SetBool("Shoot", false);

        }
        if (ammocount <= 0)
        {
            mouse.AddRecoil(0, 0);
            anim.SetBool("Reload", true);
            anim.SetBool("Shoot", false);

            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= timeBetweenNextShot)
        {
            timeBetweenNextShot = Time.time + 1f / FireRate;
            float h = Random.Range(-hRecoil, hRecoil);
            float v = Random.Range(0, vRecoil);
            anim.SetBool("Shoot", true);

            mouse.AddRecoil(h, v);
            weapon();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            mouse.AddRecoil(0, 0);
            anim.SetBool("Shoot", false);

        }
    }
    void weapon()
    {
        ammocount--;
        RaycastHit hit;
        if (Physics.Raycast(Playercamera.transform.position, Playercamera.transform.forward, out hit, range))
        {
            Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal)); //use to spaw any component(here we are adding hiteffect on ground when bullet hit)
            if (hit.transform.tag == "Enemy")
            {
                Health enemy = hit.transform.GetComponent<Health>();
                enemy.Damage(Damage);
            }

        }
    }
    public void Reload()
    {
        mouse.AddRecoil(0, 0);
        availableammo -= (maxAmmo - ammocount);
        ammocount = maxAmmo;
        anim.SetBool("Shoot", false);

        anim.SetBool("Reload", false);
    }
}
