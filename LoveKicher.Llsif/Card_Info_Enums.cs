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
 
        //more.....
    }


    /// <summary>
    /// 指定属性
    /// </summary>
    public enum Attribute
    {
        Unknown = 0,
        Smile = 1,//SIF的数据库值，不能修改
        Pure = 2,
        Cool = 3,

        All = 5,
      
        //more....
    }
}
