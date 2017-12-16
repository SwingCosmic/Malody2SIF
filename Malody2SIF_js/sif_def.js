var attribute;
(function (attribute) {
    attribute[attribute["Smile"] = 1] = "Smile";
    attribute[attribute["Pure"] = 2] = "Pure";
    attribute[attribute["Cool"] = 3] = "Cool";
    attribute[attribute["All"] = 5] = "All";
})(attribute || (attribute = {}));
var rarity;
(function (rarity) {
    rarity[rarity["N"] = 1] = "N";
    rarity[rarity["R"] = 2] = "R";
    rarity[rarity["SR"] = 3] = "SR";
    rarity[rarity["SSR"] = 5] = "SSR";
    rarity[rarity["UR"] = 4] = "UR";
})(rarity || (rarity = {}));
var note_effect;
(function (note_effect) {
    note_effect[note_effect["Normal"] = 1] = "Normal";
    note_effect[note_effect["Icon"] = 2] = "Icon";
    note_effect[note_effect["Hold"] = 3] = "Hold";
    note_effect[note_effect["Star"] = 4] = "Star";
    note_effect[note_effect["Star3"] = 5] = "Star3";
    note_effect[note_effect["Star5"] = 6] = "Star5";
    note_effect[note_effect["Star9"] = 7] = "Star9";
    note_effect[note_effect["Swing"] = 11] = "Swing";
    note_effect[note_effect["SwingIcon"] = 12] = "SwingIcon";
    note_effect[note_effect["SwingHold"] = 13] = "SwingHold";
})(note_effect || (note_effect = {}));
/**
 * SIF Beatmap
 */
var map = (function () {
    function map() {
    }
    return map;
}());
//# sourceMappingURL=sif_def.js.map