using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SortCardList : MonoBehaviour
{
    [ContextMenu("Sort card list")]
    public void Sort()
    {
        List<Transform> objList = new List<Transform>();

        // 子階層のGameObject取得
        var childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            objList.Add(transform.GetChild(i));
        }

        // オブジェクトを名前で昇順ソート
        // ★ここを用途に合わせて変更してください
        objList.Sort((obj1, obj2) => obj2.GetComponent<Card>().Base.CardID - obj1.GetComponent<Card>().Base.CardID);

        // ソート結果順にGameObjectの順序を反映
        foreach (var obj in objList)
        {
            obj.SetSiblingIndex(childCount - 1);
        }
    }
}
