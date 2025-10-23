using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    [SerializeField] int stage;
    private void Start()
    {
        if (!GameManager.Instance.datacontainer.levelclear[stage-1])
        {
            gameObject.SetActive(false);
        }
    }
}
