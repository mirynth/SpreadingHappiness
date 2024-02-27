using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestStageAlpha : AbstractStage
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
                TimeManager.Instance().SetPause(false);
                break;
            case StageState.Gameplay:
                Pools.Instance().projectilePool.Reset();
                Pools.Instance().magicalGirlPool.Reset();
                break;
            case StageState.VN_2:
                TimeManager.Instance().Reset();
                TimeManager.Instance().SetPause(false);
                break;
            case StageState.Boss:
                GameManager.Instance.SetCameraBounds(GameManager.Instance.stage_bounds);
                Pools.Instance().projectilePool.Reset();
                Pools.Instance().magicalGirlPool.Reset();
                break;
            case StageState.VN_Win:
                TimeManager.Instance().SetPause(false);
                TimeManager.Instance().Reset();
                GameManager.Instance.SetPlayerInputable(true);
                break;
            case StageState.VN_Loss:
                TimeManager.Instance().SetPause(false);
                TimeManager.Instance().Reset();
                GameManager.Instance.SetPlayerInputable(true);
                SceneManager.LoadScene("BadEnding");
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
                Debug.Log("Alpha Stage Start");
                break;
            case StageState.VN_1:
                GameManager.Instance.SetPlayerInputable(false);
                TimeManager.Instance().SetPause(true);
                VNManager.Instance().StartVN("TEST");
                break;
            case StageState.Gameplay:
                GameManager.Instance.SetPlayerInputable(true);
                GameManager.Instance.SetCameraBounds(GameManager.Instance.stage_bounds);
                gameplay_scripting.Clear();
                gameplay_scripting.Enqueue(new(1.0f, () => { 
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), false, new Vector3(-10, -10));
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), false, new Vector3(10, -10));
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), false, new Vector3(-10, 10));
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), false, new Vector3(10, 10));
                }));
                gameplay_scripting.Enqueue(new(15.0f, () => {
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), true, new Vector3(-10, -10));
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), true, new Vector3(10, -10));
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), true, new Vector3(-10, 10));
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicAngryMagicalGirl(), new BasicHappyMagicalGirl(), true, new Vector3(10, 10));
                }));
                break;
            case StageState.VN_2:
                GameManager.Instance.SetPlayerInputable(false);
                TimeManager.Instance().SetPause(true);
                VNManager.Instance().StartVN("TEST2");
                break;
            case StageState.Boss:
                GameManager.Instance.SetPlayerInputable(true);
                GameManager.Instance.SetCameraBounds(GameManager.Instance.boss_bounds);
                MainCharacterController.instance.transform.position = Vector3.zero;
                gameplay_scripting.Enqueue(new(1.0f, () => {
                    GameManager.Instance.CreatePoolableFromMagicalGirl(new BasicBossMagicalGirl(), new BasicHappyMagicalGirl(), true, new Vector3(0, 10));
                }));
                break;
            case StageState.VN_Win:
                GameManager.Instance.SetPlayerInputable(false);
                TimeManager.Instance().SetPause(true);
                VNManager.Instance().StartVN("WIN");
                break;
            case StageState.VN_Loss:
                GameManager.Instance.SetPlayerInputable(false);
                TimeManager.Instance().SetPause(true);
                VNManager.Instance().StartVN("LOSS");
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
                return false;
            case StageState.Gameplay:
                if (time_since_state_start > 60.0f)
                    return true;
                break;
            case StageState.VN_2:
                return false;
            case StageState.Boss:
                if (time_since_state_start > 60.0f)
                    return true;
                break;
            case StageState.VN_Win:
                return false;
            case StageState.VN_Loss:
                return false;
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
        else
        {

        }
    }
}