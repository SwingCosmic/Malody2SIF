//HACK:绕过TS2339
Math.rndInt = function (a, b) {
    return Math.floor(Math.random() * (b - a + 1) + a);
};
Array.prototype.last = function () {
    return this.length > 0 ?
        this[this.length - 1]
        : null;
};
Array.prototype.select = function (fn) {
    var r = [];
    for (var _a = 0, _b = this; _a < _b.length; _a++) {
        var i = _b[_a];
        r.push(fn(i));
    }
    return r;
};
var LoveKicher;
(function (LoveKicher) {
    var SifLive;
    (function (SifLive) {
        /**
         * 从Malody beat计算时刻
         * @param beat beat数组
         * @param length 每小节长度，等于60 / BPM
         * @param offset 谱面延迟
         */
        function GetTimeFromBeat(beat, length, offset) {
            if (offset === void 0) { offset = 0; }
            var section = beat[0] * length; //当前小节的起始时刻
            var diff = beat[2]; //当前小节是几分音符
            var count = beat[1]; //当前note在第几个音符的位置
            return section + (length / diff) * count + offset;
        }
        /**
         * 从时刻获取Malody beat
         * @param time 要获取的时刻
         * @param length 每小节长度
         * @param diff 按几分音符计算，默认四分音符
         * @param offset 谱面延迟。如果启用全局延迟，那么这个值为第一个note的时刻；
         * 否则，这个值应该是让每个拍子尽可能处在容易被节拍数整除的时刻上。
        例如，某首歌第一个note是2.548s，如果是全局延迟，offset就是2548ms；
        否则的话，建议的值为48ms，用2.500方便计算。
         */
        function GetBeatFromTime(time, length, diff, offset) {
            if (diff === void 0) { diff = 4; }
            if (offset === void 0) { offset = 0; }
            var realTime = time - offset;
            var beat = new Array(3);
            beat[0] = Math.floor(realTime / length);
            beat[2] = diff;
            beat[1] = parseFloat(((realTime - beat[0] * length) / (length / diff)).toFixed(1));
            return beat;
        }
        function isSwing(n) {
            return n.effect == note_effect.Swing ||
                n.effect == note_effect.SwingHold ||
                n.effect == note_effect.SwingIcon;
        }
        /**
         * 谱面转换器
         */
        var BeatmapConverter = (function () {
            function BeatmapConverter() {
                this.mapAttribute = attribute.All;
            }
            BeatmapConverter.prototype.load = function (beatmap) {
                if (beatmap.meta && beatmap.note)
                    if (beatmap.meta.mode == 0)
                        if (beatmap.meta.mode_ext.column == 9)
                            this.malodyMap = beatmap;
                        else
                            throw new Error("The column count of the beatmap must be 9.");
                    else
                        throw new Error("Only KEY mode beatmap can be converted.");
                else if (beatmap.length > 0 && beatmap[0].timing_sec != undefined)
                    this.sifMap = beatmap;
                else
                    throw new Error("The object is not a valid beatmap.");
            };
            /**
             * 转换成SIF谱面
             * @returns 转换后的谱面
             */
            BeatmapConverter.prototype.convertToSifMap = function () {
                var _this = this;
                if (this.malodyMap) {
                    var m_1 = new Array();
                    //假定BPM只有一个
                    var bpm = this.malodyMap.time[0].bpm;
                    var length_1 = 60 / bpm;
                    var setting = this.malodyMap.note.last();
                    var offset_1 = setting.offset / 1000.0;
                    this.malodyMap.note.forEach(function (n) {
                        if (!n.type) {
                            var note = new map();
                            var time = GetTimeFromBeat(n.beat, length_1, offset_1);
                            note.timing_sec = parseFloat(time.toFixed(3));
                            note.effect = note_effect.Normal;
                            note.effect_value = 2;
                            if (_this.mapAttribute == attribute.All)
                                note.notes_attribute = Math.rndInt(1, 3);
                            else
                                note.notes_attribute = _this.mapAttribute;
                            //HACK:Malody键位是从左到右0->8，而SIF是9->1
                            note.position = 9 - n.column;
                            note.notes_level = 1;
                            if (n.endbeat) {
                                note.effect = note_effect.Hold;
                                var endTime = GetTimeFromBeat(n.endbeat, length_1, offset_1);
                                note.effect_value = parseFloat((endTime - time).toFixed(3));
                            }
                            m_1.push(note);
                        }
                    });
                    this.sifMap = m_1;
                    return JSON.stringify(m_1);
                }
                else {
                    return JSON.stringify(this.sifMap);
                }
            };
            /**
             * 将SIF谱面转为Malody谱面
             *
             * @param bpm 谱面的BPM。如果歌曲BPM固定，类型为number；
             * 否则，为一个格式为 { beat: [0, 0, 1], bpm: 120 } 的歌曲BPM分段的对象数组
             * @param offset 歌曲延迟(ms)
             * @param diff 按几分音符计算，默认四分音符
             * @param title 歌曲标题
             * @param artist 歌曲艺术家
             * @param creator 谱面作者
             * @param version 谱面版本（难度）
             */
            BeatmapConverter.prototype.convertToMalodyMap = function (bpm, offset, diff, title, artist, creator, version) {
                if (offset === void 0) { offset = 0; }
                if (diff === void 0) { diff = 4; }
                if (this.sifMap) {
                    var template_1 = {
                        meta: {
                            creator: creator || "Malody2SIF",
                            background: "bg.png",
                            version: version || "Default Lv.10",
                            id: 0,
                            mode: 0,
                            time: Math.round(new Date().getTime() / 1000),
                            song: {
                                title: title || "Auto Generate",
                                artist: artist || "Unknown",
                                id: 0
                            },
                            mode_ext: {
                                column: 9
                            }
                        },
                        time: (function () {
                            if (typeof bpm === "number") {
                                return [{
                                        beat: [0, 0, 1],
                                        bpm: bpm
                                    }];
                            }
                            else
                                return bpm;
                        })(),
                        note: [
                            //{
                            //    beat: [0, 1, 0],
                            //    column: 5
                            //},
                            {
                                beat: [0, 0, 1],
                                sound: "bgm.ogg",
                                vol: 100,
                                offset: offset,
                                type: 1
                            }
                        ],
                        extra: {
                            test: {
                                divide: 4,
                                speed: 100,
                                save: 0,
                                lock: 0,
                                edit_mode: 0
                            }
                        }
                    };
                    //假定BPM只有一个
                    bpm = template_1.time[0].bpm;
                    var length_2 = 60 / bpm;
                    offset /= 1000;
                    this.sifMap.forEach(function (m) {
                        var note = {};
                        note.beat = GetBeatFromTime(m.timing_sec, length_2, diff, offset);
                        note.column = 9 - m.position;
                        if (m.effect == note_effect.Hold || m.effect == note_effect.SwingHold) {
                            note.endbeat = GetBeatFromTime(m.timing_sec + m.effect_value, length_2, diff, offset);
                        }
                        //#region 为note添加其他元数据，不确定Malody是否报错
                        note.attribute = m.notes_attribute;
                        if (isSwing(m)) {
                            note.swing = 1;
                        }
                        //#end region
                        template_1.note.push(note);
                    });
                    var setting = template_1.note.shift();
                    template_1.note.push(setting);
                    return JSON.stringify(template_1);
                }
                else
                    return JSON.stringify(this.malodyMap);
            };
            //=====================================================
            /**
             * 对SIF谱面进行滑键分组
             * @param markedSifMap 对map.effect已经标注完成的滑键谱，
             * 如果不填默认为this.sifMap
             */
            BeatmapConverter.prototype.groupSwingMap = function (markedSifMap) {
                var _map = markedSifMap ? markedSifMap : this.sifMap;
                var diff = 2;
                //第一次简单分组，根据时间差异大致分片
                var firstGroups = function (arr) {
                    var groups = new Array();
                    var temp = arr.sort(function (a, b) {
                        return a.timing_sec != b.timing_sec
                            ? a.timing_sec - b.timing_sec
                            : a.position - b.position;
                    });
                    var i = 0;
                    var group = new Array();
                    while (i < temp.length) {
                        if (isSwing(temp[i])) {
                            //第一个滑键，或者上一个也是滑键
                            if (!group.length || isSwing(temp[i - 1])) {
                                group.push(temp[i++]);
                                continue;
                            }
                            else {
                                //向前找第一个是滑键的note
                                var _i = function (j) {
                                    var flag = -1;
                                    while (j >= 0) {
                                        if (isSwing(temp[j])) {
                                            flag = j;
                                            break;
                                        }
                                        j--;
                                    }
                                    return flag;
                                }(i - 1);
                                //找到符合要求的，可能在同一个组
                                if (_i >= 0 && i - _i <= diff) {
                                    group.push(temp[i++]);
                                    continue;
                                }
                                else {
                                    var _t = group;
                                    if (_t.length > 0) {
                                        groups.push(_t);
                                        console.log(JSON.stringify(_t));
                                        group = [];
                                    }
                                    group.push(temp[i++]);
                                    continue;
                                }
                            }
                        }
                        else {
                            //向后找第一个是滑键的note
                            var _i = function (j) {
                                var flag = -1;
                                while (j < temp.length) {
                                    if (isSwing(temp[j])) {
                                        flag = j;
                                        break;
                                    }
                                    j++;
                                }
                                return flag;
                            }(i + 1);
                            //找不到符合要求的，该组结束
                            if (_i < 0 || _i - i > diff) {
                                var _t = group;
                                if (_t.length > 0) {
                                    groups.push(_t);
                                    console.log(JSON.stringify(_t));
                                    group = [];
                                }
                                i++;
                                continue;
                            }
                            else {
                                i++;
                                continue;
                            }
                        }
                    } //end of while
                    return groups;
                }(_map);
                console.log(firstGroups);
                //=======================二次分组=======================
                var level = 201; //起始level，为了便于区别从202开始标注滑键
                //todo:阈值应该根据BPM生成
                var time_diff = 0.4; //判定不是变速折返的时间阈值
                var result = [];
                firstGroups.forEach(function (g) {
                    var currentGroup = [];
                    g.forEach(function (n, i) {
                        //找符合要求的上一个滑键
                        //let nearest = currentGroup.filter(_n =>
                        //    Math.abs(n.position - _n.position) == 1 &&
                        //    n.timing_sec != _n.timing_sec);
                        var last = currentGroup.last();
                        //如果找到符合要求的上一个note
                        if (last && Math.abs(n.position - last.position) == 1 &&
                            n.timing_sec != last.timing_sec) {
                            /* HACK:针对如下特殊情况
                            9 8 7 6 5 4 3 2 1
                            ○
                              ○
                                ○
                                  ○
                            （空行）
                                ○
                                  ○
                                    ○
                                      ○
                            是否判定空行处变速折返？
                            方法：简单判定两个note的时间差是否大于一个阈值
                            */
                            if (n.timing_sec - last.timing_sec < time_diff) {
                                n.notes_level = last.notes_level;
                                currentGroup.push(n);
                            }
                            else {
                                n.notes_level = ++level;
                                currentGroup.push(n);
                            }
                        }
                        else {
                            n.notes_level = ++level;
                            currentGroup.push(n);
                        }
                    });
                    result = result.concat(currentGroup);
                });
                result = result.concat(_map.filter(function (m) { return !isSwing(m); }))
                    .sort(function (a, b) { return a.timing_sec != b.timing_sec
                    ? a.timing_sec - b.timing_sec
                    : a.position - b.position; });
                this.sifMap = result;
                return JSON.stringify(result);
            };
            return BeatmapConverter;
        }());
        SifLive.BeatmapConverter = BeatmapConverter;
    })(SifLive = LoveKicher.SifLive || (LoveKicher.SifLive = {}));
})(LoveKicher || (LoveKicher = {}));
//# sourceMappingURL=BeatmapConverter.js.map