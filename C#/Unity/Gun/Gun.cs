using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
   public float damage = 10f;
   public float range = 100f;
   public float fireRate = 15f;
   public float impactForce = 30f;

   public int maxAmmo = 10;
   private int currentAmmo;
   public float reloadTime = 1f;
   private bool isReloading = false;

   public Camera fpsCam;
   public ParticleSystem muzzelFlash;
   public GameObject impactEffect;

   private float nextTimeToFire = 0f;

   public Animator animator;
   private AudioSource shot;
   
   public delegate void GunAmmoEventHandler(int currentAmmo, int maxAmmo);
   public event GunAmmoEventHandler EventAmmoChanged;

   void Start ()
   {
       shot = GetComponent<AudioSource>();
       if (currentAmmo == -1)
          currentAmmo = maxAmmo;
   }

   void OnEnable ()
   {
       
       isReloading = false;
       animator.SetBool("Reloading", false);
   }

    void Update()
    {
        if (isReloading)
           return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;

        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    IEnumerator Reload ()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot ()
    {
        muzzelFlash.Play();
        shot.Play();

        currentAmmo--;
        CallEventAmmoChanged(currentAmmo,maxAmmo);
       RaycastHit hit;
       if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
       {
           Debug.Log(hit.transform.name);
           
           //change script to call TakeDamage from here
           EnemyDie target = hit.transform.GetComponent<EnemyDie>();
           if (target != null)
           {
               target.TakeDamage(damage);
           }

           if (hit.rigidbody != null)
           {
               hit.rigidbody.AddForce(-hit.normal * impactForce);
           }

           GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
           Destroy(impactGO, 2f);
       }
    }


    public void CallEventAmmoChanged(int currentAmmo, int maxAmmo)
    {
        if (EventAmmoChanged != null)
        {
            EventAmmoChanged(currentAmmo, maxAmmo);
        }
    }
}
