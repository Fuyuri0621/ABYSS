using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftPanel : MonoBehaviour
{
    // Start is called before the first frame update
    Transform Craftitems;
    Transform Backpackitems;
    Transform ConfirmPanel;
    [SerializeField] GameObject BackpackUIItemPrefab;
    int craftid;
    private void Awake()
    {
        Craftitems = transform.Find("Center/Craftitems");
        Backpackitems = transform.Find("Center/Backpackitems");
        ConfirmPanel = transform.Find("ConfirmPanel");
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
    public void RefreshUI()
    {
        RefreshScroll();
        RefreshBackpack();
    }

    private void RefreshScroll()
    {
        RectTransform scrollContent = Craftitems.GetComponent<RectTransform>();
        int i = 0;
        foreach (CraftTableItem localItem in BackPackManager.Instance.GetCraftTable().DataList)
        {

            CraftCell craftCell = scrollContent.GetChild(i).GetComponent<CraftCell>();
            craftCell.Refresh(localItem, this); 
            i++;
           
        }
    }
    private void RefreshBackpack()
    {
        int i = 0;
        foreach (BackpackItem localItem in BackPackManager.Instance.GetBackPackLocalData())
        {
        
            RectTransform scrollContent = Backpackitems.GetComponent<RectTransform>();

            Debug.Log(i);
            if (i == scrollContent.childCount) { break; }

            BackpackCell craftCell = scrollContent.GetChild(i).GetComponent<BackpackCell>();
            craftCell.Refresh(localItem);
            i++;

        }

    }

    public void CraftReq(int id)
    {
        craftid=id;
        ConfirmPanel.gameObject.SetActive(true);
        if (id == 0)
        {
            ConfirmPanel.Find("Text").GetComponent<TextMeshProUGUI>().text = "是否要合成 <color=#FFFD69>武器A";
        }
        if (id == 1)
        {
            ConfirmPanel.Find("Text").GetComponent<TextMeshProUGUI>().text = "是否要合成 <color=#FFFD69>武器B";
        }
        else
        {
            ConfirmPanel.Find("Text").GetComponent<TextMeshProUGUI>().text = "是否要合成 <color=#FFFD69>武器C";
        }
    }

    public void Craft()
    {
        ConfirmPanel.gameObject.SetActive(false);
        Craftitems.GetComponent<RectTransform>().GetChild(craftid).GetComponent<CraftCell>().CraftItem();
    }
}
