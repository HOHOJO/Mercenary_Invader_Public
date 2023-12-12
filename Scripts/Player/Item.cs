using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type {weapon};
    public Type type;
    public int value;

   void Update()
    {
        transform.Rotate(Vector3.up * 0 * Time.deltaTime);
    }
}
