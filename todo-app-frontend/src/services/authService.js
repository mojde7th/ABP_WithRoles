// src/services/authService.js
import axios from 'axios';

const API_URL = 'https://localhost:44355/api/customaccount';

export const login = (username, password) => {
    return axios.post(`${API_URL}/Login`, { username, password });
};
export const createRole=async (roleName)=>{
try{
const response=await axios.post(`${API_URL}/createrole`,{roleName},
    {
        headers:{
            'Content-Type':'application/json',
        },
    }
);
return response.data;
}
catch(error){
    console.error("Error Creating role:",error);
    throw error;
}

};

export const assignRole=async (username,roleName)=>{
try{
const response=await axios.post(`${API_URL}/assignrole`,{username,roleName},
    {headers:{
        'Content-Type':'application/json',
    },}
);
return response.data;
}
catch(error){
console.log('Error assigning role:',error);
throw error;
}

};