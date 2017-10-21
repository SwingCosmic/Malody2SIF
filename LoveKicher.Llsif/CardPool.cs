using LoveKicher.Llsif;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable 1591

namespace LoveKicher.Llsif
{
    /// <summary>
    /// 指定卡池类型，可使用位标志
    /// </summary>
    [Flags]
    public enum PoolType : uint
    {
        Random = 0,

        Muse = 0x1000,
        Aqours = 0x2000,
        All = Muse | Aqours,

        SupportMember = 0x4000,
        NCards = 0x5000,
        Animal = 0x8000,
        Single = 0x6000,

        Printemps = Muse | 0x100,
        LilyWhite = Muse | 0x200,
        BiBi      = Muse | 0x300,

        CYaRon     = Aqours | 0x100,
        AZALEA     = Aqours | 0x200,
        GuiltyKiss = Aqours | 0x300,

        Grade1 = 0x00100000,
        Grade2 = 0x00200000,
        Grade3 = 0x00300000,

        

    }
    /// <summary>
    /// 表示一个卡池
    /// </summary>
    public class CardPool
    {
        
        internal CardPool()
        {
            LoadCards();
        }

        private const string url_get_cards = "https://www.llsupport.cn/api/get.php?type=card&page=all";
        private static  CardPool _instance;

        /// <summary>
        /// 包含所有卡片的主卡池
        /// </summary>
        internal static List<card> MainCardList = new List<card> { };

        public static CardPool Current
        {
            get
            {
                if (_instance == null)
                    _instance = new CardPool();
                return _instance;
            }
        }




        /// <summary>
        /// 获取指定卡池
        /// </summary>
        /// <param name="type">要加载的卡池类别</param>
        /// <returns></returns>
        public List<card> GetPool(PoolType type)
        {
            if (type == PoolType.Random)
                return (from c in MainCardList
                        where c.rarity != Rarity.LR && c.rarity != Rarity.Skill
                        select c).ToList();
            else
            {
                IEnumerable<card> pool;
                var t = typeof(CardPool);
                var name = Enum.GetName(type.GetType(), type);
                var ids_0 = t.GetField("ID_" + name);
                var ids = ids_0.GetValue(this) as int[];
                pool = MainCardList.Where(c => ids.Contains(c.unit_type_id));
                return pool.ToList();
            }
        }


        public List<card> GetSinglePool(int unitTypeId)
        {
            if (Characters.ContainsKey(unitTypeId))
            {
                return MainCardList.Where(
                    c => c.unit_type_id == unitTypeId).ToList();
            }
            else
                throw new ArgumentException("无效的成员类型ID"); 
        }

        #region 公共常量



        //代码是查卡器的json解析后生成的
        /// <summary>获取<see cref="card.unit_type_id"/>的具体取值</summary>
        public static Dictionary<int, string> Characters { get; } = new Dictionary<int, string>
        {
            [1] = "高坂穂乃果",
            [2] = "絢瀬絵里",
            [3] = "南ことり",
            [4] = "園田海未",
            [5] = "星空凛",
            [6] = "西木野真姫",
            [7] = "東條希",
            [8] = "小泉花陽",
            [9] = "矢澤にこ",

            [11] = "桜坂しずく",
            [12] = "宮下ココ",
            [13] = "逢沢遊宇",
            [14] = "一之瀬マリカ",
            [15] = "結城紗菜",
            [16] = "西村文絵",
            [17] = "永山みなみ",
            [18] = "クリスティーナ",
            [19] = "菊池朱美",
            [20] = "須田いるか",
            [21] = "杉崎亜矢",
            [22] = "御堂優理",
            [23] = "佐伯麗音",
            [24] = "鳥居歩美",
            [25] = "神谷理華",
            [26] = "森嶋ななか",
            [27] = "九条聖来",
            [28] = "近江彼方",
            [29] = "近江遥",
            [30] = "下園咲",
            [31] = "田中さち子",
            [32] = "支倉かさね",
            [33] = "多々良るう",
            [34] = "篠宮あきる",
            [35] = "吉川瑞希",
            [36] = "白木凪",
            [37] = "藤城悠弓",
            [38] = "深山聡子",//====================
            [39] = "山内奈々子",//====================
            [40] = "笹原京子",//====================
            [41] = "黒崎隼",
            [42] = "設楽ふみ",
            [43] = "門田剣",
            [44] = "アルパカ",//====================
            [45] = "桐原優香",
            [46] = "斉木風",
            [47] = "福原命",
            [48] = "坂巻千鶴子",
            [49] = "志賀仁美",
            [50] = "鬼崎アキラ",
            [51] = "紫藤美咲",
            [52] = "月島結架",
            [53] = "兵藤さゆり",
            [54] = "蘭花",
            [55] = "ラクシャータ",
            [56] = "レベッカ",
            [57] = "綾小路姫乃",
            [58] = "黒羽咲良",
            [59] = "黒羽咲夜",
            [60] = "白瀬小雪",
            [61] = "相川涼",
            [62] = "イザベラ",
            [63] = "エマ",
            [64] = "高天原睦月",
            [65] = "ジェニファー",
            [66] = "マリア",
            [67] = "レオ",
            [68] = "早乙女紫",

            [69] = "矢澤ここあ",
            [70] = "矢澤虎太郎",
            [71] = "矢澤こころ",
            [72] = "ミカ",
            [73] = "フミコ",
            [74] = "ヒデコ",
            [75] = "山田博子",
            [76] = "にこの母",
            [77] = "ことりの母",
            [78] = "真姫の母",
            [79] = "穂乃果の母",

            [80] = "綺羅ツバサ",
            [81] = "優木あんじゅ",
            [82] = "統堂英玲奈",

            [101] = "高海千歌",
            [102] = "桜内梨子",
            [103] = "松浦果南",
            [104] = "黒澤ダイヤ",
            [105] = "渡辺曜",
            [106] = "津島善子",
            [107] = "国木田花丸",
            [108] = "小原鞠莉",
            [109] = "黒澤ルビィ",

            [111] = "しいたけ",
            [112] = "鹿角理亞",
            [113] = "鹿角聖良",
            [114] = "うちっちー",
            [115] = "千歌の母",
            [116] = "佐藤洋子",
            [117] = "わたあめ",
        };


