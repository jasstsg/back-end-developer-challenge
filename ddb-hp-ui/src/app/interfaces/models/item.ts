import ItemModifier from "./item-modifier"

export default interface Item {
    id: string
    name: string
    modifier: ItemModifier
}