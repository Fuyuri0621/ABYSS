using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ground : MonoBehaviour
{
    float startpos;
    [SerializeField] float blocklength;
    GameObject cam;
    [SerializeField] List<GameObject> BlockList;
    GameObject startBlock;
    [SerializeField] GameObject endBlock;
    [SerializeField] int toSpawnEndblockReq;
    int spawntimes = 0;

    private void Awake()
    {
        cam = Camera.main.gameObject;
    }
    void Start()
    {
        startpos = transform.position.x;
       // blocklength = GetComponentInChildren<SpriteRenderer>().bounds.size.x + 5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float movement = cam.transform.position.x;
        if (movement > startpos + blocklength * 1.5f)
        {
            if (toSpawnEndblockReq >= spawntimes)
            { MoveToNewBlock(false); }
            else
            {
                MoveToNewBlock(true);
            }
        }


    }

    private void MoveToNewBlock(bool isfinal)
    {


        RollNewType(isfinal);
        if (isfinal) { Debug.Log("Finalblock is Spawn!"); }

        //blocklength = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        startpos += blocklength * 2 ;
        transform.position = new Vector3(startpos, transform.position.y, transform.position.z);
    }

    private void RollNewType(bool isfinal)
    {
        spawntimes++;
        if (isfinal)
        {
            Destroy(transform.GetChild(0).gameObject);
            GameObject newBlock = Instantiate(endBlock);
            newBlock.transform.position = transform.position;
            newBlock.transform.parent = transform;
        }
        else
        {
            int rnd = UnityEngine.Random.Range(0, BlockList.Count);
            Destroy(transform.GetChild(0).gameObject);
            GameObject newBlock = Instantiate(BlockList[rnd]);
            newBlock.transform.position = transform.position;
            newBlock.transform.parent = transform;
        }
    }

    // Draw blocklength using Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 check = new Vector3(startpos + blocklength * 1.5f, transform.position.y, transform.position.z);
        Vector3 start = new Vector3(startpos, transform.position.y, transform.position.z);
        Vector3 end = start + Vector3.right * blocklength;
        Gizmos.DrawLine(start, end);

        // Optional: draw a small cube at the end to make it more visible
        Gizmos.DrawCube(end, new Vector3(0.4f, 5f, 0.2f));

        Gizmos.color = Color.red;
        Gizmos.DrawCube(check, new Vector3(0.4f, 5f, 0.2f));
    }

}
