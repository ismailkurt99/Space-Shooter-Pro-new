using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
  [SerializeField]
  private GameObject _enemyPrefab;
  [SerializeField]
  private GameObject[] powerups;
  [SerializeField]
  private GameObject _enemyContainer;
  [SerializeField]
  private GameObject _powerupContainer;
  private bool _stopSpawning = false;
  void Start()
  {
    
  }

  public void StartSpawning()
  {
    StartCoroutine(SpawnEnemyRoutine());
    StartCoroutine(SpawnPowerupRoutine());
  }

  IEnumerator SpawnEnemyRoutine()
  {
    yield return new WaitForSeconds(3.0f);
    while(_stopSpawning == false)
    {
      Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 8, 0);
      GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
      newEnemy.transform.parent = _enemyContainer.transform;
      yield return new WaitForSeconds(5.0f);
    }
  }

  IEnumerator SpawnPowerupRoutine()
  {
    yield return new WaitForSeconds(3.0f);
    while(_stopSpawning == false)
    {
    Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
    int randomPowerup = Random.Range(0, 3);
    GameObject newPowerup = Instantiate(powerups[randomPowerup], posToSpawn, Quaternion.identity);
    newPowerup.transform.parent = _powerupContainer.transform;
    yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
    }
  }

  public void onPlayerDeath()
  {
    _stopSpawning = true;
  }
}
