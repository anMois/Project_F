using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectName : MonoBehaviour
{
    public string ObjName;
    public int Price;

    private void Start()
    {
        print(this.ObjName);
        print(this.Price);
    }

}
