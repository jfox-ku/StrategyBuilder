using UnityEngine;
using System.Collections;
using UnityEngine.Events;

//Event
[CreateAssetMenu(fileName = "New String Event", menuName = "Game Event/String Event")]
[System.Serializable] public class StringEvent : BaseGameEvent<string> { }


//Unity Event 
[System.Serializable] public class UnityStringEvent : UnityEvent<string> { }

