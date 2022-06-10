using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] objectPrefabs;
    [SerializeField] GameObject[] objects;
    [SerializeField] Transform[] positionOfObject;
    [SerializeField] Transform positionOfCenter;
    [SerializeField] TMP_Text objectName;

    private GameObject objectToSpawn;
    private GameObject[] objectsSpawned;

    public void SpawnObjects()
    {
        int i = 0;
        objectToSpawn = objectPrefabs[Random.Range (0, objectPrefabs.Length)];
        objectName.text = objectToSpawn.name;
        objectsSpawned = new GameObject[objectToSpawn.transform.childCount];
        GetSameObject(objectToSpawn).SetActive(false);
        foreach (Transform transformOfChild in objectToSpawn.transform)
        {
            if (transformOfChild.name == "Center")
            {
                objectsSpawned[i] = Instantiate(transformOfChild.gameObject, positionOfCenter);
                objectsSpawned[i].transform.localScale = transformOfChild.localScale;
                continue;
            }
            objectsSpawned[i] = Instantiate(transformOfChild.gameObject, positionOfObject[i]);
            objectsSpawned[i].transform.localScale = transformOfChild.localScale;
            i++;
        }
    }

    private GameObject GetSameObject(GameObject gameObjectToCompare)
    {
        foreach (GameObject gameObject in objects)
        {
            if (gameObjectToCompare.name == gameObject.name)
            {
                return gameObject;
            }
        }
        return null;
    }

    public void DespanwObjects()
    {
        foreach (GameObject gameObject in objectsSpawned)
        {
            Destroy(gameObject);
        }
        GetSameObject(objectToSpawn).SetActive(true);
    }

    public void RespawnObject(GameObject objectToRespawn)
    {
        int i = 0;
        foreach (GameObject gameObject in objectsSpawned)
        {
            Debug.Log(gameObject.name + " , " + objectToRespawn.name);
            if (gameObject.name == objectToRespawn.name)
            {
                Destroy(objectToRespawn);
                foreach (Transform transformOfChild in objectToSpawn.transform)
                {
                    if (transformOfChild.name + "(Clone)" == gameObject.name)
                    {
                        objectsSpawned[i] = Instantiate(transformOfChild.gameObject, positionOfObject[i]);
                        objectsSpawned[i].transform.localScale = transformOfChild.localScale;
                        break;
                    }
                }
                break;
            }
            i++;
        }
    }

    public GameObject GetSpawnedObject()
    {
        return objectToSpawn;
    }
}
