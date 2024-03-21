using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    static StageManager instance;

    public static StageManager Instance()
    {
        return instance;
    }

    Queue<AbstractStage> stages = new();
    AbstractStage current_stage = null;

    private void Awake()
    {
        instance = this;
        current_stage = null;
        stages.Enqueue(new TestStageAlpha());
        stages.Enqueue(new TestStageBeta());
    }

    public void Initialize()
    {
        if(current_stage == null)
        {
            current_stage = stages.Dequeue();
        }
        current_stage.IncrementStage();
    }

    public void FixedUpdate()
    {
        if(current_stage != null)
        {
            current_stage.Update();            

            if(current_stage.GetState() == StageState.Complete)
            {
                if (stages.Count > 0)
                {
                    current_stage = stages.Dequeue();
                    current_stage.IncrementStage();
                } else
                {
                    SceneManager.LoadScene("HappyEnding");
                }
            }
        }
    }

    public void CheckSkipToWin()
    {
        if(current_stage.GetState() == StageState.Complete && stages.Count == 0)
        {
            SceneManager.LoadScene("HappyEnding");
        }
    }

    public void SkipToLoss()
    {
        current_stage.IncrementStage(true);
    }

    public AbstractStage GetCurrentStage()
    {
        return current_stage;
    }
}
