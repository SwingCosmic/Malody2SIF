using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CS1591
namespace LoveKicher.Llsif.Live.Extras
{
    /// <summary>
    /// 表示Malody所使用的谱面对象
    /// </summary>
    public class Malody_map
    {
        public metaInfo meta { get; set; }
        public timeInfo[] time { get; set; }
        public List<object> note { get; set; }
        //public dynamic extra { get; set; }
    }


    /// <summary>
    /// 表示<see cref="Malody_map"/> 对象中的meta元素
    /// </summary>
    public class metaInfo
    {
        public string creator { get; set; }
        public string background { get; set; }
        public string version { get; set; }
        public int id { get; set; }
        public SongMode mode { get; set; }
        public int time { get; set; }
        public songInfo song { get; set; }
        public Dictionary<string, object> mode_ext { get; set; }
            = new Dictionary<string, object> { ["column"] = 9 };


    }

    /// <summary>
    /// 表示谱面类型参数
    /// </summary>
    public enum SongMode
    {
        //TODO:除了Key都是猜的
        Key = 0,
        Step = 1,
        DJ = 2,
        Catch = 3,
        Pad = 4,
        Taiko = 5
    }

    /// <summary>
    /// 表示<see cref="metaInfo"/> 对象中的song元素
    /// </summary>
    public class songInfo
    {
        public string title { get; set; }
        public string artist { get; set; }
        public int id { get; set; }
    }

    public class timeInfo
    {
        public int[] beat { get; set; }
        public double bpm { get; set; }
    }


    #region notes



    /// <summary>
    /// 表示一个<see cref="Malody_map.note"/>集合的元素
    /// </summary>
    public  class noteBase
    {
        public int[] beat { get; set; }

    }

    /// <summary>
    /// 表示一个打击图示
    /// </summary>
    public class note:noteBase
    {
        public int column { get; set; }
    }

    /// <summary>
    /// 表示一个长条
    /// </summary>
    public class bar:note
    {
        public int[] endbeat { get; set; } = null;
    }

    /// <summary>
    /// 表示专用于歌曲设置的note
    /// </summary>
    public class setting_note: noteBase
    {
        public string sound { get; set; } = null;
        public int vol { get; set; } = 100;
        public int offset { get; set; } = 0;
        public int type { get; set; } = 0;
    }

    #endregion



}
