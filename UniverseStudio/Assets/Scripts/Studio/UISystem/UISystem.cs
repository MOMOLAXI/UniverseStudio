using UnityEngine;

namespace Universe
{
    public class UISystem : GameSystem
    {
        GameObject m_Root;
        Camera m_Camera;

        public Camera UICamera => m_Camera;

        public override void Init()
        {
            Engine.GetGlobalGameObject(nameof(UISystem), out m_Root);
            Engine.GetGlobalComponent("UICamera", out m_Camera);
        }
    }
}