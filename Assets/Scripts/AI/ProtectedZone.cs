using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectedZone
{
    private List<IProtector> _protectors;
    private LevelObjectTrigger _view;


    public ProtectedZone(LevelObjectTrigger view, List<IProtector> protectors)
    {
        _protectors = protectors;
        _view = view;
    }

    public void Init()
    {
        _view.TriggerEnter += OnContact;
        _view.TriggerExit += OnExit;
    }
    public void DeInit()
    {
        _view.TriggerEnter -= OnContact;
        _view.TriggerExit -= OnExit;
    }

    private void OnContact(GameObject player)
    {
        foreach (var protector in _protectors)
            protector.StartProtection(player);
    }
    private void OnExit(GameObject player)
    {
        foreach (var protector in _protectors)
            protector.FinishProtection(player);
    }
}
