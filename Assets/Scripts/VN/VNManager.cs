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
        if(vn_path == "TEST")
        {
            var obj = GameObject.Instantiate(Resources.Load<GameObject>("UI/VNPanel"), vn_canvas.transform);
            obj.GetComponent<VNTest>().Begin();
        }
        if (vn_path == "TEST2")
        {
            var obj = GameObject.Instantiate(Resources.Load<GameObject>("UI/VNPanel2"), vn_canvas.transform);
            obj.GetComponent<VNTest>().Begin();
        }
    }
}
