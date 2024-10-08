import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getDailyPlanById } from "../services/dailyPlanService";

const DailyPlanDetail=()=>{
        const {id}=useParams();
        const [plan,setPlan]=useState(null);
        useEffect(()=>
            {getDailyPlanById(id)
            .then(response=>setPlan(response.data))
            .catch(error=>console.error('Error Fetching Plan Details:',
                error));},[id]);
            
        if(!plan){
            return
            <div>Loading...</div>;
        }
           
        return(
            <div style={{direction:'ltr'}}>
                <h1>{plan.title}</h1>
                {plan.sections.map((section)=>(
                    <div key={section.id} style={{marginBottom:'20px'}}>
                        <h2>{section.name}</h2>
                        <div style={{
                            display:'flex',
                            border:'1px solid black',
                            marginBottom:'10px',
                            opacity: 0.7
                        }}>
                            {section.tasks.map(task=>(
                                <div key={task.id} style={{
                                    flex:1,
                                    padding:'10px',
                                    backgroundColor:'lightblue'}}>
                                    <strong>{task.name} </strong>
                                    {task.duration}m
                                </div>
                            ))}
                            {
                             section.planLayers.map(
                                layer=>(
                                    <div key={layer.id} style={{
                                        flex:1,
                                        padding:'10px',
                                        backgroundColor:'rgba(128,128,128,0.3)'
                                    }}>
                                        <strong>{layer.name}</strong>
                                        {layer.duration}m
                                    </div>
                                ))}
                        </div>
                        <div>Total Task Duration:{section.totalTaskDuration}m</div>
                        <div>Total Layer Duration:{section.totalLayerDuration}m</div>
                        <div>Total Duration:{section.totalDuration}m</div>
                        <div>Cumulative Task Duration:{section.cumulativeTaskDuration}m</div>
                        <div>Cumulative Layer Duration:{section.cumulativeLayerDuration}m</div>
                        <div>Cumulative Total Duration:{section.cumulativeTotalDuration}m</div>

                    </div>
                ))}
            </div>
        );
};
export default DailyPlanDetail;