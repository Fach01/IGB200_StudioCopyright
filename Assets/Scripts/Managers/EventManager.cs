using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private GameEvent nextEvent;

    public IEnumerator Flood()
    {
        yield return null;
    }
}