using System.Collections;
using System.Collections.Generic;
using ProjectReLink.Core.Battle;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BattleEventBus.Subscribe(TriggerPoint.OnTurnStart, (context) =>
        {
            Debug.Log("Turn started for: " + context.Actor.name);
            
        });
    }
}
