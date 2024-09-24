import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { getDailyPlanById } from '../services/dailyPlanService';

const DailyPlanDetail = () => {
    const { id } = useParams();
    const { dailyPlan, setDailyPlan } = 
    useState(null);
    const [sections, setSections] = useState([{ tasks: [], layers: [] }]); //initial one section

    useEffect(() => {
        const fetchDailyPlan = async () => {
          try{  const response = 
            await getDailyPlanById(id);
            setDailyPlan(response.data);}
            catch(error){
                console.error("error",error);
                
            }
        };
        fetchDailyPlan();
    }, [id]);
    const handleAddSection = () => {
        setSections([...sections, { tasks: [], layers: [] }]);
    };
    const handleTaskChange = (sectionIndex, taskIndex, key, value) => {
        const updatedSections = [...sections];
        updatedSections[sectionIndex].tasks[taskIndex][key] = value;
        setSections(updatedSections);
    };

    const handleLayerChange = (sectionIndex, layerIndex, key, value) => {
        const updatedSections = [...sections];
        updatedSections[sectionIndex].layers[layerIndex][key] = value;
        setSections(updatedSections);
    };
    const addLayerToSection = (sectionIndex) => {
        const updatedSections = [...sections];
        updatedSections[sectionIndex].layers.push({
            name: '',
            duration: 0,
        });
        setSections(updatedSections);
    };
    const addTaskToSection = (sectionIndex) => {
        const updatedSections = [...sections];
        updatedSections[sectionIndex].tasks.push({ name: '', duration: 0 });
        setSections(updatedSections);
    };
    const calculateTotalDuration = (items) => {
        return items.reduce((total, item) => total + item.duration, 0);
    };
    const convertMinutesToHoursAndMinutes = (totalMinutes) => {
        const hours = Math.floor(totalMinutes / 60);
        const minutes = totalMinutes % 60;
        return `${hours}h ${minutes}m`;
    };
    return (
        <div>
            <h1>{dailyPlan?.title || 'Daily Plan'}</h1>
            {sections.map((section, index) => (
                <div key={index} style={{ marginBottom: '20px' }}>
                    <h3>Section {index + 1}</h3>
                    <div>
                        <h4>Tasks:</h4>
                        {section.tasks.map((task, taskIndex) => (
                            <div key={taskIndex}>
                                <input
                                    type="text"
                                    value={task.name}
                                    onChange={(e) =>
                                        handleTaskChange(
                                            index,
                                            taskIndex,
                                            'name',
                                            e.target.value
                                        )
                                    }
                                    placeholder="Task Name"
                                />
                                <input
                                    type="number"
                                    value={task.duration}
                                    onChange={(e) =>
                                        handleTaskChange(
                                            index,
                                            taskIndex,
                                            'duration',
                                            e.target.value
                                        )
                                    }
                                    placeholder="Duration (in minutes)"
                                />
                            </div>
                        ))}
                        <button onClick={() => addTaskToSection(index)}>
                            Add Task
                        </button>
                    </div>
                    <div
                        style={{
                            background: 'gray',
                            opacity: '0.5',
                            padding: '10px',
                        }}
                    >
                        <h4>Layers:</h4>
                        {section.layers.map((layer, layerIndex) => (
                            <div key={layerIndex}>
                                <input
                                    type="text"
                                    value={layer.name}
                                    onChange={(e) =>
                                        handleLayerChange(
                                            index,
                                            layerIndex,
                                            'name',
                                            e.target.value
                                        )
                                    }
                                    placeholder="Layer Name"
                                />
                                <input type="number" value={layer.duration}
                                onChange={(e)=>handleLayerChange(
                                    index,layerIndex,'duration',
                                    e.target.value
                                )} placeholder='Duration (in minutes)'
                                />
                            </div>
                        ))}
                        <button onClick={()=>addLayerToSection(index)}>
                            Add Layer
                        </button>
                    </div>
                    <h5>Total Task Duration:
                        {convertMinutesToHoursAndMinutes
                        (calculateTotalDuration(section.tasks))}
                    </h5>
                    <h5>
                        Total Layer Duration:{convertMinutesToHoursAndMinutes
                        (calculateTotalDuration(section.layers))}
                    </h5>
                    <h5>
                        total Duration: {convertMinutesToHoursAndMinutes
                        (calculateTotalDuration(section.tasks)+calculateTotalDuration(section.layers))}
                    </h5>
                    <hr style={{borderTop:'3px solid black'}}/>
                </div>
            ))}
            <button onClick={handleAddSection}>
                Add New Section
            </button>
        </div>
    );
};
export default DailyPlanDetail;
