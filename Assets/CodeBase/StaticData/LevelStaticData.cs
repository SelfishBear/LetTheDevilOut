using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData
{
	[CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
	public class LevelStaticData : ScriptableObject
	{
		public Vector3 BasePosition;
		public Vector3 InitialPlayerPosition;
		public string LevelKey;
	}

}