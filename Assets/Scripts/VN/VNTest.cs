using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VNTest : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI text;

    Queue<string> dialogue = new();
    public bool test_a = false;

    public void Begin()
    {
        if (!test_a)
        {
            dialogue.Enqueue("Important Gameplay Lore");
            dialogue.Enqueue("Important Gameplay Story");
            dialogue.Enqueue("Survive for a minute, not that you can take damage or anything");
            dialogue.Enqueue("enough of that dodge balls");
        } else
        {
            dialogue.Enqueue("You survived...");
            dialogue.Enqueue("Now it's boss time");
            dialogue.Enqueue("dodge stuff for another minute");
        }
        ContinueDialogue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            ContinueDialogue();
    }

    public void ContinueDialogue()
    {
        if (dialogue.Count > 0)
        {
            text.SetText(dialogue.Dequeue());
        } else
        {
            GameObject.Destroy(this.gameObject);
            StageManager.Instance().GetCurrentStage().IncrementStage();
        }
    }
}
