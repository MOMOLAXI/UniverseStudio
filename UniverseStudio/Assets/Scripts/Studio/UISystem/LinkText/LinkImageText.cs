using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 文本控件，支持超链接、图片
/// </summary>
[AddComponentMenu("UI/LinkImageText", 10)]
public partial class LinkImageText : Text, IPointerClickHandler
{
    private static StringBuilder stringBuilder = new();
    private string outputText; //解析完最终的文本

    private static readonly HrefMatch HrefMatch = new HrefMatch(); // 超链接匹配
    private List<HrefInfo> hrefInfos = new List<HrefInfo>();       // 超链接信息列表

    public event Action OnClick;             //点击事件
    public event Action<string> OnHrefClick; //超链接点击事件

    public override float preferredHeight
    {
        get
        {
            TextGenerationSettings settings = GetGenerationSettings(new(GetPixelAdjustedRect().size.x, 0.0f));
            return cachedTextGeneratorForLayout.GetPreferredHeight(outputText, settings) / pixelsPerUnit;
        }
    }

    public override float preferredWidth
    {
        get
        {
            TextGenerationSettings settings = GetGenerationSettings(Vector2.zero);
            return cachedTextGeneratorForLayout.GetPreferredWidth(outputText, settings) / pixelsPerUnit;
        }
    }

    public override void SetVerticesDirty()
    {
        base.SetVerticesDirty();
        outputText = GetPopulateText(text);
    }

    /// <summary>
    /// 获取实际绘制的文本
    /// </summary>
    public string GetPopulateText(string ret)
    {
        string result = ret;
        result = GetPopulateUnderlineText(result);
        result = GetPopulateHrefText(result);
        result = GetUnderlineString(result, true); // 删除字符串内的下划线信息
        result = GetHrefString(result, true);      // 删除字符串内的超链接信息
        return result;
    }

    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        // 更新下划线相关网格信息
        UpdateUnderLineInfo(toFill);
        // 处理超链接
        OnPopulateHref(toFill);
        // 处理下划线
        OnPopulateUnderline(toFill);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        bool hrefProcessed = PreciseHrefClick(eventData);
        // 如果处理了链接，就不产生点击事件了
        if (!hrefProcessed)
        {
            OnClick.Invoke();
        }
    }

    /// <summary>
    /// 从某位置开始对比字符串
    /// </summary>
    static bool CompareStr(string str, string targetStr, int startIndex)
    {
        if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(targetStr) || str.Length < startIndex + targetStr.Length)
            return false;

        for (int i = 0; i < targetStr.Length; i++)
        {
            char c = str[startIndex + i];
            char t = targetStr[i];

            if (c != t)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 获取下划线字符串(去除<u></u>之外的所有标签)
    /// </summary>
    /// <param name="inputStr"> 原始文本 </param>
    /// <param name="isRemoveSelf"> 是否反向操作，删除自己不删除其他 </param>
    /// <returns> 计算后的字符串 </returns>
    public static string GetUnderlineString(string inputStr, bool isRemoveSelf = false)
    {
        if (string.IsNullOrEmpty(inputStr))
        {
            return string.Empty;
        }

        return GetText(inputStr, (_, index) =>
        {
            bool result;

            if (CompareStr(inputStr, UnderlineMatch.StartTag, index))
            {
                result = true;
            }
            else if (CompareStr(inputStr, "</u>", index))
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return isRemoveSelf ? !result : result;
        });
    }

    /// <summary>
    /// 获取超链接实际字符串(去除<a href=xx></a>之外的所有标签)
    /// </summary>
    /// <param name="text"> 原始文本 </param>
    /// <param name="isRemoveSelf"> 是否反向操作，删除自己不删除其他 </param>
    /// <returns> 计算后的字符串 </returns>
    static string GetHrefString(string text, bool isRemoveSelf = false)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        return GetText(text, (_, index) =>
        {
            bool result;

            if (CompareStr(text, "<a href=", index))
            {
                result = true;
            }
            else if (CompareStr(text, "</a", index))
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return isRemoveSelf ? !result : result;
        });
    }

    /// <summary>
    /// 删除所有富文本信息
    /// </summary>
    /// <param name="inputString"> 原始文本 </param>
    /// <returns> 计算后的字符串 </returns>
    public static string RemoveAllRichText(string inputString)
    {
        if (string.IsNullOrEmpty(inputString))
        {
            return string.Empty;
        }

        return GetText(inputString, (_, index) =>
        {
            if (CompareStr(inputString, "<", index))
                return false;
            return true;
        });
    }

    static string GetText(string inputString, Func<string, int, bool> checkIsAddLink)
    {
        if (string.IsNullOrEmpty(inputString))
        {
            return string.Empty;
        }

        int strLength = inputString.Length;
        stringBuilder.Length = 0;
        bool isAddStr = true;   // 是否添加字符串
        bool isSkipEnd = false; // 是否是跳过添加结束标记

        for (int i = 0; i < strLength; i++)
        {
            char c = inputString[i];

            if (c == '<')
            {
                isAddStr = checkIsAddLink.Invoke(inputString, i);
            }
            else if (c == '>')
            {
                if (!isAddStr)
                {
                    isAddStr = true;  // 跳过结束后开始添加字符串
                    isSkipEnd = true; // 标记这次是跳过添加结束标记
                }
            }

            if (isAddStr && !isSkipEnd && i < inputString.Length)
            {
                stringBuilder.Append(c);
            }

            isSkipEnd = false; // 跳过添加字符串标记重置
        }

        return stringBuilder.ToString();
    }
}