// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEngine;

// public class ObjectPool : MonoBehaviour
// {   
//     public static ObjectPool instance;

//     private List<GameObject> pooledObjects = new List<GameObject>();
//     private int amountToPool = 20;

//     [SerializeField] private GameObject bulletPrefab;

//     [SerializeField] private GameObject enemy1Prefab;
//     [SerializeField] private GameObject enemy2Prefab;
//     [SerializeField] private GameObject enemy3Prefab;
//     [SerializeField] private GameObject enemy4Prefab;

//     public Transform firepoint;

//     private void Awake()
//     {
//         if (instance == null)
//         {
//             instance = this;
//         }
//     }

//     // Start is called before the first frame update
//     void Start()
//     {
//         for (int i = 0; i < amountToPool; i++)
//         {
//             GameObject obj = Instantiate(bulletPrefab);
//             obj.SetActive(false);
//             pooledObjects.Add(obj);
//         }
//     }

//     public GameObject GetPooledObject()
//     {
//         for (int i = 0; i < pooledObjects.Count; i++)
//         {
//             if (!pooledObjects[i].activeInHierarchy)
//             {
//                 // Set the position of the bullet to match the firepoint
//                 pooledObjects[i].transform.position = firepoint.position;
//                 return pooledObjects[i];
//             }
//         }

//         // If none are available, create a new object and add it to the pool
//         GameObject newObj = Instantiate(bulletPrefab, firepoint.position, Quaternion.identity);
//         newObj.SetActive(false);
//         pooledObjects.Add(newObj);

//         return newObj;
//     }
// }
