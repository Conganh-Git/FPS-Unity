using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private InputReader input;

    [Header("Weapon")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawn;

    [SerializeField] private float bulletVelocity = 30f;
    [SerializeField] private float bulletLifeTime = 3f;
    [SerializeField] private float maxShootDistance = 100f;

    [Header("FX")]
    [SerializeField] private GameObject extraCross;
    [SerializeField] private AudioSource gunFire;
    [SerializeField] private AudioSource emptyGunSound;
    [SerializeField] private GameObject handGun;

    private bool canFire = true;

    private void OnEnable()
    {
        input.FireEvent += OnFire;
    }

    private void OnDisable()
    {
        input.FireEvent -= OnFire;
    }

    private void OnFire()
    {
        if (!canFire) return;

        if (GlobalAmmo.handgunAmmoCount == 0)
        {
            StartCoroutine(EmptyGun());
        }
        else
        {
            FireWeapon();
            StartCoroutine(FiringGun());
        }
    }

    void FireWeapon()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, maxShootDistance))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.origin + ray.direction * maxShootDistance;
        }

        Vector3 shootDirection = (targetPoint - bulletSpawn.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.LookRotation(shootDirection));

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
            rb.linearVelocity = shootDirection * bulletVelocity;

        Destroy(bullet, bulletLifeTime);
    }

    IEnumerator FiringGun()
    {
        canFire = false;

        gunFire.Play();
        extraCross.SetActive(true);

        GlobalAmmo.handgunAmmoCount--;

        handGun.GetComponent<Animator>().Play("HandgunFire");

        yield return new WaitForSeconds(0.5f);

        handGun.GetComponent<Animator>().Play("gunfire");

        extraCross.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        canFire = true;
    }

    IEnumerator EmptyGun()
    {
        canFire = false;

        emptyGunSound.Play();

        yield return new WaitForSeconds(0.6f);

        canFire = true;
    }
}
