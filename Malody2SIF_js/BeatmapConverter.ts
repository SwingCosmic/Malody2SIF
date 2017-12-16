
var math: any = Math;
var array: any = Array;

math.rndInt = function (a, b): Number {
    return Math.floor(Math.random() * (b - a + 1) + a);
}

array.prototype.last = function () {
    return this[this.length - 1];
}

namespace LoveKicher.ma2sif {

    function GetTimeFromBeat(beat: number, lastTime: number, offset: number)
    {
        let section = beat[0] * lastTime;//当前小节的起始时刻
        let diff = beat[2];//当前小节是几分音符
        let count = beat[1];//当前note在第几个音符的位置
        return section + (lastTime / diff) * count + offset;
    }

    export class BeatmapConverter {
    
        constructor(ma_map: any) {
            if (ma_map.meta != undefined && ma_map.note != undefined) 
                if (ma_map.meta.mode == 0) 
                    if (ma_map.meta.mode_ext.column == 9) 
                        this.malodyMap = ma_map;
                    else throw new Error("The column count of the beatmap must be 9.");
                else throw new Error("Only KEY mode beatmap can be converted.");
            else throw new Error("The object is not a valid Malody beatmap.");
        }
    
        private malodyMap: any;
        private sifMap: Array<map>;

        mapAttribute: attribute = attribute.All;
    
        /**
         * 转换成SIF谱面
         * @returns 转换后的谱面
         */
        convertToSifMap(): String {
    
            let m = new Array<map>();

            let bpm: number = this.malodyMap.time[0].bpm;
            let last = 60.0 / bpm;
            let setting = this.malodyMap.note.last();
            let offset: number = setting.offset / 1000.0;

            for (let n of this.malodyMap.note) {
                if (n.type == undefined) {
                    let note = new map();

                    let time = GetTimeFromBeat(n.beat, last, offset);
                    note.timing_sec = parseFloat(time.toFixed(3))
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
                        let endTime = GetTimeFromBeat(n.endbeat, last, offset);
                        note.effect_value = parseFloat(endTime.toFixed(2));
                    }

                    m.push(note);
                }
            }

            return JSON.stringify(m);
        }
    }
}
