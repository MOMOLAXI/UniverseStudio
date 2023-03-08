using System;

/// <summary>
/// @"<a href=([^>\n\s]+)>(.*?)(</a>)"
/// </summary>
public class HrefMatch
{
    private const string RIGHT_MARK = "</a>";

    private string inputString = string.Empty;
    private int startIndex = -1;

    private int begIndex = -1;
    private int hrefBegIndex = -1;
    private int hrefEndIndex = -1;
    private int valueBegIndex = -1;
    private int valueEndIndex = -1;
    private int endIndex = -1;
    private string href = string.Empty;
    private string value = string.Empty;

    /// <summary>
    /// 开始匹配
    /// </summary>
    public bool StartMatch(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            FinishMatch();
            return false;
        }

        inputString = input;
        startIndex = 0;

        return SeekMatch();
    }

    /// <summary>
    /// 匹配下一个
    /// </summary>
    public bool MatchNext()
    {
        if (startIndex < 0)
        {
            return false;
        }

        return SeekMatch();
    }

    /// <summary>
    /// 获取链接
    /// </summary>
    public string GetHref()
    {
        return href;
    }

    /// <summary>
    /// 获取匹配值
    /// </summary>
    public string GetValue()
    {
        return value;
    }

    /// <summary>
    /// 获取值左侧长度
    /// </summary>
    /// <returns></returns>
    public int GetLeftLength()
    {
        return valueBegIndex - begIndex;
    }

    /// <summary>
    /// 获取值右侧起始索引
    /// </summary>
    /// <returns></returns>
    public int GetRightBegIndex()
    {
        return valueEndIndex + 1;
    }

    /// <summary>
    /// 获取值右侧长度
    /// </summary>
    /// <returns></returns>
    public int GetRightLength()
    {
        return RIGHT_MARK.Length;
    }

    /// <summary>
    /// 获取匹配开始索引
    /// </summary>
    public int GetBegIndex()
    {
        return begIndex;
    }

    /// <summary>
    /// 获取匹配结束索引
    /// </summary>
    public int GetEndIndex()
    {
        return endIndex;
    }

    /// <summary>
    /// 匹配
    /// </summary>
    bool SeekMatch()
    {
        bool succ = Seek();
        if (!succ)
        {
            FinishMatch();
        }

        return succ;
    }

    /// <summary>
    /// 匹配
    /// </summary>
    bool Seek()
    {
        if (startIndex >= inputString.Length)
        {
            return false;
        }

        // 搜索开始标记
        begIndex = inputString.IndexOf("<a href=", startIndex, StringComparison.Ordinal);
        if (begIndex < 0)
        {
            return false;
        }

        hrefBegIndex = begIndex + 8;
        startIndex = hrefBegIndex;

        if (startIndex >= inputString.Length)
        {
            return false;
        }

        // 搜索链接结束标记
        hrefEndIndex = SeekHrefEnd(startIndex);
        if (hrefEndIndex < 0)
        {
            return false;
        }

        valueBegIndex = hrefEndIndex + 1;
        startIndex = valueBegIndex;

        if (startIndex >= inputString.Length)
        {
            return false;
        }

        // 搜索结束标记
        int index = inputString.IndexOf(RIGHT_MARK, startIndex, StringComparison.Ordinal);
        if (index < 0)
        {
            return false;
        }

        valueEndIndex = index - 1;
        endIndex = index + RIGHT_MARK.Length - 1;
        startIndex = endIndex + 1;

        // 截取链接
        SubHref();
        // 截取值
        SubValue();

        return true;
    }

    /// <summary>
    /// 截取链接
    /// </summary>
    void SubHref()
    {
        if (hrefEndIndex >= hrefBegIndex)
        {
            // 不用包含链接结束标记">"，因此hrefEndIndex - hrefBegIndex不用+1
            href = inputString.Substring(hrefBegIndex, hrefEndIndex - hrefBegIndex);
        }
        else
        {
            href = string.Empty;
        }
    }

    /// <summary>
    /// 截取值
    /// </summary>
    void SubValue()
    {
        value = valueBegIndex <= valueEndIndex ? inputString.Substring(valueBegIndex, valueEndIndex - valueBegIndex + 1) : string.Empty;
    }

    /// <summary>
    /// 搜索链接结束标记
    /// </summary>
    int SeekHrefEnd(int linkStartIndex)
    {
        for (int index = linkStartIndex; index < inputString.Length; index++)
        {
            char c = inputString[index];
            if (c == '>')
            {
                return index;
            }

            if (char.IsWhiteSpace(c))
            {
                return -1;
            }
        }

        return -1;
    }

    /// <summary>
    /// 结束匹配
    /// </summary>
    void FinishMatch()
    {
        inputString = string.Empty;
        startIndex = -1;

        begIndex = -1;
        hrefBegIndex = -1;
        hrefEndIndex = -1;
        valueBegIndex = -1;
        valueEndIndex = -1;
        endIndex = -1;
        href = string.Empty;
        value = string.Empty;
    }
}