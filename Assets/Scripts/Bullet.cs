using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    bool isEnemy;
    public void SetTarget(GameObject target, bool isMaybeEnemy)
    {
        StartCoroutine(HitTarget(target.transform));
        isEnemy = isMaybeEnemy;
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
        if (!isEnemy)
        {
            target.GetComponent<Enemy>().TakeDamage(20);
            Destroy(this.gameObject);
        }
        else
        {
            target.GetComponent<Turret>().TakeDamage(20);
            Destroy(this.gameObject);
        }
        
    }
}
