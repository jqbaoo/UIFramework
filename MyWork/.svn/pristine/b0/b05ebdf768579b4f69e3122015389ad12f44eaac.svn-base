using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameCore;
public class GoodsEntity : MonoBehaviour
{
    //物品ID
    public int goodsId;
    //物品类型
    public E_GoodsType type;
    //用于显示物品的Image
    Image goodsImage;
    //用于显示物品数量的Text
    Text countText;
    void Start()
    {
        GameTool.GetTheChildComponent<Button>(this.gameObject, "Img_Goods").onClick.AddListener(UpdateInfor);
        goodsImage = GameTool.GetTheChildComponent<Image>(this.gameObject, "Img_Goods");
        countText= GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_Count");
    }
    private void UpdateInfor()
    {       
        EventDispatcher.TriggerEvent<int,Image,Text>(E_MessageType.GoodsBeClick, goodsId, goodsImage, countText);
    }
}
