using ExpObj;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision objectWeHit)
    {
        ExplosiveObject explosive = objectWeHit.gameObject.GetComponent<ExplosiveObject>();

        if (explosive != null)
        {
            explosive.Explode();
        }

        if (objectWeHit.gameObject.CompareTag("Target") || objectWeHit.gameObject.CompareTag("Wall"))
        {
            BulletEffect(objectWeHit);
        }

        Destroy(gameObject);
    }

    void BulletEffect(Collision objectWeHit)
    {
        if (objectWeHit.contacts.Length == 0) return;

        ContactPoint contact = objectWeHit.contacts[0];

        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
        );

        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }
}
