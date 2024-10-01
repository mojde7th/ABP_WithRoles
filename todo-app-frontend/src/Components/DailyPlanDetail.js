import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import {
    addSectionToPlan,
    addTaskToSection,
    getDailyPlanById,
} from '../services/dailyPlanService';

const DailyPlanDetail = () => {
    const { id } = useParams();
    const [dailyPlan, setDailyPlan] = useState(null);
    const [newSection, setNewSection] = useState('');
    const [newTask, setNewTask] = useState('');
    const [newTaskDuration, setNewTasuration] = useState(0);
    const [newLayer, setNewLayer] = useState('');
    const [newLayerDuration, setNewLayerDuration] = useState(0);

    useEffect(() => {
        const fetchDailyPlan = async () => {
            const response = await getDailyPlanById(id);
            setDailyPlan(response.data);
            console.log(response.data);
        };
        fetchDailyPlan();
    }, [id]);

    const handleAddSection = async () => {
        await addSectionToPlan(id, { name: newSection });
        setNewSection('');
    };

    const handleAddTask = async (sectionId) => {
        await addTaskToSection(sectionId, {
            name: newTask,
            duration: newTaskDuration,
        });
        setNewTask('');
        setNewTasuration(0);
    };
    return <div></div>;
};
export default DailyPlanDetail;
