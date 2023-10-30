using System;
using UnityEngine;

namespace CodeBase.StaticData
{
  [Serializable]
  public class EnemySpawnerStaticData
  {
    public string Id;
    public MonsterTypeId MeleeMonsterTypeId;
    public MonsterTypeId RangeMonsterTypeId;
    public Vector3 Position;
    public Quaternion Rotation;

    public EnemySpawnerStaticData(string id, MonsterTypeId meleeMonsterTypeId,MonsterTypeId rangeMonsterTypeId, Vector3 position, Quaternion rotation)
    {
      Id = id;
      MeleeMonsterTypeId = meleeMonsterTypeId;
      RangeMonsterTypeId = rangeMonsterTypeId;
      Position = position;
      Rotation = rotation;
    }
  }
}