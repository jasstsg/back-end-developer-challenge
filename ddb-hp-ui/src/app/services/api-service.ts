
import axios, { AxiosInstance, AxiosResponse } from 'axios';
import Damage from '../interfaces/models/RequestData/damage';
import Heal from '../interfaces/models/RequestData/heal';

const API_URL: string = 'http://localhost:5011/api/characters';
const axiosInstance: AxiosInstance = axios.create({
  baseURL: API_URL,
  timeout: 1000,
  headers: { 'Content-Type' : 'application/json' }
});

export const apiService = {

  async getCharacters(): Promise<AxiosResponse> {
    return await axiosInstance.get('');
  },

  async getCharacter(id: string): Promise<AxiosResponse> {
    return await axiosInstance.get(id);
  },

  async damageCharacter(id: string, damage: Damage): Promise<AxiosResponse> {
    return await axiosInstance.post(`${id}/damage`, damage);
  },

  async healCharacter(id: string, heal: Heal): Promise<AxiosResponse> {
    return await axiosInstance.post(`${id}/heal`, heal);
  },

  async tempHealCharacter(id: string, tempHeal: Heal): Promise<AxiosResponse> {
    return await axiosInstance.post(`${id}/temp-heal`, tempHeal);
  }, 


}