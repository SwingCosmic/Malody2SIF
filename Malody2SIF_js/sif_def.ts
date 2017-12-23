enum attribute {
    Smile = 1,
    Pure = 2,
    Cool = 3,
    All = 5
}

enum rarity {
    N = 1,
    R = 2,
    SR = 3,
    SSR = 5,
    UR = 4
}

enum note_effect {
    Normal = 1,
    Icon = 2,
    Hold = 3,
    Star = 4,/*star1*/
    Star3 = 5,
    Star5 = 6,
    Star9 = 7,
    Swing = 11,
    SwingIcon = 12,
    SwingHold = 13,
}

enum random_mode {
    None = 0,
    New = 1,
    Old = 2,
    Unlimited = 3
}

/**
 * SIF Beatmap
 */
class map {
    /** note时刻 */
    public timing_sec: number;
    /** note属性，通常情况一个谱面应该为同一个值 */
    public notes_attribute: attribute;
    /** 滑键分组 */
    public notes_level: number;
    /** note效果类型 */
    public effect: note_effect;
    /** note效果的值，只有长条才有实际意义 */
    public effect_value: number;
    /** 位置，从右往左1-9 */
    public position: number;

}
