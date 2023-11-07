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

        // �q�K�w��GameObject�擾
        var childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            objList.Add(transform.GetChild(i));
        }

        // �I�u�W�F�N�g�𖼑O�ŏ����\�[�g
        // ��������p�r�ɍ��킹�ĕύX���Ă�������
        objList.Sort((obj1, obj2) => obj2.GetComponent<Card>().Base.CardID - obj1.GetComponent<Card>().Base.CardID);

        // �\�[�g���ʏ���GameObject�̏����𔽉f
        foreach (var obj in objList)
        {
            obj.SetSiblingIndex(childCount - 1);
        }
    }
}
