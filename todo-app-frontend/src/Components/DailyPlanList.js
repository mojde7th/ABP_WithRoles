import { useEffect, useState } from 'react';
import { getAllDailyPlans } from '../services/dailyPlanService';
import { Link } from 'react-router-dom';

const DailyPlanList = () => {
    const [dailyPlans, setDailyPlans] = useState([]);

    useEffect(() => {
        const fetchDailyPlans = async () => {
            const response = await getAllDailyPlans();
            setDailyPlans(response.data);
        };
        fetchDailyPlans();
    }, []);

    return (
        <div>
            <h2>Daily Plans</h2>
            <ul>
                {dailyPlans.map((plan) => (
                    <li key={plan.id}>
                        <Link to={`/dailyplans/${plan.id}`}>{plan.title}</Link>
                    </li>
                ))}
            </ul>
            <Link to="/dailyplans/new">Create New Daily Plan</Link>
        </div>
    );
};
export default DailyPlanList;
