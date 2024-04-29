using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] string _name;
    public string Name => _name;
}