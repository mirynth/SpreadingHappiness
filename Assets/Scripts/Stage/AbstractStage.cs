using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractStage 
{
    protected StageState state = StageState.Constructed;

    public virtual StageState GetState()
    {
        return state;
    }

    public virtual void IncrementStage(bool win = true)
    {
        StageState old_state = state;
        switch (state)
        {
            case StageState.Constructed:
                state = StageState.Setup;
                break;
            case StageState.Setup:
                state = StageState.VN_1;
                break;
            case StageState.VN_1:
                state = StageState.Gameplay;
                break;
            case StageState.Gameplay:
                state = StageState.VN_2;
                break;
            case StageState.VN_2:
                state = StageState.Boss;
                break;
            case StageState.Boss:
                if(win)
                {
                    state = StageState.VN_Win;
                } else
                {
                    state = StageState.VN_Loss;
                }
                break;
            case StageState.VN_Win:
                state = StageState.Complete;
                break;
            case StageState.VN_Loss:
                state = StageState.Complete;
                break;
            case StageState.Complete:
                break;
        }
        OnStateEnd(old_state);
        OnStateStart(state);
    }

    public void Update()
    {
        OnUpdate();
        if (CheckStateFinished(state))
            IncrementStage();
    }

    protected abstract void OnUpdate();
    public abstract void OnStateStart(StageState new_state);
    public abstract void OnStateEnd(StageState current_state);
    public abstract bool CheckStateFinished(StageState current_state);
}
