using UnityEngine;
using System.Collections;
using GameCore;
public class Com_Sell_UpdateInfor : Controller {

    public override void Execute(object data)
    {
        GetModel<InforData>().EditorCoin((int)data);
    }
}