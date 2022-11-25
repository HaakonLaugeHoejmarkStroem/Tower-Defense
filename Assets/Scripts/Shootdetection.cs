using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootdetection : MonoBehaviour
{
    public List<GameObject> enemiesInSight = new List<GameObject>();
    public GameObject bullet;
    Transform target;
    bool isShooting;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesInSight.Count == 0)
            target = null;

        if(enemiesInSight.Count > 0)
        {
            FindClosetTarget();
        }
        
        if(target != null && !isShooting)
        {
             print("Yuh");
             StartCoroutine(Shoot());
        }
        
    }

    IEnumerator Shoot()
    {
        print("Hey");
        isShooting = true;
        GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
        temp.GetComponent<Bullet>().SetTarget(target.gameObject);
        yield return new WaitForSeconds(1);
        isShooting = false;
        yield return null;
    }
    void FindClosetTarget()
    {
        int index = 0;
        float smallestNumer = 0;

        GameObject[] targets = enemiesInSight.ToArray();
        float[] distances = new float[targets.Length];
        for (int i = 0; i < distances.Length; i++)
        {
            distances[i] = Vector2.Distance(transform.position, targets[i].transform.position);
        }
        smallestNumer = distances[0];
        for (int i = 0; i < distances.Length; i++)
        {
            if (distances[i] < smallestNumer)
            {
                smallestNumer = distances[i];
                index = i;
            }
        }

        target = targets[index].transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            enemiesInSight.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            enemiesInSight.Remove(collision.gameObject);
    }
}
