using UnityEngine;

namespace RoadBattle
{
	[CreateAssetMenu(fileName = "ControlOutputsSO", menuName = "Scriptable Objects/ControlOutputsSO")]
	public class ControlOutputsSO : ScriptableObject
	{
		public float Steer;
		public float Brake;
	} 
}
