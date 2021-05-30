using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Event
[CreateAssetMenu(fileName = "New InfoStruct Event", menuName = "Game Event/InfoStruct Event")]
[System.Serializable] public class InfoEvent : BaseGameEvent<InfoStruct> { }


//Unity Event 
[System.Serializable] public class UnityInfoEvent : UnityEvent<InfoStruct> { }



