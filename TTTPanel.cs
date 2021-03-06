using ColossalFramework.UI;
using Klyte.Commons.Extensors;
using Klyte.Commons.Interfaces;
using Klyte.Commons.Utils;
using UnityEngine;

namespace Klyte.TouchThis
{
    public class TTTPanel : BasicKPanel<TouchThisToolMod, TTTController, TTTPanel>
    {
        public override float PanelWidth => 400;

        public override float PanelHeight => 230;

        private InfoManager.InfoMode m_currentMode = InfoManager.InfoMode.None;
        private InfoManager.SubInfoMode m_currentSubMode = InfoManager.SubInfoMode.None;

        protected override void AwakeActions()
        {
            MainPanel.eventVisibilityChanged += (x, y) => OnShow(y);

            KlyteMonoUtils.CreateUIElement(out UIPanel layoutPanel, MainPanel.transform, "LayoutPanel", new Vector4(0, 40, PanelWidth, PanelHeight - 40));
            layoutPanel.padding = new RectOffset(8, 8, 10, 10);
            layoutPanel.autoLayout = true;
            layoutPanel.autoLayoutDirection = LayoutDirection.Vertical;
            layoutPanel.autoLayoutPadding = new RectOffset(0, 0, 10, 10);
            var uiHelper = new UIHelperExtension(layoutPanel);

            uiHelper.AddCheckboxLocale("K45_TTT_SHOWUNDERGROUNDVIEW", false, OnUndergroundChange);
        }

        private void OnUndergroundChange(bool isChecked)
        {
            m_currentMode = isChecked ? InfoManager.InfoMode.Underground : InfoManager.InfoMode.None;
            m_currentSubMode = isChecked ? InfoManager.SubInfoMode.UndergroundTunnels : InfoManager.SubInfoMode.None;
            OnShow(MainPanel.isVisible);
        }
        private void OnShow(bool value) => TouchThisToolMod.Controller?.ToggleTool(value, m_currentMode, m_currentSubMode);
    }
}