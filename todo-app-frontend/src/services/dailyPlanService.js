import axios from "axios"
import { API_URL } from "./authService"
const API_URL_Dail='https://localhost:44355/api/dailyplan';
export const getAllDailyPlans=async()=>{

    return await axios.get(API_URL_Dail);
};
export const getDailyPlanById=async(id)=>{
return await axios.get(`${API_URL_Dail}/${id}`);
};
export const createDailyPlan=async(dailyPlan)=>{
return await axios.post(`${API_URL_Dail}/create`,dailyPlan);
};

export const addSectionToPlan=async(planId,section)=>{
return await axios.post(`${API_URL_Dail}/${planId}/sections`,section);
};

export const addTaskToSection=async(sectionId,task)=>{
    return await axios.post(`${API_URL_Dail}/${sectionId}/tasks`,task);
};