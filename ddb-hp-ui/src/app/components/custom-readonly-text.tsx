import { TextField } from "@mui/material";

export default function CustomReadonlyText(props: { text: string }) {
    return (
        <TextField 
            slotProps={{ 
                input: { readOnly: true }
            }} 
            defaultValue={props.text}
        />
    )
}