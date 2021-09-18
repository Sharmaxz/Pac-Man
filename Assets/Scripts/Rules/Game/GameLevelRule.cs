using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public  class GameLevelRule : MonoBehaviour
{

    private static List<List<Dictionary<string, int>>> rules = new List<List<Dictionary<string, int>>>
    {
        //Level 1
        new List<Dictionary<string, int>>
        {
            // new Dictionary<string, int> { {"scatter", 12 }},
            // new Dictionary<string, int> { {"chase", 25 }},
            // new Dictionary<string, int> { {"scatter", 12 }},
            // new Dictionary<string, int> { {"chase", 25 }},
            // new Dictionary<string, int> { {"scatter", 12 }},
            // new Dictionary<string, int> { {"chase", 25 }},
            // new Dictionary<string, int> { {"scatter", 12 }},
            
            new Dictionary<string, int> {{"scatter", 0 }},
            new Dictionary<string, int> {{"chase", 12 }},
            new Dictionary<string, int> {{"scatter", 25 }},
            new Dictionary<string, int> {{"chase", 12 }},
            new Dictionary<string, int> {{"scatter", 25 }},
            new Dictionary<string, int> {{"chase", 12 }},
            new Dictionary<string, int> {{"scatter", 25 }},
        },
        //Level 2
        new List<Dictionary<string, int>>
        {
            new Dictionary<string, int> {{"scatter", 12 }},
            new Dictionary<string, int> {{"chase", 25 }},
            new Dictionary<string, int> {{"scatter", 12 }},
            new Dictionary<string, int> {{"chase", 25 }},
            new Dictionary<string, int> {{"scatter", 10 }},
            new Dictionary<string, int> {{"chase", 1038 }},
            new Dictionary<string, int> {{"scatter", 10 }},
        },
        //Level 3
        new List<Dictionary<string, int>>
        {
            new Dictionary<string, int> {{"scatter", 12 }},
            new Dictionary<string, int> {{"chase", 25 }},
            new Dictionary<string, int> {{"scatter", 12 }},
            new Dictionary<string, int> {{"chase", 25 }},
            new Dictionary<string, int> {{"scatter", 10 }},
            new Dictionary<string, int> {{"chase", 1038 }},
            new Dictionary<string, int> {{"scatter", 10 }},
        },
        //Level 4
        new List<Dictionary<string, int>>
        {
            new Dictionary<string, int> {{"scatter", 12 }},
            new Dictionary<string, int> {{"chase", 25 }},
            new Dictionary<string, int> {{"scatter", 12 }},
            new Dictionary<string, int> {{"chase", 25 }},
            new Dictionary<string, int> {{"scatter", 10 }},
            new Dictionary<string, int> {{"chase", 1038 }},
            new Dictionary<string, int> {{"scatter", 10 }},
        },
        //Level 5
        new List<Dictionary<string, int>>
        {
            new Dictionary<string, int> {{"scatter", 10 }},
            new Dictionary<string, int> {{"chase", 25 }},
            new Dictionary<string, int> {{"scatter", 10 }},
            new Dictionary<string, int> {{"chase", 25 }},
            new Dictionary<string, int> {{"scatter", 10 }},
            new Dictionary<string, int> {{"chase", 1042 }},
            new Dictionary<string, int> {{"scatter", 10 }},
        },
    };

    public List<Dictionary<string, int>> getLevelRule(int level)
    {
        level = level - 1;
        level =  level > rules.Count ? rules.Count : level;
        
        return rules[level];
    }
}
