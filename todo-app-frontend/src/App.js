import React from 'react';
import { BrowserRouter as Router, Route, Switch, Routes } from 'react-router-dom';
import BookList from './Components/BookForm';
import BookForm from './Components/BookList';
import PublisherList from './Components/PublisherForm';
import PublisherForm from './Components/PublisherList';
import EditBookForm from './Components/EditBookForm';
// import EditPublisherForm from './Components/EditPublisherForm';
import Login from './Components/Login';
import PrivateRoute from './Components/PrivateRoute';
import CreateRoleComponent from './Components/RoleManagement/CreateRole';
import AssignRoleComponent from './Components/RoleManagement/AssignRole';
import CreateDailyPlan from './Components/CreateDailyPlan';
import DailyPlanList from './Components/DailyPlanList'
import DailyPlanDetail from './Components/DailyPlanDetail';
function App() {
  return (
    <Router>
      <div className="App">
        <Routes>
        <Route path="/login" element={<Login />} />
          <Route path="/books" element={<PrivateRoute> <BookForm /></PrivateRoute>} />
          <Route path="/books/new" element={<PrivateRoute> < BookList /></PrivateRoute>} />
          <Route path="/books/edit/:id" element={<PrivateRoute> <EditBookForm /></PrivateRoute>} />
          <Route path="/publishers" element={<PrivateRoute> < PublisherForm/></PrivateRoute>} />
          <Route path="/publishers/new" element={<PrivateRoute> <PublisherList /></PrivateRoute>} />
          <Route path="/publishers/edit/:id" element={<PrivateRoute> <PublisherList /></PrivateRoute>} />
        <Route path="/roles/create" element={<PrivateRoute><CreateRoleComponent/> </PrivateRoute>}/>
        <Route path="/roles/assign" element={<PrivateRoute><AssignRoleComponent/></PrivateRoute>}/>
       <Route path="/dailyplans" element={<DailyPlanList/>}/>
       <Route path="/dailyplans/new"  element={<CreateDailyPlan/>}/>
       <Route path="/dailyplans/:id"  element={<DailyPlanDetail/>}/>
        </Routes>
      </div>
    </Router>
  );
}
export default App;