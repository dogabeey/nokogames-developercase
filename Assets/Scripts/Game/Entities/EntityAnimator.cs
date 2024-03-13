using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Entity))]
[RequireComponent(typeof(Animator))]
public class EntityAnimator : MonoBehaviour
{
	[System.Serializable]
	public class BlendSetting
	{
		public string blendTreeName;
		public float value;
	}

	public List<BlendSetting> blendSettings;
	public float minVelocity = 0.01f;
	public string idleText;
	public string runText;
	public string jumpText;
	public string carryText;
	public string verticalText;
	public string horizontalText;

	public bool Idle
	{
        get
		{
			return entity.state == Entity.EntityState.Idle;
        }
    }
    public bool Run
    {
        get
        {
            return entity.state == Entity.EntityState.Run;
        }
    }

    protected Animator animator;
	protected Entity entity;

	#region Events

	// Set trigger events here using the event listeners.
	public virtual void OnEnable()
	{
		EventManager.StartListening(Const.GameEvents.CREATURE_DEATH, OnDeath);
	}
	public virtual void OnDisable()
	{
		EventManager.StopListening(Const.GameEvents.CREATURE_DEATH, OnDeath);
    }
	void OnDeath(EventParam e)
	{
		if (e.paramObj.GetHashCode() == gameObject.GetHashCode())
		{
			animator.SetTrigger("die");
		}
	}

	#endregion

	public virtual void Start()
	{
		entity = GetComponent<Entity>();
		animator = GetComponent<Animator>();
	}

	public virtual void Update()
	{
		AnimationController();
		SetBlendValues();
	}

	public void AnimationController()
	{
		animator.SetBool(idleText, Idle);
		animator.SetBool(runText, Run);
	}

	void SetBlendValues()
	{
		foreach (BlendSetting setting in blendSettings)
		{
			animator.SetFloat(setting.blendTreeName, setting.value);
		}
	}
}