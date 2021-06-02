using UnityEngine;
using System.Collections;
using UnityEngine.Events;

//Event
[CreateAssetMenu(fileName = "New GameObj Event", menuName = "Game Event/GameObj Event")]
[System.Serializable] public class GameObjEvent : BaseGameEvent<GameObject> { }


//Unity Event 
[System.Serializable] public class UnityGameObjEvent : UnityEvent<GameObject> { }

