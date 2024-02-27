using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VNTest : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI text;

    Queue<string> dialogue = new();
    public int test_value = 0;

    public void Setup(int value)
    {
        test_value = value;
        switch(test_value)
        {
            case 0:
                dialogue.Enqueue("Important Gameplay Lore");
                dialogue.Enqueue("Important Gameplay Story");
                dialogue.Enqueue("Survive for a minute, not that you can take damage or anything");
                dialogue.Enqueue("enough of that... dodge balls");
                break;
            case 1:
                dialogue.Enqueue("You survived...");
                dialogue.Enqueue("Now it's boss time");
                dialogue.Enqueue("dodge stuff for another minute");
                break;
            case 2:
                dialogue.Enqueue("You are winner!");
                dialogue.Enqueue("Next stages starts now");
                break;
            case 3:
                dialogue.Enqueue("You died");
                dialogue.Enqueue("All the magical girls are sad now");
                dialogue.Enqueue(" :'( ");
                break;
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
