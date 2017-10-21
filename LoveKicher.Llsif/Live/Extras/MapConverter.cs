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
            var offset = setting?.offset / 1000 ?? 0;

            foreach (var _n in MalodyMap.note)
            {
                var note = new map();

                if (_n is note)//普通note
                {

                    var n = _n as note;

                    double section = n.beat[0] * last;//当前小节的起始时刻
                    int diff = n.beat[2];//当前小节是几分音符
                    int count = n.beat[1];//当前note在第几个音符的位置
                    double time = section + (last / diff) * count + offset; ;

                    note.effect = NoteEffect.Normal;
                    note.effect_value = 2;

                    if (_n is bar)//长条
                    {
                        var n2 = _n as bar;
                        note.effect = NoteEffect.Hold;

                        var endSection = n2.endbeat[0] * last;
                        int endDiff = n2.endbeat[2];
                        int endCount = n2.endbeat[1];
                        var endTime = endSection + (last / endDiff) * endCount + offset;

                        note.effect_value = Math.Round(endTime - time, 3);
                    }

                    note.timing_sec = Math.Round(time, 3);
                    note.notes_attribute = (Attribute)Enum.ToObject(
                        typeof(Attribute), rnd.Next(1, 4));
                    note.position = 9 - n.column;
                    note.notes_level = 1;

                    m.Add(note);

                }

            }


            return m;
        }


    }
}
