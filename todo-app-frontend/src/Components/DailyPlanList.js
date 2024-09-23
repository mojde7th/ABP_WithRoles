import { useEffect } from "react";
import { useState } from "react"
import { getAllDailyPlans } from "../services/dailyPlanService";

 

const DailyPlanList = () => {
    const [dailyPlans, setDailyPlans] = useState([]); // Initialize with an empty array
    const [loading, setLoading] = useState(true); // Track loading state
    const [error, setError] = useState(null); // Track errors

    useEffect(() => {
        const fetchDailyPlans = async () => {
            try {
                const response = await getAllDailyPlans();
                
                console.log('API response:', response.data);

                if (Array.isArray(response.data)) {
                    setDailyPlans(response.data);
                } else {
                    throw new Error('Invalid data format');
                }
            } catch (error) {
                console.error('Error fetching daily plans:', error);
                setError('Failed to fetch daily plans');
            } finally {
                setLoading(false);
            }
        };

        fetchDailyPlans();
    }, []);

    // Convert minutes to hours and minutes in a readable format
    const convertMinutesToHoursAndMinutes = (totalMinutes) => {
        const hours = Math.floor(totalMinutes / 60);
        const minutes = totalMinutes % 60;
        return `${hours}h ${minutes}m`;
    };

    // Handle loading state
    if (loading) {
        return <div>Loading...</div>;
    }

    // Handle error state
    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div>
            <h1>Daily Plans</h1>
            {dailyPlans.length > 0 ? (
                <table border="1" cellPadding="10">
                    <thead>
                        <tr>
                            <th>Plan Title</th>
                            <th>Task Name</th>
                            <th>Task Duration</th>
                            <th>Layer Name</th>
                            <th>Layer Duration</th>
                            <th>Total Task Duration</th>
                            <th>Total Layer Duration</th>
                            <th>Total Duration (Tasks + Layers)</th>
                        </tr>
                    </thead>
                    <tbody>
                        {dailyPlans.map(plan => {
                            const totalTaskDuration = plan.tasks ? plan.tasks.reduce((sum, task) => sum + task.duration, 0) : 0;
                            const totalLayerDuration = plan.layers ? plan.layers.reduce((sum, layer) => sum + layer.duration, 0) : 0;
                            const totalDuration = totalTaskDuration + totalLayerDuration;

                            return (
                                <tr key={plan.id}>
                                    {/* Plan Title */}
                                    <td>{plan.title}</td>

                                    {/* Task Details */}
                                    <td>
                                        {plan.tasks && plan.tasks.length > 0 ? (
                                            plan.tasks.map(task => (
                                                <div key={task.id}>
                                                    {task.name}
                                                </div>
                                            ))
                                        ) : (
                                            <div>No tasks</div>
                                        )}
                                    </td>

                                    {/* Task Durations */}
                                    <td>
                                        {plan.tasks && plan.tasks.length > 0 ? (
                                            plan.tasks.map(task => (
                                                <div key={task.id}>
                                                    {convertMinutesToHoursAndMinutes(task.duration)}
                                                </div>
                                            ))
                                        ) : (
                                            <div>-</div>
                                        )}
                                    </td>

                                    {/* Layer Details */}
                                    <td>
                                        {plan.layers && plan.layers.length > 0 ? (
                                            plan.layers.map(layer => (
                                                <div key={layer.id}>
                                                    {layer.name}
                                                </div>
                                            ))
                                        ) : (
                                            <div>No layers</div>
                                        )}
                                    </td>

                                    {/* Layer Durations */}
                                    <td>
                                        {plan.layers && plan.layers.length > 0 ? (
                                            plan.layers.map(layer => (
                                                <div key={layer.id}>
                                                    {convertMinutesToHoursAndMinutes(layer.duration)}
                                                </div>
                                            ))
                                        ) : (
                                            <div>-</div>
                                        )}
                                    </td>

                                    {/* Total Task Duration */}
                                    <td>{convertMinutesToHoursAndMinutes(totalTaskDuration)}</td>

                                    {/* Total Layer Duration */}
                                    <td>{convertMinutesToHoursAndMinutes(totalLayerDuration)}</td>

                                    {/* Total Duration (Tasks + Layers) */}
                                    <td>{convertMinutesToHoursAndMinutes(totalDuration)}</td>
                                </tr>
                            );
                        })}
                    </tbody>
                </table>
            ) : (
                <div>No daily plans available.</div>
            )}
        </div>
    );
};

export default DailyPlanList;
