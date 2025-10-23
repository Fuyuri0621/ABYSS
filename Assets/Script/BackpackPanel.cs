using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackPanel : MonoBehaviour
{
    // Start is called before the first frame update
    Transform UIScrolView;
    Transform Backpackitems;
    [SerializeField] GameObject BackpackUIItemPrefab;
    private void Awake()
    {
       // UIScrolView = transform.Find("Center/Scroll View");
        Backpackitems = transform.Find("Center/Backpackitems");
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        RefreshUI();
    }
    private void RefreshUI()
    {
        // RefreshScroll();
        RefreshBackpack();
    }

    private void RefreshScroll()
    {
        RectTransform scrollContent = UIScrolView.GetComponent<ScrollRect>().content;
        for (int i = 0; i < scrollContent.childCount; i++)
        {
            Destroy(scrollContent.GetChild(i).gameObject);
        }
        foreach (BackpackItem localItem in BackPackManager.Instance.GetBackPackLocalData())
        {
            Transform BackpackUIItem = Instantiate(BackpackUIItemPrefab.transform, scrollContent) as Transform;
            BackpackCell backpackCell = BackpackUIItem.GetComponent<BackpackCell>();
            backpackCell.Refresh(localItem);
        }
    }

    private void RefreshBackpack()
    {
        int i = 0;
        foreach (BackpackItem localItem in BackPackManager.Instance.GetBackPackLocalData())
        {
            RectTransform scrollContent = Backpackitems.GetComponent<RectTransform>();

            BackpackCell craftCell = scrollContent.GetChild(i).GetComponent<BackpackCell>();
            craftCell.Refresh(localItem);
            i++;

        }

    }
}
