using UnityEngine;
using TMPro;

namespace Michsky.MUIP
{
    [CreateAssetMenu(fileName = "New UI Manager", menuName = "Modern UI Pack/New UI Manager")]
    public class UIManager : ScriptableObject
    {
        // Settings
        [HideInInspector] public bool enableDynamicUpdate = true;
        [HideInInspector] public bool enableExtendedColorPicker = true;
        [HideInInspector] public bool editorHints = true;

        // Animated Icon
        public Color animatedIconColor = new(255, 255, 255, 255);

        // Context Menu
        public Color contextBackgroundColor = new(255, 255, 255, 255);

        // Button
        public TMP_FontAsset buttonFont;
        public Color buttonNormalColor = new(255, 255, 255, 255);
        public Color buttonAccentColor = new(255, 255, 255, 255);
        [Range(0, 1)] public float buttonDisabledAlpha = 0.4f;

        // Dropdown
        public TMP_FontAsset dropdownFont;
        public TMP_FontAsset dropdownItemFont;
        public Color dropdownBackgroundColor = new(255, 255, 255, 255);
        public Color dropdownContentBackgroundColor = new(255, 255, 255, 255);
        public Color dropdownPrimaryColor = new(255, 255, 255, 255);
        public Color dropdownItemBackgroundColor = new(255, 255, 255, 255);
        public Color dropdownItemPrimaryColor = new(255, 255, 255, 255);

        // Horizontal Selector
        public TMP_FontAsset selectorFont;
        public Color selectorColor = new(255, 255, 255, 255);
        public Color selectorHighlightedColor = new(255, 255, 255, 255);

        // Input Field
        public TMP_FontAsset inputFieldFont;
        public Color inputFieldColor = new(255, 255, 255, 255);

        // Modal Window
        public TMP_FontAsset modalWindowTitleFont;
        public TMP_FontAsset modalWindowContentFont;
        public Color modalWindowTitleColor = new(255, 255, 255, 255);
        public Color modalWindowDescriptionColor = new(255, 255, 255, 255);
        public Color modalWindowIconColor = new(255, 255, 255, 255);
        public Color modalWindowBackgroundColor = new(255, 255, 255, 255);
        public Color modalWindowContentPanelColor = new(255, 255, 255, 255);

        // Notification
        public TMP_FontAsset notificationTitleFont;
        public float notificationTitleFontSize = 22.5f;
        public TMP_FontAsset notificationDescriptionFont;
        public float notificationDescriptionFontSize = 18;
        public NotificationThemeType notificationThemeType;
        public Color notificationBackgroundColor = new(255, 255, 255, 255);
        public Color notificationTitleColor = new(255, 255, 255, 255);
        public Color notificationDescriptionColor = new(255, 255, 255, 255);
        public Color notificationIconColor = new(255, 255, 255, 255);

        // Progress Bar
        public TMP_FontAsset progressBarLabelFont;
        public float progressBarLabelFontSize = 25;
        public Color progressBarColor = new(255, 255, 255, 255);
        public Color progressBarBackgroundColor = new(255, 255, 255, 255);
        public Color progressBarLoopBackgroundColor = new(255, 255, 255, 255);
        public Color progressBarLabelColor = new(255, 255, 255, 255);

        // Scrollbar
        public Color scrollbarColor = new(255, 255, 255, 255);
        public Color scrollbarBackgroundColor = new(255, 255, 255, 255);

        // Slider
        public TMP_FontAsset sliderLabelFont;
        public SliderThemeType sliderThemeType;
        public Color sliderColor = new(255, 255, 255, 255);
        public Color sliderBackgroundColor = new(255, 255, 255, 255);
        public Color sliderLabelColor = new(255, 255, 255, 255);
        public Color sliderPopupLabelColor = new(255, 255, 255, 255);
        public Color sliderHandleColor = new(255, 255, 255, 255);

        // Switch
        public Color switchBorderColor = new(255, 255, 255, 255);
        public Color switchBackgroundColor = new(255, 255, 255, 255);
        public Color switchHandleOnColor = new(255, 255, 255, 255);
        public Color switchHandleOffColor = new(255, 255, 255, 255);

        // Toggle
        public TMP_FontAsset toggleFont;
        public ToggleThemeType toggleThemeType;
        public Color toggleTextColor = new(255, 255, 255, 255);
        public Color toggleBorderColor = new(255, 255, 255, 255);
        public Color toggleBackgroundColor = new(255, 255, 255, 255);
        public Color toggleCheckColor = new(255, 255, 255, 255);

        // Tooltip
        public TMP_FontAsset tooltipFont;
        public float tooltipFontSize = 22;
        public Color tooltipTextColor = new(255, 255, 255, 255);
        public Color tooltipBackgroundColor = new(255, 255, 255, 255);

        public enum ModalWindowThemeType { Basic, Custom }
        public enum NotificationThemeType { Basic, Custom }
        public enum SliderThemeType { Basic, Custom }
        public enum ToggleThemeType { Basic, Custom }
    }
}