        //所有以ID_开头的都是分组编号

        public static readonly int[] ID_Muse = Enumerable.Range(1, 9).ToArray();
        public static readonly int[] ID_Aqours = Enumerable.Range(101, 9).ToArray();
        public static readonly int[] ID_All = ID_Muse.Concat(ID_Aqours).ToArray();

        public static readonly int[] ID_Printemps = { 1, 3, 8 };
        public static readonly int[] ID_LilyWhite = { 4, 5, 7 };
        public static readonly int[] ID_BiBi = { 2, 6, 9 };

        public static readonly int[] ID_CYaRon = { 101, 105, 109 };
        public static readonly int[] ID_AZALEA = { 103, 104, 107 };
        public static readonly int[] ID_GuiltyKiss = { 102, 106, 108 };

        public static readonly int[] ID_Grade1 = { 5, 6, 8, 106, 107, 109 };
        public static readonly int[] ID_Grade2 = { 1, 3, 4, 101, 102, 105 };
        public static readonly int[] ID_Grade3 = { 2, 7, 9, 103, 104, 108 };

        public static readonly int[] ID_SupportMember =
            Enumerable.Range(69, 79 - 69 + 1).Concat(
            new int[] { 44, 111, 114, 115, 116, 117 }).ToArray();

        public static readonly int[] ID_NCards = Enumerable.Range(11, 68 - 11 + 1)
            .Except(new int[] { 38, 39, 40, 44 }).ToArray();

        public static readonly int[] ID_Animal = { 44, 111, 114, 117, /*千歌*/101 };

        #endregion


        #region 辅助方法


        private async void LoadCards()
        {
            try
            {
                var filePath = Path.Combine(GetPath(), "cards.json");

                if (!File.Exists(filePath))
                {
                    var req = WebRequest.Create(url_get_cards);
                    var rsp = (await req.GetResponseAsync())?.GetResponseStream();
                    var rspJson = "";
                    using (var sr = new StreamReader(rsp))
                        rspJson = sr.ReadToEnd();
                    MainCardList = JsonConvert.DeserializeObject<List<card>>(rspJson);

                    //保存JSON
                    File.WriteAllText(filePath, rspJson);

                    //var msg = $"模块[{ModuleName}]：已从服务器更新卡片列表，共计{cardList.Count}张";
                    //Api.AddLog(CoolQLogLevel.Info, msg);
                }
                else
                {
                    var jsonString = File.ReadAllText(filePath);
                    MainCardList = JsonConvert.DeserializeObject<List<card>>(jsonString);
                    //log
                    //var msg = $"模块[{ModuleName}]：已加载本地卡片{cardList.Count}张";
                    //Api.AddLog(CoolQLogLevel.Info, msg);
                }
                //加载其它卡池
                //LoadAllCardPool();
            }
            catch (Exception ex)
            {
                throw new TypeInitializationException("卡池加载失败！", ex);
            }
        }

        private static string GetPath()
        {
            var assembly = typeof(CardPool).Assembly;
            var path = Path.GetDirectoryName(
                new Uri(assembly.CodeBase).LocalPath);
            return path;
        }
        #endregion
    }
}
