import { Input, InputLabel } from "@mui/material";
import { ChangeEvent, Dispatch, SetStateAction } from "react";

interface CustomNumberInputProps {
    id: string
    label: string
    defaultValue: number
    setStateActionOnChange: Dispatch<SetStateAction<number>>
}

export default function CustomNumberInput(props: CustomNumberInputProps) {

    const handleChange = (event: ChangeEvent<HTMLInputElement>) => {
        props.setStateActionOnChange(parseInt(event.target.value));
    };

    return (
        <>
            <InputLabel id={`${props.id}-label`}>{props.label}</InputLabel>
            <Input 
                type="number" 
                defaultValue={props.defaultValue}
                onChange={handleChange}
            />
        </>
        
    )
}