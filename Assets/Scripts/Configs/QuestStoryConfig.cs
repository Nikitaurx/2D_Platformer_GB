using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlatformerMVC
{
    public enum StoryType
    {
        Common,
        Resettable
    }

    [CreateAssetMenu(fileName = "QuestStoryCfg", menuName = "Configs / QuestSystem / QuestCfg", order = 1)]
    public class QuestStoryConfig : ScriptableObject
    {
        public QuestConfig[] questConfig;
        public QuestType type;

    }
}