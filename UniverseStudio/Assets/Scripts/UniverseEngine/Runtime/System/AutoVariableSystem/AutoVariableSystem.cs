using System.Collections.Generic;

namespace Universe
{
    internal class AutoVariableSystem : EngineSystem
    {
        static readonly List<Variables> s_UsingList = new(100);
        static readonly List<Variables> s_FreeList = new(100);

        /// <summary>
        /// 分配一个自动释放的VarList，会在本帧末尾释放，不能持有
        /// </summary>
        /// <returns></returns>
        public static Variables AllocNonHold()
        {
            if (s_FreeList.Count == 0)
            {
                Variables variables = new();
                s_UsingList.Add(variables);

                return variables;
            }
            else
            {
                int pos = s_FreeList.Count - 1;

                Variables variables = s_FreeList[pos];
                s_FreeList.RemoveAt(pos);
                s_UsingList.Add(variables);

                return variables;
            }
        }
        
        public override void Update(float dt)
        {
            if (s_UsingList.Count == 0)
            {
                return;
            }

            for (int i = 0; i < s_UsingList.Count; ++i)
            {
                Variables variables = s_UsingList[i];
                if (variables.Count > 20)
                {
                    // 参数过多的VarList不回收
                    continue;
                }

                if (s_FreeList.Count >= 20)
                {
                    // 保持缓存的最大数量
                    break;
                }

                variables.Clear();
                s_FreeList.Add(variables);
            }

            s_UsingList.Clear();
        }
    }
}