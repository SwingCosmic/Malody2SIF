using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveKicher.Llsif.Live.Extras
{
    /// <summary>
    /// Malody谱面转换器
    /// </summary>
    public class MapConverter
    {
        public MapConverter(Malody_map map)
        {
            MalodyMap = map;
        }
        public MapConverter(string mapJson)
        {
            var map = JsonConvert.DeserializeObject<Malody_map>(mapJson);
            //Box
            List<object> l = new List<object>();
            foreach (var o in map.note)
            {
                var n = o as JObject;
                if (n["sound"] == null)
                {
                    if (n["endbeat"] != null)
                        l.Add(n.ToObject<bar>());
                    else
                        l.Add(n.ToObject<note>());
                }
                else
                    l.Add(n.ToObject<setting_note>());
            }
            map.note = null;
            map.note = l;
            MalodyMap = map;
        }
        public Malody_map MalodyMap { get; set; }

        /// <summary>
        /// 获取或设置歌曲属性。
        /// 默认为<see cref="Attribute.All"/>，
        /// 该值表示全曲note的属性随机。
        /// </summary>
        public Attribute MapAttribute { get; set; } =
            Attribute.All;
        /// <summary>
        /// 执行转换
        /// </summary>
        /// <returns>转换后的SIF谱面</returns>
        public List<map> Convert()
        {
            var m = new List<map>();
            var rnd = new Random();

            var bpm = MalodyMap.time[0].bpm;
            var last = 60 / bpm;

            var setting = MalodyMap.note.Last() as setting_note;
            double offset = setting?.offset / 1000.0 ?? 0.0;

            foreach (var _n in MalodyMap.note)
            {
                var note = new map();

                if (_n is note)//普通note
                {
                    var n = _n as note;

                    double time = GetTimeFromBeat(n.beat, last, offset);
                    note.timing_sec = Math.Round(time, 3);

                    note.effect = NoteEffect.Normal;
                    note.effect_value = 0.05;

                    if (MapAttribute == Attribute.All)
                        note.notes_attribute = (Attribute)Enum.ToObject(
                            typeof(Attribute), rnd.Next(1, 4));
                    else
                        note.notes_attribute = MapAttribute;
                    //HACK:Malody键位是从左到右0->8，而SIF是9->1
                    note.position = 9 - n.column;
                    note.notes_level = 1;

                    if (_n is bar)//长条
                    {
                        var n2 = _n as bar;
                        note.effect = NoteEffect.Hold;

                        var endTime = GetTimeFromBeat(n2.endbeat, last, offset);

                        note.effect_value = Math.Round(endTime - time, 3);
                    }
                    

                    m.Add(note);

                }

            }

            return m;
        }

        static double GetTimeFromBeat(int[] beat, double lastTime, double offset)
        {
            double section = beat[0] * lastTime;//当前小节的起始时刻
            int diff = beat[2];//当前小节是几分音符
            int count = beat[1];//当前note在第几个音符的位置
            return section + (lastTime / diff) * count - offset;
        }
    }
}
