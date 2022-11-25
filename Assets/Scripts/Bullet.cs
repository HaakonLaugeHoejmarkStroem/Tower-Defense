using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public void SetTarget(GameObject target)
    {
        StartCoroutine(HitTarget(target.transform));
    }

    IEnumerator HitTarget(Transform target)
    {
        Vector2 originPos = transform.position;
        Vector2 targetPos = target.position;

        float elapsedTime = 0;
        float TimeToHit = Vector2.Distance(originPos,targetPos)/ bulletSpeed;

        while (elapsedTime < TimeToHit)
        {
            if (target == null)
                Destroy(this.gameObject);
            targetPos = target.position;
            TimeToHit = Vector2.Distance(originPos,targetPos)/ bulletSpeed;
            transform.position = Vector2.Lerp(originPos, targetPos, elapsedTime / TimeToHit);
            elapsedTime += Time.deltaTime;
            yield return null;

        }
        transform.position = target.position;
        target.GetComponent<Enemy>().TakeDamage(20);
        Destroy(this.gameObject);
    }
}
