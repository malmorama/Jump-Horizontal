using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelGeneratorVariables", order = 1)]
public class LevelGeneratorVariables : ScriptableObject
{
    [System.Serializable]
    public class LevelContent
    {
        public string methodName;
        public bool methodActive;
        public int randomNumberFrom;
        public int randomNumberTo;
        public string objRespawn;
        public string objOnTop;
        public int objOnTopHeight;
    }

    public int Level;
    public int levelIDHeader;
    public int randomNumberGeneratedFrom;
    public int randomNumberGeneratedTo;
    public List<LevelContent> levelContent;
}
