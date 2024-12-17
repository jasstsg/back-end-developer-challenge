"use client"

import { InputLabel, MenuItem, Select, SelectChangeEvent } from "@mui/material";
import { Dispatch, SetStateAction } from "react";

interface CustomSelectProps {
    id: string
    label: string
    defaultValue: number
    selectOptions: string[]
    setStateActionOnChange: Dispatch<SetStateAction<number>>
    disabled?: boolean
}

export default function CustomSelect(props: CustomSelectProps) {
    const handleChange = (event: SelectChangeEvent) => {
        props.setStateActionOnChange(parseInt(event.target.value));
    };

    return (
        <>
            <InputLabel id={`${props.id}-label`}>{props.label}</InputLabel>
            <Select
                labelId={`${props.id}-label`}
                id={`${props.id}-select`}
                label={props.label}
                defaultValue={props.defaultValue.toString()}
                onChange={handleChange}
                disabled={props.disabled ?? false}
            >
                {
                    props.selectOptions.map((dt, index) => (
                        <MenuItem key={dt} value={index}>{dt}</MenuItem>
                    ))
                }
            </Select>
        </>
    )
}

