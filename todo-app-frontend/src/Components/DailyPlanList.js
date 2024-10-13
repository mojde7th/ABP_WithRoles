// src/components/DailyPlanList.js
import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import '../DailyPlanList.css';

const DailyPlanList = () => {
  const [plans, setPlans] = useState([]);

  useEffect(() => {
    axios.get('https://localhost:44355/api/dailyplan')
      .then(response => setPlans(response.data))
      .catch(error => console.error('Error fetching daily plans:', error));
  }, []);

  return (
    <div className="daily-plan-list">
      <h1>Daily Plans</h1>
      <ul>
        {plans.map((plan) => (
          <li key={plan.id}>
            <Link to={`/dailyplan/${plan.id}`}>{plan.title}</Link>
            <span>{new Date(plan.creationTime).toLocaleDateString()}</span>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default DailyPlanList;
