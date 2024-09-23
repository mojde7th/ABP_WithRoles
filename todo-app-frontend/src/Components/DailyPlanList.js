import { useEffect } from "react";
import { useState } from "react"
import { getAllDailyPlans } from "../services/dailyPlanService";

const DailyPlanlist=()=>{
const [dailyPlans,setDailyPlans]=useState([]);
useEffect(()=>{
    const fetchDailyPlans=async()=>{
        const response=await getAllDailyPlans();
        setDailyPlans(response.data);
    };
    fetchDailyPlans();
},[]

);

return (
<div>
<h1>
    Daily Plans
</h1>


<ul>
    
        
        {dailyPlans.map(plan=>(

            <li key={plan.id}>
{plan.title} - Tasks:{plan.tasks.length},
Layers:{plan.Layers.length}
            </li>
        ))
        }
    
</ul>

</div>
);

};
export default DailyPlanlist;