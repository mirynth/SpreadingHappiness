using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Object Pooling primarily meant for projectiles.
 * 
 * Access by calling Pools.Instance().<type>.Func
 * 
 * You probably want CreatePoolable and DestroyPoolable(type)
 **/
public class ObjectPool<Type> where Type: MonoBehaviour, IPoolable
{
    bool b_valid = false;

    HashSet<Type> m_active;
    Queue<Type> m_inactive;
    Func<Type> Func_Instantiate;

    /*
     * Create the Object Pool, take the instantiation function and frontload.
     * 
     * Instantiation function should create a GameObject Prefab and return a Monobehaviour that is also IPoolable 
     * */
    public void Initialize(int size_hints, Func<Type> Func_Instantiate)
    {
        b_valid = true;
        m_active = new HashSet<Type>((size_hints * 2) + 1);
        m_inactive = new Queue<Type>((size_hints * 2) + 1);
        this.Func_Instantiate = Func_Instantiate;

        for (int i = 0; i < size_hints; i++) {
            CreatePooledObject();
        }
    }

    /*
     * This function gets a fresh instance of a Poolable Object for you to setup, call this instead of GameObject.Instantiate()
     * */
    public Type CreatePoolable()
    {
        if (!b_valid)
            return null;

        if (m_inactive.Count == 0)
        {
            CreatePooledObject();
        }
        Type result = m_inactive.Dequeue();
        result.gameObject.SetActive(true);
        m_active.Add(result);
        result.OnPooled();
        return result;
    }

    /*
     * This function cleans up pooled object created in the world, call this instead of GameObject.Destroy()
     * */
    public void DestroyPoolable(Type pooled_object)
    {
        if (!b_valid)
            return;

        if (m_active.Contains(pooled_object))
        {
            m_active.Remove(pooled_object);
            pooled_object.gameObject.SetActive(false);
            m_inactive.Enqueue(pooled_object);
            pooled_object.OnUnPooled();
        }
    }

    void CreatePooledObject()
    {
        Type t = Func_Instantiate();
        t.OnPoolCreate();
        m_inactive.Enqueue(t);
        t.gameObject.SetActive(false);
    }
    
    /*
     * Cleanup and invalidate the Object Pool
     * */
    public void Destroy()
    {
        b_valid = false;

        foreach (Type t in m_active)
        {
            if (t != null)
            {
                t.OnPoolDestroy();
                if (t.gameObject != null)
                {
                    GameObject.Destroy(t.gameObject);
                }
            }                        
        }
        foreach(Type t in m_inactive)
        {
            if (t != null)
            {
                t.OnPoolDestroy();
                if (t.gameObject != null)
                {
                    GameObject.Destroy(t.gameObject);
                }
            }
        }

        m_active.Clear();
        m_inactive.Clear();
    }

    /*
     * Reset the object pool, moves all active instances to inactive.
     * */
    public void Reset()
    {
        if (!b_valid)
            return;

        foreach(var t in m_active)
        {
            t.OnPoolReset();
            m_inactive.Enqueue(t);
            t.gameObject.SetActive(false);
        }
        m_active.Clear();
    }

    public int ActiveCount()
    {
        return m_active.Count;
    }
    
    public int InactiveCount()
    {
        return m_inactive.Count;
    }

    public bool IsValid()
    {
        return b_valid;
    }
}
