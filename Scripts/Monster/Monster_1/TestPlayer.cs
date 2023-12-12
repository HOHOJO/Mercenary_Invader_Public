using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    [SerializeField] public float hp;
    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hit(float n)
    {
        hp -= n;
        Debug.Log("피격당했다! "+hp);
    }
}
