using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireScript : MonoBehaviour
{
    //Gerekli
    [SerializeField] private GameStats _gameStats;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private ParticleSystem bloodEfect;
    [SerializeField] private Animator myAnim;
    [SerializeField] private Camera fpsCam;
    [SerializeField] private ParticleSystem muzzleFlash;


    //Silah Ayarları
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private float fireRate = 0.25f;
    private float weaponRange = 500f;
    private int maxammo = 150;
    private int currentammo = 30;

    private float reloadCooldown = 0f;
    private float nextFire = 0f;

    //Mermi Gösterge
    public Text ammoText;

    


   
    void Update()
    {
        IsRunning(_playerStats.Speed);

        if (_gameStats.isGameOver == false)
        {
            ReloadAmmo();
            ammoText.text = currentammo.ToString() + "/" + maxammo.ToString(); ;
            reloadCooldown -= Time.deltaTime;
            if (Input.GetButtonUp("Fire1"))
            {
                myAnim.SetBool("isFire", false);
            }

            if (Input.GetButton("Fire1") && Time.time >= nextFire)
            {
                if (maxammo == 0 & currentammo == 0)
                {
                    myAnim.SetBool("isFire", false);
                }
                   
                nextFire = Time.time + fireRate;
                Shoot();
            }

            if (reloadCooldown > 0f)
                myAnim.SetBool("isFire", false);
        }
    }

   private void Shoot()
    {
        if (reloadCooldown <= 0f && currentammo > 0 )
        {
            myAnim.SetBool("isFire", true);
            currentammo -= 1;
            muzzleFlash.Play();
            RaycastHit hit;
            if (Physics.Raycast(transform.position, fpsCam.transform.forward, out hit, weaponRange))
            {
                Debug.DrawLine(transform.position, fpsCam.transform.forward, Color.red);
                DummyController dummy = hit.transform.GetComponent<DummyController>();
                if (dummy != null)
                {
                    dummy.hitDummy();
                }
                EnemyController enemy = hit.transform.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    
                    if (hit.collider.CompareTag("zombiehead"))
                    {
                        enemy.getDamage(50);
                        Debug.Log("HİT");
                    }
                    enemy.getDamage(20);
                    Instantiate(bloodEfect, hit.point,Quaternion.FromToRotation(Vector3.up, hit.transform.position));
                }
            }
            
        }
       
    }

    private void ReloadAmmo()
    {
        if (Input.GetKeyDown(KeyCode.R) && maxammo > 0 && currentammo < 30)
        {
            if (currentammo > 0)
            {
                int tempAmmo = 30 - currentammo;
                currentammo += tempAmmo;
                maxammo -= tempAmmo;
                reloadCooldown = 2f;
                myAnim.SetTrigger("reload");
            }
        }
        if (currentammo == 0 && maxammo > 0)
        {
            if (maxammo > 30)
            {
                currentammo = 30;
                maxammo -= 30;
                reloadCooldown = 2f;
                myAnim.SetTrigger("reload");
            }
            else
            {
                currentammo += maxammo;
                maxammo = 0;
                reloadCooldown = 2f;
                myAnim.SetTrigger("reload");
            }
            
        }
    }

    private void IsRunning(float speed)
    {
        if (speed > 6f)
        {
            myAnim.SetBool("isRunning", true);
        }
        else
            myAnim.SetBool("isRunning", false);
    }
}
