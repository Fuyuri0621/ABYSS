using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    float startpos;
    [SerializeField] float length = 70;
    [SerializeField] float offset;
    GameObject cam;
    private Vector3 startCameraPos;
    [Header("視差程度 -1最近 1最遠"), Range(-1,1),SerializeField] float parallaxEffect;

    private void Awake()
    {
        cam = Camera.main.gameObject;
        startCameraPos = cam.transform.position;
    }
    void Start()
    {
        startpos = transform.position.x;
        if (GetComponent<SpriteRenderer>() != null)
        { length = GetComponent<SpriteRenderer>().bounds.size.x; }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance =(cam.transform.position.x) * parallaxEffect;
        float movement = (cam.transform.position.x ) * (1-parallaxEffect);

        transform.position = new Vector3(startpos+ distance+offset,transform.position.y,transform.position.z);


        if (movement > startpos + length)
        {
            startpos += length;
        }
        else if (movement < startpos - length)
        {
            startpos -= length;
        }

    }

}
