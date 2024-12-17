"use server"

import { apiService } from "./services/api-service";
import CustomForm from "./components/custom-form";
import Character from "./interfaces/models/character";

export default async function Home() {
  const getCharactersResponse = await apiService.getCharacters();
  const briv = getCharactersResponse.data[0] as Character;
  if (getCharactersResponse){
    return (
        <CustomForm props={briv} />
    );
  }
  else {
    return (
      <div>Loading...</div>
    )
  }
}
