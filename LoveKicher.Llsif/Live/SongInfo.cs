using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveKicher.Llsif.Live
{
    /// <summary>
    /// 指定歌曲类别
    /// </summary>
    public enum SongCategory
    {
        /// <summary>μ's（包括A-Rise）</summary>
        Muse = 1,
        /// <summary>Aqours</summary>
        Aqours = 2
    }


    public class SongInfo
    {
        /// <summary>歌曲ID，主键</summary>
        public int live_track_id { get; set; }

        /// <summary>歌曲名称</summary>
        public string name { get; set; }

        /// <summary>歌曲名的假名注音，但写法是错误的</summary>
        public string name_kana { get; set; }

        /// <summary>title文件名</summary>
        public string title_asset { get; set; }

        /// <summary>歌曲文件名</summary>
        public string sound_asset { get; set; }

        /// <summary>歌曲类别</summary>
        public SongCategory member_category { get; set; }

        /// <summary>歌曲分类ID</summary>
        public int member_tag_id { get; set; }

        public string release_tag { get; set; }

        public string _encryption_release_id { get; set; }









    }
}
