using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData
{
  [Serializable]
  public class EnemySpawnerStaticData
  {
    public string Id;
    public List<MonsterTypeId> MeleeMonsterTypeId;
    public Vector3 Position;
    public Quaternion Rotation;

    public EnemySpawnerStaticData(string id, List<MonsterTypeId> meleeMonsterTypeId, Vector3 position, Quaternion rotation)
    {
      Id = id;
      MeleeMonsterTypeId = meleeMonsterTypeId;
      Position = position;
      Rotation = rotation;
    }
  }
}