using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 Offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 pos = Player.instance.transform.position - Offset;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * 3);
    }
}
