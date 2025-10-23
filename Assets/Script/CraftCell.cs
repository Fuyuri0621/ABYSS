using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftCell : MonoBehaviour
{
    private Transform UIIcon;
    private Transform UIName;
    private Transform itemACount;
    private Transform itemBCount;
    private Transform itemCCount;

    Button craftButton;
    Image itemicon;
    private CraftTableItem Data;

    private BackpackContainer backpackContainer;


    private CraftPanel uiParent;
    void Awake()
    {
        InitUIName();
    }

    private void InitUIName()
    {
        UIIcon = transform.Find("item/Icon");
        UIName = transform.Find("info/name");
        itemACount = transform.Find("info/needitemA/require");
        itemBCount = transform.Find("info/needitemB/require");
        itemCCount = transform.Find("info/needitemC/require");

        craftButton = transform.Find("info/CraftButton").GetComponent<Button>();
        itemicon = UIIcon.GetComponent<Image>();
    }


    public void Refresh(CraftTableItem Datas, CraftPanel uiParent)
    {
        if (UIName == null) { InitUIName(); }


        this.uiParent = uiParent;
        this.Data = Datas;

        UIName.GetComponent<TextMeshProUGUI>().text = Data.name;
        itemACount.GetComponent<TextMeshProUGUI>().text = Data.needItems[0].require.ToString();
        itemBCount.GetComponent<TextMeshProUGUI>().text = Data.needItems[1].require.ToString();
        itemCCount.GetComponent<TextMeshProUGUI>().text = Data.needItems[2].require.ToString();

        craftButton.interactable = true;
        itemicon.color = Color.white;

        if (BackPackManager.Instance.GetBackpackItemCountByName(Data.needItems[0].itemName) < Data.needItems[0].require)
            {
                itemACount.GetComponent<TextMeshProUGUI>().color = Color.red; craftButton.interactable = false; itemicon.color = Color.gray;
        }
            else { itemACount.GetComponent<TextMeshProUGUI>().color = Color.white; }

        if (BackPackManager.Instance.GetBackpackItemCountByName(Data.needItems[1].itemName) < Data.needItems[1].require)
        {
            itemBCount.GetComponent<TextMeshProUGUI>().color = Color.red; craftButton.interactable = false; itemicon.color = Color.gray;
        }
        else { itemBCount.GetComponent<TextMeshProUGUI>().color = Color.white; }

        if (BackPackManager.Instance.GetBackpackItemCountByName(Data.needItems[2].itemName) < Data.needItems[2].require)
        {
            itemCCount.GetComponent<TextMeshProUGUI>().color = Color.red; craftButton.interactable = false; itemicon.color = Color.gray;
        }
        else { itemCCount.GetComponent<TextMeshProUGUI>().color = Color.white; }

        if (GameManager.Instance.datacontainer.craftedWeapon.Find(x => x == Data.name) != null)
        {
            craftButton.interactable=false;
        }
       


    }



    public void CraftItem()
    {
        BackPackManager.Instance.AddItem(Data.needItems[0].itemName, -Data.needItems[0].require);
        BackPackManager.Instance.AddItem(Data.needItems[1].itemName, -Data.needItems[1].require);
        BackPackManager.Instance.AddItem(Data.needItems[2].itemName, -Data.needItems[2].require);

        uiParent.RefreshUI();
        GameManager.Instance.datacontainer.craftedWeapon.Add(Data.name);
        Debug.Log("жXжи" + Data.name);

    }
}
