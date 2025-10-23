using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackpackCell : MonoBehaviour
{
    private Transform UIIcon;
    private Transform UIName;
    private Transform UICount;

    private BackpackItem Data;

    private BackpackContainer backpackContainer;
    private BackpackTableItem backpackTableitem;


    void Awake()
    {
        InitUIName();
    }

    private void InitUIName()
    {
        UIIcon = transform.Find("Top/Icon");
        UIName = transform.Find("Bottom/ItemName");
        UICount = transform.Find("Top/Count");

    }


    public void Refresh(BackpackItem Data)
    {
        if (UIIcon == null)
        {
            InitUIName();
        }
        this.Data = Data;
        this.backpackTableitem = BackPackManager.Instance.GetBackpackTableItemByName(Data.itemName);

        Texture2D t = (Texture2D)Resources.Load(this.backpackTableitem.iconPath);
        Sprite temp = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0, 0));

        UIIcon.GetComponent<Image>().sprite = temp;
        UIName.GetComponent<TextMeshProUGUI>().text = Data.itemName;
        UICount.GetComponent<TextMeshProUGUI>().text = Data.itemcount.ToString();
    }
}
