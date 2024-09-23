import { useState } from "react";
import { createDailyPlan } from "../services/dailyPlanService";

const CreateDailyPlan=()=>{
const [title,setTitle]=useState('');
const [tasks,setTasks]=useState([{name:'',duration:0}]);
const [layers,setLayers]=useState([{name:'',duration:0}]);
const handleSubmit=async(e)=>{
    e.preventDefault();
    const newPlan={
        title,tasks,layers
    };
    await createDailyPlan(newPlan);
    setTitle('');
    setTasks([{name:'',duration:0}]);
    setLayers([{name:'',duration:0}]);
}


return (
    <form onSubmit={handleSubmit}>
<div>
<label>Title</label>
<input type="text" value={title} onChange={(e)=>
    setTitle(e.target.value)
}/>
</div>
<div>
    <label>Tasks</label>
    {
        tasks.map((task,index)=>(
        <div key={index}>
           <input type="text" placeholder="Task Name"
           value={task.name} onChange={(e)=>{

            const newTasks=[...tasks];
            newTasks[index].name=e.target.value;
            setTasks(newTasks);
           }}
           />
           <input type="number" placeholder="Duration (minutes)"
           value={task.duration}
           onChange={(e)=>{
            const newTasks=[...tasks];
            newTasks[index].duration=e.target.value;
            setTasks(newTasks);
           }}
           />
        </div>
        ))
    }
</div>
<div>

    <label for="">Layers</label>
    {
        layers.map((layer,index)=>(
            <div key={index}>
          <input type="text" placeholder="Layer Name"
          value={layer.name}
          onChange={(e)=>{
            const newLayers=[...layers];
            newLayers[index].name=e.target.value;
            setLayers(newLayers);
          }}
          />
          <input type="number" placeholder="Duration (minutes)"
          value={layer.duration}
          onChange={(e)=>{
            
            const newLayers=[...layers];
            newLayers[index].duration=e.target.value;
            setLayers(newLayers);
         }}
          />
            </div>
        ))
    }
</div>


<button type="submit">
    Create Daily Plan
</button>
    </form>
);
};
export default CreateDailyPlan;
