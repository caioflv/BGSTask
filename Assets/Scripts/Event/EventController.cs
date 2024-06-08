using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventController
{
    public static Action<bool, Vector2> InteractableElement { get; set; }
    public static Action<bool> TogglePlayerController { get; set; }
}
