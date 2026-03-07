using UnityEngine;

public class PistolAmmo : MonoBehaviour
{
    [SerializeField] private AudioSource ammoCollect;
 
    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        ammoCollect.Play();
        GlobalAmmo.handgunAmmoCount += 10;
        Destroy(gameObject);
    }
}
