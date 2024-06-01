using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Variables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Action<Character, bool> action; // need for movetolocation
    public Character character;
    public GlobalNameVariables globalNameVariables;
    public LocalNameVariables localNameVariables;

    

    // Start is called before the first frame update

    private void Awake()
    {
        localNameVariables = GetComponent<LocalNameVariables>();
    }
    void Start()
    {
        if (GlobalNameVariablesManager.Instance.Exists(globalNameVariables, "Crazy"))
        {
            Debug.Log("Crazy exist");
        }
        double myVariable = (double)GlobalNameVariablesManager.Instance.Get(globalNameVariables, "Crazy");
        Debug.Log("Crazy is = " + myVariable);

        GlobalNameVariablesManager.Instance.Set(globalNameVariables, "Crazy", 10f);
        myVariable = (double)GlobalNameVariablesManager.Instance.Get(globalNameVariables, "Crazy");
        Debug.Log("Crazy is changed to 10? " + myVariable);

        if (localNameVariables != null)
        {
            if (localNameVariables.Exists("Nothing"))
            {
                Debug.Log("Nothing exist");
            }
            double mylocalVariable = (double)localNameVariables.Get("Nothing");
            Debug.Log("Nothing is = " + mylocalVariable);

            localNameVariables.Set("Nothing", 20f);
            mylocalVariable = (double)localNameVariables.Get("Nothing");
            Debug.Log("Nothing is changed to 20? " + mylocalVariable);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        GlobalNameVariablesManager.Instance.Register(globalNameVariables, Crazy_Callback);
        localNameVariables.Register(Nothing_Callback);
    }

    

    private void OnDisable()
    {
        GlobalNameVariablesManager.Instance.Unregister(globalNameVariables, Crazy_Callback);
        localNameVariables.Unregister(Nothing_Callback);
    }

    private void Crazy_Callback(string obj)
    {
        Debug.Log("Crazy changed, callback");
    }

    private void Nothing_Callback(string obj)
    {
        Debug.Log("Nothing changed, callback");
    }
}
