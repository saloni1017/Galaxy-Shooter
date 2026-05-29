using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefeb;
    
    [SerializeField]
    private GameObject[] _powerupPrefeb;

    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false;
    // Start is called before the first frame update
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutien());
        StartCoroutine(SpawnPowerup());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // spawn game object at every 5 sec
    // create coroutien
    // while loop
    IEnumerator SpawnEnemyRoutien()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-11f, 11f), 5.54f, 0);
            //crete enemy prefeb and store it into new gameobject
            GameObject newEnemy = Instantiate(_enemyPrefeb,posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerup()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-11f, 11f), 5.54f, 0);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerupPrefeb[randomPowerup], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3f,8f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
   
}
