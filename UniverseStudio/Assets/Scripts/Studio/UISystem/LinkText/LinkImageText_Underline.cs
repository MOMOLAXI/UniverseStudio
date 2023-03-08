using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 富文本组件，超链接相关
/// </summary>
public partial class LinkImageText
{
    // 下划线匹配
    private static readonly UnderlineMatch UnderlineMatch = new();
    // 下划线信息列表
    private List<UnderlineInfo> underlineInfos = new();
    // 下划线顶点数据
    private UIVertex[] m_UnderLineVertexs = new UIVertex[4];
    // 单个下划线大小
    private Vector2 underlineSize;
    // 下划线之间的间隔
    private float underlineOffset;

    /// <summary>
    /// 更新下划线数据
    /// </summary>
    string GetPopulateUnderlineText(string input)
    {
        string result = GetUnderlineString(input);
        underlineInfos.Clear();

        if (!UnderlineMatch.StartMatch(input))
        {
            return result;
        }

        int length = 0;
        int index = 0;
        do
        {
            int begIndex = UnderlineMatch.GetBegIndex();
            if (begIndex > index)
            {
                length += begIndex - index;
            }

            string matchValue = UnderlineMatch.GetValue();
            UnderlineInfo underlineInfo = new()
            {
                startIndex = length * 4,
                endIndex = (length + matchValue.Length - 1) * 4 + 3,
            };

            underlineInfos.Add(underlineInfo);
            length += matchValue.Length;

            index = UnderlineMatch.GetEndIndex() + 1;
        } while (UnderlineMatch.MatchNext());
        return result;
    }

    /// <summary>
    /// 更新下划线大小和偏移信息
    /// </summary>
    void UpdateUnderLineInfo(VertexHelper toFill)
    {
        string orignText = m_Text;

        if (underlineInfos.Count > 0)
        {
            m_Text = "__";
            base.OnPopulateMesh(toFill);
            for (int i = 0; i < m_UnderLineVertexs.Length; ++i)
            {
                toFill.PopulateUIVertex(ref m_UnderLineVertexs[i], i);
            }

            // 单个下划线大小
            underlineSize.x = m_UnderLineVertexs[1].position.x - m_UnderLineVertexs[0].position.x;
            underlineSize.y = m_UnderLineVertexs[1].position.y - m_UnderLineVertexs[2].position.y;

            // 下划线之间的间隔
            UIVertex vertex = new();
            toFill.PopulateUIVertex(ref vertex, 4);
            underlineOffset = vertex.position.x - m_UnderLineVertexs[0].position.x;
        }

        m_Text = outputText;
        base.OnPopulateMesh(toFill);
        m_Text = orignText;
    }

    /// <summary>
    /// 更新下滑线渲染信息
    /// </summary>
    void OnPopulateUnderline(VertexHelper toFill)
    {
        for (int i = 0; i < underlineInfos.Count; ++i)
        {
            UnderlineInfo underlineInfo = underlineInfos[i];
            underlineInfo.boxes.Clear();

            if (underlineInfo.startIndex >= toFill.currentVertCount)
            {
                continue;
            }

            // 计算包围盒
            UIVertex lastVert = new();
            UIVertex vert = new();
            toFill.PopulateUIVertex(ref vert, underlineInfo.startIndex);
            Vector3 pos = vert.position;
            Bounds bounds = new(pos, Vector3.zero);

            bool colorInited = false;

            for (int j = underlineInfo.startIndex, m = underlineInfo.endIndex; j < m; j++)
            {
                if (j >= toFill.currentVertCount)
                {
                    break;
                }

                toFill.PopulateUIVertex(ref vert, j);
                pos = vert.position;

                // 获取要显示字符的颜色
                if (!colorInited)
                {
                    if (((j % 4) == 1))
                    {
                        if (lastVert.position.x + 0.1f < vert.position.x)
                        {
                            colorInited = true;
                            underlineInfo.color = vert.color;
                        }
                    }

                    lastVert = vert;
                }

                if (pos.x + 0.1f < bounds.min.x)
                {
                    // 换行重新添加包围框
                    underlineInfo.boxes.Add(new(bounds.min, bounds.size));
                    bounds = new(pos, Vector3.zero);
                }
                else
                {
                    // 扩展包围框
                    bounds.Encapsulate(pos);
                }
            }

            underlineInfo.boxes.Add(new(bounds.min, bounds.size));

            for (int j = 0; j < underlineInfo.boxes.Count; ++j)
            {
                Rect rect = underlineInfo.boxes[j];
                int count = (int)(rect.size.x / underlineOffset + 0.5f);

                for (int k = 0; k < count; ++k)
                {
                    Vector2 position = rect.position + new Vector2(underlineOffset, 0) * k;

                    m_UnderLineVertexs[0].position = position;
                    m_UnderLineVertexs[1].position = position + new Vector2(underlineSize.x, 0);
                    m_UnderLineVertexs[2].position = position + new Vector2(underlineSize.x, -underlineSize.y);
                    m_UnderLineVertexs[3].position = position + new Vector2(0, -underlineSize.y);

                    m_UnderLineVertexs[0].color = underlineInfo.color;
                    m_UnderLineVertexs[1].color = underlineInfo.color;
                    m_UnderLineVertexs[2].color = underlineInfo.color;
                    m_UnderLineVertexs[3].color = underlineInfo.color;

                    toFill.AddUIVertexQuad(m_UnderLineVertexs);
                }
            }
        }
    }

    class UnderlineInfo
    {
        public int startIndex;
        public int endIndex;
        public Color color;
        public List<Rect> boxes = new();
    }
}