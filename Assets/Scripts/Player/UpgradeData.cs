using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeData 
{
    public readonly Sprite sprite;
    public readonly string name;
    public readonly string description;
    Action<MainCharacterController> func;

    int upgrade_count;

    public UpgradeData(Sprite upgrade_sprite, string upgrade_name, string upgrade_desription, Action<MainCharacterController> upgrade_func) {
        sprite = upgrade_sprite;
        name = upgrade_name;
        description = upgrade_desription;
        func = upgrade_func;
        upgrade_count = 0;
    }

    public int UpgradeCount()
    {
        return upgrade_count;
    }

    public void Upgrade(MainCharacterController controller)
    {
        func(controller);
        upgrade_count++;
    }

}
