using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    //Called when "Added" to the gameworld
    public abstract void OnPooled();

    //Called when "Removed" from the gameworld
    public abstract void OnUnPooled();

    //Custom Behaviour for ObjectPool.Pool().Initialize() (Or when extra instances are required to be instantiated at runtime)
    public abstract void OnPoolCreate();

    //Behaviour if ObjectPool.Pool().DestroyPool() is called
    public abstract void OnPoolDestroy();

    //Behaviour if ObjectPool.Pool().ResetPool() is called
    public abstract void OnPoolReset();
}
