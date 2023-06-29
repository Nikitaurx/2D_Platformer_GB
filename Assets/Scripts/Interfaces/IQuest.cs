using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public interface IQuest : IDisposable
    {
        event EventHandler<IQuest> QuestCompleted;

        bool IsComleted { get; }
        void Reset();
    }
}