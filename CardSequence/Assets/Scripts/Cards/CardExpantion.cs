using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardExpantion : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform cardPanel; // �J�[�h��UI Panel

    private bool isCursorOverCard = false;
    private float cursorOverTime = 0f;
    private float requiredCursorTime = 1f; // �J�[�h���g�傷��܂ł̕K�v�ȃJ�[�\�����킹����
    private int defaultSiblingIndex;
    private Transform defaultParent;

    // Update is called once per frame
    void Update()
    {
        // �J�[�h��ɃJ�[�\�������邩�ǂ����̌��o
        if (isCursorOverCard)
        {
            // �J�[�\�����J�[�h��ɂ��鎞�Ԃ̌v��
            cursorOverTime += Time.deltaTime;

            // ��莞�Ԉȏ�J�[�\�������킳���Ă����ꍇ�A�J�[�h���g��
            if (cursorOverTime >= requiredCursorTime)
            {
                ExpandCard();
            }
        }
        else
        {
            // �J�[�\�����J�[�h��ɂȂ��ꍇ�̓��Z�b�g
            ResetCard();
        }
    }

    // �}�E�X��UI Panel�ɓ��������̏���
    public void OnPointerEnter(PointerEventData eventData)
    {
        isCursorOverCard = true;
        defaultParent = transform.parent.transform;
        defaultSiblingIndex = defaultParent.GetSiblingIndex();
        defaultParent.SetAsLastSibling();
    }

    // �}�E�X��UI Panel����o�����̏���
    public void OnPointerExit(PointerEventData eventData)
    {
        isCursorOverCard = false;
        defaultParent.SetSiblingIndex(defaultSiblingIndex);
    }

    // �J�[�h���g�傷�鏈��
    private void ExpandCard()
    {
        // �����ɃJ�[�h���g�傷�鏈����ǉ�
        transform.localScale = Vector3.one * 1.5f;
        GetComponentInChildren<Canvas>().sortingLayerName = "Card";
    }

    private void ResetCard()
    {
        cursorOverTime = 0f;
        transform.localScale = Vector3.one;
        GetComponentInChildren<Canvas>().sortingLayerName = "Default";
    }
}
