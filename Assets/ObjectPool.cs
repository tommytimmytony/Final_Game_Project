using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {
    [SerializeField] private GameObject vulturePrefab;
    [SerializeField] private int poolSize = 10; // Default size of the pool
    private List<GameObject> pool;              // List of pooled objects

    void Start() {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++) {
            GameObject vulture = Instantiate(vulturePrefab);
            vulture.SetActive(false);
            pool.Add(vulture);
        }
    }

    public GameObject GetPooledObject() {
        foreach (GameObject obj in pool) {
            if (!obj.activeInHierarchy) {
                obj.SetActive(true);
                return obj;
            }
        }


        GameObject newObject = Instantiate(vulturePrefab);
        newObject.SetActive(true);
        pool.Add(newObject);
        return newObject;
    }

    public void ReturnObjectToPool(GameObject obj) {
        obj.SetActive(false);
    }
}
