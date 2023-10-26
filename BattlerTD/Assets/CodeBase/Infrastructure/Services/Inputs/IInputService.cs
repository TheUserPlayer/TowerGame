using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Inputs
{
  public interface IInputService : IService
  {
    event Action TowerButtonPressed;
    event Action TowerButtonUnpressed;
    Vector2 Axis { get; }
    Touch Touch { get; set; }
    bool Tap { get; set; }
    bool IsDragging { get; }
    bool SwipeLeft{ get; set; } 
    bool SwipeRight { get; set; }
    bool SwipeUp { get; set; } 
    bool SwipeDown { get; set; }
    void Update();
    bool IsAttackButtonUp();
  }
}