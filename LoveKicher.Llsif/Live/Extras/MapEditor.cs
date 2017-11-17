using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoveKicher.Llsif.Live.Extras
{
    /// <summary>
    /// 指定随机模式
    /// </summary>
    public enum RandomMode
    {
        /// <summary>无随机</summary>
        None = 0,
        /// <summary>新随机</summary>
        New = 1,
        /// <summary>旧随机</summary>
        Old = 2,
        /// <summary>无限制随机</summary>
        Unlimited = 3
    }



    /// <summary>
    /// 用于修改SIF谱面数据
    /// </summary>
    public class MapEditor
    {

        #region random


        private static Random rnd = new Random();

        /// <summary>
        /// 通过指定的随机模式生成随机谱面
        /// </summary>
        /// <param name="map">原谱面</param>
        /// <param name="mode">随机模式</param>
        /// <returns>生成的随机谱面</returns>
        public static List<map> GenerateRandomMap(List<map> map, RandomMode mode)
        {
            List<map> rndMap;
            switch (mode)
            {
                
                case RandomMode.Unlimited:
                    rndMap = CreateUnlimited(map);
                    break;
                case RandomMode.Old:
                    rndMap = CreateOldRandom(map);
                    break;
                case RandomMode.New:
                default:
                    rndMap = map;
                    break;
            }

            return rndMap;
        }
        /// <summary>
        /// 生成无限制随机谱，即不管谱面内容随机修改位置
        /// </summary>
        /// <param name="sourceMap">原谱面</param>
        /// <returns>转换后的谱面</returns>
        internal static List<map> CreateUnlimited(List<map> sourceMap)
        {
            rnd = new Random();
            var r = new List<map>(sourceMap);
            foreach (var m in r)
            {
                m.position = rnd.Next(1, 10);
            }
            return r;
        }

        /// <summary>生成旧随机谱，即将非双押的单点随机修改位置</summary>
        /// <param name="sourceMap">原谱面</param>
        /// <returns>转换后的谱面</returns>
        internal static List<map> CreateOldRandom(List<map> sourceMap)
        {
            rnd = new Random();
            var r = new List<map>(sourceMap);
            foreach (var m in r)
            {
                if (m.effect == NoteEffect.Normal ||
                    m.effect == NoteEffect.Star ||
                    m.effect == NoteEffect.Token)
                {//如果是单点
                    if (sourceMap.Except(new map[] { m })
                        .Where(n => n.timing_sec == m.timing_sec)
                        .Count() == 0)//如果不是双押
                    {
                        m.position = rnd.Next(1, 10);
                    } 
                }  
            }
            return r;
        }


       

        //===================================

        private static IEnumerable<T> GetRandomList<T>( IEnumerable<T> source)
        {
            var temp = source.ToList();
            while (temp.Count > 0)
            {
                var i = rnd.Next(0, temp.Count);
                var obj = temp[i];
                temp.RemoveAt(i);

                yield return obj;

            }
        }

        #endregion

        public static List<map> GenerateSwingMap(List<map> map, double minDiff)
        {
            try
            {
                //假设已经排序好，并且没有双押
                var temp = new List<map>(map);
                int currentLevel = 102;
                int i = 0;
                while (i < map.Count)
                {
                    //如果间隔小于阈值，就一直往下找这一组滑键
                    //todo:用类似BFS的方法，利用队列储存当前值，防止碰到
                    //滑键组被大量非滑键分割时，i一直++的问题
                    while (i < map.Count - 1 && map[i + 1].timing_sec - map[i].timing_sec <= minDiff)
                    {
                        if (map[i + 1].timing_sec - map[i].timing_sec > 0)
                        {
                            if (Math.Abs(map[i + 1].position - map[i].position) == 1)
                            {
                                map[i].notes_level = currentLevel;
                                if (map[i].effect == NoteEffect.Hold)
                                {
                                    map[i].effect = NoteEffect.SwingHold;
                                    currentLevel++;
                                    break;//滑键长按只能是滑键组的结尾，下一个可能是另一个组
                                }
                                map[i].effect = NoteEffect.Swing;
                            }
                        }
                        i++;
                    }
                    //判断是滑键组的最后一个还是跳过的普通note
                    if (i > 0 && map[i].timing_sec - map[i - 1].timing_sec <= minDiff &&
                        Math.Abs(map[i - 1].position - map[i].position) == 1)
                    {
                        map[i].notes_level = currentLevel;
                        map[i].effect = map[i].effect == NoteEffect.Hold
                            ? NoteEffect.SwingHold : NoteEffect.Swing;
                        currentLevel++;
                    }

                    i++;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            


            return map;
        }

    }
}
