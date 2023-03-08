using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 富文本组件，超链接相关
/// </summary>
public partial class LinkImageText
{
   

   

    /// <summary>
    /// 建立字符串中超链接数据信息
    /// </summary>
    string GetPopulateHrefText(string input)
    {
        var result = GetHrefString(input);
        hrefInfos.Clear();

        if (!HrefMatch.StartMatch(input))
        {
            return result;
        }

        int length = 0;
        int index = 0;
        do
        {
            int begIndex = HrefMatch.GetBegIndex();

            if (index < begIndex)
            {
                length += begIndex - index;
            }

            string href = HrefMatch.GetHref();
            string value = HrefMatch.GetValue();
            HrefInfo hrefInfo = new()
            {
                StartIndex = length * 4, // 超链接里的文本起始顶点索引
                EndIndex = (length + value.Length - 1) * 4 + 3,
                Name = href
            };

            hrefInfos.Add(hrefInfo);
            length += value.Length;

            index = HrefMatch.GetEndIndex() + 1;
        } while (HrefMatch.MatchNext());
        return result;
    }

    /// <summary>
    /// 处理超链接点击框区域信息
    /// </summary>
    void OnPopulateHref(VertexHelper toFill)
    {
        // 处理超链接包围框
        for (int i = 0; i < hrefInfos.Count; ++i)
        {
            HrefInfo hrefInfo = hrefInfos[i];
            hrefInfo.Boxes.Clear();

            if (hrefInfo.StartIndex >= toFill.currentVertCount)
            {
                continue;
            }

            // 计算包围框
            UIVertex vert = new UIVertex();
            toFill.PopulateUIVertex(ref vert, hrefInfo.StartIndex);
            Vector3 pos = vert.position;
            Bounds bounds = new Bounds(pos, Vector3.zero);

            for (int j = hrefInfo.StartIndex, m = hrefInfo.EndIndex; j < m; j++)
            {
                if (j >= toFill.currentVertCount)
                {
                    break;
                }

                toFill.PopulateUIVertex(ref vert, j);
                pos = vert.position;
                if (pos.x < bounds.min.x)
                {
                    // 换行重新添加包围框
                    hrefInfo.Boxes.Add(new Rect(bounds.min, bounds.size));
                    bounds = new Bounds(pos, Vector3.zero);
                }
                else
                {
                    // 扩展包围框
                    bounds.Encapsulate(pos);
                }
            }

            hrefInfo.Boxes.Add(new Rect(bounds.min, bounds.size));
        }
    }

    /// <summary>
    /// 准确点击到HrefInfo区域才会执行相关操作
    /// </summary>
    /// <param name="eventData"></param>
    bool PreciseHrefClick(PointerEventData eventData)
    {
        if (OnHrefClick == null)
        {
            return false;
        }

        Vector2 lp;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
                                                                rectTransform, eventData.position, eventData.pressEventCamera, out lp);

        for (int i = 0; i < hrefInfos.Count; ++i)
        {
            HrefInfo hrefInfo = hrefInfos[i];

            List<Rect> boxes = hrefInfo.Boxes;
            for (int j = 0; j < boxes.Count; ++j)
            {
                if (boxes[j].Contains(lp))
                {
                    OnHrefClick.Invoke(hrefInfo.Name);
                    return true;
                }
            }
        }

        return false;
    }

    // 超链接信息类
    class HrefInfo
    {
        public int StartIndex;
        public int EndIndex;
        public string Name;
        public readonly List<Rect> Boxes = new List<Rect>();
    }
}