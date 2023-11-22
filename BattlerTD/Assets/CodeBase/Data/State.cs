using System;

namespace CodeBase.Data
{
  [Serializable]
  public class State
  {
    public float CurrentHP;
    public float MaxHP;
    public int Regeneration; 

    public void ResetHp()
    {
      CurrentHP = MaxHP;
    }
  }
}