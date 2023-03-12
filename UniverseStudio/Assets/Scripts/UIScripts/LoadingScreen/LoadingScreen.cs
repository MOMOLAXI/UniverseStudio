using System;
using UnityEngine;
using UnityEngine.UI;
using Universe;

namespace UniverseStudio
{
    public class LoadingScreen : UIWidget
    {
        [SerializeField]
        Text m_TipText;

        [SerializeField]
        Slider m_Progress;

        public override void OnCreate()
        {
            GameMessage.Subscribe(EMessage.OnLaunchSequenceUpdate, OnSequenceUpdate);
        }
        void OnSequenceUpdate(Variables args)
        {
            float progress = args.GetFloat(0);
            string text = args.GetString(1);
            m_TipText.text = text;
            m_Progress.value = progress;
        }
    }
}