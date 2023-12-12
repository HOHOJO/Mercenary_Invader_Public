using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIMonsterInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text MonsterNameText;
    [SerializeField] private TMP_Text MonsterStroy;
    [SerializeField] private Image MonsterImage;
    [SerializeField] private Sprite MonsterImage1;
    [SerializeField] private Sprite MonsterImage2;
    [SerializeField] private Sprite MonsterImage3;

    private string[] Names;
    private string[] Stroys;
    private Sprite[] images ;

    private int num;

    // Start is called before the first frame update
    void Start()
    {
        num = 0;

        Names = new string[3];
        Stroys = new string[3];
        images = new Sprite[3];

        images[0] = MonsterImage1;
        images[1] = MonsterImage2;
        images[2] = MonsterImage3;

        Names[0] = "����";
        Names[1] = "T16";
        Names[2] = "???";

        Stroys[0] = "�༺�� ������ ��Ű�� ��ȣ�ڷμ� ������ �Ҳ� ������ ���� ������ ������.";
        Stroys[1] = "���� �����ɷ��� ���� ��ü ����̵�. ���� ������ ���� ���α׷��ֵǾ��ִ�.";
        Stroys[2] = "�ĺ� �Ұ��� ����ü�� ��� ����. �� �� ���� �������� �����ϰ� �ִ�.";

        MonsterNameText.text = Names[num];
        MonsterStroy.text = Stroys[num];
        MonsterImage.sprite = images[num];
    }

    public void NextMonster()
    {
        Debug.Log("����");
        if (num==2)
        {
            num = 0;
        }
        else
        {
            num++;
        }

        MonsterNameText.text = Names[num];
        MonsterStroy.text = Stroys[num];
        MonsterImage.sprite = images[num];
    }

    public void PreviousMonster()
    {
        Debug.Log("����");
        if (num == 0)
        {
            num = 2;
        }
        else
        {
            num--;
        }

        MonsterNameText.text = Names[num];
        MonsterStroy.text = Stroys[num];
        MonsterImage.sprite = images[num];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
