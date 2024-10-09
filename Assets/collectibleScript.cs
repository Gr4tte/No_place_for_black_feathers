using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectibleScript : MonoBehaviour
{
    private CircleCollider2D _col;
    private LevelManager _levelScript;
    
    // Start is called before the first frame update
    void Start()
    {
        //_col = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _levelScript = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        _levelScript.lvl2CollectibleGot = true;
        gameObject.SetActive(false);
    }
}
