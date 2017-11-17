using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LoveKicher.Llsif.Live
{
    /// <summary>
    /// 表示SIF的谱面对象
    /// </summary>
    public class map
    {
        /// <summary>note时刻</summary>
        public double timing_sec { get; set; }

        /// <summary>note属性，通常情况一个谱面应该为同一个值</summary>
        public Attribute notes_attribute { get; set; }

        /// <summary>滑键分组</summary>
        public int notes_level { get; set; }

        /// <summary>note效果类型</summary>
        public NoteEffect effect { get; set; }

        /// <summary>note效果的值</summary>
        public double effect_value { get; set; }

        /// <summary>位置，从右往左1-9</summary>
        public int position { get; set; }

    }

    /// <summary>
    /// 提供<see cref="map.effect"/>的值 
    /// </summary>
    public enum NoteEffect
    {
        /// <summary>未知，虽然lua代码写的是random</summary>
        Unknown = 0,
        /// <summary>单点，默认类型</summary>
        Normal = 1,
        /// <summary>道具单点</summary>
        Token = 2,
        /// <summary>长条</summary>
        Hold = 3,
        /// <summary>星星单点（通常的类型）</summary>
        Star = 4,//star1
        /// <summary>星星单点</summary>
        Star3 = 5,
        /// <summary>星星单点</summary>
        Star5 = 6,
        /// <summary>星星单点</summary>
        Star9 = 7,
        /// <summary>单滑键</summary>
        Swing = 11,
        /// <summary>滑键道具</summary>
        SwingToken = 12,
        /// <summary>滑键开始的长条</summary>
        SwingHold = 13,
    }
}
