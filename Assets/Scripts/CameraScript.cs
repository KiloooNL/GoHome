using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    
    public GameObject player;
    private Vector3 _offset;

    public void Start()
    {
        _offset = transform.position - player.transform.position;
    }

    public void Update()
    {
        if(player != null)
        {
            transform.position = player.transform.position + _offset;
        }
    }
}
