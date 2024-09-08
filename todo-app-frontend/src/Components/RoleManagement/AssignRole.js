import { useEffect, useState } from "react";
import { assignRole,getAllUsernames } from "../../services/authService";
import { Link } from "react-router-dom";



const AssignRoleComponent=()=>{
const [username,setUsername]=useState('');
const [roleName,setRoleName]=useState('');
const [message,setMessage]=useState('');
const [usernames,setUsernames]=useState([]);
useEffect(()=>{getAllUsernames().then(response=>setUsernames(response.data));},[]);
const handleAssignRole=async()=>{
    try{
await assignRole(username,roleName);
setMessage(`Role '${roleName}'
    assigned to '${username}'
    successfully.`);
    setUsername('');
    setRoleName('');
    }
    catch(error){
setMessage(`Failed to assign role
    '${roleName}'
    to '${username}'.`);
    }
}
return(
    <div className="role-management">
<h2>Assign Role</h2>
<Link to={`/publishers`}>Back to publishers Page</Link>
<br/>
      <Link to={`/roles/create`}>Back to CreateRole Page</Link>
      {/* <Link to={`/roles/assign`}>Back to Assign Role Page</Link> */}
      <br/>
      <Link to="/books/new">Back to Adding new Book</Link>
      <br/>
      <Link to="/publishers/new">Back to Adding Publishers page</Link>
      <br/>
      <Link to={`/books`}>Back to Book List</Link>
      <br/>
      <select value={username} onChange={(e)=>setUsername(e.target.value)} placeholder="Select Username">
<option value="">Select a User</option>
{usernames.map((user,index)=>(
    <option key={index} value={user}>{user}</option>
))}
      </select>
      {/* <Link to={`/publishers`}>Back to Publishers' list Page</Link> */}
     
      {/* <Link to={`/roles/create`}>Back to Creating new Roles Page</Link> */}
      <br/>
      {/* <Link to={`/roles/assign`}>Back to Assigning Roles to users Page</Link> */}
      {/* <br/> */}
      {/* <Link to="/books/new">Back to Addong new Books</Link> */}
      <br/>
<input type="text" value={username}
onChange={(e)=>setUsername(e.target.value)}
placeholder="Username"
/>
<input type="text" value={roleName}
onChange={(e)=>{setRoleName(e.target.value)}}
placeholder="Role Name"/>

<button onClick={handleAssignRole}>
    Assign Role
</button>
{message && <p>{message}</p>}
    </div>
);
};

export default AssignRoleComponent;