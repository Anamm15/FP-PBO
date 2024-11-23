using UnityEngine;
public class Item : MonoBehaviour
{
    private string _name;
    

    public string Name { 
        get { return _name; }
        set { 
            if (!string.IsNullOrEmpty(value)) _name = value;
         }
    }
}