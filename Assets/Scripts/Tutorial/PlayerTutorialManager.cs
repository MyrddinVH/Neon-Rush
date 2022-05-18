using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerTutorialManager : MonoBehaviour
{
    public static event Action ShowJumpHintEvent;
    public static event Action ShowDashHintEvent;
    public static event Action JumpDeathEvent;
    public static event Action DashDeathEvent;
    public static event Action JumpDashDeathEvent;
    public static event Action ShowJumpDashHintEvent;
    public static event Action ShowMidAirHint;
    public static event Action ShowTutotialEndEvent;

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "JumpNotiTriggerBox"){
            ShowJumpHintEvent?.Invoke();
        }

        if(other.gameObject.name == "DashNotiTriggerBox"){
            ShowDashHintEvent?.Invoke();
        }

        if(other.gameObject.name == "JumpDeath"){
            JumpDeathEvent?.Invoke();
        }

        if(other.gameObject.name == "DashDeath"){
            DashDeathEvent?.Invoke();
        }

        if(other.gameObject.name == "JumpDashNotiTriggerBox"){
            ShowJumpDashHintEvent?.Invoke();
        }

        if(other.gameObject.name == "JumpDashDeath"){
            JumpDashDeathEvent?.Invoke();
        }

        if(other.gameObject.name == "MidAirHintTrigger"){
            ShowMidAirHint?.Invoke();
        }

        if(other.gameObject.name == "TutorialEnd"){
            ShowTutotialEndEvent?.Invoke();
        }
    }
}
