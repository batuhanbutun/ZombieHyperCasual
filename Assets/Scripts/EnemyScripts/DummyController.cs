using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    [SerializeField] private GameObject dummy;
    private float leftxRange = -3.84f;
    private float rightxRange = 6f;
    private float downyRange = 0.3f;
    private float upyRange = 3.5f;
    private float inzRange = 10f;
    private float outzRange = 20f;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(leftxRange, rightxRange), Random.Range(downyRange,upyRange),Random.Range(inzRange,outzRange));
    }

   public void hitDummy()
    {
        Instantiate(dummy, RandomSpawnPos(), dummy.transform.rotation);
        Destroy(gameObject);
    }
}
