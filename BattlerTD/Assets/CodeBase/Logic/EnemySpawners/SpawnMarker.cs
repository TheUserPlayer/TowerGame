using System.Collections.Generic;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
  public class SpawnMarker : MonoBehaviour
  {
    public List<MonsterTypeId> MeleeMonsterTypeId;

    public Vector3 modelOffset;
    public GameObject mobModelPrefab;
  }
}