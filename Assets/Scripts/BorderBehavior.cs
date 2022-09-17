using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderBehavior : MonoBehaviour
{
    public Transform PlaygroundCentre;

    public float ForceMultiplier = 0.0001f, ForceGrowthFactor = 2f;

    private Dictionary<GameObject, StayInfo> stayInfos = new();

    private void OnTriggerEnter(Collider other)
    {
        stayInfos[other.gameObject] = new StayInfo(other);
    }

    private void OnTriggerStay(Collider other)
    {
        var info = stayInfos[other.gameObject];
        var elapsedTimeMillis = (Time.timeAsDouble - info.EntryTime)*1000d;

        var force =  PlaygroundCentre.position - other.transform.position;
        force *= Mathf.Pow((float)elapsedTimeMillis, ForceGrowthFactor) * ForceMultiplier;
        info.Rigidbody.AddForce(force);
    }

    private class StayInfo
    {
        public double EntryTime;
        public Rigidbody Rigidbody;

        public StayInfo(Collider c)
        {
            EntryTime = Time.timeAsDouble;
            Rigidbody = c.GetComponent<Rigidbody>();
        }
    }
}
