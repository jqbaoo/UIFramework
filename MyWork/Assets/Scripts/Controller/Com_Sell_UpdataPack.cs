using UnityEngine;
using System.Collections;
using GameCore;

public class Com_Sell_UpdataPack : Controller {

    public override void Execute(object data)
    {
        ChooseInfor ci = (ChooseInfor)data;
        GetModel<PackData>().EditorGoodsCount(ci.beChooseId, ci.count);
    }
}
