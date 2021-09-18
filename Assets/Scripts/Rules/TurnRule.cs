using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Dictionary : SerializableDictionary<string, bool> { }

public class TurnRule : Rule
{
    //Allowed Directions
    public Dictionary directions;
    
    public List<bool> Check(List<bool> path)
    {
        const int index = 0;
        foreach (var direction in directions.Where(direction => !direction.Value))
        {
            path[index] = direction.Value;
        }
        return path;
    }
}
