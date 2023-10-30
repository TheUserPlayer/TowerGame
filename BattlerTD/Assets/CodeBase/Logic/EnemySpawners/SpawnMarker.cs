using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
  public class SpawnMarker : MonoBehaviour
  {
    public MonsterTypeId MeleeMonsterTypeId;
    public MonsterTypeId RangeMonsterTypeId;

    public Vector3 modelOffset;
    public GameObject mobModelPrefab;
  }
}