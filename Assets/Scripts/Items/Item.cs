public class Item : MonoBehaviour
{
    private string _name;
    

    public string Name { get => _name; set => if(!string.IsNullOrEmpty(value)) _name = value; }
}