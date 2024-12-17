"use client"

import { useEffect, useState } from "react";
import { Button, FormControl, Grid2 } from "@mui/material"
import { apiService } from "../services/api-service";
import Character from "../interfaces/models/character"
import CustomSelect from "./custom-select"
import CustomReadonlyText from "./custom-readonly-text";
import CustomNumberInput from "./custom-number-input";
import Damage from "../interfaces/models/RequestData/damage";
import Heal from "../interfaces/models/RequestData/heal";

export default function CustomForm( props: { props: Character }) {
    const [character, setCharacter] = useState(props.props);
    const [characterInfo, setCharacterInfo] = useState("");
    
    useEffect(() => {
        const resistances = character.defenses.filter(d => d.defense == "Resistance");
        const immunities = character.defenses.filter(d => d.defense == "Immunity");
        setCharacterInfo(
            `${character.name}
HP:               ${character.currentHitPoints} / ${character.hitPoints}
Temp HP:      ${character.tempHitPoints}
Resistances: ${resistances.map(r => r.type).join(",")}
Immunities:    ${immunities.map(r => r.type).join(",")}`
        );
    }, [character])

    const actions = ["Deal Damage", "Heal", "Add Temporary Hit Points"]
    
    // Ideally this would be retrieved from the API or a shared resource that the API uses too
    // This would ensure that the damage types stay consistent is something were to change
    const damageTypes = [
        "Bludgeoning",
        "Piercing",
        "Slashing",
        "Fire",
        "Cold",
        "Acid",
        "Thunder",
        "Lightning",
        "Poison",
        "Radiant",
        "Necrotic",
        "Psychic",
        "Force"
    ]

    const [action, setAction] = useState(0);
    const [damageType, setDamageType] = useState(0);
    const [value, setValue] = useState(0);

    const handleExecuteAction = async () => {
        let res = null;
        switch (action) {
            case 0: // Deal Damage
                const damage: Damage = { damageType: damageType, value: value }
                res = await apiService.damageCharacter(character.id, damage);
                setCharacter(res.data as Character);
                break;
            case 1: // Heal
                const heal: Heal = { value: value }
                res = await apiService.healCharacter(character.id, heal);
                setCharacter(res.data as Character);
                break;
            case 2: //Add Temporary Hit Points
                const tempHeal: Heal = { value: value }
                res = await apiService.tempHealCharacter(character.id, tempHeal);
                setCharacter(res.data as Character);
                break;
            default:
                alert(`Attempted to execute an unknown action '${action}'`);
                break;
        }
    }

    return (
        <>
            <Grid2 container spacing={1} justifyContent="center">
                <Grid2 size={12} justifyContent="center">
                    <FormControl fullWidth>
                        <CustomReadonlyText text={characterInfo} />
                    </FormControl>
                </Grid2>
                <Grid2 size={3}>
                    <FormControl fullWidth>
                        <CustomSelect 
                            id="actions"
                            label="Actions" 
                            defaultValue={0}
                            selectOptions={actions} 
                            setStateActionOnChange={setAction}
                        />
                    </FormControl>
                </Grid2>
                <Grid2 size={1}>
                    <FormControl fullWidth>
                        <CustomNumberInput 
                            id="value" 
                            label="Value" 
                            defaultValue={0}
                            setStateActionOnChange={setValue} />
                    </FormControl>
                </Grid2>
                <Grid2 size={3}>
                    <FormControl fullWidth>
                        <CustomSelect 
                            id="damage-types" 
                            label="Damage Types" 
                            defaultValue={0}
                            selectOptions={damageTypes} 
                            setStateActionOnChange={setDamageType}
                            disabled={action != 0}
                        />
                    </FormControl>
                </Grid2>
                <Grid2>
                    <FormControl>
                        <Button onClick={handleExecuteAction}>
                            Execute action
                        </Button>
                    </FormControl>
                </Grid2>
            </Grid2>
        </>
    )
}