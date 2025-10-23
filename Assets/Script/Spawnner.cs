using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    [SerializeField] float spawntime = 1f;
    [SerializeField] Vector3 offset;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ActivateTrap", spawntime, spawntime);
    }

    void ActivateTrap()
    {
        Instantiate(spawnObject,transform.position+offset,Quaternion.identity);
    }

}
