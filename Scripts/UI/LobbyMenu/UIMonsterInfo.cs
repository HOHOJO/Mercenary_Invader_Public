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

        Names[0] = "레드";
        Names[1] = "T16";
        Names[2] = "???";

        Stroys[0] = "행성의 균형을 지키는 수호자로서 강력한 불꽃 마법과 높은 지능을 가졌다.";
        Stroys[1] = "강한 전투능력을 지닌 생체 드로이드. 오직 살육을 위해 프로그래밍되어있다.";
        Stroys[2] = "식별 불가한 생명체의 골격 잔해. 알 수 없는 에너지로 동작하고 있다.";

        MonsterNameText.text = Names[num];
        MonsterStroy.text = Stroys[num];
        MonsterImage.sprite = images[num];
    }

    public void NextMonster()
    {
        Debug.Log("다음");
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
        Debug.Log("이전");
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
