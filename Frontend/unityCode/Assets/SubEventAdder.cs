using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubEventAdder : MonoBehaviour
{
    public AddSection newEvent =new AddSection();
    public void AddNewSection()
    {
        GetComponentInParent<AddNewEvent>().addNewSection();
    }
    public void SetEName(string name)
    {
        newEvent.name=name;
    }
    public void SetEPrice(string name)
    {
        newEvent.price = name;
    }
    public void SetEdet(string name)
    {
        newEvent.description = name;
    }
}
