using System.Collections;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject obs1;
    public GameObject obs2;
    public GameObject obs3;
    public GameObject obs4;

    private ObstacleController oc1;
    private ObstacleController oc2;
    private ObstacleController oc3;
    private ObstacleController oc4;

    private WaitForSeconds waitForSec;

    private int num;

    private void Awake()
    {
        Instance = this;
        oc1 = obs1.GetComponent<ObstacleController>();
        oc2 = obs2.GetComponent<ObstacleController>();
        oc3 = obs3.GetComponent<ObstacleController>();
        oc4 = obs4.GetComponent<ObstacleController>();

        waitForSec = new WaitForSeconds(1f);
    }
    void Start()
    {

        StartCoroutine(Loop());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Loop()
    {
        while (true) 
        {
            yield return waitForSec;
            num = Random.Range(1, 5);
            switch (num)
            {
                case 1:
                    //Debug.Log("obs1 active");
                    
                    oc1.isActive = true;
                    oc1.gameObject.SetActive(true);
                    break;
                case 2:
                    //Debug.Log("obs2 active");
                    
                    oc2.isActive = true;
                    oc2.gameObject.SetActive(true);
                    break;
                case 3:
                    //Debug.Log("obs3 active");
                    
                    oc3.isActive = true;
                    oc3.gameObject.SetActive(true);
                    break;
                case 4:
                    //Debug.Log("obs4 active");
                    
                    oc4.isActive = true;
                    oc4.gameObject.SetActive(true);
                    break;
                default:
                    Debug.Log("Random number out of index");
                    break;
            }
        }
    }
}
