using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable 1591
namespace LoveKicher.Llsif
{
    /// <summary>
    /// 表示对应SIF 数据库卡片数据的完整信息实体类
    /// </summary>
    public class cardEx:card
    {


        public int normal_card_id { get; set; }

        public int rank_max_card_id { get; set; }

        public int normal_unit_navi_asset_id { get; set; }

        public int rank_max_unit_navi_asset_id { get; set; }

        public int? default_unit_skill_id { get; set; }

        public int? skill_asset_voice_id { get; set; }

        public int? default_leader_skill_id { get; set; }

        /// <summary>觉醒前最大绊</summary>
        public int before_love_max { get; set; }

        /// <summary>觉醒后最大绊</summary>
        public int after_love_max { get; set; }

        /// <summary>觉醒前最大等级</summary>
        public int before_level_max { get; set; }

        /// <summary>觉醒后最大等级</summary>
        public int after_level_max { get; set; }

        /// <summary>初始技能槽个数</summary>
        public int default_removable_skill_capacity { get; set; }

        /// <summary>最大技能槽个数</summary>
        public int max_removable_skill_capacity { get; set; }

        public int/*bool?*/ disable_rank_up { get; set; }

        public int unit_level_up_pattern_id { get; set; }

        /// <summary>最大HP</summary>
        public int hp_max { get; set; }

        /// <summary>觉醒最大<see cref="Attribute.Smile"/>值</summary>
        public int smile_max { get; set; }

        /// <summary>觉醒最大<see cref="Attribute.Pure"/>值</summary>
        public int pure_max { get; set; }

        /// <summary>觉醒最大<see cref="Attribute.Cool"/>值</summary>
        public int cool_max { get; set; }

        /// <summary>觉醒花费</summary>
        public int rank_up_cost { get; set; }

        public int exchange_point_rank_up_cost { get; set; }

        public string/*?*/ release_tag { get; set; }

        public string/*?*/ _encryption_release_id { get; set; }


    }

    /// <summary>
    /// 表示对应SIF 数据库卡片数据的实体类
    /// </summary>
    public class card
    {
        /// <summary>内部ID</summary>
        public int unit_id { get; set; }

        /// <summary>相册编号</summary>
        public int unit_number { get; set; }

        /// <summary>人物编号</summary>
        public int unit_type_id { get; set; }

        /// <summary>卡片名称</summary>
        public string eponym { get; set; }

        /// <summary>人物名称</summary>
        public string name { get; set; }

        /// <summary>未觉醒头像链接</summary>
        /// <remarks>assets/image/units/u_normal_icon_xxxxxxxx.png</remarks>
        public string normal_icon_asset { get; set; }

        /// <summary>觉醒头像链接</summary>
        /// <remarks>assets/image/units/u_rankup_icon_xxxxxxxx.png</remarks>
        public string rank_max_icon_asset { get; set; }

        /// <summary>稀有度</summary>
        public Rarity rarity { get; set; }

        /// <summary>属性</summary>
        public Attribute attribute_id { get; set; }

        public override string ToString()
        {
            return $"{{name={name},unit_number={unit_number},rarity={rarity}}}";
        }
    }
}
