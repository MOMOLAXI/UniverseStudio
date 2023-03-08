using System;

/// <summary>
/// @"<u>(.*?)(</u>)"
/// </summary>
public class UnderlineMatch
{
    string inputString = string.Empty;
    int startIndex = -1;

    int leftBegIndex = -1;
    int rightBegIndex = -1;
    int rightEndIndex = -1;
    int valueBegIndex = -1;
    string value = string.Empty;

    public const string StartTag = "<u>";
    public const string EndTag = "</u>";

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
            return false;
        
        return SeekMatch();
    }

    /// <summary>
    /// 获取匹配值
    /// </summary>
    public string GetValue()
    {
        return value;
    }

    /// <summary>
    /// 获取匹配开始索引
    /// </summary>
    public int GetBegIndex()
    {
        return leftBegIndex;
    }

    /// <summary>
    /// 获取匹配结束索引
    /// </summary>
    public int GetEndIndex()
    {
        return rightEndIndex;
    }

    /// <summary>
    /// 匹配
    /// </summary>
    bool SeekMatch()
    {
        bool succ = Seek();
        if (!succ)
            FinishMatch();
        
        return succ;
    }

    bool Seek()
    {
        if (!SeekBeg())
            return false;

        if (!SeekEnd())
            return false;
        
        SubValue();
        return true;
    }

    /// <summary>
    /// 截取匹配值
    /// </summary>
    void SubValue()
    {
        int charCount = rightBegIndex - valueBegIndex;
        if (rightBegIndex > valueBegIndex)
            value = inputString.Substring(valueBegIndex, charCount);
        else
            value = string.Empty;
    }

    /// <summary>
    /// 匹配开始标记
    /// </summary>
    /// <returns></returns>
    bool SeekBeg()
    {
        if (startIndex >= inputString.Length)
            return false;
        
        leftBegIndex = inputString.IndexOf(StartTag, startIndex, StringComparison.Ordinal);
        if (leftBegIndex < 0)
            return false;
        
        valueBegIndex = leftBegIndex + 3;
        startIndex = valueBegIndex;
        return true;
    }

    /// <summary>
    /// 匹配结束标记
    /// </summary>
    bool SeekEnd()
    {
        if (startIndex >= inputString.Length)
            return false;
        
        rightBegIndex = inputString.IndexOf(EndTag, startIndex, StringComparison.Ordinal);
        if (rightBegIndex < 0)
            return false;
        
        rightEndIndex = rightBegIndex + 3;
        startIndex = rightEndIndex + 1;
        return true;
    }

    /// <summary>
    /// 结束匹配
    /// </summary>
    void FinishMatch()
    {
        inputString = string.Empty;
        startIndex = -1;
        leftBegIndex = -1;
        rightBegIndex = -1;
        rightEndIndex = -1;
        valueBegIndex = -1;
        value = string.Empty;
    }
}