using System;
using UnityEngine.UIElements;

namespace Unity.AI.Assistant.UI.Editor.Scripts.Components
{
    abstract class AdaptiveListViewEntry : ManagedTemplate
    {
        int m_Index;

        protected AdaptiveListViewEntry(string basePath = null)
            : base(basePath ?? AssistantUIConstants.UIModulePath)
        {
        }

        public event Action<int, VisualElement> SelectionChanged;

        public int Index => m_Index;

        public virtual void SetData(int index, object data, bool isSelected = false)
        {
            m_Index = index;
        }

        protected void NotifySelectionChanged()
        {
            SelectionChanged?.Invoke(m_Index, this);
        }
    }
}
