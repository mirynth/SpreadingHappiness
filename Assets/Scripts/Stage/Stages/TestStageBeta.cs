using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStageBeta : AbstractStage
{
    float time_since_state_start = 0.0f;
    Queue<KeyValuePair<float, Action>> gameplay_scripting = new Queue<KeyValuePair<float, Action>>();


    public override void OnStateEnd(StageState current_state)
    {
        switch (current_state)
        {
            case StageState.Constructed:
                break;
            case StageState.Setup:
                break;
            case StageState.VN_1:
                break;
            case StageState.Gameplay:
                Pools.Instance().magicalGirlPool.Reset();
                Pools.Instance().projectilePool.Reset();
                break;
            case StageState.VN_2:
                break;
            case StageState.Boss:
                Pools.Instance().projectilePool.Reset();
                Pools.Instance().magicalGirlPool.Reset();
                break;
            case StageState.VN_Win:
                break;
            case StageState.VN_Loss:
                break;
            case StageState.Complete:
                break;
        }
    }

    public override void OnStateStart(StageState new_state)
    {
        time_since_state_start = 0.0f;
        switch (new_state)
        {
            case StageState.Constructed:
                //This never happens
                break;
            case StageState.Setup:
                Debug.Log("Beta Stage Start");
                break;
            case StageState.VN_1:
                break;
            case StageState.Gameplay:
                gameplay_scripting.Clear();
                gameplay_scripting.Enqueue(new(1.0f, () => {
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), false, new Vector3(-10, -10));
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), false, new Vector3(10, -10));
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), false, new Vector3(-10, 10));
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), false, new Vector3(10, 10));
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), false, new Vector3(-15, -15));
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), false, new Vector3(15, -15));
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), false, new Vector3(-15, 15));
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), false, new Vector3(15, 15));
                }));
                gameplay_scripting.Enqueue(new(15.0f, () => {
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), true, new Vector3(-10, -10));
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), true, new Vector3(10, -10));
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), true, new Vector3(-10, 10));
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), true, new Vector3(10, 10));
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), true, new Vector3(-15, -15));
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), true, new Vector3(15, -15));
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), true, new Vector3(-15, 15));
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), true, new Vector3(15, 15));
                }));
                break;
            case StageState.VN_2:
                break;
            case StageState.Boss:
                break;
            case StageState.VN_Win:
                break;
            case StageState.VN_Loss:
                break;
            case StageState.Complete:
                break;
        }
    }
    public override bool CheckStateFinished(StageState current_state)
    {
        switch (current_state)
        {
            case StageState.Constructed:
                return true;
            case StageState.Setup:
                return true;
            case StageState.VN_1:
                //External Increment From VN System (Set false here)
                return true;
            case StageState.Gameplay:
                if (time_since_state_start > 60.0f)
                    return true;
                break;
            case StageState.VN_2:
                //External Increment From VN System (Set false here)
                return true;
            case StageState.Boss:
                if (time_since_state_start > 60.0f)
                    return true;
                break;
            case StageState.VN_Win:
                //External Increment From VN System (Set false here)
                return true;
            case StageState.VN_Loss:
                //External Increment From VN System (Set false here)
                return true;
            case StageState.Complete:
                return true;
        }
        return false;
    }

    protected override void OnUpdate()
    {
        time_since_state_start += Time.fixedDeltaTime;

        if (state == StageState.Gameplay || state == StageState.Boss)
        {
            if (gameplay_scripting.Count > 0)
            {
                if (gameplay_scripting.Peek().Key <= time_since_state_start)
                {
                    var pair = gameplay_scripting.Dequeue();
                    pair.Value();
                }
            }
        }
    }
}