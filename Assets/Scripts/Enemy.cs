using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Test test;
    public float speed;
    public 
    void Awake()
    {
        currentHealth = maxHealth; 
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
            Destroy(this.gameObject);

    }


    public void SetRoute(Test newTest)
    {
        test = newTest;
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        for (int i = 0; i < test.positions.Length; i++)
        {
            if (i == 0)
            {
                transform.position = test.positions[i].position + new Vector3(0.5f, 0.5f);
                yield return null;
            }
            else
            {
                Vector2 origin = transform.position;
                Vector2 targetPos = test.positions[i].position + new Vector3(0.5f, 0.5f);
                float timeToGo = Vector2.Distance(transform.position, test.positions[i].position) / speed;
                float timeElapsed = 0;

                while (timeElapsed < timeToGo)
                {
                    transform.position = Vector2.Lerp(origin, targetPos, timeElapsed / timeToGo);
                    timeElapsed += Time.deltaTime;
                    yield return null;
                }
                transform.position = targetPos;
            }

            if (i + 1 == test.positions.Length)
            {
                GameManager.gm.health--;
                Destroy(this.gameObject);
            }

                



        }


    }
}
