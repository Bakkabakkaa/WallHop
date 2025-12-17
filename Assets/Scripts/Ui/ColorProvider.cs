using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorProvider", menuName = "ColorProvider")]
public class ColorProvider : ScriptableObject
{
    public IReadOnlyList<Color> Colors => _colors;
    [field:SerializeField] public Color CurrentColor { get; set; }
    [SerializeField] private List<Color> _colors;
}