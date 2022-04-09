using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public int maxNumberOfTrack = 10;
    public GameObject track;
    public Transform startPoint;
    public Transform trackParent;
    public float currentTrackLength = 0;
    public Queue<GameObject> allTracks=new Queue<GameObject>();

    public static TrackManager instance;
    private void Awake()
    {
        instance=this;
    }

    private void Start()
    {
        for (int i = 0; i <= maxNumberOfTrack; i++)
        {
            GameObject inst = Instantiate(track);
            inst.transform.position = new Vector3(startPoint.position.x, startPoint.position.y, startPoint.position.z + currentTrackLength);
            currentTrackLength += inst.transform.localScale.z;
            inst.transform.SetParent(trackParent);
            allTracks.Enqueue(inst);
        }
    }
    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.A))
        {
            ShiftTracks();
        }
    }
    public void ShiftTracks()
    {
        GameObject obj = allTracks.Dequeue();
        obj.transform.position= new Vector3(startPoint.position.x, startPoint.position.y, startPoint.position.z + currentTrackLength);
        currentTrackLength += obj.transform.localScale.z;
        allTracks.Enqueue(obj);
        ScoreManager.scoreEvent(1);
    }
}
