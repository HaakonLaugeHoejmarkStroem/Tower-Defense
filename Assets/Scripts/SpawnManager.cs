using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] SpawnableEnemies;
    [SerializeField] Test[] Routes;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());

    }

    IEnumerator Spawn() 
    {
        while (true)
        {
            int type = Random.Range(0, SpawnableEnemies.Length);
            int route = Random.Range(0, Routes.Length);

            GameObject enemy = Instantiate(SpawnableEnemies[type]);
            enemy.GetComponent<Enemy>().SetRoute(Routes[route]);
            yield return new WaitForSeconds(1.5f);
            yield return null;
        }

        
    }
}
