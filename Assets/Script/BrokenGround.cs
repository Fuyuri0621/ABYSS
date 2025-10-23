using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenGround : MonoBehaviour
{
    GameObject disppearTraget;
    [SerializeField] float disappearTime = 2;
   [SerializeField] bool disppaering;
    float timer;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        disppearTraget = transform.Find("traget").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (disppaering)
        {
            
            timer += Time.deltaTime;
            if (timer > disappearTime) { disppear(); }
        }
    }

    void disppear()
    {
        animator.Play("Break");
        disppearTraget.SetActive(false);
        disppaering = false;
        timer = 0;
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&disppearTraget.activeInHierarchy)
        {
            disppaering = true;
        }
    }

    public void Reset()
    {
     
     disppearTraget.SetActive(true);   
    }
}
