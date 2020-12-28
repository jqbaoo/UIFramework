using UnityEngine;
using System.Collections;
using GameCore;
using System.Collections.Generic;

public class PackData : Model//Singleton<PackData>
{
    public override string Name
    {
        get { return "PackData"; }
    }
    //存放物品数量的字典<物品ID,数量>
    private Dictionary<string, int> packDataDic = new Dictionary<string, int>();
    //供外界调用,初始化背包数据的方法
    public void InitPackData()
    {
        if (!GameTool.HasKey("PackData"))
        {
            GameTool.SetString("PackData", "1'0;2'20;3'30;4'40;5'50;6'60;7'70;8'80;9'20;10'20;11'20;12'20;13'20;14'20;15'20;16'20;17'20;18'20;19'20;20'20;21'20;22'20");
        }
        string data = GameTool.GetString("PackData");
        string[] arrData = GameTool.SplitString(data,';');
        ToDictionary(arrData);
    }
    private void ToDictionary(string[] arrData)
    {
        packDataDic.Clear();
        for (int i = 0; i < arrData.Length; i++)
        {
            string[] theArr = GameTool.SplitString(arrData[i],'\'');            
            packDataDic.Add(theArr[0],int.Parse(theArr[1]));
        }
    }
    //供外界调用,根据物品ID获取物品数量的方法
    public int ReadCountById(string goodsId)
    {
        if (packDataDic.ContainsKey(goodsId))
        {
            return packDataDic[goodsId];
        }
        else
        {
            return 0;
        }
    }
    //供外界调用,更改物品数量的方法(物品ID,最新的物品数量)
    public void EditorGoodsCount(string goodsId,int newCount)
    {
        packDataDic[goodsId] = newCount;
        SaveData();
        //发送消息给视图层
        SendEvent(GameDefine.message_UpdatePack);
    }
    private void SaveData()
    {
        string strData = null;
        int index = 0;
        foreach (KeyValuePair<string,int> item in packDataDic)
        {
            index++;
            strData += item.Key + "'" + item.Value;
            if (index != packDataDic.Count)
            {
                strData += ";";
            }
        }
        GameTool.SetString("PackData", strData);
    }
}
