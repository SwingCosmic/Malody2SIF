var math = Math;
var array = Array;
math.rndInt = function (a, b) {
    return Math.floor(Math.random() * (b - a + 1) + a);
};
array.prototype.last = function () {
    return this[this.length - 1];
};
var LoveKicher;
(function (LoveKicher) {
    var ma2sif;
    (function (ma2sif) {
        function GetTimeFromBeat(beat, lastTime, offset) {
            var section = beat[0] * lastTime; //当前小节的起始时刻
            var diff = beat[2]; //当前小节是几分音符
            var count = beat[1]; //当前note在第几个音符的位置
            return section + (lastTime / diff) * count + offset;
        }
        var BeatmapConverter = (function () {
            function BeatmapConverter(ma_map) {
                this.mapAttribute = attribute.All;
                if (ma_map.meta != undefined && ma_map.note != undefined)
                    if (ma_map.meta.mode == 0)
                        if (ma_map.meta.mode_ext.column == 9)
                            this.malodyMap = ma_map;
                        else
                            throw new Error("The column count of the beatmap must be 9.");
                    else
                        throw new Error("Only KEY mode beatmap can be converted.");
                else
                    throw new Error("The object is not a valid Malody beatmap.");
            }
            /**
             * 转换成SIF谱面
             * @returns 转换后的谱面
             */
            BeatmapConverter.prototype.convertToSifMap = function () {
                var m = new Array();
                var bpm = this.malodyMap.time[0].bpm;
                var last = 60.0 / bpm;
                var setting = this.malodyMap.note.last();
                var offset = setting.offset / 1000.0;
                for (var _i = 0, _a = this.malodyMap.note; _i < _a.length; _i++) {
                    var n = _a[_i];
                    var note = new map();
                    if (n.type == undefined) {
                        var time = GetTimeFromBeat(n.beat, last, offset);
                        note.timing_sec = parseFloat(time.toFixed(3));
                        note.effect = note_effect.Normal;
                        note.effect_value = 2;
                        if (this.mapAttribute == attribute.All)
                            note.notes_attribute = math.rndInt(1, 3);
                        else
                            note.notes_attribute = this.mapAttribute;
                        //HACK:Malody键位是从左到右0->8，而SIF是9->1
                        note.position = 9 - n.column;
                        note.notes_level = 1;
                        if (n.endbeat != undefined) {
                            note.effect = note_effect.Hold;
                            var endTime = GetTimeFromBeat(n.endbeat, last, offset);
                            note.effect_value = parseFloat(endTime.toFixed(2));
                        }
                    }
                    m.push(note);
                }
                return JSON.stringify(m);
            };
            return BeatmapConverter;
        }());
        ma2sif.BeatmapConverter = BeatmapConverter;
    })(ma2sif = LoveKicher.ma2sif || (LoveKicher.ma2sif = {}));
})(LoveKicher || (LoveKicher = {}));
//# sourceMappingURL=BeatmapConverter.js.map