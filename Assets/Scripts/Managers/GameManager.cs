using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Range(1, 255)]
    public int level = 1;
    
    
    [Header("Ghost")] 
    public string mode;
    public List<Ghost> ghosts;
    
    [Header("Ghost Rule")] 
    public GameLevelRule gameLevelRule;
    private List<Dictionary<string, int>> _rule; 
    private int _ruleIndex = 0; 
    
    void Start()
    {
        setLevel(level);
    }

    void Update()
    {
        
    }

    public void setLevel(int level)
    {
        _rule = gameLevelRule.getLevelRule(level);
        setGhostMode(_rule[_ruleIndex].Keys.First());
    }
    
    
    public void setGhostMode(string mode)
    {
        foreach (var ghost in ghosts)
        {
            ghost.mode = mode;
        }

        if (_rule.Count >= _ruleIndex)
        {
            _ruleIndex = _rule.Count > _ruleIndex + 1 ? _ruleIndex + 1 :  _ruleIndex;
            Debug.Log(_rule[_ruleIndex].Values);
            Debug.Log(_rule[_ruleIndex].Values.First());

            var seconds = _rule[_ruleIndex].Values.First();
            mode = _rule[_ruleIndex].Keys.First();
            ThreadManager.instance.GameModeWaitFor(seconds, this, mode);
        }
    }

}
