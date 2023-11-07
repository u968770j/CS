using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Create GachaTable", fileName = "GachaTableEntity")]
public class GachaTable : ScriptableObject
{
    public new string name; // ���A���e�B�̖��́iNormal �ARare�Ȃǁj
    public int probability; // ������m��
    public new List<string> cards; // ������A�C�e���̃��X�g
}
