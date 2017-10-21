using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable 1591
namespace LoveKicher.Llsif.Live
{
    /// <summary>
    /// 枚举可用的歌曲难度
    /// </summary>
    public enum Difficulty
    {

        Unknown = 0,
        Easy = 1,
        Normal = 2,
        Hard = 3,
        Expert = 4,
        Technical = 5,
        Master = 6,//在数据库中AC也是6，但是读取时改成了7
        AC = 7
    }




    /// <summary>
    /// 表示数据库中live_setting_m表的实体类
    /// </summary>
    public class LiveInfo
    {
        /// <summary>谱面ID</summary>
        public int live_setting_id { get; set; }
        /// <summary>歌曲ID</summary>
        public int live_track_id { get; set; }
        /// <summary>谱面难度</summary>
        public Difficulty difficulty { get; set; }
        /// <summary>舞台星数</summary>
        public int stage_level { get; set; }
        /// <summary>谱面属性</summary>
        public Attribute attribute_icon_id { get; set; }
        /// <summary>谱面封面文件名</summary>
        public string live_icon_asset { get; set; }
        /// <summary>3D live的id？？？永远为null</summary>
        public int? asset_movie_id { get; set; } = null;
        /// <summary>舞台背景ID？？？</summary>
        public int asset_background_id { get; set; }
        /// <summary>谱面下落速度</summary>
        public double notes_speed { get; set; }
        /// <summary>谱面JSON文件名</summary>
        public string notes_setting_asset { get; set; }
        /// <summary>Score C值</summary>
        public int c_rank_score { get; set; }
        /// <summary>Score B值</summary>
        public int b_rank_score { get; set; }
        /// <summary>Score A值</summary>
        public int a_rank_score { get; set; }
        /// <summary>Score S值</summary>
        public int s_rank_score { get; set; }
        /// <summary>Combo C值</summary>
        public int c_rank_combo { get; set; }
        /// <summary>Combo B值</summary>
        public int b_rank_combo { get; set; }
        /// <summary>Combo A值</summary>
        public int a_rank_combo { get; set; }
        /// <summary>Combo S值</summary>
        public int s_rank_combo { get; set; }




    }
}
