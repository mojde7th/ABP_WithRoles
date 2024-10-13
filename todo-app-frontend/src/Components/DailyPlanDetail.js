// src/components/DailyPlanDetail.js
import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';
import '../DailyPlanDetail.css';

const DailyPlanDetail = () => {
    const { id } = useParams();
    const [plan, setPlan] = useState(null);

    useEffect(() => {
        axios
            .get(`https://localhost:44355/api/dailyplan/${id}`)
            .then((response) => setPlan(response.data))
            .catch((error) =>
                console.error('Error fetching daily plan details:', error)
            );
    }, [id]);

    const formatDuration = (minutes) => {
        const hours = Math.floor(minutes / 60);
        const mins = minutes % 60;
        return hours > 0
            ? `${hours}h ${mins > 0 ? `${mins}m` : ''}`
            : `${mins}m`;
    };

    if (!plan) return <p>Loading...</p>;

    return (
        <div className="daily-plan-detail">
            <h1>{plan.title}</h1>
            {plan.sections.map((section, sectionIndex) => (
                <div key={sectionIndex} className="section">
                    <h2>{section.name}</h2>
                    <div className="task-layer-container">
                        {section.tasks.map((task, taskIndex) => (
                            <div key={taskIndex} className="task-box">
                                <p>
                                    <strong>{task.name}</strong>
                                </p>
                                <p>{formatDuration(task.duration)}</p>
                            </div>
                        ))}
                        {section.planLayers.map((layer, layerIndex) => (
                            <div key={layerIndex} className="layer-box">
                                <p>
                                    <strong>{layer.name}</strong>
                                </p>
                                <p>{formatDuration(layer.duration)}</p>
                            </div>
                        ))}
                    </div>
                    <table className="totals-table">
                        <thead>
                            <tr>
                                <th>Total Task Duration</th>
                                <th>Total Layer Duration</th>
                                <th>Total Duration</th>
                                <th>Cumulative Task Duration</th>
                                <th>Cumulative Layer Duration</th>
                                <th>Cumulative Total Duration</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    {formatDuration(section.totalTaskDuration)}
                                </td>
                                <td>
                                    {formatDuration(section.totalLayerDuration)}
                                </td>
                                <td>{formatDuration(section.totalDuration)}</td>
                                <td>
                                    {formatDuration(
                                        section.cumulativeTaskDuration
                                    )}
                                </td>
                                <td>
                                    {formatDuration(
                                        section.cumulativeLayerDuration
                                    )}
                                </td>
                                <td>
                                    {formatDuration(
                                        section.cumulativeTotalDuration
                                    )}
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            ))}
        </div>
    );
};

export default DailyPlanDetail;
