using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface IInteractable
{
    public event EventHandler OnInteractEvent;
    public void Interact(Player player);
    public void InteractAlternate(Player player);
}
