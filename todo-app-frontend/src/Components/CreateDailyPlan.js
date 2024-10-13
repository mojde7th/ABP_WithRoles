// src/components/CreateDailyPlan.js
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import '../CreateDailyPlan.css';

const CreateDailyPlan = () => {
  const [title, setTitle] = useState('');
  const [sections, setSections] = useState([
    { name: '', tasks: [{ name: '', duration: 0 }], planLayers: [{ name: '', duration: 0 }] }
  ]);
  const [createdPlan, setCreatedPlan] = useState(null);

  const handleSectionChange = (index, e) => {
    const updatedSections = [...sections];
    updatedSections[index][e.target.name] = e.target.value;
    setSections(updatedSections);
  };

  const handleTaskChange = (sectionIndex, taskIndex, e) => {
    const updatedSections = [...sections];
    updatedSections[sectionIndex].tasks[taskIndex][e.target.name] = e.target.value;
    setSections(updatedSections);
  };

  const handleLayerChange = (sectionIndex, layerIndex, e) => {
    const updatedSections = [...sections];
    updatedSections[sectionIndex].planLayers[layerIndex][e.target.name] = e.target.value;
    setSections(updatedSections);
  };

  const addNewSection = () => {
    setSections([...sections, { name: '', tasks: [{ name: '', duration: 0 }], planLayers: [{ name: '', duration: 0 }] }]);
  };

  const addNewTask = (sectionIndex) => {
    const updatedSections = [...sections];
    updatedSections[sectionIndex].tasks.push({ name: '', duration: 0 });
    setSections(updatedSections);
  };

  const addNewLayer = (sectionIndex) => {
    const updatedSections = [...sections];
    updatedSections[sectionIndex].planLayers.push({ name: '', duration: 0 });
    setSections(updatedSections);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const newPlan = {
      title,
      sections
    };

    try {
      const response = await axios.post('/api/dailyplans/createFull', newPlan, {
        headers: {
          'Content-Type': 'application/json',
        },
      });
      setCreatedPlan(response.data);
      setTitle('');
      setSections([{ name: '', tasks: [{ name: '', duration: 0 }], planLayers: [{ name: '', duration: 0 }] }]);
      alert('Daily plan created successfully!');
    } catch (error) {
      console.error('Error creating daily plan:', error);
      alert('Failed to create daily plan.');
    }
  };

  const formatDuration = (minutes) => {
    const hours = Math.floor(minutes / 60);
    const mins = minutes % 60;
    return hours > 0 ? `${hours}h ${mins > 0 ? `${mins}m` : ''}` : `${mins}m`;
  };

  const calculateTotals = (section) => {
    const totalTaskDuration = section.tasks.reduce((sum, task) => sum + parseInt(task.duration || 0, 10), 0);
    const totalLayerDuration = section.planLayers.reduce((sum, layer) => sum + parseInt(layer.duration || 0, 10), 0);
    const totalDuration = totalTaskDuration + totalLayerDuration;
    return { totalTaskDuration, totalLayerDuration, totalDuration };
  };

  const calculateCumulativeTotals = () => {
    let cumulativeTaskDuration = 0;
    let cumulativeLayerDuration = 0;
    let cumulativeTotalDuration = 0;

    return sections.map((section) => {
      const totals = calculateTotals(section);
      cumulativeTaskDuration += totals.totalTaskDuration;
      cumulativeLayerDuration += totals.totalLayerDuration;
      cumulativeTotalDuration += totals.totalDuration;
      return {
        cumulativeTaskDuration,
        cumulativeLayerDuration,
        cumulativeTotalDuration,
        totals
      };
    });
  };

  const cumulativeTotals = calculateCumulativeTotals();

  return (
    <div className="create-daily-plan">
      <h1>Create New Daily Plan</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Daily Plan Title:</label>
          <input
            type="text"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            required
          />
        </div>

        {sections.map((section, sectionIndex) => (
          <div key={sectionIndex} className="section-form">
            <h3>Section {sectionIndex + 1}</h3>

            <div>
              <label>Section Name:</label>
              <input
                type="text"
                name="name"
                value={section.name}
                onChange={(e) => handleSectionChange(sectionIndex, e)}
                required
              />
            </div>

            <h4>Tasks</h4>
            {section.tasks.map((task, taskIndex) => (
              <div key={taskIndex} className="task-form">
                <label>Task Name:</label>
                <input
                  type="text"
                  name="name"
                  value={task.name}
                  onChange={(e) => handleTaskChange(sectionIndex, taskIndex, e)}
                  required
                />
                <label>Duration (in minutes):</label>
                <input
                  type="number"
                  name="duration"
                  value={task.duration}
                  onChange={(e) => handleTaskChange(sectionIndex, taskIndex, e)}
                  required
                />
              </div>
            ))}
            <button type="button" onClick={() => addNewTask(sectionIndex)}>Add Task</button>

            <h4>Plan Layers</h4>
            {section.planLayers.map((layer, layerIndex) => (
              <div key={layerIndex} className="layer-form">
                <label>Layer Name:</label>
                <input
                  type="text"
                  name="name"
                  value={layer.name}
                  onChange={(e) => handleLayerChange(sectionIndex, layerIndex, e)}
                  required
                />
                <label>Duration (in minutes):</label>
                <input
                  type="number"
                  name="duration"
                  value={layer.duration}
                  onChange={(e) => handleLayerChange(sectionIndex, layerIndex, e)}
                  required
                />
              </div>
            ))}
            <button type="button" onClick={() => addNewLayer(sectionIndex)}>Add Plan Layer</button>

            <div className="totals-table">
              <table>
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
                    <td>{formatDuration(cumulativeTotals[sectionIndex].totals.totalTaskDuration)}</td>
                    <td>{formatDuration(cumulativeTotals[sectionIndex].totals.totalLayerDuration)}</td>
                    <td>{formatDuration(cumulativeTotals[sectionIndex].totals.totalDuration)}</td>
                    <td>{formatDuration(cumulativeTotals[sectionIndex].cumulativeTaskDuration)}</td>
                    <td>{formatDuration(cumulativeTotals[sectionIndex].cumulativeLayerDuration)}</td>
                    <td>{formatDuration(cumulativeTotals[sectionIndex].cumulativeTotalDuration)}</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        ))}

        <button type="button" onClick={addNewSection}>Add New Section</button>
        <button type="submit">Create Daily Plan</button>
      </form>
    </div>
  );
};

export default CreateDailyPlan;
