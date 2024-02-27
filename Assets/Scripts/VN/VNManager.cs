using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VNManager : MonoBehaviour
{
    public Canvas vn_canvas;

    public static VNManager Instance()
    {
        return instance;
    }

    static VNManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void Initialize()
    {
        
    }

    public void StartVN(string vn_path)
    {
        var obj = GameObject.Instantiate(Resources.Load<GameObject>("UI/VNPanel"), vn_canvas.transform);
        if (vn_path == "TEST")
        {
            obj.GetComponent<VNTest>().Setup(0);
        }
        if (vn_path == "TEST2")
        {
            obj.GetComponent<VNTest>().Setup(1);
        }
        if (vn_path == "WIN")
        {
            obj.GetComponent<VNTest>().Setup(2);
        }
        if (vn_path == "LOSS")
        {
            obj.GetComponent<VNTest>().Setup(3);
        }
    }
}
