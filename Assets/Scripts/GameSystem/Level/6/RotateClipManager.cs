using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateClipManager : ClipManager
{
    private string clickEventName = "OnSquareClick";
    private Dictionary<int, List<RotateSquare>> squareListDicForRotate = new Dictionary<int, List<RotateSquare>>();
    protected override void Start()
    {
        Square[] lc = gameObject.transform.GetComponentsInChildren<RotateSquare>();
        foreach (var l in lc)
        {
            SquareList.Add(l);
        }
        SetAs1st();
        GlobalEventManager.instance.RegisterEvent<RotateSquare>("RegisterSquareGroup", SquareGroupRegister);
        GlobalEventManager.instance.RegisterEvent<int>(clickEventName, CheckDic);
    }

    private void SquareGroupRegister(RotateSquare rs)
    {
        if (rs.belongingGroup == -1) return;

        int groupId = rs.belongingGroup;

        if (!squareListDicForRotate.ContainsKey(groupId))
        {
            squareListDicForRotate[groupId] = new List<RotateSquare>();
        }


        if (!squareListDicForRotate[groupId].Contains(rs))
        {
            squareListDicForRotate[groupId].Add(rs);

        }
    }

    private void CheckDic(int group)
    {
        bool canLock = true;
        foreach(var s in squareListDicForRotate[group])
        {
            if (!s.squareComplete) canLock = false;
        }
        if (canLock)
        {
            foreach (var s in squareListDicForRotate[group])
            {
                //有分开的必要时再说
                s.SetSquareComplete();
                s.SetVideoShow();
            }
        }
    }
    
    protected override void CheckAll()
    {
        if (!InteractManager.instance.canInteract)
        {
            return;
        }

        InteractManager.instance.SetDisable();
        bool isAllSelect = true;
        for (int i = 0; i < SquareList.Count; i++)
        {
            if (isCorrectTarget1st[i])
            {
                if (!SquareList[i].isSelect)
                {
                    SquareList[i].SetFalse();
                    isAllSelect = false;
                }
            }
            else
            {
                if (SquareList[i].isSelect)
                {
                    SquareList[i].SetFalse();
                }
            }
        }
        if (isAllSelect)
        {
            ap.PlaySoundEffects(0);
            //GameManager.instance.GoToNextScene(nextScene.name);
        }
        else
        {
            LevelSetFault();
            ap.PlaySoundEffects(1);
        }
        LevelCa();
        GameManager.instance.GoToNextScene(nextScene);
    }

    protected override void LevelCa()
    {
        if (levelState)
        {
            GameManager.instance.AddHumanValue(25);
        }
        else
        {
            GameManager.instance.AddHumanValue(-10);
        }
    }



    protected override void OnDestroy()
    {
        GlobalEventManager.instance.UnregisterEvent<RotateSquare>("RegisterSquareGroup", SquareGroupRegister);
        GlobalEventManager.instance.UnregisterEvent<int>(clickEventName, CheckDic);
        //GlobalEventManager.instance.UnregisterEvent(receivedEventName, CheckAll);
        //GlobalEventManager.instance.UnregisterEvent(onVideoEventName, OnVideoFinnished);
    }
}
