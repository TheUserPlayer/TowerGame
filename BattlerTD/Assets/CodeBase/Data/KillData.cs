using System;

namespace CodeBase.Data
{
  [Serializable]
  public class KillData
  {
    public int KilledMobs;
    public int CurrentMonsterWaves;
    public int MonsterWavesForFinish;
    
    public Action LootChanged;
    public Action WaveChanged;


    public void NextWave()
    {
      CurrentMonsterWaves += 1;
      WaveChanged?.Invoke();
    }

    public void ResetWaveData()
    {
      MonsterWavesForFinish = 5;
      CurrentMonsterWaves = 0;
      WaveChanged?.Invoke();
    }
    public void Add(int lootValue)
    {
      KilledMobs += lootValue;
      LootChanged?.Invoke();
    } 
    
    public void ResetKillData()
    {
      KilledMobs = 0;
      LootChanged?.Invoke();
    }
  }
}