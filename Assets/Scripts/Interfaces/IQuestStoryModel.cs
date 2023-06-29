using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public interface IQuestStoryModel : IDisposable
    {
        bool IsDone { get; }
    }
}
