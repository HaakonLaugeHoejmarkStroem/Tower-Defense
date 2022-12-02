using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootdetection : MonoBehaviour
{
    public List<GameObject> enemiesInSight = new List<GameObject>();
    public List<GameObject> turretsInSight = new List<GameObject>();
    public GameObject bullet;
    Transform target;
    Transform turret;
    bool isShooting;

    public bool isEnemy;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnemy) 
        {
            if (enemiesInSight.Count == 0)
                target = null;

            if (enemiesInSight.Count > 0)
            {
                FindClosetTarget();
            }

            if (target != null && !isShooting)
            {
                print("Yuh");
                StartCoroutine(Shoot());
            }
        }
        else
        {
            if (turretsInSight.Count == 0)
                target = null;

            if (turretsInSight.Count > 0)
            {
                FindClosestTurret();
            }

            if (turret != null && !isShooting)
            {
                print("Yuh");
                StartCoroutine(ShootTurret());
            }
        }
       
        
    }

    IEnumerator Shoot()
    {
        print("Hey");
        isShooting = true;
        GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
        temp.GetComponent<Bullet>().SetTarget(target.gameObject, isEnemy);
        yield return new WaitForSeconds(1);
        isShooting = false;
        yield return null;
    }
    IEnumerator ShootTurret()
    {
        print("Hey");
        isShooting = true;
        GameObject temp = Instantiate(bullet, transform.position, Quaternion.identity);
        temp.GetComponent<Bullet>().SetTarget(turret.gameObject, isEnemy);
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
        if (!isEnemy)
        {
            if (collision.tag == "Enemy")
                enemiesInSight.Add(collision.gameObject);
        }
        else
        {
            if (collision.tag == "Turret")
                turretsInSight.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isEnemy)
        {
            if (collision.tag == "Enemy")
                enemiesInSight.Remove(collision.gameObject);
        }
        else
        {
            if (collision.tag == "Turret")
                turretsInSight.Remove(collision.gameObject);
        }
       
    }

    void FindClosestTurret()
    {
        int index = 0;
        float smallestNumer = 0;

        GameObject[] turrets = turretsInSight.ToArray();
        float[] TurretDistances = new float[turrets.Length];
        for (int i = 0; i < TurretDistances.Length; i++)
        {
            TurretDistances[i] = Vector2.Distance(transform.position, turrets[i].transform.position);
        }
        smallestNumer = TurretDistances[0];
        for (int i = 0; i < TurretDistances.Length; i++)
        {
            if (TurretDistances[i] < smallestNumer)
            {
                smallestNumer = TurretDistances[i];
                index = i;
            }
        }

        turret = turrets[index].transform;
    }
    
}
