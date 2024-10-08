import { useEffect, useState } from 'react';
import { getAllDailyPlans } from '../services/dailyPlanService';
import { Link } from 'react-router-dom';

const DailyPlanList = () => {
    const [plans, setPlans] = useState([]);
    useEffect(() => {
        getAllDailyPlans()
            .then((response) => setPlans(response.data))
            .catch((error) =>
                console.error('Error fetching daily plans:', error)
            );
    }, []);

    return (
        <div>
            <h1>Daily Plans</h1>
            <ul>
                {plans.map((plan) => (
                    <li key={plan.id}>
                        <Link to={`/dailyPlans/${plan.id}`}>
                            {plan.title} - Created on:
                            {plan.CreationTime}
                        </Link>
                    </li>
                ))}
            </ul>
            <Link to="/dailyplans/new">Create New Daily Plan</Link>
        </div>
    );
};
export default DailyPlanList;
