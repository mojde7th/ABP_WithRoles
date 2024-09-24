import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getAllDailyPlans } from "../services/dailyPlanService";

const DailyPlanList=()=>{
const[dailyPlans,setDailyPlans]=useState([]);
const navigate=useNavigate();
useEffect(()=>{
const fetchDailyPlans=async()=>{
const response=await getAllDailyPlans();
setDailyPlans(response.data ||[]);
};
fetchDailyPlans();
},[]);

return(
    <div>
        <h1>Daily Plans</h1>
        <ul>
            {dailyPlans.map(
                plan=>(
                    <li key={plan.id}
                    onClick={()=>navigate(`/dailyPlan/${plan.id}`)}
                    >
                    {plan.title}
                    </li>
                )

            )}
        </ul>
    </div>
)




};
export default DailyPlanList;
