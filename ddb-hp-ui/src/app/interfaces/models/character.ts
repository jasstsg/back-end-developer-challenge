import Class from "./class"
import Defense from "./defense"
import Item from "./item"
import Stats from "./stats"

export default interface Character {
    id: string
    name: string
    level: number
    hitPoints: number
    tempHitPoints: number
    currentHitPoints: number
    classes: Class[]
    stats: Stats
    items: Item[]
    defenses: Defense[]
}