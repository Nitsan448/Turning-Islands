using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class SetCursorOnHover : MonoBehaviour
{
	private EventTrigger _eventTrigger;
	private delegate void SetCursor();
	private SetCursor _setCursorDelegate;
	private Selectable _selectable;

	private List<Type> _buttonCursorTypes = new List<Type>
	{
		typeof(Toggle),
		typeof(TMP_Dropdown),
		typeof(Dropdown),
		typeof(Button),
		typeof(Slider),
	};

	private List<Type> _inputCursorTypes = new List<Type>
	{
		typeof(TMP_InputField),
		typeof(InputField),
	};

	private void Start()
	{
		_selectable = GetComponent<Selectable>();
		_eventTrigger = gameObject.AddComponent<EventTrigger>();
		SetCursorDelegateValue();

		AddOnPointerEnterListener();
		AddOnPointerExitListener();
	}

	private void OnDisable()
	{
		CursorSetter.SetCursorToStandard();
	}

	private void SetCursorDelegateValue()
	{
		if (_buttonCursorTypes.Contains(_selectable.GetType()))
		{
			_setCursorDelegate = CursorSetter.SetCursorToButton;
		}
		else if (_inputCursorTypes.Contains(_selectable.GetType()))
		{
			_setCursorDelegate = CursorSetter.SetCursorToInputField;
		}
		else
		{
			Destroy(this);
		}
	}

	private void AddOnPointerEnterListener()
	{
		EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
		pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
		pointerEnterEntry.callback.AddListener(delegate 
		{
			if (_selectable.interactable)
			{
				_setCursorDelegate(); 
			}
		});
		_eventTrigger.triggers.Add(pointerEnterEntry);
	}

	private void AddOnPointerExitListener()
	{
		EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
		pointerEnterEntry.eventID = EventTriggerType.PointerExit;
		pointerEnterEntry.callback.AddListener(delegate { CursorSetter.SetCursorToStandard(); });
		_eventTrigger.triggers.Add(pointerEnterEntry);
	}
}
