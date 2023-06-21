using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu_GroupHelper : CustomMonoBehaviorWrapper, ResetOptionInterface
{
    public List<GameObject> objectsWithRestInterfaces;
    public List<ResetOptionInterface> items;

    private void Awake()
    {

        items = new List<ResetOptionInterface>();

        foreach (GameObject gameObject in objectsWithRestInterfaces)
        {
            try
            {
                items.Add(gameObject.GetComponent<ResetOptionInterface>());
            }
            catch (System.Exception)
            {
                
            }
        }
    }

    public void Reset_Option()
    {
        foreach (ResetOptionInterface item in items)
        {
            item.Reset_Option();
        }
    }
}
