using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    private Dictionary<string, Action<EventParam>> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, Action<EventParam>>();
        }
    }

    public static void StartListening(string eventName, Action<EventParam> listener)
    {
        Action<EventParam> thisEvent;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            instance.eventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, Action<EventParam> listener)
    {
        if (eventManager == null) return;
        Action<EventParam> thisEvent;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            instance.eventDictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(string eventName, EventParam eventParam)
    {
        Action<EventParam> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventParam);
        }
    }
}


public class EventParam
{
    public GameObject paramObj;
    public int paramInt;
    public float paramFloat;
    public string paramStr;
    public Type paramType;
    public bool paramBool;
    public Dictionary<string, object> paramDictionary;
    public Vector3[] vectorList;

    public Transform transform;
    public Entity entity;

    public EventParam(GameObject paramObj = null, int paramInt = 0, float paramFloat = 0f, string paramStr = "", Type paramType = null, Dictionary<string, object> paramDictionary = null,
    Vector3[] vectorList = null, bool paramBool = false, Entity entity = null)
    {
        this.paramObj = paramObj;
        this.paramInt = paramInt;
        this.paramFloat = paramFloat;
        this.paramStr = paramStr;
        this.paramType = paramType;
        this.paramDictionary = paramDictionary;
        this.paramBool = paramBool;
        this.vectorList = vectorList;
        this.entity = entity;
    }
}