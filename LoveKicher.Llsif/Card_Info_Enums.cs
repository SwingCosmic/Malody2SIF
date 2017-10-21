using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable 1591
namespace LoveKicher.Llsif
{
    /// <summary>
    /// 指定卡片稀有度
    /// </summary>
    public enum Rarity
    {
        Unknown = 0,
        N = 1,
        R = 2,
        SR = 3,
        SSR = 5,
        UR = 4,
        UUR = 6,//玩
        LR = 7,
        Skill = 32//技能卡专属
        //more.....
    }


    /// <summary>
    /// 指定卡片属性
    /// </summary>
    public enum Attribute
    {
        Unknown = 0,
        Smile = 1,//SIF的数据库值，不能修改
        Pure = 2,//..
        Cool = 3,//..
        Happy = 4,
        All = 5,//..
        Moe = 6,
        Kowai = 7,
        Gay = 8,
        Afire = 66,//梨梨

        
        //more....
    }
}
